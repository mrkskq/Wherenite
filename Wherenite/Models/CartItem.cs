using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Wherenite.Models
{
	public class CartItem
	{
        [Key]
        public int Id { get; set; }
        public int EventId { get; set; }
        public virtual Event Event { get; set; }

        [DisplayName("Категорија на билет")]
        public string TicketCategoryId { get; set; }

        [DisplayName("Цена по билет")]
        public decimal TicketCategoryPrice { get; set; }

        [DisplayName("Број на карти")]
        public int Quantity { get; set; }

        public string UserId { get; set; }
    }
}