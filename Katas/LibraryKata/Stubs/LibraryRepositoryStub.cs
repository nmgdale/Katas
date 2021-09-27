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

        private readonly Dictionary<string, Book> _books = Enumerable
            .Range(0, 100)
            .Select(_ => new Book(Faker.Random.Guid().ToString(), Faker.Commerce.ProductName()))
            .ToDictionary(x => x.Id, x => x);

        private readonly Dictionary<string, List<string>> _memberBooks = Enumerable
            .Range(0, 100)
            .Select(_ => Faker.Random.Guid().ToString())
            .ToDictionary(x => x, _ => new List<string>());

        public LibraryRepositoryStub Create()
        {
            Stub = new Mock<ILibraryRepository>();

            Stub
                .Setup(x => x.BookOut(It.IsAny<string>(), It.IsAny<string>()))
                .Callback<string, string>((memberId, bookId) => _memberBooks[memberId].Add(bookId));

            Stub
                .Setup(x => x.GetMembersBooks(It.IsAny<string>()))
                .Returns<string>(memberId => _memberBooks.ContainsKey(memberId) ? _memberBooks[memberId] : null);

            Stub
                .Setup(x => x.GetBook(It.IsAny<string>()))
                .Returns<string>(bookId => _books.ContainsKey(bookId) ? _books[bookId] : null);

            Stub
                .Setup(x => x.Return(It.IsAny<string>(), It.IsAny<string>()))
                .Callback<string, string>((memberId, bookId) => _memberBooks[memberId].Remove(bookId));

            Stub
                .Setup(x => x.OwnedBy(It.IsAny<string>()))
                .Returns<string>(id =>
                {
                    return _memberBooks
                        .SelectMany(x => x.Value)
                        .Any(bookId => bookId == id)
                            ? _memberBooks.First(m => m.Value.Contains(id)).Key
                            : null;
                });

            return this;
        }

        public Book RandomBook()
            => Faker.PickRandom(_books.Select(x => x.Value));

        public string RandomMember()
            => Faker.PickRandom(_memberBooks.Select(x => x.Key));
    }
}