using ApiBreweryManagement.Domain.Common;
using System.ComponentModel.DataAnnotations;

namespace ApiBreweryManagement.Domain.Entities
{
    public class WholesalerStock : EntityBaseAuditable
    {
        public int WholesalerId { get; set; }
        public int BeerId { get; set; }
        [Range(0, int.MaxValue, ErrorMessage = "La quantité ne peut pas être négative.")]
        public int Quantity { get; set; }
        public virtual Wholesaler Wholesaler { get; set; } = null!;
        public virtual Beer Beer { get; set; } = null!;
    }
}
