namespace Katas.LibraryKata.Models
{
    public class Book
    {
        public readonly string Id;
        public readonly string Name;

        public Book(string id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}