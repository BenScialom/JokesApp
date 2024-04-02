using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace JokesApp.Models
{

    public class Categories
    {
        public bool Error { get; set; }
        [JsonPropertyName("categories")]
        public string[] CategoriesList { get; set; }
       
    }

  
}
