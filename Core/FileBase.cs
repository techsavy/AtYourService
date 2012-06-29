// -----------------------------------------------------------------------
// <copyright file="FileBase.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace AtYourService.Core
{

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public abstract class FileBase
    {
        public virtual string FileName { get; protected set; }

        public virtual string ContentType { get; protected set; }

        public virtual System.IO.Stream InputStream { get; protected set; }
    }
}
