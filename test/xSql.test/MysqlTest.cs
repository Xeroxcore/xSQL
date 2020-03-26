using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace xSql.test
{
    [TestClass]
    public class MysqlTest
    {
        MySql sql = new MySql("Server=127.0.0.1;port=3306;Database=testdb;Uid=testuser;Pwd=helloworld;");

        [TestMethod]
        public void InsertToDBMySql()
        {
            var user = new User()
            {
                UserName = "Nasar",
                Password = "Nasar123"
            };
            sql.AlterDataQuery("insert into useraccount(username,password) values(@UserName, @Password)", user);
            var emptyUser = new
            {
                UserName = "Nasar"
            };
            var data = sql.SelectQuery("select * from testdb.useraccount where username = @UserName", emptyUser);
            var selectedUser = data.Rows[0][0];
            Assert.AreEqual("Nasar", selectedUser);
        }

        [TestMethod]
        public void SelectFromDBMySql()
        {
            var user = new User();
            var data = sql.SelectQuery("select * from testdb.useraccount", user);
            var selectedUser = data.Rows[0][0];
            Assert.AreEqual("Nasar", selectedUser);
        }

        [TestMethod]
        public void ScalarMySql()
        {
            var user = new User()
            {
                UserName = "NasarScalar",
                Password = "Nasar123"
            };
            var query = "insert into useraccount(username,password) values(@UserName, @Password);" +
            " select username from testdb.useraccount where username = @Username";
            var username = (string)sql.AlterDataQueryScalar(query, user);
            Assert.AreEqual("NasarScalar", username);
        }

        [TestMethod]
        public void DeleteDataFromDBMySql()
        {
            var user = new { UserName = "NasarScalar" };
            sql.AlterDataQuery("delete from useraccount where username = @UserName", user);
            var data = sql.SelectQuery("select * from testdb.useraccount where username = @UserName", user);
            Assert.IsTrue(data.Rows.Count == 0);
        }
    }
}