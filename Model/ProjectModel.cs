using SE_Project.Model;
using System.Collections.Generic;
using System;
using System.Linq;

public class ProjectModel : IModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int CreatedBy { get; set; }
    public string CreatorName { get; set; }
    public DateTime CreatedAt { get; set; }
    public List<UserModel> Participants { get; set; }
    public List<TaskModel> Tasks { get; set; }

    public ProjectModel()
    {
        Participants = new List<UserModel>();
        Tasks = new List<TaskModel>();
    }
    public string ParticipantsNames
    {
        get
        {
            return Participants != null ? string.Join(", ", Participants.Select(p => p.Name)) : "";
        }
    }

}