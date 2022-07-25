using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Catalog.Host.Data.Entities;
using Catalog.Host.Models.Dtos;

namespace Catalog.UnitTests.Services
{
    public class CatalogItemServiceTest
    {
        private readonly Mock<IMapper> _mapper;
        private readonly ICatalogItemService _catalogItemService;
        private readonly Mock<ICatalogItemRepository> _catalogItemRepository;
        private readonly Mock<IDbContextWrapper<ApplicationDbContext>> _dbContextWrapper;
        private readonly Mock<ILogger<CatalogService>> _logger;

        private readonly CatalogItem _testItem = new CatalogItem()
        {
            Id = 1,
            Name = "Name",
            Description = "Description",
            Price = 1000,
            AvailableStock = 100,
            CatalogBrandId = 1,
            CatalogTypeId = 1,
            PictureFileName = "1.png"
        };

        public CatalogItemServiceTest()
        {
            _mapper = new Mock<IMapper>();
            _catalogItemRepository = new Mock<ICatalogItemRepository>();
            _dbContextWrapper = new Mock<IDbContextWrapper<ApplicationDbContext>>();
            _logger = new Mock<ILogger<CatalogService>>();

            var dbContextTransaction = new Mock<IDbContextTransaction>();
            _dbContextWrapper.Setup(s => s.BeginTransactionAsync(CancellationToken.None)).ReturnsAsync(dbContextTransaction.Object);

            _catalogItemService = new CatalogItemService(
                _mapper.Object,
                _dbContextWrapper.Object,
                _logger.Object,
                _catalogItemRepository.Object);
        }

        [Fact]
        public async Task AddAsync_Success()
        {
            // arrange
            var catalogItem = new CatalogItem()
            {
                Name = "Name",
                Description = "Description",
                Price = 1000,
                AvailableStock = 100,
                CatalogBrandId = 1,
                CatalogTypeId = 1,
                PictureFileName = "1.png"
            };

            var catalogItemDto = new CatalogItemDto()
            {
                Name = "Name",
                Description = "Description",
                Price = 1000,
                AvailableStock = 100,
                PictureUrl = "1.png"
            };

            _catalogItemRepository.Setup(s => s.Add(
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<decimal>(),
                It.IsAny<int>(),
                It.IsAny<int>(),
                It.IsAny<int>(),
                It.IsAny<string>()))
                .ReturnsAsync(catalogItem);

            _mapper.Setup(s => s.Map<CatalogItemDto>(
            It.Is<CatalogItem>(i => i.Equals(catalogItem))))
            .Returns(catalogItemDto);

            // act
            var result = await _catalogItemService.AddAsync(_testItem.Name, _testItem.Description, _testItem.Price, _testItem.AvailableStock, _testItem.CatalogBrandId, _testItem.CatalogTypeId, _testItem.PictureFileName);

            // assert
            result.Should().NotBeNull();
        }

        [Fact]
        public async Task AddAsync_Failed()
        {
            // arrange
            CatalogItem? catalogItem = null;

            var catalogItemDto = new CatalogItemDto()
            {
                Name = "Name",
                Description = "Description",
                Price = 1000,
                AvailableStock = 100,
                PictureUrl = "1.png"
            };

            _catalogItemRepository.Setup(s => s.Add(
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<decimal>(),
                It.IsAny<int>(),
                It.IsAny<int>(),
                It.IsAny<int>(),
                It.IsAny<string>()))
                .ReturnsAsync(catalogItem);

            _mapper.Setup(s => s.Map<CatalogItemDto>(
            It.Is<CatalogItem>(i => i.Equals(catalogItem))))
            .Returns(catalogItemDto);

            // act
            var result = await _catalogItemService.AddAsync(_testItem.Name, _testItem.Description, _testItem.Price, _testItem.AvailableStock, _testItem.CatalogBrandId, _testItem.CatalogTypeId, _testItem.PictureFileName);

            // assert
            result.Should().BeNull();
        }

        [Fact]
        public async Task UpdateAsync_Success()
        {
            // arrange
            var id = 1;

            var catalogItem = new CatalogItem()
            {
                Name = "Name",
                Description = "Description",
                Price = 1000,
                AvailableStock = 100,
                CatalogBrandId = 1,
                CatalogTypeId = 1,
                PictureFileName = "1.png"
            };

            var catalogItemDto = new CatalogItemDto()
            {
                Name = "Name",
                Description = "Description",
                Price = 1000,
                AvailableStock = 100,
                PictureUrl = "1.png"
            };

            _catalogItemRepository.Setup(s => s.Update(
                It.Is<int>(i => i == id),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<decimal>(),
                It.IsAny<int>(),
                It.IsAny<int>(),
                It.IsAny<int>(),
                It.IsAny<string>()))
                .ReturnsAsync(catalogItem);

            _mapper.Setup(s => s.Map<CatalogItemDto>(
            It.Is<CatalogItem>(i => i.Equals(catalogItem))))
            .Returns(catalogItemDto);

            // act
            var result = await _catalogItemService.Update(_testItem.Id, _testItem.Name, _testItem.Description, _testItem.Price, _testItem.AvailableStock, _testItem.CatalogBrandId, _testItem.CatalogTypeId, _testItem.PictureFileName);

            // assert
            result.Should().NotBeNull();
        }

        [Fact]
        public async Task UpdateAsync_Failed()
        {
            // arrange
            var id = 1;

            CatalogItem? catalogItem = null;

            var catalogItemDto = new CatalogItemDto()
            {
                Name = "Name",
                Description = "Description",
                Price = 1000,
                AvailableStock = 100,
                PictureUrl = "1.png"
            };

            _catalogItemRepository.Setup(s => s.Update(
                It.Is<int>(i => i == id),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<decimal>(),
                It.IsAny<int>(),
                It.IsAny<int>(),
                It.IsAny<int>(),
                It.IsAny<string>()))
                .ReturnsAsync(catalogItem);

            _mapper.Setup(s => s.Map<CatalogItemDto>(
            It.Is<CatalogItem>(i => i.Equals(catalogItem))))
            .Returns(catalogItemDto);

            // act
            var result = await _catalogItemService.Update(_testItem.Id, _testItem.Name, _testItem.Description, _testItem.Price, _testItem.AvailableStock, _testItem.CatalogBrandId, _testItem.CatalogTypeId, _testItem.PictureFileName);

            // assert
            result.Should().BeNull();
        }

        [Fact]
        public async Task DeleteAsync_Success()
        {
            // arrange
            var id = 1;

            var catalogItem = new CatalogItem()
            {
                Name = "Name",
                Description = "Description",
                Price = 1000,
                AvailableStock = 100,
                CatalogBrandId = 1,
                CatalogTypeId = 1,
                PictureFileName = "1.png"
            };

            var catalogItemDto = new CatalogItemDto()
            {
                Name = "Name",
                Description = "Description",
                Price = 1000,
                AvailableStock = 100,
                PictureUrl = "1.png"
            };

            _catalogItemRepository.Setup(s => s.Delete(
                It.Is<int>(i => i == id)))
                .ReturnsAsync(catalogItem);

            _mapper.Setup(s => s.Map<CatalogItemDto>(
            It.Is<CatalogItem>(i => i.Equals(catalogItem))))
            .Returns(catalogItemDto);

            // act
            var result = await _catalogItemService.Delete(_testItem.Id);

            // assert
            result.Should().NotBeNull();
        }

        [Fact]
        public async Task DeleteAsync_Failed()
        {
            // arrange
            var id = 1;

            CatalogItem? catalogItem = null;

            var catalogItemDto = new CatalogItemDto()
            {
                Name = "Name",
                Description = "Description",
                Price = 1000,
                AvailableStock = 100,
                PictureUrl = "1.png"
            };

            _catalogItemRepository.Setup(s => s.Delete(
                It.Is<int>(i => i == id)))
                .ReturnsAsync(catalogItem);

            _mapper.Setup(s => s.Map<CatalogItemDto>(
            It.Is<CatalogItem>(i => i.Equals(catalogItem))))
            .Returns(catalogItemDto);

            // act
            var result = await _catalogItemService.Delete(_testItem.Id);

            // assert
            result.Should().BeNull();
        }

        [Fact]
        public async Task GetItemsAsync_Success()
        {
            // arrange
            var testResult = new List<CatalogItem>()
            {
               new CatalogItem()
                {
                Name = "Name",
                Description = "Description",
                Price = 1000,
                AvailableStock = 100,
                CatalogBrandId = 1,
                CatalogTypeId = 1,
                PictureFileName = "1.png"
                },
            };

            var catalogItem = new CatalogItem()
            {
                Name = "Name",
                Description = "Description",
                Price = 1000,
                AvailableStock = 100,
                CatalogBrandId = 1,
                CatalogTypeId = 1,
                PictureFileName = "1.png"
            };

            var catalogItemDto = new CatalogItemDto()
            {
                Name = "Name",
                Description = "Description",
                Price = 1000,
                AvailableStock = 100,
                PictureUrl = "1.png"
            };

            _catalogItemRepository.Setup(s => s.GetItemsAsync()).ReturnsAsync(testResult);

            _mapper.Setup(s => s.Map<CatalogItemDto>(
            It.Is<CatalogItem>(i => i.Equals(catalogItem))))
            .Returns(catalogItemDto);

            // act
            var result = await _catalogItemService.GetItemsAsync();

            // assert
            result.Should().NotBeNull();
            result.Should().HaveCount(1);
        }

        [Fact]
        public async Task GetBrandsAsync_Failed()
        {
            // arrange
            var testResult = new List<CatalogItem>();

            _catalogItemRepository.Setup(s => s.GetItemsAsync()).ReturnsAsync(testResult);

            // act
            var result = await _catalogItemService.GetItemsAsync();

            // assert
            result.Should().HaveCount(0);
        }

        [Fact]
        public async Task GetByIdAsync_Success()
        {
            // arrange
            var testId = 1;

            var catalogItem = new CatalogItem()
            {
                Name = "TestName"
            };

            var catalogItemDto = new CatalogItemDto()
            {
                Name = "TestName"
            };

            _catalogItemRepository.Setup(s => s.GetByIdAsync(
                It.Is<int>(i => i == testId)))
                .ReturnsAsync(catalogItem);

            _mapper.Setup(s => s.Map<CatalogItemDto>(
                It.Is<CatalogItem>(i => i.Equals(catalogItem))))
                .Returns(catalogItemDto);

            // act
            var result = await _catalogItemService.GetItemAsync(testId);

            // assert
            result.Should().NotBeNull();
            result.Should().Be(catalogItemDto);
        }

        [Fact]
        public async Task GetByIdAsync_Failed()
        {
            // arrange
            var testId = 1;
            CatalogItem? testItem = null;

            _catalogItemRepository.Setup(s => s.GetByIdAsync(
                It.Is<int>(i => i == testId)))
                .ReturnsAsync(testItem);

            // act
            var result = await _catalogItemService.GetItemAsync(testId).ConfigureAwait(false);

            // assert
            result.Should().BeNull();
        }
    }
}
