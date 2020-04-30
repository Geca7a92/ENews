namespace ENews.Web.ViewModels.Galleries
{
    using System.Collections.Generic;

    public class GalleriesIndexViewModel
    {
        public GalleryPreviewViewModel LastGallery { get; set; }

        public IEnumerable<GalleryPreviewViewModel> LastestGalleries { get; set; }
    }
}
