using Application.Services.CommandHandlers.DonutHandlers;
using Application.Services.Commands;
using Domain.Entities;
using Domain.Services.Repositories;
using Moq;
using System.Net;

namespace UnitTests.DonutTests
{
    [TestClass]
    public class DonutDeleteTest : BaseTest<DonutDeleteHandler>
    {
        [TestMethod]
        public async Task Delete()
        {
            var repoMock = new Mock<IRepository<Donut>>();
            var id = 1;

            repoMock.Setup(x => x.FindAsync(default, id))
                .ReturnsAsync(new Donut())
                .Verifiable();

            _unitOfWorkMock.Setup(x => x.Donuts).Returns(repoMock.Object);
            var handler = new DonutDeleteHandler(_unitOfWorkMock.Object, _loggerMock.Object);
            var result = await handler.Handle(new DeleteCommand<Donut, int>(id), default);
            repoMock.VerifyAll();
            repoMock.Verify(x => x.Remove(It.IsAny<Donut>()));
            _unitOfWorkMock.Setup(x => x.SaveChangesAsync(default));
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Succeeded);
            Assert.AreEqual(HttpStatusCode.NoContent, result.StatusCode);
        }

        [TestMethod]
        public async Task DeleteNonExistingDonut()
        {
            _unitOfWorkMock.Setup(x => x.Donuts)
                .Returns(new Mock<IRepository<Donut>>().Object);

            var handler = new DonutDeleteHandler(_unitOfWorkMock.Object, _loggerMock.Object);
            var result = await handler.Handle(new DeleteCommand<Donut, int>(1), default);
            Assert.IsNotNull(result);
            Assert.IsFalse(result.Succeeded);
            Assert.AreEqual(HttpStatusCode.NotFound, result.StatusCode);
        }
    }
}
