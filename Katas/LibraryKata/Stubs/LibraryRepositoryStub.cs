using System.Collections.Generic;
using System.Linq;
using Bogus;
using Katas.LibraryKata.Models;
using Moq;

namespace Katas.LibraryKata.Stubs
{
    public class LibraryRepositoryStub
    {
        private static readonly Faker Faker = new();

        public Mock<ILibraryRepository> Stub;

        private readonly Dictionary<string, List<string>> _memberBooks = new()
        {
            { "0001", new List<string>() }
        };

        private readonly Dictionary<string, Book> _books = Enumerable
            .Range(0, 100)
            .Select(i => new Book(Faker.Random.Guid().ToString(), Faker.Commerce.ProductName()))
            .ToDictionary(x => x.Id, x => x);

        public LibraryRepositoryStub Create()
        {
            Stub = new Mock<ILibraryRepository>();

            Stub
                .Setup(x => x.BookOut(It.IsAny<string>(), It.IsAny<string>()))
                .Callback<string, string>((memberId, bookId) => _memberBooks[memberId].Add(bookId));

            Stub
                .Setup(x => x.GetMembersBooks(It.IsAny<string>()))
                .Returns<string>(memberId => _memberBooks[memberId]);

            Stub
                .Setup(x => x.GetBook(It.IsAny<string>()))
                .Returns<string>(bookId => _books[bookId]);

            return this;
        }

        public Book RandomBook()
        {
            return Faker.PickRandom(_books.Select(x => x.Value));
        }
    }
}