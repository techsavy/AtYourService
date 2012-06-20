// -----------------------------------------------------------------------
// <copyright file="ModelInspector.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace AtYourService.Data
{
    using System.Reflection;
    using NHibernate.Mapping.ByCode;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class ModelInspector : ExplicitlyDeclaredModel
    {
        public override bool IsOneToMany(MemberInfo member)
        {
            if (IsSet(member))
            {
                return true;
            }

            return base.IsOneToMany(member);
        }
    }
}
