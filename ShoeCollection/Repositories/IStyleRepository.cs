using ShoeCollection.Models;
using System.Collections.Generic;

namespace ShoeCollection.Repositories
{
    public interface IStyleRepository
    {
        void AddAStyle(Style style);
        void DeleteStyle(int styleId);
        List<Style> GetAllStyles();
        Style GetStyleById(int id);
        void UpdateStyle(Style style);
    }
}