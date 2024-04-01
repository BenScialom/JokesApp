﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace JokesApp.Models
{

    public class CategoriesClass
    {
        public bool Error { get; set; }
        [JsonPropertyName("categories")]
        public string[] CategoriesList { get; set; }
        public Categoryalias[] CategoryAliases { get; set; }
        public long Timestamp { get; set; }
    }

    public class Categoryalias
    {
        public string alias { get; set; }
        public string resolved { get; set; }
    }

}
