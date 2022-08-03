using ShoeCollection.Models;

namespace ShoeCollection.Models
{
    public class Shoe
    {
        public  int Id { get; set; }
       public int UserId { get; set; }

        public User User { get; set; }

        public int Size { get; set; }

        public int StyleId { get; set; }

        public Style Style { get; set; }

        
        public int BrandId { get; set; }
        public Brand Brand { get; set; }
        public string ImageUrl { get; set; }

        public Favorite Favorite { get; set; }

    }
}
