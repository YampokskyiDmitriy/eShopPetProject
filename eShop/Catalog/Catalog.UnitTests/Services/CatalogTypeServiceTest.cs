using System.Threading;
using Catalog.Host.Data.Entities;
using Catalog.Host.Models.Dtos;

namespace Catalog.UnitTests.Services
{
    public class CatalogTypeServiceTest
    {
        private readonly ICatalogTypeService _catalogTypeService;
        private readonly Mock<ICatalogTypeRepository> _catalogTypeRepository;
        private readonly Mock<IMapper> _mapper;
        private readonly Mock<IDbContextWrapper<ApplicationDbContext>> _dbContextWrapper;
        private readonly Mock<ILogger<CatalogTypeService>> _logger;

        private readonly CatalogType _testItem = new CatalogType()
        {
            Id = 1,
            Type = "Type"
        };

        public CatalogTypeServiceTest()
        {
            _catalogTypeRepository = new Mock<ICatalogTypeRepository>();
            _mapper = new Mock<IMapper>();
            _dbContextWrapper = new Mock<IDbContextWrapper<ApplicationDbContext>>();
            _logger = new Mock<ILogger<CatalogTypeService>>();

            var dbContextTransaction = new Mock<IDbContextTransaction>();
            _dbContextWrapper.Setup(s => s.BeginTransactionAsync(CancellationToken.None)).ReturnsAsync(dbContextTransaction.Object);

            _catalogTypeService = new CatalogTypeService(
                _dbContextWrapper.Object,
                _logger.Object,
                _catalogTypeRepository.Object,
                _mapper.Object);
        }

        [Fact]
        public async Task AddAsync_Success()
        {
            // arrange
            var catalogItem = new CatalogType()
            {
                Type = "TestName"
            };

            var catalogItemDto = new CatalogTypeDto()
            {
                Type = "TestName"
            };

            _catalogTypeRepository.Setup(s => s.Create(
                It.IsAny<string>()))
                .ReturnsAsync(catalogItem);

            _mapper.Setup(s => s.Map<CatalogTypeDto>(
            It.Is<CatalogType>(i => i.Equals(catalogItem))))
            .Returns(catalogItemDto);

            // act
            var result = await _catalogTypeService.Create(_testItem.Type);

            // assert
            result.Should().NotBeNull();
        }

        [Fact]
        public async Task AddAsync_Failed()
        {
            // arrange
            CatalogType? catalogItem = null;

            var catalogItemDto = new CatalogTypeDto()
            {
                Type = "TestName"
            };

            _catalogTypeRepository.Setup(s => s.Create(
                It.IsAny<string>()))
                .ReturnsAsync(catalogItem);

            _mapper.Setup(s => s.Map<CatalogTypeDto>(
            It.Is<CatalogType>(i => i.Equals(catalogItem))))
            .Returns(catalogItemDto);

            // act
            var result = await _catalogTypeService.Create(_testItem.Type);

            // assert
            result.Should().BeNull();
        }

        [Fact]
        public async Task UpdateAsync_Success()
        {
            // arrange
            var id = 1;

            var catalogItem = new CatalogType()
            {
                Type = "TestName"
            };

            var catalogItemDto = new CatalogTypeDto()
            {
                Type = "TestName"
            };

            _catalogTypeRepository.Setup(s => s.Update(
                It.Is<int>(i => i == id),
                It.IsAny<string>()))
                .ReturnsAsync(catalogItem);

            _mapper.Setup(s => s.Map<CatalogTypeDto>(
            It.Is<CatalogType>(i => i.Equals(catalogItem))))
            .Returns(catalogItemDto);

            // act
            var result = await _catalogTypeService.Update(_testItem.Id, _testItem.Type);

            // assert
            result.Should().NotBeNull();
        }

        [Fact]
        public async Task UpdateAsync_Failed()
        {
            // arrange
            var id = 1;

            CatalogType? catalogItem = null;

            var catalogItemDto = new CatalogTypeDto()
            {
                Type = "TestName"
            };

            _catalogTypeRepository.Setup(s => s.Update(
                It.Is<int>(i => i == id),
                It.IsAny<string>()))
                .ReturnsAsync(catalogItem);

            _mapper.Setup(s => s.Map<CatalogTypeDto>(
            It.Is<CatalogType>(i => i.Equals(catalogItem))))
            .Returns(catalogItemDto);

            // act
            var result = await _catalogTypeService.Update(_testItem.Id, _testItem.Type);

            // assert
            result.Should().BeNull();
        }

        [Fact]
        public async Task DeleteAsync_Success()
        {
            // arrange
            var id = 1;

            var catalogItem = new CatalogType()
            {
                Type = "TestName"
            };

            var catalogItemDto = new CatalogTypeDto()
            {
                Type = "TestName"
            };

            _catalogTypeRepository.Setup(s => s.Delete(
                It.Is<int>(i => i == id)))
                .ReturnsAsync(catalogItem);

            _mapper.Setup(s => s.Map<CatalogTypeDto>(
            It.Is<CatalogType>(i => i.Equals(catalogItem))))
            .Returns(catalogItemDto);

            // act
            var result = await _catalogTypeService.Delete(_testItem.Id);

            // assert
            result.Should().NotBeNull();
        }

        [Fact]
        public async Task DeleteAsync_Failed()
        {
            // arrange
            var id = 1;

            CatalogType? catalogItem = null;

            var catalogItemDto = new CatalogTypeDto()
            {
                Type = "TestName"
            };

            _catalogTypeRepository.Setup(s => s.Delete(
                It.Is<int>(i => i == id)))
                .ReturnsAsync(catalogItem);

            _mapper.Setup(s => s.Map<CatalogTypeDto>(
            It.Is<CatalogType>(i => i.Equals(catalogItem))))
            .Returns(catalogItemDto);

            // act
            var result = await _catalogTypeService.Delete(_testItem.Id);

            // assert
            result.Should().BeNull();
        }

        [Fact]
        public async Task GetTypesAsync_Success()
        {
            // arrange
            var testResult = new List<CatalogType>()
            {
                new CatalogType()
                {
                    Type = "TestName",
                },
            };

            var catalogTypeSuccess = new CatalogType()
            {
                Type = "TestName"
            };

            var catalogTypeDtoSuccess = new CatalogTypeDto()
            {
                Type = "TestName"
            };

            _catalogTypeRepository.Setup(s => s.GetTypesAsync()).ReturnsAsync(testResult);

            _mapper.Setup(s => s.Map<CatalogTypeDto>(
            It.Is<CatalogType>(i => i.Equals(catalogTypeSuccess)))).Returns(catalogTypeDtoSuccess);

            // act
            var result = await _catalogTypeService.GetTypesAsync();

            // assert
            result.Should().NotBeNull();
            result.Should().HaveCount(1);
        }

        [Fact]
        public async Task GetTypesAsync_Failed()
        {
            // arrange
            var testResult = new List<CatalogType>();

            _catalogTypeRepository.Setup(s => s.GetTypesAsync()).ReturnsAsync(testResult);

            // act
            var result = await _catalogTypeService.GetTypesAsync();

            // assert
            result.Should().HaveCount(0);
        }

        [Fact]
        public async Task GetByIdAsync_Success()
        {
            // arrange
            var testId = 1;

            var catalogItem = new CatalogType()
            {
                Type = "TestName"
            };

            var catalogItemDto = new CatalogTypeDto()
            {
                Type = "TestName"
            };

            _catalogTypeRepository.Setup(s => s.GetType(
                It.Is<int>(i => i == testId)))
                .ReturnsAsync(catalogItem);

            _mapper.Setup(s => s.Map<CatalogTypeDto>(
                It.Is<CatalogType>(i => i.Equals(catalogItem))))
                .Returns(catalogItemDto);

            // act
            var result = await _catalogTypeService.GetTypeAsync(testId);

            // assert
            result.Should().NotBeNull();
            result.Should().Be(catalogItemDto);
        }

        [Fact]
        public async Task GetByIdAsync_Failed()
        {
            // arrange
            var testId = 1;
            CatalogType? testItem = null;

            _catalogTypeRepository.Setup(s => s.GetType(
                It.Is<int>(i => i == testId)))
                .ReturnsAsync(testItem);

            // act
            var result = await _catalogTypeService.GetTypeAsync(testId).ConfigureAwait(false);

            // assert
            result.Should().BeNull();
        }
    }
}