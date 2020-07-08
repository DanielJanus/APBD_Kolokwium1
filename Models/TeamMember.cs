using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace s18782_Daniel_Janus.Models
{
    public class TeamMember
    {
        public int IdTeamMember { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        
        public List<Task> AssignedTo { get; set; }

        public List<Task> Created { get; set; }
    }
}
