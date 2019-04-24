using AutoMapper;
using KOZ.API.Controllers;
using KOZ.API.Controllers.RequestParameters;
using KOZ.API.Data.DataClasses;
using KOZ.API.Data.Dtos;
using KOZ.API.Data.Repositories;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using NUnit.Framework;
using System.Collections.Generic;

namespace KOZ.API.Tests.Controllers
{
    [TestFixture]
    partial class OrdersControllerTests
    {

        [Test]
        public void Get_AnyCollectionInRepository_ReturnsOk()
        {
            var orders = new List<Order>();
            var repoMock = Substitute.For<IOrdersRepository>();
            var workersRepoMock = Substitute.For<IRepository<Worker>>();
            var mapperMock = Substitute.For<IMapper>();
            repoMock.GetFiltered(Arg.Any<GetOrdersParameters>()).Returns(orders);
            mapperMock.Map<IEnumerable<Order>, List<OrderByStatusReadDto>>(orders)
                .ReturnsForAnyArgs(new List<OrderByStatusReadDto>());

            var controller = new OrdersController(repoMock, workersRepoMock, mapperMock);

            IActionResult result = controller.GetAll(new GetOrdersParameters());

            Assert.IsInstanceOf<OkObjectResult>(result);
        }

        [Test]
        public void Get_InvalidParameters_BadRequestReturned()
        {
            var invalidWorkerId = 10;
            var orders = new List<Order>();
            var repoMock = Substitute.For<IOrdersRepository>();
            var workersRepoMock = Substitute.For<IRepository<Worker>>();
            var mapperMock = Substitute.For<IMapper>();
            repoMock.GetById(invalidWorkerId).ReturnsNull();

            var controller = new OrdersController(repoMock, workersRepoMock, mapperMock);

            IActionResult result = 
                controller.GetAll(new GetOrdersParameters {ProcessingWorkerId = invalidWorkerId });

            Assert.IsInstanceOf<BadRequestResult>(result);
        }

        [Test]
        public void Get_AnyCollectionInRepository_CorrectCollectionReturned()
        {
            var orders = new List<Order>();
            var parameters = new GetOrdersParameters();
            var expectedCollection = new List<OrderByStatusReadDto>();
            var repoMock = Substitute.For<IOrdersRepository>();
            var workersRepoMock = Substitute.For<IRepository<Worker>>();
            var mapperMock = Substitute.For<IMapper>();
            repoMock.GetFiltered(parameters).Returns(orders);
            mapperMock.Map<IEnumerable<Order>, List<OrderByStatusReadDto>>(orders)
                .ReturnsForAnyArgs(expectedCollection);

            var controller = new OrdersController(repoMock, workersRepoMock, mapperMock);

            IActionResult result =
                controller.GetAll(parameters);

            Assert.AreEqual(((ObjectResult)result).Value, expectedCollection);
        }

        [Test]
        public void Get_AnyCollectionInRepository_MethodsCalledInOrder()
        {
            var orders = new List<Order>();
            var parameters = new GetOrdersParameters { ProcessingWorkerId = 10 };
            var repoMock = Substitute.For<IOrdersRepository>();
            var workersRepoMock = Substitute.For<IRepository<Worker>>();
            var mapperMock = Substitute.For<IMapper>();
            workersRepoMock.GetById(parameters.ProcessingWorkerId.Value).Returns(new Worker());
            repoMock.GetFiltered(parameters).Returns(orders);
            mapperMock.Map<IEnumerable<Order>, List<OrderByStatusReadDto>>(orders)
                .ReturnsForAnyArgs(new List<OrderByStatusReadDto>());

            var controller = new OrdersController(repoMock, workersRepoMock, mapperMock);

            IActionResult result = 
                controller.GetAll(parameters);

            Received.InOrder(() =>
            {
                workersRepoMock.GetById(parameters.ProcessingWorkerId.Value);
                repoMock.GetFiltered(parameters);
                mapperMock.Map<IEnumerable<Order>, List<OrderByStatusReadDto>>(orders);
            });
        }
    }
}
