using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab5
{
    public class Client
    {
        public string ID { get; set; } = Guid.NewGuid().ToString();
        public string Name { get; set; }
        public double Money { get; set; } = Random.Shared.Next(3500, 12000);
        private List<Project>? Projects = new List<Project>();
        public Client(string name)
        {
            Name = name;
        }
        public IEnumerable<Project> GetProjects()
        {
            foreach (Project pr in Projects)
            {
                yield return pr;
            }
        }
        public void AddProject(Project project)
        {
            Projects.Add(project);
            Money -= project.TotalPrice;
        }
        public override string ToString()
        {
            return $"Id: {ID}\nName: {Name}\nMoney: {Money}\nProject count: {Projects?.Count ?? 0}";
        }
    }
}
