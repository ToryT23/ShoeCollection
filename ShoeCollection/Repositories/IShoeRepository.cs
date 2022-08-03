using ShoeCollection.Models;
using System.Collections.Generic;

namespace ShoeCollection.Repositories
{
    public interface IShoeRepository
    {
        void AddShoe(Shoe shoe);
        void DeleteAShoe(int shoeId);
        List<Shoe> GetAllShoes();
        Shoe GetShoeById(int id, int userId);
        List <Shoe> GetShoesByLoggedUser(int id);
        List<Favorite> GetFavoritesByLoggedUser(int id);
        void AddAFavorite(Favorite favorite);
        void DeleteAFavorite(int favId, int userId);
        void UpdateAShoe(Shoe shoe, int userId);
    }
}