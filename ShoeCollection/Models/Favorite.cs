namespace ShoeCollection.Models
{
    public class Favorite
    {
       public int Id { get; set; }
        public int UserId  { get; set; }

        public User User { get; set; }
        public int ShoeId { get; set; }

        public Shoe Shoe { get; set; }
    }
}
