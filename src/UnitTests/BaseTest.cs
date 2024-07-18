using Domain.Services;
using Microsoft.Extensions.Logging;
using Moq;

namespace UnitTests
{
    public abstract class BaseTest<THandler> where THandler : class
    {
        protected readonly Mock<IUnitOfWork> _unitOfWorkMock = new();
        protected readonly Mock<ILogger<THandler>> _loggerMock = new();
    }
}