// -----------------------------------------------------------------------
// <copyright file="RenameCategoryCommand.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace AtYourService.Domain.Categories
{
    using Core.Commanding;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class RenameCategoryCommand : EntityCommand
    {
        public RenameCategoryCommand(int categoryId, string name)
        {
            CategoryId = categoryId;
            Name = name;
        }

        public int CategoryId { get; private set; }

        public string Name { get; private set; }

        protected override void OnExecute()
        {
            var category = Session.Load<Category>(CategoryId);
            category.Name = Name;

            //todo: length validation and duplicate name validation
            OnUpdate(category);
        }
    }
}
