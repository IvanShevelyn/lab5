namespace lab5
{
    public class Project
    {
        public string ID { get; set; } = Guid.NewGuid().ToString(); 
        public string Name { get; set; }
        public int CountOfIteration { get; set; } = Random.Shared.Next(11, 20);
        public int TotalPrice { get; set; } = Random.Shared.Next(2222, 3333);
        public string Status { get; set; } = StatusOfProject.TODO.ToString();
        public Client ProjectOwner { get; set; }

        public Project(string name, Client client)
        {
            Name = name;
            ProjectOwner = client;
        }
        public override string ToString()
        {
            return $"Id: {ID}\nName: {Name}\nCount of iterations: {CountOfIteration}\n" +
               $"Total price: {TotalPrice}\nStatus: {Status}\nProject owner name: {ProjectOwner.Name}";
        }
    }
}