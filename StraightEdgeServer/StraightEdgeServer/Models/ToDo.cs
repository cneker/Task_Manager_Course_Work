namespace StraightEdgeServer.Models
{
    public class ToDo
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsCompleted { get; set; }

        public int TaskId { get; set; }
        //public Task Task { get; set; }
    }
}
