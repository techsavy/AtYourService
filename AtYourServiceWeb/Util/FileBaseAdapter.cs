// -----------------------------------------------------------------------
// <copyright file="FileBaseAdapter.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace AtYourService.Web.Util
{
    using System.Diagnostics.Contracts;
    using System.IO;
    using System.Web;
    using Core;

    public class FileBaseAdapter : FileBase
    {
        private readonly HttpPostedFileBase _httpPostedFile;

        public FileBaseAdapter(HttpPostedFileBase httpPostedFile)
        {
            Contract.Assert(httpPostedFile != null);

            _httpPostedFile = httpPostedFile;
            ContentType = _httpPostedFile.ContentType;
            FileName = _httpPostedFile.FileName;
            InputStream = _httpPostedFile.InputStream;
            InputStream.Seek(0, SeekOrigin.Begin);
        }
    }
}