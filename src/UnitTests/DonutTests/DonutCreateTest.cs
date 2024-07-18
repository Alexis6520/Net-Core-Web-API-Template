using Application.Services.CommandHandlers.DonutHandlers;
using Application.Services.Commands.DonutCommands;
using Application.Services.Validators.DonutValidators;
using Domain.Entities;
using Domain.Services.Repositories;
using Moq;
using System.Net;

namespace UnitTests.DonutTests
{
    [TestClass]
    public class DonutCreateTest : BaseTest<DonutCreateHandler>
    {
        public static IEnumerable<object[]> ValidationData
        {
            get
            {
                return
                [
                    [new DonutCreateCommand("",10)],
                    [new DonutCreateCommand(null,10)],
                ];
            }
        }

        [TestMethod]
        [DynamicData(nameof(ValidationData))]
        public void ValidateCommand(DonutCreateCommand command)
        {
            var validator = new DonutCreateValidator();
            var result = validator.Validate(command);
            Assert.IsFalse(result.IsValid);
        }

        [TestMethod]
        public async Task Create()
        {
            _unitOfWorkMock.Setup(x => x.Donuts)
                .Returns(new Mock<IRepository<Donut>>().Object);

            var handler = new DonutCreateHandler(_unitOfWorkMock.Object, _loggerMock.Object);
            var command = new DonutCreateCommand("Frambuesa", 19);
            var result = await handler.Handle(command, default);
            _unitOfWorkMock.Verify(x => x.Donuts.AddAsync(It.IsAny<Donut>(), default));
            _unitOfWorkMock.Verify(x => x.SaveChangesAsync(default));
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Succeeded);
            Assert.AreEqual(HttpStatusCode.Created, result.StatusCode);
        }
    }
}
