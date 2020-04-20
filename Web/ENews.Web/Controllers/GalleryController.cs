using ENews.Services.Data;
using ENews.Web.ViewModels.Galleries;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ENews.Web.Controllers
{
    public class GalleryController : Controller
    {
        private readonly IGalleriesService galleryService;

        public GalleryController(IGalleriesService galleryService)
        {
            this.galleryService = galleryService;
        }

        public IActionResult Preview(int id)
        {
            var model = this.galleryService.PreviewGalleryById<GalleryPreviewViewModel>(id);
            return this.View(model);
        }
    }
}
