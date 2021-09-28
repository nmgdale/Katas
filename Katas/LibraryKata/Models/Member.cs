namespace Katas.LibraryKata.Models
{
    public class Member
    {
        public string Id { get; set; }
        public string Name { get; set; }

        public Member(string id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}