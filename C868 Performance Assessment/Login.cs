using Elements.Database;
using MySql.Data.MySqlClient;

namespace C868_Performance_Assessment
{
    public class Login
    {
        public static User SetGlobalUser(string user, string pass, string dbConn)
        {
            User u = new User();
            MySqlDatabase.spl.Add(new MySqlParameter("@Username", user));
            MySqlDatabase.spl.Add(new MySqlParameter("@Password", pass));
            MySqlDataReader dr = MySqlDatabase.ExecuteReader(
                "select userId, UserName from user where userName = @Username and password = @Password",
                MySqlDatabase.spl, dbConn);
            if (dr != null && dr.HasRows)
            {
                dr.Read();
                u = new User(dr.GetInt32(0), dr.GetString(1));
            }

            return u;
        }
    }

    public class User
    {
        public int UserId { get; set; }
        public string UserName { get; set; }

        public User() { }

        public User(int userId, string userName)
        {
            UserId = userId;
            UserName = userName;
        }
    }
}