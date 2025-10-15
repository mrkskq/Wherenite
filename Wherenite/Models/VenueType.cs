using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace Wherenite.Models
{
    public class VenueType
    {
        
        public int Id { get; set; }
        [DisplayName("Тип на простор")]
        public string Name { get; set; }
        [DisplayName("Настани")]

        public virtual List<Venue> Venues { get; set; }
    }
}