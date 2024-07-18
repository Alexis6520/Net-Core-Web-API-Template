using Application.Services.CommandHandlers.DonutHandlers;
using Application.Services.Commands.DonutCommands;
using Application.Services.Validators.DonutValidators;
using Domain.Entities;
using Moq;
using System.Net;

namespace UnitTests.DonutTests
{
    [TestClass]
    public class DonutUpdateTest : BaseTest<DonutUpdateHandler>
    {
        public static IEnumerable<object[]> ValidationData
        {
            get
            {
                return
                [
                    [new DonutUpdateCommand("",10)],
                    [new DonutUpdateCommand(null,10)],
                ];
            }
        }

        [TestMethod]
        [DynamicData(nameof(ValidationData))]
        public void ValidateCommand(DonutUpdateCommand command)
        {
            var validator = new DonutUpdateValidator();
            var result = validator.Validate(command);
            Assert.IsFalse(result.IsValid);
        }

        [TestMethod]
        public async Task Update()
        {
            var donut = new Donut
            {
                Id = 1,
                Name = "test",
                Price = 10,
            };

            _unitOfWorkMock.Setup(x => x.Donuts.FindAsync(default, donut.Id))
                .ReturnsAsync(donut)
                .Verifiable();

            var command = new DonutUpdateCommand
            {
                Id = donut.Id,
                Name = "Frambuesa",
                Price = 5
            };

            var handler = new DonutUpdateHandler(_unitOfWorkMock.Object, _loggerMock.Object);
            var result = await handler.Handle(command, default);
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Succeeded);
            Assert.AreEqual(HttpStatusCode.NoContent, result.StatusCode);
            _unitOfWorkMock.VerifyAll();
            _unitOfWorkMock.Verify(x => x.SaveChangesAsync(default));
            Assert.AreEqual(donut.Name, command.Name);
            Assert.AreEqual(donut.Price, command.Price);
        }

        [TestMethod]
        public async Task UpdateNonExistingDonut()
        {
            _unitOfWorkMock.Setup(x => x.Donuts.FindAsync(default, It.IsAny<int>()))
                .Returns(Task.FromResult<Donut>(null));

            var command = new DonutUpdateCommand
            {
                Id = 1,
                Name = "Frambuesa",
                Price = 5
            };

            var handler = new DonutUpdateHandler(_unitOfWorkMock.Object, _loggerMock.Object);
            var result = await handler.Handle(command, default);
            Assert.IsNotNull(result);
            Assert.IsFalse(result.Succeeded);
            Assert.AreEqual(HttpStatusCode.NotFound, result.StatusCode);
        }
    }
}
