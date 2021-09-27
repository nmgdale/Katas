using System;
using System.Collections.Generic;
using System.Linq;

namespace Katas.LibraryKata.Models
{
    public class Library
    {
        private readonly ILibraryRepository _libraryRepository;

        public Library(ILibraryRepository libraryRepository)
        {
            _libraryRepository = libraryRepository;
        }

        public void BookOut(string memberId, string bookId)
        {
            if (QueryUser(memberId).Count() == 3)
                throw new InvalidOperationException("A member can only have three books out at one time");

            _libraryRepository.BookOut(memberId, bookId);
        }

        public void Return(string memberId, string bookId)
            => _libraryRepository.Return(memberId, bookId);

        public IEnumerable<Book> QueryUser(string memberId)
        {
            return _libraryRepository.GetMembersBooks(memberId)
                .Select(bookId => _libraryRepository.GetBook(bookId));
        }
    }
}