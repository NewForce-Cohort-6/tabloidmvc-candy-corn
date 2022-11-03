using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;
using TabloidMVC.Models;
using TabloidMVC.Utils;

namespace TabloidMVC.Repositories
{
    public class SubscriptionRepository : BaseRepository, ISubscriptionRepository
    {
        public SubscriptionRepository(IConfiguration config) : base(config) { }

        public void Add(Subscription subscription)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        INSERT INTO Subscription (
                            SubscriberUserProfileId, ProviderUserProfileId, BeginDateTime, EndDateTime)
                        OUTPUT INSERTED.ID
                        VALUES (
                            @subId, @provId, @begin, @end )";
                    cmd.Parameters.AddWithValue("@subId", subscription.SubscriberUserProfileId);
                    cmd.Parameters.AddWithValue("@provId", subscription.ProviderUserProfileId);
                    cmd.Parameters.AddWithValue("@begin", subscription.BeginDateTime);
                    cmd.Parameters.AddWithValue("@end", subscription.EndDateTime);

                    subscription.Id = (int)cmd.ExecuteScalar();
                }
            }
        }

        public List<Subscription> GetUserSubscriptions(int id)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                       SELECT s.Id, s.SubscriberUserProfileId, s.ProviderUserProfileId, s.BeginDateTime, s.EndDateTime, up.Id
                         FROM Subscription s
                              LEFT JOIN UserProfile up ON s.ProviderUserProfileId = up.Id
                               WHERE up.Id = @id";

                    cmd.Parameters.AddWithValue("@id", id);
                    var reader = cmd.ExecuteReader();

                    var subscriptions = new List<Subscription>();

                    while (reader.Read())
                    {
                        subscriptions.Add(NewSubscriptionFromReader(reader));
                    }

                    reader.Close();

                    return subscriptions;
                }
            }
        }

        private Subscription NewSubscriptionFromReader(SqlDataReader reader)
        {
            return new Subscription()
            {
                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                SubscriberUserProfileId = reader.GetInt32(reader.GetOrdinal("SubscriberUserProfileId")),
                ProviderUserProfileId = reader.GetInt32(reader.GetOrdinal("ProviderUserProfileId")),
                BeginDateTime = reader.GetDateTime(reader.GetOrdinal("BeginDateTime")),
                EndDateTime = reader.GetDateTime(reader.GetOrdinal("EndDateTime")),
                UserProfileId = reader.GetInt32(reader.GetOrdinal("UserProfileId")),
                UserProfile = new UserProfile()
                {
                    Id = reader.GetInt32(reader.GetOrdinal("UserProfileId")),
                    FirstName = reader.GetString(reader.GetOrdinal("FirstName")),
                    LastName = reader.GetString(reader.GetOrdinal("LastName")),
                    DisplayName = reader.GetString(reader.GetOrdinal("DisplayName")),
                    Email = reader.GetString(reader.GetOrdinal("Email")),
                    CreateDateTime = reader.GetDateTime(reader.GetOrdinal("CreateDateTime")),
                    ImageLocation = DbUtils.GetNullableString(reader, "AvatarImage"),
                    UserTypeId = reader.GetInt32(reader.GetOrdinal("UserTypeId")),
                    UserType = new UserType()
                }
            
            };
        }

    }
}
