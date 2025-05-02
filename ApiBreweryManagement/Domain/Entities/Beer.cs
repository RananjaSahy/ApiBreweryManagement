using ApiBreweryManagement.Domain.Common;
using System.ComponentModel.DataAnnotations;

namespace ApiBreweryManagement.Domain.Entities
{
    public class Beer : EntityBaseAuditable
    {
        [StringLength(25, ErrorMessage = "Le nom de la bière ne peut pas dépasser 25 caractères.")]
        public string Name { get; set; } = null!;
        [Range(0, 100, ErrorMessage = "L'alcool doit être entre 0 et 100.")]
        public double AlcoholContent { get; set; }
        [Range(0, double.MaxValue, ErrorMessage = "Le prix ne peut pas être négatif.")]
        public decimal Price { get; set; }

        public int BreweryId { get; set; }
        public virtual Brewery Brewery { get; set; } = null!;
        public virtual ICollection<WholesalerStock> WholesalerStocks { get; set; } = new List<WholesalerStock>();

    }
}
