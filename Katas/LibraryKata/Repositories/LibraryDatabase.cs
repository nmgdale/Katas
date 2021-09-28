using System.Collections.Generic;
using Dapper;
using Katas.LibraryKata.Models;
using Microsoft.Data.Sqlite;

namespace Katas.LibraryKata.Repositories
{
    public class LibraryDatabase : ILibraryRepository
    {
        public IEnumerable<string> GetMembersBooks(string memberId)
        {
            using var connection = new SqliteConnection("Data Source=Library.sqlite");

            return connection.Query<string>("SELECT BookId FROM MemberBooks WHERE MemberId = @Id", new { Id = memberId });
        }

        public void BookOut(string memberId, string bookId)
        {
            using var connection = new SqliteConnection("Data Source=Library.sqlite");

            connection.Execute("INSERT INTO MemberBooks (MemberId, BookId) VALUES (@memberId, @bookId)", new { memberId, bookId});
        }

        public void Return(string memberId, string bookId)
        {
            using var connection = new SqliteConnection("Data Source=Library.sqlite");

            connection.Execute("DELETE FROM MemberBooks WHERE MemberId = @memberId AND BookId = @bookId", new { memberId, bookId });
        }

        public Book GetBook(string id)
        {
            using var connection = new SqliteConnection("Data Source=Library.sqlite");

            return connection.QueryFirstOrDefault<Book>("SELECT * FROM Books WHERE Id = @id", new { id });
        }

        public Member GetMember(string id)
        {
            using var connection = new SqliteConnection("Data Source=Library.sqlite");

            return connection.QueryFirstOrDefault<Member>("SELECT * FROM Members WHERE Id = @id", new { id });
        }

        public string OwnedBy(string bookId)
        {
            using var connection = new SqliteConnection("Data Source=Library.sqlite");

            return connection.QueryFirst<string>("SELECT MemberId FROM MemberBooks mb WHERE mb.BookId = @Id", new { Id = bookId });
        }
    }
}