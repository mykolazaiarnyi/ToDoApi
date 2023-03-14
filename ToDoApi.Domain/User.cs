﻿namespace ToDoApi.Domain
{
    public class User
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public ICollection<ToDoItem> ToDoItems { get; set; }
    }
}
