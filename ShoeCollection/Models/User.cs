﻿namespace ShoeCollection.Models
{
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        
        public string LastName { get; set; }
         
        public string FirebaseUserId { get; set; }

        public string Email { get; set; }
    }
}
