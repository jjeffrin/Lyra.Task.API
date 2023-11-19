using System.ComponentModel.DataAnnotations;

namespace Lyra.TaskService.API.Models
{
    public class WorkItemStatusCode
    {
        [Key]
        [MaxLength(3)]
        public string Code { get; set; }

        public string? Description { get; set; }
        public ICollection<WorkItem> WorkItems { get; set; }
    }
}
