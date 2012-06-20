// -----------------------------------------------------------------------
// <copyright file="EntityCommand.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace AtYourService.Core.Commanding
{
    using System;
    using Data;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public abstract class EntityCommand : Command
    {
        protected virtual void OnCreate(Entity entity)
        {
            entity.CreationDate = entity.LastModifiedDate = DateTime.Now;
            entity.CreatedBy = entity.LastModifiedBy = Context.User;
        }

        protected virtual void OnUpdate(Entity entity)
        {
            entity.LastModifiedDate = DateTime.Now;
            entity.LastModifiedBy = Context.User;
        }
    }

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public abstract class EntityCommand<TResult> : Command<TResult>
    {
        protected virtual void OnCreate(Entity entity)
        {
            entity.CreationDate = entity.LastModifiedDate = DateTime.Now;
            entity.CreatedBy = entity.LastModifiedBy = Context.User;
        }

        protected virtual void OnUpdate(Entity entity)
        {
            entity.LastModifiedDate = DateTime.Now;
            entity.LastModifiedBy = Context.User;
        }
    }
}
