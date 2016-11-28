using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EF_ContainsAndStartsWith.DomainAndData.Models
{
    public class Category
    {
        public Category()
        {
            Products = new HashSet<Product>();
        }
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        
        [MaxLength(5)]
        public string Code { get; set; }

        [MaxLength(200)]
        public string Name { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
