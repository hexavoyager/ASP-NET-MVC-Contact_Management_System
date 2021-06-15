using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using MVC_Demo_2.Models.Data;

namespace MVC_Demo_2.Models.Mappers
{
    public static class DataRecord
    {
        public static Contact ToContact(this IDataRecord dataRecord)
        {
            return new Contact()
            {
                Id = (int)dataRecord["id"],
                LastName = (string)dataRecord["LastName"],
                FirstName = (string)dataRecord["FirstName"],
                Email = (string)dataRecord["Email"],
                CategoryId = (int)dataRecord["CategoryId"]
            };
        }
    }
}
