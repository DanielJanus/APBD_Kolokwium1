using s18782_Daniel_Janus.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace s18782_Daniel_Janus.Services
{
    public interface ITaskDbService
    {
        public TeamMember GetTeamMember(int IdTeamMember);

        public string DeleteProject(int IdProject);
    }
}
