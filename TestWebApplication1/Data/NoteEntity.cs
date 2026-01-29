namespace TestWebApplication1.Data
{
    public class NoteEntity
    {
        public int Id { get; set; }
        public string Title { get; set; } = "";
        public string Content { get; set; } = "";
        public DateTime CreatedAt { get; set; }
    }
}
