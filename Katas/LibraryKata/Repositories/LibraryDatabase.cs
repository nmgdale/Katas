using System.Collections.Generic;
using Dapper;
using Katas.LibraryKata.Models;
using Microsoft.Data.Sqlite;

namespace Katas.LibraryKata.Repositories
{
    public class LibraryDatabase : ILibraryRepository
    {
        public IEnumerable<string> GetMembersBooks(string memberId)
            => CreateDatabase().Query<string>("SELECT BookId FROM MemberBooks WHERE MemberId = @Id", new { Id = memberId });

        public void BookOut(string memberId, string bookId)
            => CreateDatabase().Execute("INSERT INTO MemberBooks (MemberId, BookId) VALUES (@memberId, @bookId)", new { memberId, bookId});

        public void Return(string memberId, string bookId)
            => CreateDatabase().Execute("DELETE FROM MemberBooks WHERE MemberId = @memberId AND BookId = @bookId", new { memberId, bookId });

        public Book GetBook(string id)
            => CreateDatabase().QueryFirstOrDefault<Book>("SELECT * FROM Books WHERE Id = @id", new { id });

        public Member GetMember(string id)
            => CreateDatabase().QueryFirstOrDefault<Member>("SELECT * FROM Members WHERE Id = @id", new { id });

        public string OwnedBy(string bookId)
            => CreateDatabase().QueryFirst<string>("SELECT MemberId FROM MemberBooks mb WHERE mb.BookId = @Id", new { Id = bookId });

        private static SqliteConnection CreateDatabase()
        {
            using var connection = new SqliteConnection("Data Source=Library.sqlite");
            return connection;
        }
    }
}