using System.Threading;
using Catalog.Host.Data.Entities;
using Catalog.Host.Data.Enums;
using Catalog.Host.Models.Dtos;
using Catalog.Host.Models.Response;

namespace Catalog.UnitTests.Services;

public class CatalogServiceTest
{
    private readonly ICatalogService _catalogService;

    private readonly Mock<ICatalogItemRepository> _catalogItemRepository;
    private readonly Mock<IMapper> _mapper;
    private readonly Mock<IDbContextWrapper<ApplicationDbContext>> _dbContextWrapper;
    private readonly Mock<ILogger<CatalogService>> _logger;

    public CatalogServiceTest()
    {
        _catalogItemRepository = new Mock<ICatalogItemRepository>();
        _mapper = new Mock<IMapper>();
        _dbContextWrapper = new Mock<IDbContextWrapper<ApplicationDbContext>>();
        _logger = new Mock<ILogger<CatalogService>>();

        var dbContextTransaction = new Mock<IDbContextTransaction>();
        _dbContextWrapper.Setup(s => s.BeginTransactionAsync(CancellationToken.None)).ReturnsAsync(dbContextTransaction.Object);

        _catalogService = new CatalogService(_dbContextWrapper.Object, _logger.Object, _catalogItemRepository.Object, _mapper.Object);
    }

    [Fact]
    public async Task GetCatalogItemsAsync_Success()
    {
        // arrange
        var testPageIndex = 0;
        var testPageSize = 4;
        var testTotalCount = 12;

        var pagingPaginatedItemsSuccess = new PaginatedItems<CatalogItem>()
        {
            Data = new List<CatalogItem>()
            {
                new CatalogItem()
                {
                    Name = "TestName",
                },
            },
            TotalCount = testTotalCount,
        };

        var catalogItemSuccess = new CatalogItem()
        {
            Name = "TestName"
        };

        var catalogItemDtoSuccess = new CatalogItemDto()
        {
            Name = "TestName"
        };

        _catalogItemRepository.Setup(s => s.GetByPageAsync(
            It.Is<int>(i => i == testPageIndex),
            It.Is<int>(i => i == testPageSize),
            It.IsAny<int?>(),
            It.IsAny<int?>(),
            It.Is<CatalogTypeSorting?>(i => i == CatalogTypeSorting.NameAsc))).ReturnsAsync(pagingPaginatedItemsSuccess);

        _mapper.Setup(s => s.Map<CatalogItemDto>(
            It.Is<CatalogItem>(i => i.Equals(catalogItemSuccess)))).Returns(catalogItemDtoSuccess);

        // act
        var result = await _catalogService.GetCatalogItemsAsync(testPageSize, testPageIndex, null, CatalogTypeSorting.NameAsc);

        // assert
        result.Should().NotBeNull();
        result?.Data.Should().NotBeNull();
        result?.Count.Should().Be(testTotalCount);
        result?.PageIndex.Should().Be(testPageIndex);
        result?.PageSize.Should().Be(testPageSize);
    }

    [Fact]
    public async Task GetCatalogItemsAsync_Failed()
    {
        // arrange
        var testPageIndex = 1000;
        var testPageSize = 10000;

        _catalogItemRepository.Setup(s => s.GetByPageAsync(
            It.Is<int>(i => i == testPageIndex),
            It.Is<int>(i => i == testPageSize),
            It.IsAny<int?>(),
            It.IsAny<int?>(),
            It.IsAny<CatalogTypeSorting?>())).Returns((Func<PaginatedItemsResponse<CatalogItemDto>>)null!);

        // act
        var result = await _catalogService.GetCatalogItemsAsync(testPageSize, testPageIndex, null, null);

        // assert
        result.Should().BeNull();
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
        var result = await _catalogService.GetByIdAsync(testId);

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
        var result = await _catalogService.GetByIdAsync(testId).ConfigureAwait(false);

        // assert
        result.Should().BeNull();
    }
}