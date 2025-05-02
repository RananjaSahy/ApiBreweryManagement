using ApiBreweryManagement.Domain.Common;
using System.ComponentModel.DataAnnotations;

namespace ApiBreweryManagement.Domain.Entities
{
    public class Brewery : EntityBaseAuditable
    {
        [StringLength(25, ErrorMessage = "Le nom de la brasserie ne peut pas dépasser 25 caractères.")]
        public string Name { get; set; } = null!;
        public virtual ICollection<Beer> Beers { get; set; } = new List<Beer>();

        public void AddBeer(Beer beer)
        {
            if (beer == null) throw new ArgumentNullException(nameof(beer));
            if (Beers.Contains(beer)) throw new InvalidOperationException("La bière existe déjà dans la brasserie");

            beer.BreweryId = this.Id;
            Beers.Add(beer);
        }
        public void RemoveBeer(Beer beer)
        {
            if (beer == null) throw new ArgumentNullException(nameof(beer));
            if (!Beers.Contains(beer)) throw new InvalidOperationException("La bière n'existe pas dans la brasserie");
            Beers.Remove(beer);
        }
    }
}
