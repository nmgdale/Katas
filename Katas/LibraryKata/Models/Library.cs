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
            _libraryRepository.BookOut(memberId, bookId);
        }

        public IEnumerable<Book> QueryUser(string memberId)
        {
            return _libraryRepository.GetMembersBooks(memberId)
                .Select(bookId => _libraryRepository.GetBook(bookId));
        }
    }
}