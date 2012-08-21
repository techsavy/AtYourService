

namespace AtYourService.Web.Controllers
{
    using System.Web.Mvc;
    using Core;
    using Domain.Files;
    using Util;

    public class ImagesController : BaseController
    {
        private readonly IFileSystem _fileSystem;
        private readonly ImageResizer _imageResizer;
        //
        // GET: /Images/

        public ImagesController(NHibernateContext nHibernateContext, IFileSystem fileSystem, ImageResizer imageResizer) : base(nHibernateContext)
        {
            _fileSystem = fileSystem;
            _imageResizer = imageResizer;
        }

        public FileResult Get(int id)
        {
            return GetImage(id, Size.Large);
        }

        public FileResult GetThumbnail(int id)
        {
            return GetImage(id, Size.Thumbnail);
        }

        public FileResult GetSmall(int id)
        {
            return GetImage(id, Size.Small);
        }

        public FileResult GetMedium(int id)
        {
            return GetImage(id, Size.Medium);
        }

        public FileResult GetLarge(int id)
        {
            return GetImage(id, Size.Large);
        }

        private FileResult GetImage(int id, Size size)
        {
            var image = ExecuteQuery(session => session.Load<Image>(id));
            var file = _fileSystem.Load(image.FileName);

            return File(_imageResizer.Resize(file, size), image.ContentType, image.FileName);
        }
    }
}
