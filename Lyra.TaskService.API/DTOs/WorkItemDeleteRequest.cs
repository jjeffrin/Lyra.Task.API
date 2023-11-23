namespace Lyra.TaskService.API.DTOs
{
    public class WorkItemDeleteRequest
    {
        public required int UserId { get; set; }
        public required int Id { get; set; }
    }
}
