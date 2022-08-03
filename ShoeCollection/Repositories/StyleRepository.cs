    using Microsoft.Data.SqlClient;
    using Microsoft.Extensions.Configuration;
    using ShoeCollection.Models;
    using System.Collections.Generic;
    using ShoeCollection.Utils;

namespace ShoeCollection.Repositories
{
    public class StyleRepository : BaseRepository, IStyleRepository
    {
        public StyleRepository(IConfiguration configuration) : base(configuration) { }

        public List<Style> GetAllStyles()
        {
            var styles = new List<Style>();

            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                           Select Style.Id as Id, Style.Name as Name
                            From Style ";
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            styles.Add(NewPostFromReader(reader));
                        }
                    }
                }
            }
            return styles;
        }

        public Style GetStyleById(int id)
        {
            Style style = null;
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        select Style.Id as Id, Style.Name as Name
                        from Style
                        where Style.Id = @id  ";
                    DbUtils.AddParameter(cmd, "id", id);


                    var reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        style = NewPostFromReader(reader);
                    }

                }

            }
            return style;
        }

        public void AddAStyle(Style style)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        Insert Into Style(Name)
                        Output inserted.Id
                        Values (@name)";
                    DbUtils.AddParameter(cmd, "@id", style.Id);
                    DbUtils.AddParameter(cmd, "name", style.Name);

                    style.Id = (int)cmd.ExecuteScalar();
                }
            }
        }

        public void DeleteStyle(int styleId)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                                Delete
                                From Style
                                where Id = @id";
                    cmd.Parameters.AddWithValue("@id", styleId);
                    cmd.ExecuteNonQuery();
                }

            }
        }

        public void UpdateStyle(Style style)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                      update Style
                        set
                    Name = @name
                    Where Id = @id";
                    cmd.Parameters.AddWithValue("@name", style.Name);
                    cmd.Parameters.AddWithValue("id", style.Id);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        private Style NewPostFromReader(SqlDataReader reader)
        {
            return new Style
            {
                Id = DbUtils.GetInt(reader, "Id"),
                Name = DbUtils.GetString(reader, "Name"),
            };

        }
    }
}
