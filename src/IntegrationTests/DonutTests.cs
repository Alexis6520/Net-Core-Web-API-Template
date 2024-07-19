using Application.Services.Commands.DonutCommands;
using Application.Services.Queries.DTOs;
using Application.Services.Queries.DTOs.DonutDTOs;
using Application.Services.Wrappers;
using IntegrationTests.Services;
using System.Net;
using System.Net.Http.Json;

namespace IntegrationTests
{
    public class DonutTests(CustomWebAppFactory<Program> factory) : BaseTest(factory, "donuts")
    {
        private List<int> _toDelete = [];

        [Fact]
        public async Task Create()
        {
            var command = new DonutCreateCommand("Glaseada original", 19);
            var response = await AuthenticatedClient.PostAsJsonAsync(_baseUrl, command);
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadFromJsonAsync<Result<int>>();
            Assert.NotNull(result);
            Assert.True(result.Value > 0);
            _toDelete.Add(result.Value);
        }

        [Fact]
        public async Task Get()
        {
            await Create();
            var id = _toDelete.First();
            var result = await Client.GetFromJsonAsync<Result<DonutDTO>>($"{_baseUrl}/{id}");
            Assert.NotNull(result.Value);
        }

        [Fact]
        public async Task GetAll()
        {
            for (int i = 0; i < 3; i++)
            {
                await Create();
            }

            var pageSize = 2;
            var result = await Client.GetFromJsonAsync<Result<PagedList<DonutDTO>>>($"{_baseUrl}?page=1&pagesize={pageSize}");
            Assert.True(result.Value.Count >= 3);
            Assert.Equal(2, result.Value.Items.Count);
            var pages = (int)Math.Ceiling(result.Value.Count / (decimal)pageSize);
            Assert.Equal(pages, result.Value.Pages);
            result = await Client.GetFromJsonAsync<Result<PagedList<DonutDTO>>>($"{_baseUrl}?page={pages}&pagesize={pageSize}");
            Assert.True(result.Value.Items.Count > 0 && result.Value.Items.Count <= pageSize);
        }

        [Fact]
        public async Task Update()
        {
            await Create();
            var id = _toDelete.First();
            var command = new DonutUpdateCommand("Frambuesa", 10) { Id = id };
            var response = await Client.PutAsJsonAsync($"{_baseUrl}/{id}", command);
            response.EnsureSuccessStatusCode();
            var donut = UnitOfWork.Donuts.Find(id);
            Assert.Equal(command.Name, donut.Name);
            Assert.Equal(command.Price, donut.Price);
        }

        [Fact]
        public async Task Delete()
        {
            await Create();
            var id = _toDelete.First();
            var response = await Client.DeleteAsync($"{_baseUrl}/{id}");
            response.EnsureSuccessStatusCode();
            var donut = UnitOfWork.Donuts.Find(keys: [id]);
            Assert.Null(donut);
        }

        [Fact]
        public async Task UnauthorizedCreate()
        {
            var command = new DonutCreateCommand("Glaseada original", 19);
            var response = await Client.PostAsJsonAsync(_baseUrl, command);
            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        public override void Dispose()
        {
            if (_toDelete.Count > 0)
            {
                UnitOfWork.Donuts.RemoveRange(UnitOfWork.Donuts.GetAll(x => _toDelete.Contains(x.Id)));
                UnitOfWork.SaveChanges();
            }

            _toDelete.Clear();
            _toDelete = null;
            base.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
