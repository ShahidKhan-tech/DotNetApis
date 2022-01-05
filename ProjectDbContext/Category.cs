using System;
using System.Collections.Generic;

#nullable disable

namespace ProjectAssnmt.Model.ProjectDbContext
{
    public partial class Category
    {
        public Category()
        {
            Movies = new HashSet<Movie>();
        }

        public int CategoryId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Movie> Movies { get; set; }
    }
}
