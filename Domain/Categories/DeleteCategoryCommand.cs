// -----------------------------------------------------------------------
// <copyright file="DeleteCategoryCommand.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace AtYourService.Domain.Categories
{
    using System;
    using Core.Commanding;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class DeleteCategoryCommand : EntityCommand
    {
        public int CategoryId { get; private set; }

        public DeleteCategoryCommand(int categoryId)
        {
            CategoryId = categoryId;
        }

        protected override void OnExecute()
        {
            var category = Session.QueryOver<Category>()
                .Where(c => c.Id == CategoryId)
                .SingleOrDefault();

            if (category == null)
                throw new DomainException("Invalid Category");

            if (category.AdCount > 0)
                throw new DomainException(string.Format("Unable to delete category {0} becase there are services associated with it.", category.Name));

            var count = Session.QueryOver<Category>()
                .Where(c => c.ParentCategory.Id == CategoryId).RowCount();

            if (count > 0)
                throw new DomainException(string.Format("Unable to delete category {0} becase there are sub categories.", category.Name));

            Session.Delete(category);
        }
    }
}
