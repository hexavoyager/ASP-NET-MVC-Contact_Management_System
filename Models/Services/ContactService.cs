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
    public class ContactService
    {
        private readonly Connection _connection;
        public ContactService(Connection connection)
        {
            _connection = connection;
        }
        public IEnumerable<Contact> Get()
        {
            Command command = new Command("SELECT [Id], [LastName], [FirstName], [Email], [CategoryId] FROM [Contact];", false);
            return _connection.ExecuteReader(command, dr => new Contact() { Id = (int)dr["id"], LastName = (string)dr["LastName"], FirstName = (string)dr["FirstName"], Email = (string)dr["Email"], CategoryId = (int)dr["CategoryId"] });
        }
        public Contact Get(int id)
        {
            Command command = new Command("SELECT [Id], [LastName], [FirstName], [Email], [CategoryId] FROM [Contact] WHERE Id = @Id", false);
            command.AddParameter("Id", id);
            return _connection.ExecuteReader(command, dr => new Contact() { Id = (int)dr["id"], LastName = (string)dr["LastName"], FirstName = (string)dr["FirstName"], Email = (string)dr["Email"], CategoryId = (int)dr["CategoryId"] }).SingleOrDefault();
        }
        public void Insert(Contact c)
        {
            Command command = new Command("INSERT INTO [Contact] ([LastName], [FirstName], [Email], [CategoryId]) VALUES (@LastName, @FirstName, @Email, @CategoryId);", false);

            command.AddParameter("LastName", c.LastName);
            command.AddParameter("FirstName", c.FirstName);
            command.AddParameter("Email", c.Email);
            command.AddParameter("CategoryId", c.CategoryId);

            _connection.ExecuteNonQuery(command);
        }

        public void Update(Contact c)
        {
            Command command = new Command("UPDATE [Contact] SET [LastName] = @LastName, [FirstName] = @FirstName, [Email] = @Email, [CategoryId] = @CategoryId WHERE Id = @Id", false);
            command.AddParameter("Id", c.Id);
            command.AddParameter("LastName", c.LastName);
            command.AddParameter("FirstName", c.FirstName);
            command.AddParameter("Email", c.Email);
            command.AddParameter("CategoryId", c.CategoryId);
            _connection.ExecuteNonQuery(command);
        }

        public void Delete(int id)
        {
            Command command = new Command("DELETE FROM [Contact] WHERE Id = @Id", false);
            command.AddParameter("Id", id);
            _connection.ExecuteNonQuery(command);
        }
    }
}
