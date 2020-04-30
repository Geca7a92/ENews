namespace ENews.Services.Data.Tests
{
    using System;
    using System.Linq;
    using System.Reflection;

    using ENews.Data.Models;
    using ENews.Services.Data.Tests.Repositories;
    using ENews.Services.Data.Tests.Seed;
    using ENews.Services.Mapping;
    using ENews.Web.ViewModels.Comments;
    using Xunit;

    public class CommentsServiceTests
    {
        [Fact]
        public void CreateAsyncTest()
        {
            var repository = CommentRepository.Create();

            var comment = SeedComment.Create();

            repository.AddAsync(comment).GetAwaiter().GetResult();
            repository.SaveChangesAsync().GetAwaiter().GetResult();

            var service = new CommentsService(repository);

            AutoMapperConfig.RegisterMappings(typeof(CreateCommentInputModel).GetTypeInfo().Assembly);
            service.CreateAsync(new CreateCommentInputModel { Content = "New comment" }).GetAwaiter();

            Assert.Equal(2, repository.All().Count());
        }

        //Not working
        [Fact]
        public void GetLatesByCreatedOnTest()
        {
            var repository = CommentRepository.Create();

            var comment = SeedComment.Create();
            var secondComment = SeedComment.CreateSecond();
            var thirdComment = SeedComment.CreateThird();

            repository.AddAsync(comment).GetAwaiter().GetResult();
            repository.SaveChangesAsync().GetAwaiter().GetResult();

            var service = new CommentsService(repository);

            AutoMapperConfig.RegisterMappings(typeof(CommentPreviewViewModel).GetTypeInfo().Assembly);
            var result = service.GetLatesByCreatedOn<CommentPreviewViewModel>(1);

            Assert.Equal(0, result.Count());
        }

        public class CommentDummy : IMapFrom<Comment>
        {
            public string UserId { get; set; }

            public string UserUserName { get; set; }

            public int ArticleId { get; set; }

            public string Content { get; set; }

            public DateTime CreatedOn { get; set; }
        }
    }
}
