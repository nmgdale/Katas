using System.Collections.Generic;
using Dapper;
using Katas.LibraryKata.Models;
using Microsoft.Data.Sqlite;

namespace Katas.LibraryKata.Repositories
{
    public class LibraryDatabase : ILibraryRepository
    {
        public void CreateTables()
        {
            using var connection = new SqliteConnection("Data Source=Library.sqlite");
            //connection.Execute("DROP TABLE Books;");
            //connection.Execute("DROP TABLE Members;");
            //connection.Execute("DROP TABLE MemberBooks;");
            connection.Execute("CREATE TABLE Books ([Id] GUID PRIMARY KEY, [Name] VARCHAR(8000));");
            connection.Execute("CREATE TABLE Members ([Id] GUID PRIMARY KEY, [Name] VARCHAR(8000));");
            connection.Execute("CREATE TABLE MemberBooks ([Id] INTEGER PRIMARY KEY AUTOINCREMENT, [MemberId] GUID, [BookId] GUID, FOREIGN KEY([MemberId]) REFERENCES Members(Id), FOREIGN KEY([BookId]) REFERENCES Books(Id));");
        }

        public IEnumerable<string> GetMembersBooks(string memberId)
        {
            throw new System.NotImplementedException();
        }

        public void BookOut(string memberId, string bookId)
        {
            throw new System.NotImplementedException();
        }

        public void Return(string memberId, string bookId)
        {
            throw new System.NotImplementedException();
        }

        public Book GetBook(string bookId)
        {
            using var connection = new SqliteConnection("Data Source=Library.sqlite");

            return connection.QueryFirstOrDefault<Book>("SELECT * FROM Books WHERE Id = @Id", new { Id = bookId });
        }

        public string OwnedBy(string bookId)
        {
            throw new System.NotImplementedException();
        }
    }
}