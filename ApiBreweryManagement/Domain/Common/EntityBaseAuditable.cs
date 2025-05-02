namespace ApiBreweryManagement.Domain.Common
{
    public abstract class EntityBaseAuditable : EntityBase
    {
        public DateTimeOffset Created { get; set; }

        public DateTimeOffset LastModified { get; set; }
    }
}
