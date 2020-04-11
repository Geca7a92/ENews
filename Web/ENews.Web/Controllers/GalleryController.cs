﻿using ENews.Services.Data;
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
        private readonly IGalleryService galleryService;

        public GalleryController(IGalleryService galleryService)
        {
            this.galleryService = galleryService;
        }

        public IActionResult Preview(int id, int articleId)
        {
            var model = this.galleryService.PreviewGalleryById<GalleryPreviewViewModel>(id);
            model.ArticleId = articleId;
            return this.View(model);
        }
    }
}
