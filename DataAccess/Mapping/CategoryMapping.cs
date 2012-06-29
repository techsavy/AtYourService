// -----------------------------------------------------------------------
// <copyright file="CategoryMapping.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------


namespace AtYourService.Data.Mapping
{
    using Domain.Categories;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class CategoryMapping : EntityMapping<Category>
    {
        public CategoryMapping()
        {
            Property(category => category.Name, m => { m.NotNullable(true); m.Length(50); });
            ManyToOne(category => category.ParentCategory, m => { m.Column("ParentId"); m.NotNullable(false); m.ForeignKey("FK_Category_Category"); });
            Set(category => category.SubCategories, m => { m.OrderBy(c => c.Name); m.Key(k => k.Column("ParentId")); }, l => l.OneToMany());
            Set(category => category.Services, m => { m.OrderBy(c => c.Title); m.Key(k => k.Column("CategoryId")); }, l => l.OneToMany());
        }
    }
}
