using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace EF_ContainsAndStartsWith.DomainAndData.Models
{
    public class Product
    {
        [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }


        public string Name { get; set; }
        public decimal PriceExcl { get; set; }

        public int inStockAmount { get; set; }

        public bool isActive { get; set; }

        public Guid CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        public virtual Category Category { get; set; }
    }
}
