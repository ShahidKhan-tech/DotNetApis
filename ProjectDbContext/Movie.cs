using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace ProjectAssnmt.Model.ProjectDbContext
{
    public partial class Movie
    {
        [Key]
        public int MovieId { get; set; }
        public string Title { get; set; }
        public int? Price { get; set; }
        public int? Quantity { get; set; }
        public string Photo { get; set; }
        public int? CategoryId { get; set; }
        
        [NotMapped]
        public IFormFile imageFile { get; set; }
        public virtual Category Category { get; set; }
    }
}
