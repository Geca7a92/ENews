namespace ENews.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using ENews.Web.ViewModels.Comments;

    public interface ICommentsService
    {
        Task CreateAsync(CreateCommentInputModel model);

        IEnumerable<T> GetLatesByCreatedOn<T>(int? take);
    }
}
