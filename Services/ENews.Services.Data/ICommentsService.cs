using ENews.Web.ViewModels.Comments;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ENews.Services.Data
{
    public interface ICommentsService
    {
        Task Create(CreateCommentInputModel model);
    }
}
