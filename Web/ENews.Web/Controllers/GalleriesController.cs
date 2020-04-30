namespace ENews.Web.Controllers
{
    using ENews.Services.Data;
    using ENews.Web.ViewModels.Galleries;
    using Microsoft.AspNetCore.Mvc;

    public class GalleriesController : Controller
    {
        private readonly IGalleriesService galleryService;

        public GalleriesController(IGalleriesService galleryService)
        {
            this.galleryService = galleryService;
        }

        public IActionResult Preview(int id)
        {
            var model = this.galleryService.PreviewGalleryById<GalleryPreviewViewModel>(id);
            if (model == null)
            {
                return this.NotFound();
            }

            return this.View(model);
        }

        public IActionResult Index()
        {
            var model = new GalleriesIndexViewModel()
            {
                LastGallery = this.galleryService.GetNewestGallery<GalleryPreviewViewModel>(),
                LastestGalleries = this.galleryService.GetLatestGalleries<GalleryPreviewViewModel>(1),
            };
            return this.View(model);
        }
    }
}
