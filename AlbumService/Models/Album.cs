using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AlbumService.Models
{
    public class Album
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        public int Year { get; set; }
        public decimal Price { get; set; }
        public string Genre { get; set; }

        // Foreign Key
        public int ArtistId { get; set; }
        
        // Navigation property
        public Artist Artist { get; set; } // Lazy Loading: By adding the 'virtual' keyword it will auto references other SQL tables, but this can cause multiple db calls and performance issues
    }
}