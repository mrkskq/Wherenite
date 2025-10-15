using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Wherenite.Models
{
	public class Ticket
	{
        [Key]
        public int Id { get; set; }
        [DisplayName("Име и презиме")]

        public string Name { get; set; }
        [DisplayName("E-mail")]

        public string Email { get; set; }
        [DisplayName("Број на карти")]

        public int TicketCount { get; set; }
        [DisplayName("Настан")]

        public int EventId { get; set; }
        public virtual Event Event { get; set; }

        public string UserId { get; set; }
        public DateTime PurchaseDate { get; set; } = DateTime.Now;
    }
}