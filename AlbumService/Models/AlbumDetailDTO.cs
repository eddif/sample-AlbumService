using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AlbumService.Models
{
    public class AlbumDetailDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int Year { get; set; }
        public decimal Price { get; set; }
        public string ArtistName { get; set; }
        public string Genre { get; set; }

    }
}