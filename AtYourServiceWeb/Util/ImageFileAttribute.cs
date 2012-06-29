﻿// -----------------------------------------------------------------------
// <copyright file="ImageFileAttribute.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace AtYourService.Web.Util
{
    using System.ComponentModel.DataAnnotations;
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.Web;

    public class ImageFileAttribute : ValidationAttribute
    {
        /// <summary>
        /// Determines whether the specified value of the object is valid. 
        /// </summary>
        /// <returns>
        /// true if the specified value is valid; otherwise, false.
        /// </returns>
        /// <param name="value">The value of the object to validate. </param>
        public override bool IsValid(object value)
        {
            if (value == null)
            {
                return true;                
            }                

            var file = value as HttpPostedFileBase;

            if (file == null)
            {
                return false;
            }

            if (file.ContentLength > 1 * 1024 * 1024)
            {
                return false;
            }

            try
            {
                using (var img = Image.FromStream(file.InputStream))
                {
                    return img.RawFormat.Equals(ImageFormat.Png) || img.RawFormat.Equals(ImageFormat.Jpeg) || img.RawFormat.Equals(ImageFormat.Gif);
                }
            }
// ReSharper disable EmptyGeneralCatchClause
            catch { }
// ReSharper restore EmptyGeneralCatchClause

            return false;
        }
    }
}