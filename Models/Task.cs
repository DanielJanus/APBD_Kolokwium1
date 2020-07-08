using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace s18782_Daniel_Janus.Models
{
    public class Task
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Deadline { get; set; }

        public string ProjectName { get; set; }

        public string TaskTypeName { get; set; }

    }
}
