using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace Wherenite.Models
{
	public class Song
	{
        public int Id { get; set; }

        [DisplayName("Линк од песната")]
        public string SongURL { get; set; }
        public int EventId { get; set; }
        public virtual Event Event { get; set; }
    }
}