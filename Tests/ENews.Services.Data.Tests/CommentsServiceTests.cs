namespace ENews.Services.Data.Tests
{
    using System.Linq;
    using System.Reflection;
    using ENews.Data.Models;
    using ENews.Data.Repositories;
    using ENews.Services.Data.Tests.Models;
    using ENews.Services.Data.Tests.Repositories;
    using ENews.Services.Data.Tests.Seed;
    using ENews.Services.Mapping;
    using ENews.Web.ViewModels.Comments;
    using Xunit;

    public class CommentsServiceTests
    {
        public CommentsServiceTests()
        {
            AutoMapperConfig.RegisterMappings(typeof(CommentDummyModel).GetTypeInfo().Assembly);

        }

        [Fact]
        public void CreateAsyncTest()
        {
            var repository = CommentRepository.Create();
            var comment = this.CreateComment(repository, SeedComment.Create());
            var service = this.GetCommentService(repository);

            service.CreateAsync(new CreateCommentInputModel { Content = "New comment" }).GetAwaiter();

            Assert.Equal(2, repository.All().Count());
        }

        [Fact]
        public void GetLatesByCreatedOnTest()
        {
            var repository = CommentRepository.Create();

            var comment = this.CreateComment(repository, SeedComment.Create());
            var secondComment = this.CreateComment(repository, SeedComment.CreateSecond());
            var thirdComment = this.CreateComment(repository, SeedComment.CreateThird());
            var service = this.GetCommentService(repository);

            var result = service.GetLatesByCreatedOn<CommentDummyModel>(1);

            Assert.Empty(result);
        }

        private CommentsService GetCommentService(EfDeletableEntityRepository<Comment> repository)
        {
            var service = new CommentsService(repository);
            return service;
        }

        private Comment CreateComment(EfDeletableEntityRepository<Comment> repository, Comment comment)
        {
            repository.AddAsync(comment).GetAwaiter().GetResult();
            repository.SaveChangesAsync().GetAwaiter().GetResult();
            return comment;
        }
    }
}
