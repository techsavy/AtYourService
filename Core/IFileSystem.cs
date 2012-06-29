// -----------------------------------------------------------------------
// <copyright file="IFileSystem.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace AtYourService.Core
{
    using System.IO;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public interface IFileSystem
    {
        void Save(Stream fileStream, string filename, bool overwrite);

        void Save(byte[] bytes, string filename, bool overwrite);

        byte[] Load(string filename);

        bool Exists(string filename);

        void Delete(string filename);
    }
}
