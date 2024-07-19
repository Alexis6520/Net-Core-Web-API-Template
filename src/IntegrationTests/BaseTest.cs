using Domain.Services;
using IntegrationTests.Extensions;
using IntegrationTests.Services;
using Microsoft.Extensions.DependencyInjection;

namespace IntegrationTests
{
    /// <summary>
    /// Clase base para todas las pruebas
    /// </summary>
    /// <param name="factory"></param>
    public abstract class BaseTest(CustomWebAppFactory<Program> factory, string baseUrl) : IClassFixture<CustomWebAppFactory<Program>>, IDisposable
    {
        protected readonly CustomWebAppFactory<Program> _factory = factory;
        protected readonly string _baseUrl = baseUrl;
        private HttpClient _authenticatedClient;
        private HttpClient _client;
        private IServiceScope _serviceScope;
        private IUnitOfWork _unitOfWork;

        public HttpClient AuthenticatedClient => _authenticatedClient ??= _factory.CreateAuthenticatedClient();
        public HttpClient Client => _client ??= _factory.CreateClient();

        public IUnitOfWork UnitOfWork
        {
            get
            {
                if (_unitOfWork == null)
                {
                    _serviceScope ??= _factory.Services.CreateScope();
                    _unitOfWork = _serviceScope.ServiceProvider.GetRequiredService<IUnitOfWork>();
                }

                return _unitOfWork;
            }
        }

        public virtual void Dispose()
        {
            _authenticatedClient?.Dispose();
            _client?.Dispose();
            _authenticatedClient = null;
            _client = null;
            _unitOfWork?.Dispose();
            _unitOfWork = null;
            _serviceScope?.Dispose();
            _serviceScope = null;
            GC.SuppressFinalize(this);
        }
    }
}