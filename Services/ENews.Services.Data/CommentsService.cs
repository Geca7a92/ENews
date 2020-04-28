using ENews.Data.Common.Repositories;
using ENews.Data.Models;
using ENews.Services.Mapping;
using ENews.Web.ViewModels.Comments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ENews.Services.Data
{
    public class CommentsService : ICommentsService
    {
        private readonly IDeletableEntityRepository<Comment> commentRepository;

        public CommentsService(IDeletableEntityRepository<Comment> commentRepository)
        {
            this.commentRepository = commentRepository;
        }

        public async Task Create(CreateCommentInputModel model)
        {
            var comment = new Comment()
            {
                ArticleId = model.ArticleId,
                UserId = model.UserId,
                Content = model.Content,
            };
            await this.commentRepository.AddAsync(comment);
            await this.commentRepository.SaveChangesAsync();
        }

        public IEnumerable<T> GetLatesByCreatedOn<T>(int take)
        {
            IQueryable<Comment> query
                = this.commentRepository.All().OrderByDescending(x => x.CreatedOn).Take(take);

            return query.To<T>().ToList();
        }
    }
}
