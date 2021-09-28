using Katas.LibraryKata.Repositories;
using Xunit;

namespace Katas.LibraryKata
{
    public class LibraryKataIntegrationTests
    {
        [Fact(Skip = "skip")]
        public void Test()
        {
            var database = new LibraryDatabase();

            var memberBooks = database.GetMembersBooks("B8C2AD89-69CB-4968-A4E6-95F10EBAEFF8");
            database.BookOut("B8C2AD89-69CB-4968-A4E6-95F10EBAEFF8", "76C98406-F115-4E5D-A820-CC688E45C046");
            var ownedBy = database.OwnedBy("76C98406-F115-4E5D-A820-CC688E45C046");
            var book = database.GetBook("76C98406-F115-4E5D-A820-CC688E45C046");
            database.Return("B8C2AD89-69CB-4968-A4E6-95F10EBAEFF8", "76C98406-F115-4E5D-A820-CC688E45C046");
        }
    }
}