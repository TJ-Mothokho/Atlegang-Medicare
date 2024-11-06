using Moq;
using Atlegang_Medicare.Controllers.Consumable_Manager;
using DataAccesslayer.Repository.Administrator;
using DataAccesslayer.Repository.Consumable_And_Script_Manager;
using FakeItEasy;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using FluentAssertions;
using DataAccesslayer.Models.Domains.Consumable_and_Script_Manager;

namespace Atlegang_Unit_Tests.Controllers.ConsumableManager
{
    public class ConsumableManagerControllerTests
    {
        private IConsumableRepository _consumableRepository;
        private IWardRepository _wardRepository;
        private IStockRepository _stockRepository;
        private ISupplierRepository _supplierRepository;
        private IOrderRepository _orderRepository;
        private IOrderDetailRepository _orderDetailsRepository;
        private HttpContextAccessor _httpContextAccessor;
        private Mock<IConsumableRepository> _mockConsumableRepo;

        private ConsumableManagerController _controller;
        private ConsumableManagerController _mockController;

        public ConsumableManagerControllerTests()
        {
            //Dependencies
            _consumableRepository = A.Fake<IConsumableRepository>();
            _wardRepository = A.Fake<IWardRepository>();
            _stockRepository = A.Fake<IStockRepository>();
            _supplierRepository = A.Fake<ISupplierRepository>();
            _orderRepository = A.Fake<IOrderRepository>();
            _orderDetailsRepository = A.Fake<IOrderDetailRepository>();
            _httpContextAccessor = A.Fake<HttpContextAccessor>();
            _mockConsumableRepo = new Mock<IConsumableRepository>();


            //SUT
            _controller = new ConsumableManagerController(_wardRepository, _httpContextAccessor, _consumableRepository, _stockRepository, _supplierRepository, _orderRepository, _orderDetailsRepository);

            _mockController = new ConsumableManagerController(_wardRepository, _httpContextAccessor, _mockConsumableRepo.Object, _stockRepository, _supplierRepository, _orderRepository, _orderDetailsRepository);

        }

        [Fact]
        public void ViewConsumables_Returns_ViewResult()
        {
            //Arrange
            var consumables = A.Fake<IEnumerable<dynamic>>();
            A.CallTo(() => _consumableRepository.GetAllConsumablesAsync()).Returns(consumables);


            // Act
            var result = _controller.ViewConsumables();

            // Assert
            result.Should().BeOfType<Task<IActionResult>>();
        }

        [Fact]
        public void ViewConsumables_Post_Returns_ViewResult()
        {
            //Arrange
            var consumables = A.Fake<IEnumerable<dynamic>>();
            A.CallTo(() => _consumableRepository.GetAllConsumablesAsync()).Returns(consumables);

            var filtered = A.Fake<IEnumerable<dynamic>>();
            A.CallTo(() => _consumableRepository.FilterConsumableByIdAsync(1)).Returns(filtered);

            // Act
            var result = _controller.ViewConsumables(1);

            // Assert
            result.Should().BeOfType<Task<IActionResult>>();
        }

        [Fact]
        public async Task AddConsumable_Returns_ViewResult()
        {
            // Arrange
            var suppliers = A.Fake<IEnumerable<Supplier>>();
            A.CallTo(() => _supplierRepository.GetAllSuppliersAsync()).Returns(suppliers);

            // Act
            var result = await _controller.AddConsumable();

            // Assert
            result.Should().BeOfType<ViewResult>();
            var viewResult = result as ViewResult;
            var model = viewResult.Should().BeNull;
        }

        [Fact]
        public void AddConsumable_Post_Returns_ViewResult()
        {
            // Arrange
            var consumable = new Consumable
            {
                ConsumableID = 1,
                Name = "Test Consumable",
                QuantityOnHand = 20,
                Cost = 100,
                SupplierID = 10,
                OnHandThreshold = 15,
                WardThreshold = 10,
                Status = "Active"
            };

            // Act
            var result = _mockController.AddConsumable(consumable);

            // Assert
            _mockConsumableRepo.Verify(repo => repo.AddConsumableAsync(consumable), Moq.Times.Once);
            result.Should().BeOfType<Task<IActionResult>>();
            var viewResult = result.Should().BeOfType<Task<IActionResult>>();

            consumable.Should().NotBeNull();
            consumable.SupplierID.Should().BeInRange(1, int.MaxValue, "Should contain a Supplier");
        }

    }
}
