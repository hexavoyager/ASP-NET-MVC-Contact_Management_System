using Microsoft.Data.SqlClient;
using MVC_Demo_2.Models.Data;
using MVC_Demo_2.Tools.Connections.Database;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;

namespace MVC_Demo_2.Models.Services
{
    public class UserService
    {
        private readonly Connection _connection;
        public UserService(Connection connection)
        {
            _connection = connection;
        }
        public User Get(string e, string p)
        {
            Command command = new Command("SELECT [Id], [LastName], [FirstName], [Email] FROM [Utilisateur] WHERE Email = @email AND Passwd = @passwd;", false);
            command.AddParameter("Email", e);
            command.AddParameter("Passwd", p);
            return _connection.ExecuteReader(command, dr => new User() { Id = (int)dr["id"], LastName = (string)dr["LastName"], FirstName = (string)dr["FirstName"], Email = (string)dr["Email"] }).SingleOrDefault();
        }
        public void Insert(User u)
        {
            Command command = new Command("INSERT INTO [Utilisateur] ([LastName], [FirstName], [Email], [Passwd]) VALUES (@LastName, @FirstName, @Email, @Passwd);", false);

            command.AddParameter("LastName", u.LastName);
            command.AddParameter("FirstName", u.FirstName);
            command.AddParameter("Email", u.Email);
            command.AddParameter("Passwd", u.Passwd);

            _connection.ExecuteNonQuery(command);
        }
    }
}
