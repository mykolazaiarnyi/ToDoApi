namespace ToDoApi.Domain
{
    public class ToDoItem
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public bool IsDone { get; set; }
    }
}
