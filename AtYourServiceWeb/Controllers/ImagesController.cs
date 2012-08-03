

namespace AtYourService.Web.Controllers
{
    using System.Web.Mvc;
    using Core;
    using Domain.Files;
    using Util;

    public class ImagesController : BaseController
    {
        private readonly IFileSystem _fileSystem;
        //
        // GET: /Images/

        public ImagesController(NHibernateContext nHibernateContext, IFileSystem fileSystem) : base(nHibernateContext)
        {
            _fileSystem = fileSystem;
        }

        public FileResult Get(int id)
        {
            var image = ExecuteQuery(session => session.Load<Image>(id));
            var file = _fileSystem.Load(image.FileName);

            return File(file, image.ContentType, image.FileName);
        }

    }
}
