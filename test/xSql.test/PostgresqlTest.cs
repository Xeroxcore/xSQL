using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace xSql.test
{
    [TestClass]
    public class PostgresqlTest
    {
        NpgSql sql = new NpgSql("Server=127.0.0.1;port=5432;Database=testdb;Uid=testuser;Pwd=helloworld;");

        [TestMethod]
        public void InsertToDB()
        {
            var user = new User()
            {
                UserName = "Nasar",
                Password = "Nasar123"
            };
            sql.AlterDataQuery("insert into useraccount(username,password) values(@UserName, @Password)", user);
            var emptyUser = new User()
            {
                UserName = "Nasar"
            };
            var data = sql.SelectQuery("select * from useraccount where username = @UserName", emptyUser);
            var selectedUser = data.Rows[0][0];
            Assert.AreEqual("Nasar", selectedUser);
        }

        [TestMethod]
        public void SelectFromDB()
        {
            var user = new User();
            var data = sql.SelectQuery("select * from useraccount", user);
            var selectedUser = data.Rows[0][0];
            Assert.AreEqual("Nasar", selectedUser);
        }

        [TestMethod]
        public void Scalar()
        {
            var user = new User()
            {
                UserName = "NasarScalar",
                Password = "Nasar123"
            };
            var username = (string)sql.AlterDataQueryScalar("insert into useraccount(username,password) values(@UserName, @Password) returning username", user);
            Assert.AreEqual("NasarScalar", username);
        }

        [TestMethod]
        public void DeleteDataFromDB()
        {
            var user = new { UserName = "NasarScalar" };
            sql.AlterDataQuery("delete from useraccount where username = @UserName", user);
            var data = sql.SelectQuery("select * from useraccount where username = @UserName", user);
            Assert.IsTrue(data.Rows.Count == 0);
        }
    }
}
