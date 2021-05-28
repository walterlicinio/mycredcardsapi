using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace myCredCardsAPI.Data.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Mail { get; set; }

        //Navigation Properties
        public List<Card> Cards { get; set; }
    }
}
