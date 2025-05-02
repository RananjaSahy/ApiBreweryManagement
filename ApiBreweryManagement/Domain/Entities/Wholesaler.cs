using ApiBreweryManagement.Domain.Common;
using System.ComponentModel.DataAnnotations;

namespace ApiBreweryManagement.Domain.Entities
{
    public class Wholesaler : EntityBaseAuditable
    {
        [StringLength(25, ErrorMessage = "Le nom du grossiste ne peut pas dépasser 25 caractères.")]
        public string Name { get; set; } = null!;
        public virtual ICollection<WholesalerStock> WholesalerStocks { get; set; } = new List<WholesalerStock>();

    }
}
