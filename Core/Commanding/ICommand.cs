// -----------------------------------------------------------------------
// <copyright file="ICommand.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace AtYourService.Core.Commanding
{
    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public interface ICommand
    {
        void Execute(CommandContext context);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TResult"></typeparam>
    public interface ICommand<out TResult> : ICommand
    {
        TResult Result { get; }
    }
}
