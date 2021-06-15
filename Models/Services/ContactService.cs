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
            Console.WriteLine("CREATION DU CONTACT SERVICE");
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
        public void Insert(Contact newContact)
        {
            Command command = new Command("INSERT INTO [Contact] ([LastName], [FirstName], [Email], [CategoryId]) VALUES (@LastName, @FirstName, @Email, @CategoryId);", false);

            command.AddParameter("LastName", newContact.LastName);
            command.AddParameter("FirstName", newContact.FirstName);
            command.AddParameter("Email", newContact.Email);
            command.AddParameter("CategoryId", newContact.CategoryId);

            _connection.ExecuteNonQuery(command);
        }
    }
}

//1.créer la méthode Create au niveau du ContactController
//2. Créer la vue créant un formulaire pour la création des contacts
//3. Créer la méthode Create en Post recevant les différente valeurs formulaire
//4. Ajouter la méthode insert au niveau de votre service (BFSP_AddContact)
//5.Insérer après vérification votre contact en DB
//6. Rediriger vers l'action index (return RedirecttoAction("Index");)