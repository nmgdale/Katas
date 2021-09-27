using System;
using System.Collections.Generic;
using System.Linq;

namespace Katas.LibraryKata.Models
{
    public class Library
    {
        private readonly ILibraryRepository _libraryRepository;

        private const int MaxNumberOfBooks = 3;

        public Library(ILibraryRepository libraryRepository) => _libraryRepository = libraryRepository;

        public void BookOut(string memberId, string bookId)
        {
            if (QueryBook(bookId).Book == null)
                throw new InvalidOperationException("Invalid Book");

            if (QueryBook(bookId).OwnedBy != null)
                throw new InvalidOperationException("Book is already booked out");

            if (QueryUser(memberId).Count() == MaxNumberOfBooks)
                throw new InvalidOperationException("A member can only have three books out at one time");

            _libraryRepository.BookOut(memberId, bookId);
        }

        public void Return(string memberId, string bookId)
            => _libraryRepository.Return(memberId, bookId);

        public IEnumerable<Book> QueryUser(string memberId)
        {
            var membersBooks = _libraryRepository.GetMembersBooks(memberId);

            if (membersBooks == null)
                throw new InvalidOperationException("Invalid member");

            return membersBooks
                .Select(bookId => _libraryRepository.GetBook(bookId));
        }

        public string OwnedBy(string bookId)
            => _libraryRepository.OwnedBy(bookId);

        private (Book Book, string OwnedBy) QueryBook(string bookId)
            => (_libraryRepository.GetBook(bookId), _libraryRepository.OwnedBy(bookId));
    }
}