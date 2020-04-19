﻿using ENews.Web.ViewModels.MembersArea.Articles;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ENews.Services.Data
{
    public interface IArticleService
    {
        IEnumerable<T> GetAll<T>(int? count = null);

        IEnumerable<T> GetAllByCategoryId<T>(int id, int? take = null, int skip = 0);

        IEnumerable<T> GetAllBySubCategoryId<T>(int id, int? take = null, int skip = 0);

        IEnumerable<T> GetAllByAtuthorId<T>(string id);

        IEnumerable<T> GetLatesByCreatedOn<T>(int? count = null);

        int GetCountByCategoryId(int id);

        int GetCountBySubCategoryId(int id);

        T GetArticleById<T>(int id);

        Task<int> CreateAsync(ArticleCreateInputModel model, string userId);
    }
}
