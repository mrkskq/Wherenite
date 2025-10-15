using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace Wherenite.Models
{
	public class TicketCategory
	{
        public int Id { get; set; }

        [DisplayName("Категорија")]
        public string Name { get; set; }

        [DisplayName("Цена")]
        public decimal Price { get; set; }

        public int EventId { get; set; }
        public virtual Event Event { get; set; }
    }
}