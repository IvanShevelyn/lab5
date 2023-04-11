using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Remoting;
using System.Security.Cryptography.X509Certificates;

namespace lab5
{
    public class Company
    {
        public string ID { get; set; } = Guid.NewGuid().ToString();
        public int Cash { get; set; } = default;
        private List<Client> Clients = new List<Client>();
        private List<Project> Projects = new List<Project>();    
        private List<Department> Departments = new List<Department>();
        private const int countOfDep = 5;
        public Company()
        {
            for (int i = 0; i < countOfDep; i++)
            {
                Departments.Add(new Department()
                {
                    Name = $"Department #{i + 1}"
                });
            }
        }
        public IEnumerable<Client> GetClients()
        {
            foreach (Client cl in Clients)
            {
                yield return cl;
            }
        }
        public IEnumerable<Project> GetProjects()
        {
            foreach (Project pr in Projects)
            {
                yield return pr;
            }
        }
        public IEnumerable<Department> GetDepartments()
        {
            foreach (Department dep in Departments)
            {
                yield return dep;
            }
        }
        public void RemoveClients()
        {
            Clients.RemoveAll(x => x.Money < 3333 && x.GetProjects().Where(p => p.Status == "DONE").Count() == x.GetProjects().Count());
            //удалить всех клиентов у которых мало денег и у которых все заказаные проекты уже выполнены. 
        }
        public void RemoveDoneProjects()
        {
            Projects.RemoveAll(x => x.Status == "DONE");
        }
        public void AddClient(Client cl)
        {
            Clients.Add(cl);
        }
        public void AddProject(Project pr)
        {
            Projects.Add(pr);
        }
    }
}
