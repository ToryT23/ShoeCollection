using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using ShoeCollection.Models;
using ShoeCollection.Utils;
using Microsoft.Data.SqlClient;


namespace ShoeCollection.Repositories
{
    public class ShoeRepository : BaseRepository, IShoeRepository
    {
        public ShoeRepository(IConfiguration configuration) : base(configuration) { }

        public List<Shoe> GetAllShoes()
        {
            var shoes = new List<Shoe>();
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText  = @"
                              select s.Id as shoeId, s.Size as Size, s.ImageUrl as ImageUrl, s.StyleId as StyleId,
                             [User].Id as UserId,  [User].FirstName as UserFirstName, [User].LastName as UserLastName, 
   [User].FirebaseUserId as FirebaseUserId, [User].Email as Email,
                              St.Id as stId, St.Name as stName,
                              B.BrandName as BrandName, B.Id as BrandId
                              from Shoe s
                              Join [User] on s.[UserId] = [User].Id
                              Join Style St on s.StyleId = St.Id
                              Join Brand B on s.BrandId = B.Id 
                              
                              
                        ";
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            shoes.Add(ReaderMethod(reader));
                        }
                    }
                }

            }
            return shoes;
        }
      

        public Shoe GetShoeById(int id, int userId)
        {
            Shoe shoe = null;
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                            select s.Id as shoeId, s.Size as Size, s.ImageUrl as ImageUrl, s.StyleId as StyleId,
                             [User].Id as UserId,  [User].FirstName as UserFirstName, [User].LastName as UserLastName, 
   [User].FirebaseUserId as FirebaseUserId, [User].Email as Email,
                              St.Id as stId, St.Name as stName,
                              B.BrandName as BrandName, B.Id as BrandId
                              from Shoe s
                              Join [User] on s.[UserId] = [User].Id
                              Join Style St on s.StyleId = St.Id
                              Join Brand B on s.BrandId = B.Id 
                              where s.Id = @id and s.userId = @userId";
                            
                    DbUtils.AddParameter(cmd, "id", id);
                    DbUtils.AddParameter(cmd, "userId", userId);
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            shoe = ReaderMethod(reader);
                        }
                    }
                }
            }
            return shoe;
        }
        public List<Shoe> GetShoesByLoggedUser(int id)
        {
          var  shoes = new List<Shoe>();
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
          select s.Id as shoeId, s.Size as Size, s.ImageUrl as ImageUrl, s.StyleId as StyleId,
                             [User].Id as UserId,  [User].FirstName as UserFirstName, [User].LastName as UserLastName, 
   [User].FirebaseUserId as FirebaseUserId, [User].Email as Email,
                              St.Id as stId, St.Name as stName,
                              B.BrandName as BrandName, B.Id as BrandId
                              from Shoe s
                              Join [User] on s.[UserId] = [User].Id
                              Join Style St on s.StyleId = St.Id
                              Join Brand B on s.BrandId = B.Id 
                              where [User].Id = @id";
                              
                    DbUtils.AddParameter(cmd, "id", id);
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            shoes.Add(ReaderMethod(reader));
                        }
                    }
                }
            }
            return shoes;
        }

        public List<Favorite> GetFavoritesByLoggedUser(int id)
        {
            var favorites = new List<Favorite>();
            using(var conn = Connection)
            {
                conn.Open();
                using(var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
select DISTINCT (shoeId),
Favorite.Id as FavId , Favorite.ShoeId as ShoeId, Favorite.UserId as FavUser,
[User].FirstName as FirstName, [User].LastName as LastName,
Shoe.ImageUrl as ImageUrl
from Favorite
left Join [User] on Favorite.UserId = [User].Id
left Join Shoe on Favorite.ShoeId = Shoe.Id
where [User].Id = @id


                                        ";
                    DbUtils.AddParameter(cmd, "id", id);
                    using(var reader = cmd.ExecuteReader())
                    {
                        while(reader.Read())
                        {
                        favorites.Add(ReaderForFavorite(reader));
                        }
                    }
                }
            }
            return favorites;
        }



        public void AddShoe(Shoe shoe)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText =@"


Insert into Shoe
(Size, ImageUrl, BrandId, StyleId, UserId)
OUTPUT INSERTED.Id
VALUES(@size, @imageUrl, @brandId, @styleId, @userId) ";
                    DbUtils.AddParameter(cmd, "@id", shoe.Id);
                    DbUtils.AddParameter(cmd, "@size", shoe.Size);
                    DbUtils.AddParameter(cmd, "@imageUrl", shoe.ImageUrl);
                    DbUtils.AddParameter(cmd, "@brandId", shoe.BrandId);
                    DbUtils.AddParameter(cmd, "@styleId", shoe.StyleId);
                    DbUtils.AddParameter(cmd, "@userId", shoe.UserId);
                    shoe.Id = (int)cmd.ExecuteScalar();

                }
            }
        }

        public void AddAFavorite(Favorite favorite)
        {
            using(var conn = Connection)
            {
                conn.Open();
                using(var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
Insert into Favorite
(ShoeId, UserId)
OUTPUT INSERTED.Id
Values(@shoeId, @userId)";
                    DbUtils.AddParameter(cmd, "@id", favorite.Id);
                    DbUtils.AddParameter(cmd, "@shoeId", favorite.ShoeId);
                    DbUtils.AddParameter(cmd, "@userId", favorite.UserId);
                    favorite.Id = (int)cmd.ExecuteScalar();
                }
            }
        }
        public void DeleteAFavorite(int favId, int userId)
        {
            using(var conn = Connection)
            {
                conn.Open();
                using(var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                   
  Delete
 From Favorite
 where Favorite.ShoeId =@id  and Favorite.UserId = @uId
                                        ";
                    cmd.Parameters.AddWithValue("@id", favId);
                    cmd.Parameters.AddWithValue("@uId", userId);
                    cmd.ExecuteScalar();
                }
            }
        }


        public void DeleteAShoe(int shoeId)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                    Delete
                    From Shoe
                    Where Id = @id                               
                    ";
                    cmd.Parameters.AddWithValue("@id", shoeId);
                    cmd.ExecuteScalar();
                }
            }
        }

        public void UpdateAShoe(Shoe shoe, int userId)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        Update Shoe
                        Set
                        UserId = @userId,
                        Size = @size,
                        StyleId = @styleId,
                        BrandId = @brandId,
                        ImageUrl = @imageUrl
                        Where Id = @id and Shoe.userId = @userId" ;
                    cmd.Parameters.AddWithValue("@id", shoe.Id);
                    cmd.Parameters.AddWithValue("@userId", userId);
                    cmd.Parameters.AddWithValue("@size", shoe.Size);
                    cmd.Parameters.AddWithValue("@styleId", shoe.StyleId);
                    cmd.Parameters.AddWithValue("@brandid", shoe.BrandId);
                    cmd.Parameters.AddWithValue("@imageUrl", shoe.ImageUrl);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        private Favorite ReaderForFavorite(SqlDataReader reader)
        {
            return new Favorite()
            {
                Id = DbUtils.GetInt(reader, "FavId"),
                UserId = DbUtils.GetInt(reader, "FavUser"),
                ShoeId = DbUtils.GetInt(reader, "ShoeId"),
                User = new User()
                {
                    FirstName = DbUtils.GetString(reader, "FirstName"),
                    LastName = DbUtils.GetString(reader, "LastName"),
                },
                Shoe = new Shoe()
                {
                    ImageUrl = DbUtils.GetString(reader, "ImageUrl")
                }
               
            };
        }


        private Shoe ReaderMethod(SqlDataReader reader)
        {
            return new Shoe
            {
                Id = DbUtils.GetInt(reader, "shoeId"),
                BrandId = DbUtils.GetInt(reader, "BrandId"),
                StyleId = DbUtils.GetInt(reader, "StyleId"),
                Size = DbUtils.GetInt(reader, "Size"),
                ImageUrl = DbUtils.GetString(reader, "ImageUrl"),
                UserId = DbUtils.GetInt(reader, "UserId"),

                User = new User()
                {
                    Id = DbUtils.GetInt(reader, "UserId"),
                    FirstName = DbUtils.GetString(reader, "UserFirstName"),
                    LastName = DbUtils.GetString(reader, "UserLastName"),
                    FirebaseUserId = DbUtils.GetString(reader, "FirebaseUserId"),
                    Email = DbUtils.GetString(reader, "Email"),
                },
                Brand = new Brand()
                {

                    Id = DbUtils.GetInt(reader, "BrandId"),
                    BrandName = DbUtils.GetString(reader, "BrandName"),
                },
                Style = new Style()
                {
                    Id = DbUtils.GetInt(reader, "stId"),
                    Name = DbUtils.GetString(reader, "stName"),
                }


            };

        }

      


            };

        }





    

