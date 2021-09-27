using System;
using System.Collections.Generic;
using System.Linq;
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

            var member = _libraryRepositoryStub.RandomMember();
            var expectedBooks = new List<Book>();
            var library = new Library(_libraryRepositoryStub.Stub.Object);

            for (var i = 0; i < numberOfBooks; i++)
            {
                var book = _libraryRepositoryStub.RandomBook();

                expectedBooks.Add(book);

                library.BookOut(member, book.Id);
            }

            library.QueryUser(member)
                .Should()
                .BeEquivalentTo(expectedBooks);
        }

        [Fact]
        public void A_member_can_only_have_three_books_out_at_a_time()
        {
            Setup();

            var member = _libraryRepositoryStub.RandomMember();
            var library = new Library(_libraryRepositoryStub.Stub.Object);

            library.BookOut(member, _libraryRepositoryStub.RandomBook().Id);
            library.BookOut(member, _libraryRepositoryStub.RandomBook().Id);
            library.BookOut(member, _libraryRepositoryStub.RandomBook().Id);

            Action act = () => library.BookOut(member, _libraryRepositoryStub.RandomBook().Id);

            act.Should()
                .Throw<InvalidOperationException>()
                .WithMessage("A member can only have three books out at one time");
        }

        [Fact]
        public void A_member_can_return_books()
        {
            Setup();

            var member = _libraryRepositoryStub.RandomMember();
            var library = new Library(_libraryRepositoryStub.Stub.Object);

            var book = _libraryRepositoryStub.RandomBook();

            library.BookOut(member, book.Id);
            library.QueryUser(member).Count().Should().Be(1);

            library.Return(member, book.Id);
            library.QueryUser(member).Count().Should().Be(0);
        }

        [Fact]
        public void A_non_member_cannot_book_out_a_book()
        {
            Setup();

            var library = new Library(_libraryRepositoryStub.Stub.Object);

            Action act = () => library.BookOut("FAKEID", _libraryRepositoryStub.RandomBook().Id);

            act.Should()
                .Throw<Exception>();
        }

        [Fact]
        public void A_member_can_only_book_out_a_valid_book()
        {
            Setup();

            var member = _libraryRepositoryStub.RandomMember();
            var library = new Library(_libraryRepositoryStub.Stub.Object);

            Action act = () => library.BookOut(member, "FAKEID");

            act.Should()
                .Throw<InvalidOperationException>()
                .WithMessage("Invalid book");
        }
    }
}