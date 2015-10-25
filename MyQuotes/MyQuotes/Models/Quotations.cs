using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyQuotes.Models
{
    public class Quotations
    {
        public int ID { get; set; }
        [Required(ErrorMessage = "Please enter an Author")]
        public string Author { get; set; }

        [Required(ErrorMessage = "Please enter a quote")]
        public string Quote { get; set; }

        [DisplayName("Date Added")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        public DateTime DateAdded { get; set; }

        public int CategoryID { get; set; }


        public virtual Category Category { get; set; }
    }
}