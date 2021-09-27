using System.Collections.Generic;
using Bogus;
using FluentAssertions;
using Katas.LibraryKata.Models;
using Katas.LibraryKata.Stubs;
using Xunit;

namespace Katas.LibraryKata
{
    //Robs KATA: Write code in c# that uses SQL and no lists, dictionaries etc
    //Tom, Dick and Harry are members of the local library.
    //each member can only have 3 books out a time.
    //Your app should allow you to be able to book out a book, book in a book, query what books a person currently has out, and if a specific book is in or out and if out who has it
    //Logical Validation must apply, you cant book out a book to someone else when its not in the library etc

    public class LibraryKataTests
    {
        private readonly Faker _faker = new();
        private static LibraryRepositoryStub _libraryRepositoryStub;

        private static void Setup()
            => _libraryRepositoryStub = new LibraryRepositoryStub().Create();

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public void A_member_can_book_out_books(int numberOfBooks)
        {
            Setup();

            var expectedBooks = new List<Book>();
            var library = new Library(_libraryRepositoryStub.Stub.Object);

            for (var i = 0; i < numberOfBooks; i++)
            {
                var book = _libraryRepositoryStub.RandomBook();

                expectedBooks.Add(book);
                
                library.BookOut("0001", book.Id);
            }

            library.QueryUser("0001")
                .Should()
                .BeEquivalentTo(expectedBooks);
        }
    }
}