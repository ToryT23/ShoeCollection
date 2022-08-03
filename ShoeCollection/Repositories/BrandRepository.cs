using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using ShoeCollection.Models;
using System.Collections.Generic;
using ShoeCollection.Utils;

namespace ShoeCollection.Repositories
{
    public class BrandRepository : BaseRepository, IBrandRepository
    {

        public BrandRepository(IConfiguration configuration) : base(configuration) { }

        public List<Brand> GetAllBrands()
        {
            var brand = new List<Brand>();

            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                    select Brand.Id as Id, Brand.BrandName as Name
                    from Brand

                                        ";
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            brand.Add(NewPostFromReader(reader));
                        }
                    }
                }
            }
            return brand;
        }

        public Brand GetBrandById(int id)
        {
            Brand brand = null;

            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        select Brand.Id as Id, Brand.BrandName as Name
                            from Brand
                            where Brand.Id = @id ";
                    DbUtils.AddParameter(cmd, "id", id);

                    var reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        brand = NewPostFromReader(reader);
                    }
                }
            }
            return brand;
        }

        public void AddABrand(Brand brand)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                    Insert Into Brand( BrandName)
                     Output inserted.Id
                     Values(@brandName)";
                    DbUtils.AddParameter(cmd, "id", brand.Id);
                    DbUtils.AddParameter(cmd, "brandName", brand.BrandName);

                    brand.Id = (int)cmd.ExecuteScalar();
                }
            }
        }

        public void DeleteABrand(int brandId)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        Delete 
                        From Brand
                        Where Id = @id";
                    cmd.Parameters.AddWithValue("@id", brandId);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void UpdateBrand(Brand brand)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        Update Brand
                         set 
                        BrandName = @name
                        Where Id = @id";
                    cmd.Parameters.AddWithValue("@name", brand.BrandName);
                    cmd.Parameters.AddWithValue("id", brand.Id);
                    cmd.ExecuteNonQuery();
                }
            }
        }


        private Brand NewPostFromReader(SqlDataReader reader)
        {
            return new Brand
            {
                Id = DbUtils.GetInt(reader, "Id"),
                BrandName = DbUtils.GetString(reader, "Name"),
            };
        }


    }
}
