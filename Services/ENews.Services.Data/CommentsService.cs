using ENews.Data.Common.Repositories;
using ENews.Data.Models;
using ENews.Web.ViewModels.Comments;
using System;
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
    }
}
