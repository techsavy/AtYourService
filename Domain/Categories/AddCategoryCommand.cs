// -----------------------------------------------------------------------
// <copyright file="AddCategoryCommand.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace AtYourService.Domain.Categories
{
    using Core.Commanding;
    using Iesi.Collections.Generic;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class AddCategoryCommand : EntityCommand
    {
        public AddCategoryCommand(int? parentId, string name)
        {
            ParentId = parentId;
            Name = name;
        }

        public int? ParentId { get; private set; }
        public string Name { get; private set; }

        protected override void OnExecute()
        {
            var category = new Category { Name = Name };
            if (ParentId != null)
            {
                var parentCategory = Session.QueryOver<Category>()
                    .Where(parent => parent.Id == ParentId.Value)
                    .Fetch(parent => parent.SubCategories).Eager
                    .SingleOrDefault();

                category.ParentCategory = parentCategory;

                if (parentCategory.SubCategories == null)
                    parentCategory.SubCategories = new HashedSet<Category>();

                parentCategory.SubCategories.Add(category);
            }

            //todo: validation
            OnCreate(category);
            Session.Save(category);
        }
    }
}
