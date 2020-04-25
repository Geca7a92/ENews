using System;
using System.Collections.Generic;
using System.Text;

namespace ENews.Web.ViewModels.Galleries
{
    public class GalleriesIndexViewModel
    {
        public GalleryPreviewViewModel LastGallery { get; set; }

        public IEnumerable<GalleryPreviewViewModel> LastestGalleries { get; set; }
    }
}
