using Microsoft.AspNetCore.Components;
using System;
using System.Configuration;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Linq;
using Timer = System.Windows.Forms.Timer;
using System.Xml.Serialization;
using System.Runtime.CompilerServices;
using ExtentionMethodsForListBox;

namespace lab5
{
    public partial class Form1 : Form
    {
        object locker = new object(); //locker для синхронізації потоків
        private bool exec = true; //стан виконання програми 
        private bool stopping = false; //стан зупинки (true, лише коли проект зупиняється)
        private const string filePath = "ConsoleSerialization.xml";
        private long clients = default; // змінна для номеру клієнта
        private long projects = default; // змінна для номеру проекта 
        private Company company = new Company();
        private List<Thread> threads = new List<Thread>();
        private List<Task> tasks = new List<Task>(); //списки потоків та тасків, які постійно оновлюються.
                                                     //Виконані потоки та таски видаляються кожні 20 секунд в івенті DeletingOldInformationTimer_Tick
        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e) //при загрузці форми створюється поток,
                                                            //який починає створювати клієнтів та проекти.
                                                            //Такі ж потоки генеруються й надалі в івенті GenerateProjectsTimer_Tick кожні 3.2 секунди.
                                                            //Також створюється поток виконання проектів.
        {
            DepartmentsListBox.DataSource = company.GetDepartments().ToList();
            DepartmentsListBox.DisplayMember = "Name";
            Thread GenerateProjects = new Thread(AdditingClientsAndProjects);
            GenerateProjects.Name = "GenerateProj";
            threads.Add(GenerateProjects);
            GenerateProjects.IsBackground = true;
            GenerateProjects.Start();

            Thread ExecutingProjectsThread = new Thread(ExecutingProjects);
            ExecutingProjectsThread.Name = "ExecPR";
            threads.Add(ExecutingProjectsThread);
            ExecutingProjectsThread.IsBackground = true;
            ExecutingProjectsThread.Start();
        }

        #region Івенти, які демонструють інформацію про клієнта, проект або відділ
        private void ClientsListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ClientsListBox.SelectedItems.Count == 0)
                return;
            try
            {
                Client selected = (Client)ClientsListBox.SelectedItem;
                MessageBox.Show(selected.ToString());
            }
            catch { }
        }
        private void ProjectsListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ProjectsListBox.SelectedItems.Count == 0)
                return;
            try
            {
                Project selected = (Project)ProjectsListBox.SelectedItem;
                MessageBox.Show(selected.ToString());
            }
            catch { }
        }
        private void DepartmentsListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (DepartmentsListBox.SelectedItems.Count == 0)
                return;
            try
            {
                Department selected = (Department)DepartmentsListBox.SelectedItem;
                MessageBox.Show(selected.ToString());
            }
            catch { }
        }
        #endregion 

        private void UpdateForm() //оновлення списків
        {
            Invoke(new Action(() => ClientsListBox.SelectionMode = SelectionMode.None));
            Invoke(new Action(() => ProjectsListBox.SelectionMode = SelectionMode.None));
            Invoke(new Action(() => DepartmentsListBox.SelectionMode = SelectionMode.None));
            Invoke(new Action(() => ClientsListBox.DataSource = company.GetClients().ToList()));
            Invoke(new Action(() => ProjectsListBox.DataSource = company.GetProjects().ToList()));
            Invoke(new Action(() => ClientsListBox.DisplayMember = "Name"));
            Invoke(new Action(() => ProjectsListBox.DisplayMember = "Name"));
            Invoke(new Action(() => DepartmentsListBox.DisplayMember = "Name"));
            Invoke(new Action(() => ClientsListBox.SelectionMode = SelectionMode.One));
            Invoke(new Action(() => ProjectsListBox.SelectionMode = SelectionMode.One));
            Invoke(new Action(() => DepartmentsListBox.SelectionMode = SelectionMode.One));
        }
        private void ExecutingProjects() //Функція, яка виконується у фоновому потоці, який створюється при завантаженні форми.
                                         //Поток працює увесь час за допомогою циклу while. Він синхронізований з іншими
                                         //потоками завдяки lock. Потік завершується коли стан exec = false (після натискання на кнопку "Зупинити та серіалізувати.")
                                         //Поток заново створюється після поновлення роботи у івенті continueExecuting_Click.
                                         //З її допомогою відбувається виконання усіх проектів відділами компанії.
                                         //Функція всередині себе створює таски, які й виконують проекти.
        {
            while (exec)
            {
                lock (locker)
                {
                    int i = Thread.CurrentThread.ManagedThreadId;
                    List<Project> projects = new List<Project>();
                    if (company.GetProjects() != null)
                        projects = company.GetProjects().Where(x => x.Status == "TODO").ToList();
                    List<Department> departments = company.GetDepartments().Where(x => x.Project == null).ToList();
                    foreach (Project p in projects)
                    {
                        foreach (Department dep in departments.Where(x => x.Project == null))
                        {
                            dep.Project = p;
                            p.Status = StatusOfProject.IN_PROCESS.ToString();
                            tasks.Add(Task.Run(() =>
                            {
                                double progress;
                                console.Invoke(new Action(() => console.Items.Add($"Команди відділу {dep.Name} почали роботу над {p.Name}")));
                                console.Invoke(console.SelectBottomIndex);
                                console.Invoke(new Action(() => console.Items.Add($"Статус проекту {p.Name} змінено на IN PROCESS")));
                                console.Invoke(console.SelectBottomIndex);
                                for (int i = 1; i < p.CountOfIteration; i += 5)
                                {
                                    progress = i * 100 / p.CountOfIteration;
                                    console.Invoke(new Action(() => console.Items.Add($"{p.Name} виконано на {progress}%")));
                                    console.Invoke(console.SelectBottomIndex);
                                    Thread.Sleep(4000);
                                }
                                console.Invoke(new Action(() => console.Items.Add($"{p.Name} виконано на 100%")));
                                console.Invoke(console.SelectBottomIndex);
                                console.Invoke(new Action(() => console.Items.Add($"Команди відділу {dep.Name} закінчили роботу над {p.Name}")));
                                console.Invoke(console.SelectBottomIndex);
                                console.Invoke(new Action(() => console.Items.Add($"Статус проекту {p.Name} змінено на DONE!")));
                                console.Invoke(console.SelectBottomIndex);
                                company.Cash += p.TotalPrice;
                                console.Invoke(new Action(() => textBox1.Text = company.Cash.ToString()));
                                p.Status = StatusOfProject.DONE.ToString();
                                dep.Project = null;
                            }));
                            break;
                        }
                    }
                }
            }
        }
        private void RemovingDoneProjectsAndClients() //функція, яка виконується у фоновому потоці,
                                                      //який створюється кожні 20 секунд в івенті DeletingOldInformationTimer_Tick.
        {
            lock (locker)
            {
                threads.RemoveAll(x => x.ThreadState.Equals(ThreadState.Stopped));
                tasks.RemoveAll(x => x.Status.Equals(TaskStatus.RanToCompletion));
                company.RemoveDoneProjects();
                company.RemoveClients();
                UpdateForm();
                console.Invoke(new Action(() => console.Items.Add($"LISTS HAVE BEEN UPDATED!")));
                console.Invoke(console.SelectBottomIndex);
            }
        }
        private void DeletingOldInformationTimer_Tick(object sender, EventArgs e) //таймер, який кожні 20 сек створює потік,
                                                                                  //який видаляє виконані проекти та людей, які вже не мають 
                                                                                  //змоги замовити ще проекти
        {
            if (!exec)
            {
                DeletingOldInformationTimer.Stop();
                return;
            }
            Thread DeleteClientsAndProjects = new Thread(RemovingDoneProjectsAndClients);
            DeleteClientsAndProjects.Name = "DeleteCLandPR";
            threads.Add(DeleteClientsAndProjects);
            DeleteClientsAndProjects.IsBackground = true;
            DeleteClientsAndProjects.Start();
        }
        private void GenerateProjectsForClient(Client cl) //генерація проектів для тільки що створеного клієнта
        {
            lock (locker)
            {
                int num = Random.Shared.Next(1, 4);
                company.AddClient(cl);
                for (int i = 0; i < num; i++)
                {
                    Project p = new Project($"Project #{projects + 1}", cl);
                    if (cl.Money < p.TotalPrice)
                        return;
                    cl.AddProject(p);
                    projects++;
                    company.AddProject(p);
                    console.Invoke(new Action(() => console.Items.Add($"Клієнт {cl.Name} замовив проект {p.Name}")));
                    console.Invoke(console.SelectBottomIndex);
                }
            }
        }
        private void AdditingClientsAndProjects() //функція, яка виконується у фоновому потоці,
                                                  //який створюється кожні 3.2 секунди в івенті GenerateProjectsTimer_Tick
        {
            lock (locker)
            {
                int chance = Random.Shared.Next(1, 5);
                if (chance == 1)
                {
                    Client cl = new Client($"Client #{clients + 1}");
                    clients++;
                    GenerateProjectsForClient(cl);
                    UpdateForm();
                }
                else
                {
                    List<Client> cl = company.GetClients().Where(x => x.Money >= 3333).ToList();
                    if (cl.Count == 0)
                        return;
                    int num = Random.Shared.Next(1, cl.Count);
                    Project p = new Project($"Project #{projects + 1}", cl[num - 1]);
                    cl[num - 1].AddProject(p);
                    console.Invoke(new Action(() => console.Items.Add($"Наш постійний клієнт {cl[num - 1].Name} замовив ще один проект {p.Name}")));
                    console.Invoke(console.SelectBottomIndex);
                    projects++;
                    company.AddProject(p);
                    UpdateForm();
                }
            }
        }
        private void GenerateProjectsTimer_Tick(object sender, EventArgs e) //таймер, який кожні 3.2 сек створює потік,
                                                                            //який генерує нові проекти для старих клієнтів
                                                                            //або створює нових клієнтів із проектами 
        {
            if (!exec)
            {
                GenerateProjectsTimer.Stop();
                return;
            }
            Thread GenerateProjects = new Thread(AdditingClientsAndProjects);
            GenerateProjects.Name = "GenerateProj";
            threads.Add(GenerateProjects);
            GenerateProjects.IsBackground = true;
            GenerateProjects.Start();
        }
        private async void StopAndSerialize_Click(object sender, EventArgs e) //асинхронний метод для зупинки всіх потоків і серіалізації,
                                                                              //асинхронний для того щоб дочекатися завершення усіх тасків,
                                                                              //які виконують проекти. 
        {
            if (!exec)
                return;

            stopping = true;
            console.Items.Add($"STOPPING PROCESSING!");
            console.SelectBottomIndex();
            exec = false;
            List<Thread> thr = threads.Where(x => !x.ThreadState.Equals(ThreadState.Stopped)).ToList();
            foreach (Thread th in thr)
            {
                th.Join();
            }
            List<Task> tsk = tasks.Where(x => x.Status.Equals(TaskStatus.Running)).ToList();
            foreach (Task task in tsk)
            {
                await task;
            }

            console.Items.Add($"STOPPED!");
            console.SelectBottomIndex();
            console.Items.Add($"Сумарний дохід: {company.Cash}");
            console.SelectBottomIndex();

            XmlSerializer xmlFormat = new XmlSerializer(typeof(System.Windows.Forms.ListBox.ObjectCollection));
            using (Stream fStream = new FileStream(filePath,
            FileMode.Create, FileAccess.Write))
            {
                xmlFormat.Serialize(fStream, console.Items);
            }
            stopping = false;
            MessageBox.Show("Роботу зупинено!\nЗбережено історію роботи компанії у xml файл!", "Повідомлення!", MessageBoxButtons.OK);
        }
        private void continueExecuting_Click(object sender, EventArgs e) //метод продовження роботи програми,
                                                                         //який запускає усі процеси після зупинки. 
        {
            if (stopping | exec)
                return;
            exec = true;
            console.Items.Add($"РОБОТУ ПОНОВЛЕНО!");
            console.SelectBottomIndex();
            DeletingOldInformationTimer.Start();
            GenerateProjectsTimer.Start();
            Thread ExecutingProjectsThread = new Thread(ExecutingProjects);
            ExecutingProjectsThread.Name = "ExecPR";
            threads.Add(ExecutingProjectsThread);
            ExecutingProjectsThread.IsBackground = true;
            ExecutingProjectsThread.Start();
        }
    }
}