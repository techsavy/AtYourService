// -----------------------------------------------------------------------
// <copyright file="Entity.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace AtYourService.Core.Data
{
    using System;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public abstract class Entity
    {
        public virtual int Id { get; set; }

        public virtual DateTime CreationDate { get; set; }

        public virtual string CreatedBy { get; set; }

        public virtual DateTime LastModifiedDate { get; set; }

        public virtual string LastModifiedBy { get; set; }

        public virtual bool IsTransient()
        {
            return Id == default(int);
        }

        public virtual bool Equals(Entity other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            if (IsTransient() || other.IsTransient()) return false;

            return other.Id == Id;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof (Entity)) return false;

            return Equals((Entity) obj);
        }

        public override int GetHashCode()
        {
            return Id;
        }
    }
}
