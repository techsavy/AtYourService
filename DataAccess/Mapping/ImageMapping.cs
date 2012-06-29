// -----------------------------------------------------------------------
// <copyright file="ImageMapping.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace AtYourService.Data.Mapping
{
    using Domain.Files;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class ImageMapping : EntityMapping<Image>
    {
        public ImageMapping()
        {
            Property(image => image.FileName, m => { m.NotNullable(true); m.Length(50); m.UniqueKey("UQ_Image_FileName"); });
            Property(image => image.ContentType, m => { m.NotNullable(true); });

            Set(image => image.Services, m => { m.OrderBy(c => c.Title); m.Key(k => k.Column("ImageId")); }, l => l.OneToMany());
        }
    }
}
