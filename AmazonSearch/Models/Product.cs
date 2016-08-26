using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// MODEL
/// </summary>

namespace AmazonSearch.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public float Price { get; set; }
        public string ImgURL { get; set; }
    }
}