using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace Wherenite.Models
{
	public class Event
	{
        public int Id { get; set; }
        [DisplayName("Настан")]

        public string Title { get; set; }

        [DisplayName("Слика")]
        public string EventImage { get; set; }
        [DisplayName("Датум")]

        public DateTime Date { get; set; }
        [DisplayName("Категорија")]

        public string Description { get; set; }
        [DisplayName("Цена")]

        public int Price { get; set; }
        [DisplayName("Опис")]

        public string About { get; set; }
        [DisplayName("Локација")]

        public int VenueId { get; set; }
        public virtual Venue Venue { get; set; }
        public virtual List<Ticket> Tickets { get; set; }
        public virtual List<Song> Songs { get; set; }
        public virtual List<TicketCategory> TicketCategories { get; set; }

    }
}