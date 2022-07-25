using System.Threading;
using Catalog.Host.Data.Entities;
using Catalog.Host.Models.Dtos;

namespace Catalog.UnitTests.Services
{
    public class CatalogBrandServiceTest
    {
        private readonly ICatalogBrandService _catalogBrandService;
        private readonly Mock<ICatalogBrandRepository> _catalogBrandRepository;
        private readonly Mock<IMapper> _mapper;
        private readonly Mock<IDbContextWrapper<ApplicationDbContext>> _dbContextWrapper;
        private readonly Mock<ILogger<CatalogBrandService>> _logger;

        private readonly CatalogBrand _testItem = new CatalogBrand()
        {
            Id = 1,
            Brand = "Brand"
        };

        public CatalogBrandServiceTest()
        {
            _catalogBrandRepository = new Mock<ICatalogBrandRepository>();
            _mapper = new Mock<IMapper>();
            _dbContextWrapper = new Mock<IDbContextWrapper<ApplicationDbContext>>();
            _logger = new Mock<ILogger<CatalogBrandService>>();

            var dbContextTransaction = new Mock<IDbContextTransaction>();
            _dbContextWrapper.Setup(s => s.BeginTransactionAsync(CancellationToken.None)).ReturnsAsync(dbContextTransaction.Object);

            _catalogBrandService = new CatalogBrandService(
                _dbContextWrapper.Object,
                _logger.Object,
                _catalogBrandRepository.Object,
                _mapper.Object);
        }

        [Fact]
        public async Task AddAsync_Success()
        {
            // arrange
            var catalogItem = new CatalogBrand()
            {
                Brand = "TestName"
            };

            var catalogItemDto = new CatalogBrandDto()
            {
                Brand = "TestName"
            };

            _catalogBrandRepository.Setup(s => s.Create(
                It.IsAny<string>()))
                .ReturnsAsync(catalogItem);

            _mapper.Setup(s => s.Map<CatalogBrandDto>(
            It.Is<CatalogBrand>(i => i.Equals(catalogItem))))
            .Returns(catalogItemDto);

            // act
            var result = await _catalogBrandService.Create(_testItem.Brand);

            // assert
            result.Should().NotBeNull();
        }

        [Fact]
        public async Task AddAsync_Failed()
        {
            // arrange
            CatalogBrand? catalogItem = null;

            var catalogItemDto = new CatalogBrandDto()
            {
                Brand = "TestName"
            };

            _catalogBrandRepository.Setup(s => s.Create(
                It.IsAny<string>()))
                .ReturnsAsync(catalogItem);

            _mapper.Setup(s => s.Map<CatalogBrandDto>(
            It.Is<CatalogBrand>(i => i.Equals(catalogItem))))
            .Returns(catalogItemDto);

            // act
            var result = await _catalogBrandService.Create(_testItem.Brand);

            // assert
            result.Should().BeNull();
        }

        [Fact]
        public async Task UpdateAsync_Success()
        {
            // arrange
            var id = 1;

            var catalogItem = new CatalogBrand()
            {
                Brand = "TestName"
            };

            var catalogItemDto = new CatalogBrandDto()
            {
                Brand = "TestName"
            };

            _catalogBrandRepository.Setup(s => s.Update(
                It.Is<int>(i => i == id),
                It.IsAny<string>()))
                .ReturnsAsync(catalogItem);

            _mapper.Setup(s => s.Map<CatalogBrandDto>(
            It.Is<CatalogBrand>(i => i.Equals(catalogItem))))
            .Returns(catalogItemDto);

            // act
            var result = await _catalogBrandService.Update(_testItem.Id, _testItem.Brand);

            // assert
            result.Should().NotBeNull();
        }

        [Fact]
        public async Task UpdateAsync_Failed()
        {
            // arrange
            var id = 1;

            CatalogBrand? catalogItem = null;

            var catalogItemDto = new CatalogBrandDto()
            {
                Brand = "TestName"
            };

            _catalogBrandRepository.Setup(s => s.Update(
                It.Is<int>(i => i == id),
                It.IsAny<string>()))
                .ReturnsAsync(catalogItem);

            _mapper.Setup(s => s.Map<CatalogBrandDto>(
            It.Is<CatalogBrand>(i => i.Equals(catalogItem))))
            .Returns(catalogItemDto);

            // act
            var result = await _catalogBrandService.Update(_testItem.Id, _testItem.Brand);

            // assert
            result.Should().BeNull();
        }

        [Fact]
        public async Task DeleteAsync_Success()
        {
            // arrange
            var id = 1;

            var catalogItem = new CatalogBrand()
            {
                Brand = "TestName"
            };

            var catalogItemDto = new CatalogBrandDto()
            {
                Brand = "TestName"
            };

            _catalogBrandRepository.Setup(s => s.Delete(
                It.Is<int>(i => i == id)))
                .ReturnsAsync(catalogItem);

            _mapper.Setup(s => s.Map<CatalogBrandDto>(
            It.Is<CatalogBrand>(i => i.Equals(catalogItem))))
            .Returns(catalogItemDto);

            // act
            var result = await _catalogBrandService.Delete(_testItem.Id);

            // assert
            result.Should().NotBeNull();
        }

        [Fact]
        public async Task DeleteAsync_Failed()
        {
            // arrange
            var id = 1;

            CatalogBrand? catalogItem = null;

            var catalogItemDto = new CatalogBrandDto()
            {
                Brand = "TestName"
            };

            _catalogBrandRepository.Setup(s => s.Delete(
                It.Is<int>(i => i == id)))
                .ReturnsAsync(catalogItem);

            _mapper.Setup(s => s.Map<CatalogBrandDto>(
            It.Is<CatalogBrand>(i => i.Equals(catalogItem))))
            .Returns(catalogItemDto);

            // act
            var result = await _catalogBrandService.Delete(_testItem.Id);

            // assert
            result.Should().BeNull();
        }

        [Fact]
        public async Task GetBrandsAsync_Success()
        {
            // arrange
            var testResult = new List<CatalogBrand>()
            {
                new CatalogBrand()
                {
                    Brand = "TestName",
                },
            };

            var catalogBrandSuccess = new CatalogBrand()
            {
                Brand = "TestName"
            };

            var catalogBrandDtoSuccess = new CatalogBrandDto()
            {
                Brand = "TestName"
            };

            _catalogBrandRepository.Setup(s => s.GetBrandsAsync()).ReturnsAsync(testResult);

            _mapper.Setup(s => s.Map<CatalogBrandDto>(
            It.Is<CatalogBrand>(i => i.Equals(catalogBrandSuccess)))).Returns(catalogBrandDtoSuccess);

            // act
            var result = await _catalogBrandService.GetBrandsAsync();

            // assert
            result.Should().NotBeNull();
            result.Should().HaveCount(1);
        }

        [Fact]
        public async Task GetBrandsAsync_Failed()
        {
            // arrange
            var testResult = new List<CatalogBrand>();

            _catalogBrandRepository.Setup(s => s.GetBrandsAsync()).ReturnsAsync(testResult);

            // act
            var result = await _catalogBrandService.GetBrandsAsync();

            // assert
            result.Should().HaveCount(0);
        }

        [Fact]
        public async Task GetByIdAsync_Success()
        {
            // arrange
            var testId = 1;

            var catalogItem = new CatalogBrand()
            {
                Brand = "TestName"
            };

            var catalogItemDto = new CatalogBrandDto()
            {
                Brand = "TestName"
            };

            _catalogBrandRepository.Setup(s => s.GetBrand(
                It.Is<int>(i => i == testId)))
                .ReturnsAsync(catalogItem);

            _mapper.Setup(s => s.Map<CatalogBrandDto>(
                It.Is<CatalogBrand>(i => i.Equals(catalogItem))))
                .Returns(catalogItemDto);

            // act
            var result = await _catalogBrandService.GetBrandAsync(testId);

            // assert
            result.Should().NotBeNull();
            result.Should().Be(catalogItemDto);
        }

        [Fact]
        public async Task GetByIdAsync_Failed()
        {
            // arrange
            var testId = 1;
            CatalogBrand? testItem = null;

            _catalogBrandRepository.Setup(s => s.GetBrand(
                It.Is<int>(i => i == testId)))
                .ReturnsAsync(testItem);

            // act
            var result = await _catalogBrandService.GetBrandAsync(testId).ConfigureAwait(false);

            // assert
            result.Should().BeNull();
        }
    }
}