using s18782_Daniel_Janus.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace s18782_Daniel_Janus.Services
{ 
    public class TaskDbService : ITaskDbService
    {

        private readonly string connString = @"Data Source=db-mssql;Initial Catalog=s18782;Integrated Security=True";

        public TeamMember GetTeamMember(int IdTeamMember)
        {
            try
            {
                using (var con = new SqlConnection(connString))
                using (var com = new SqlCommand("Select t.IdTeamMember, t.FirstName, t.LastName, t.Email From TeamMember t Where t.IdTeamMember = @IdTeamMember", con))
                {
                    com.Parameters.AddWithValue("IdTeamMember", IdTeamMember);
                    con.Open();

                    var dr = com.ExecuteReader();
                    dr.Read();

                    var TeamMemberData = new TeamMember
                    {
                        IdTeamMember = IdTeamMember,
                        FirstName = dr["FirstName"].ToString(),
                        LastName = dr["LastName"].ToString(),
                        Email = dr["Email"].ToString(),
                        AssignedTo = new List<Models.Task>(),
                        Created = new List<Models.Task>()
                    };

                    dr.Close();
                    com.Parameters.Clear();

                    com.CommandText = "SELECT t.Name, t.Description, t.Deadline, p.Name AS NameProject, tt.Name AS TaskTypeName FROM Task t JOIN TeamMember tm on tm.IdTeamMember = t.IdAssignedTo JOIN Project p ON p.IdProject = t.IdProject JOIN TaskType tt ON tt.IdTaskType = t.IdTaskType WHERE tm.IdTeamMember = @IdAssignedTo ORDER BY Deadline DESC;";

                    com.Parameters.AddWithValue("IdAssignedTo", IdTeamMember);

                    dr = com.ExecuteReader();

                    while (dr.Read())
                    {
                        var TaskAssignedToData = new Models.Task
                        {
                            Name = dr["Name"].ToString(),
                            Description = dr["Description"].ToString(),
                            Deadline = Convert.ToDateTime(dr["Deadline"]),
                            ProjectName = dr["NameProject"].ToString(),
                            TaskTypeName = dr["TaskTypeName"].ToString()
                            

                        };
                        TeamMemberData.AssignedTo.Add(TaskAssignedToData);
                    }
                        dr.Close();
                        com.Parameters.Clear();

                        com.CommandText = "SELECT t.Name, t.Description, t.Deadline, p.Name AS NameProject, tt.Name AS TaskTypeName  FROM Task t JOIN TeamMember tm on tm.IdTeamMember = t.IdCreator JOIN Project p ON p.IdProject = t.IdProject JOIN TaskType tt ON tt.IdTaskType = t.IdTaskType WHERE tm.IdTeamMember = @IdCreator ORDER BY Deadline DESC;";

                        com.Parameters.AddWithValue("IdCreator", IdTeamMember);

                        dr = com.ExecuteReader();

                        while (dr.Read())
                        {
                            var TaskCreatorData = new Models.Task
                            {
                                Name = dr["Name"].ToString(),
                                Description = dr["Description"].ToString(),
                                Deadline = Convert.ToDateTime(dr["Deadline"]),
                                ProjectName = dr["NameProject"].ToString(),
                                TaskTypeName = dr["TaskTypeName"].ToString()


                            };
                            TeamMemberData.Created.Add(TaskCreatorData);
                        }
                        {
                            return TeamMemberData;
                        }
                    

                }
            }
            catch(Exception e)
            {
                Console.WriteLine("Błędne Dane");
                return null;
            }
        }

        public string DeleteProject(int IdProject)
        {

            using (var con = new SqlConnection(connString))
            {
                using (var com = new SqlCommand("Delete from Task Where IdProject = @IdProject", con))
                {
                    con.Open();
                    var tran = con.BeginTransaction();
                    com.Transaction = tran;
                    try
                    {
                        com.Parameters.AddWithValue("IdProject", IdProject);
                        com.ExecuteNonQuery();
                    }
                    catch
                    {
                        tran.Rollback();
                        return "Nie udało się usunąć Zadania";
                    }
                    try
                    {
                        com.Parameters.Clear();
                        com.CommandText = "Delete from Project Where IdProject = @IdProject";
                        com.Parameters.AddWithValue("IdProject", IdProject);
                        com.ExecuteNonQuery();
                    }
                    catch
                    {
                        tran.Rollback();
                        return "Nie udało się usunąć Projektu";
                    }
                    tran.Commit();
                    return "Usunięto Projekt mającego id = " + IdProject;
                }
            }
        }




    }
}
