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
            if (QueryUser(memberId).Count() == MaxNumberOfBooks)
                throw new InvalidOperationException("A member can only have three books out at one time");

            if (QueryBook(bookId) == null)
                throw new InvalidOperationException("Invalid Book");

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

        private Book QueryBook(string bookId)
            => _libraryRepository.GetBook(bookId);
    }
}