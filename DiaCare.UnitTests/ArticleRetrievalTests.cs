using AutoMapper;
using DiaCare.Application.DTOS;
using DiaCare.Application.Services;
using DiaCare.Domain.Entities;
using DiaCare.Domain.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Threading;

namespace DiaCare.UnitTests
{
    [TestClass]
    public class ArticleRetrievalTests
    {
        [TestMethod]
        public async Task GetArticleById_ShouldReturnCorrectData()
        {
            // 1. Arrange: (Setup Mocks)
            int expectedId = 1;
            string expectedTitle = "Diabetes Care 101";
            // Mocks are like "fake" versions of our dependencies  without needing a real database or actual mapping logic.
            var mockRepo = new Mock<IBaseRepository<Article>>();
            var mockUow = new Mock<IUnitOfWork>();
            var mockMapper = new Mock<IMapper>();

            // returning a fake article when the repository's GetByIdAsync method is called with the expected ID
            var fakeArticle = new Article { Id = 1, Title = "Diabetes Care 101" };
            var fakeDto = new ArticleDto { Id = 1, Title = "Diabetes Care 101" };

            mockRepo.Setup(repo => repo.GetByIdAsync(expectedId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(fakeArticle);
            mockMapper.Setup(m => m.Map<ArticleDto>(fakeArticle)).Returns(fakeDto);

            // Create the service instance with the mocked dependencies
            var service = new ArticleService(mockRepo.Object, mockUow.Object, mockMapper.Object);


            // 2. Act: Execute the method
            var result = await service.GetArticleByIdAsync(expectedId);

            // 3. Assert 
            Assert.IsNotNull(result); // Is Data Returned? (Not Null)
            Assert.AreEqual(expectedId, result.Id); // Is ID is correct?
            Assert.AreEqual(expectedTitle, result.Title); // Is Title is correct?
        }
    }
}