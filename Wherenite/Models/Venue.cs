using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace Wherenite.Models
{
	public class Venue
	{
        public int Id { get; set; }
        [DisplayName("Локација")]
        public string Name { get; set; }
        [DisplayName("Слика")]

        public string Address { get; set; }
        [DisplayName("Тип на простор")]

        public int VenueTypeId { get; set; }
        public virtual VenueType VenueType { get; set; }

        [DisplayName("Телефон за резервации 1:")]

        public string PhoneNumber1 { get; set; }

        [DisplayName("Телефон за резервации 2:")]

        public string PhoneNumber2 { get; set; }
        
        [DisplayName("Телефон за резервации 3:")]

        public string PhoneNumber3 { get; set; }
        public virtual List<Event> Events { get; set; }
    }
}