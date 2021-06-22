using MVC_Demo_2.Models.Data;
using MVC_Demo_2.Models.Mappers;
using MVC_Demo_2.Tools.Connections.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC_Demo_2.Models.Services
{
    public class CategoryService
    {
        private readonly Connection _connection;

        public CategoryService(Connection connection)
        {
            _connection = connection;
        }

        public IEnumerable<Category> Get()
        {
            Command command = new Command("Select Id, Name from Category;", false);
            return _connection.ExecuteReader(command, dr => dr.ToCategory());
        }
    }
}
