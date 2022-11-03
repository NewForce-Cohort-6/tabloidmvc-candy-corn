using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using TabloidMVC.Models;

namespace TabloidMVC.Repositories
{
    public class CommentRepository : BaseRepository, ICommentRepository
    {
        public CommentRepository(IConfiguration config) : base(config) { }
    

        public List<Comment> GetByPostId(int id)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                       SELECT Id, PostId, UserProfileId, Subject, Content, CreateDateTime
                       FROM Comment
                       WHERE PostId = @PostId";
                    cmd.Parameters.AddWithValue("@PostId", id);

                    List<Comment> comments = new List<Comment>();
                    var reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Comment comment = new()
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            PostId = reader.GetInt32(reader.GetOrdinal("PostId")),
                            UserProfileId = reader.GetInt32(reader.GetOrdinal("UserProfileId")),
                            Subject = reader.GetString(reader.GetOrdinal("Subject")),
                            Content = reader.GetString(reader.GetOrdinal("Content")),
                            CreateDateTime = reader.GetDateTime(reader.GetOrdinal("CreateDateTime")),
                        };
                        comments.Add(comment);
                    }

                    reader.Close();

                    return comments;
                }
            }
        }
    }
}
