// -----------------------------------------------------------------------
// <copyright file="EntityMapping.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace AtYourService.Data.Mapping
{
    using Core.Data;
    using NHibernate.Mapping.ByCode;
    using NHibernate.Mapping.ByCode.Conformist;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public abstract class EntityMapping<TEntity> : ClassMapping<TEntity>
        where TEntity : Entity
    {
        protected EntityMapping()
        {
            Id(c => c.Id, c => c.Generator(Generators.Identity));

            Property(c => c.CreationDate, c => c.NotNullable(true));
            Property(c => c.CreatedBy, c => c.NotNullable(true));

            Property(c => c.LastModifiedDate, c => c.NotNullable(true));
            Property(c => c.LastModifiedBy, c => c.NotNullable(true));

            Schema("dbo");
        }
    }
}
