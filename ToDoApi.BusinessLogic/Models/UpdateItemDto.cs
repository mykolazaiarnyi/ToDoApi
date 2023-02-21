namespace ToDoApi.BusinessLogic.Models
{
    public class UpdateItemDto
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public bool IsDone { get; set; }
    }
}
