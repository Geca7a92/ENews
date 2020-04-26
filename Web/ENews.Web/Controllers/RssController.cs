﻿using ENews.Common;
using ENews.Data;
using ENews.Services.Data;
using ENews.Web.ViewModels.Rss;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ENews.Web.Controllers
{
    [Produces("application/xml")]
    public class RssController : Controller
    {
        private readonly IArticlesService articlesService;

        public RssController(IArticlesService articlesService)
        {
            this.articlesService = articlesService;
        }

        public ActionResult ENews()
        {
            XNamespace ns = "http://www.w3.org/2005/Atom";
            var rss = new XElement("rss", new XAttribute("version", "2.0"), new XAttribute(XNamespace.Xmlns + "atom", ns));

            var channel = new XElement(
                "channel",
                new XElement("title", $"{GlobalConstants.SystemName}"),
                new XElement("link", $"{GlobalConstants.SystemUrl}"),
                new XElement("description", $"{GlobalConstants.SystemName}"),
                new XElement("language", "en-us"),
                new XElement("copyright", $"Copyright 2020-{DateTime.UtcNow.Year} Georgi Georgiev"),
                new XElement("lastBuildDate", this.articlesService.LastesArticleCreationDate().ToUniversalTime().ToString("r")),
                new XElement(ns + "link", new XAttribute("href", "https://localhost:44319/Rss/Enews"), new XAttribute("rel", "self"), new XAttribute("type", "application/rss+xml")),
                new XElement(
                    "image",
                    new XElement("url", $"{GlobalConstants.SystemImageUrl}"),
                    new XElement("title", $"{GlobalConstants.SystemName}")));

            var articles = this.articlesService.GetLatesByCreatedOn<ArticleRssModel>(10);

            foreach (var article in articles)
            {
                var postInRss = new XElement("item");
                postInRss.Add(new XElement("title", article.Title));
                postInRss.Add(new XElement("image", article.PictureImageUrl));
                postInRss.Add(new XElement("description", article.SanitizedContent));
                postInRss.Add(new XElement("link", $"{GlobalConstants.SystemArticlePreviewUrl}" + article.Id));
                postInRss.Add(new XElement("author", $"{article.AuthorFirstName} {article.AuthorLastName}"));
                postInRss.Add(new XElement("pubDate", article.CreatedOn.ToUniversalTime().ToString("r")));
                postInRss.Add(new XElement("guid", article.Title + "#When" + article.CreatedOn.ToUniversalTime()
                                           .ToString(CultureInfo.InvariantCulture).Replace(" ", string.Empty), new XAttribute("isPermaLink", "false")));
                channel.Add(postInRss);
            }

            rss.Add(channel);

            return Ok(rss);
        }
    }
}
