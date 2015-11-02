using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Movies.Models
{
    public class Movie
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "Please enter a movie")]
        [StringLength(60)]
        public string Title { get; set; }

        [Required(ErrorMessage = "Enter a valid date")]
        [DataType(DataType.Date)]
        [Display(Name = "Release Date")]
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        public DateTime ReleaseDate { get; set; }

        [Required(ErrorMessage = "Please enter a genre")]
        [StringLength(15)]
        public string Genre { get; set; }

        [Range(1, 100)]
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }

        [StringLength(5)]
        [RegularExpression("^[GPRNgprn][GCgc]?[-173]*$")]
        public string Rating { get; set; }

    }

    public class MovieDBContext : DbContext
    {
        public DbSet<Movie> Movies { get; set; }
    }
}