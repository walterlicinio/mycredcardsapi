using System;

namespace myCredCardsAPI.Data.Models
{
    public class Card
    {
        public int Id { get; set; }
        public string CardNumber { get; set; }
        public DateTime CreatedAt { get; set; }
        //navigational properties
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
