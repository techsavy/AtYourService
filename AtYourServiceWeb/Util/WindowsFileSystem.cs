// -----------------------------------------------------------------------
// <copyright file="WindowsFileSystem.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace AtYourService.Web.Util
{
    using System;
    using System.IO;
    using System.Web;
    using Core;

    public class WindowsFileSystem : IFileSystem
    {
        private readonly HttpServerUtilityBase _server;
        private const string _root = "App_Data";

        public WindowsFileSystem(HttpServerUtilityBase server)
        {
            _server = server;
        }

        private string GetServerSideFullname(string filename)
        {
            return Path.Combine(_server.MapPath("/" + _root), filename);
        }

        public void Save(Stream fileStream, string filename, bool overwrite)
        {
            var bytes = new byte[fileStream.Length];
            fileStream.Read(bytes, 0, (int)fileStream.Length);

            Save(bytes, filename, overwrite);
        }

        public void Save(byte[] bytes, string filename, bool overwrite)
        {
            filename = GetServerSideFullname(filename);
            var directory = Path.GetDirectoryName(filename);
            if (!Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            if (Exists(filename))
            {
                if (overwrite)
                {
                    Delete(filename);
                }
                else
                {
                    throw new ApplicationException(string.Format("Existed file {0} please select another name or set the overwrite = true."));
                }
            }

            using (var stream = File.Create(filename))
            {
                stream.Write(bytes, 0, bytes.Length);
            }
        }

        public byte[] Load(string filename)
        {
            filename = GetServerSideFullname(filename);
            byte[] bytes;

            using (var stream = File.OpenRead(filename))
            {
                bytes = new byte[stream.Length];
                stream.Read(bytes, 0, bytes.Length);
            }

            return bytes;
        }

        public bool Exists(string filename)
        {
            filename = GetServerSideFullname(filename);
            if (File.Exists(filename))
            {
                return true;
            }
            else
            {
                return Directory.Exists(filename);
            }
        }

        public void Delete(string filename)
        {
            filename = GetServerSideFullname(filename);
            if (File.Exists(filename))
            {
                File.Delete(filename);
            }
        }
    }
}