using System;
using System.ComponentModel.DataAnnotations;

namespace PrashTechnologies.Core.Entities
{
    public class Product
    {
        [Required(ErrorMessage = "Name is required.")]
        [MaxLength(20)]
        public string Name { get; set; }

        public Guid Code { get; }

        public long? Clicks { get; }

        [MaxLength(50)]
        public string Description { get; set; }

        [Required(ErrorMessage = "Current Cost is required.")]
        [DisplayFormat(DataFormatString = "{0:C0}")]
        public decimal? CurrentCost { get; set; }

        public string ExchangeCode { get; set; }
    }
}
