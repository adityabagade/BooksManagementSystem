namespace BooksManagementSystem.Models
{
    public class Books
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }
        public float Price { get; set; }
    }
}
