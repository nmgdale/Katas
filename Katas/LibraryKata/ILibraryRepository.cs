using System.Collections.Generic;
using Katas.LibraryKata.Models;

namespace Katas.LibraryKata
{
    public interface ILibraryRepository
    {
        IEnumerable<string> GetMembersBooks(string memberId);
        void BookOut(string memberId, string bookId);
        void Return(string memberId, string bookId);
        Book GetBook(string id);
        Member GetMember(string id);
        string OwnedBy(string bookId);
    }
}