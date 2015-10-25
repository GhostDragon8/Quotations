using System.ComponentModel;

namespace MyQuotes.Models
{
    public class Category
    {
        public int ID { get; set; }
        [DisplayName("Category")]

        public string Name { get; set; }
    }
}