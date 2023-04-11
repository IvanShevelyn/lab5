using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace lab5
{
    public class Department
    {
        public string ID { get; set; } = Guid.NewGuid().ToString(); 
        public string Name { get; set; }
        public int CountOfCommands { get; set; } = Random.Shared.Next(1, 5);
        public Project? Project { get; set; }
        public override string ToString()
        {
            return $"Id: {ID}\nTitle: {Name}\nCount of commands: {CountOfCommands}\n" +
                $"Working on a project: {Project?.Name ?? "Waiting on a project!"}";
        }
    }
}
