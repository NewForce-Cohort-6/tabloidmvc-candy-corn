using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using TabloidMVC.Models;

namespace TabloidMVC.Repositories
{
    public class UserTypeRepository: BaseRepository, IUserTypeRepository
    {
        public UserTypeRepository(IConfiguration config) : base(config) { }

        public List<UserType> GetUserTypes()
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        SELECT id, [Name]
                        FROM UserType";
                    var reader = cmd.ExecuteReader();

                    var userTypes = new List<UserType>();

                    while (reader.Read())
                    {
                        userTypes.Add(new UserType()
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            Name = reader.GetString(reader.GetOrdinal("Name")),
                        });
                    }
                    reader.Close();

                    return userTypes;
                }
            }
        }
    }
}
