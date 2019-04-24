using AutoMapper;
using KOZ.API.Controllers;
using KOZ.API.Data.DataClasses;
using KOZ.API.Data.Dtos;
using KOZ.API.Data.Repositories;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace KOZ.API.Tests.Controllers
{
    [TestFixture]
    partial class OrdersControllerTests
    {
        [Test]
        public void Get_ForCorrectOrderId_ReturnsOk()
        {
            var repoMock = Substitute.For<IOrdersRepository>();
            var workersRepoMock = Substitute.For<IRepository<Worker>>();
            var mapperMock = Substitute.For<IMapper>();
            repoMock.GetById(Arg.Any<int>()).Returns(new Order());
            mapperMock.Map<OrderReadDto>(Arg.Any<Order>()).Returns(new OrderReadDto());

            var controller = new OrdersController(repoMock, workersRepoMock, mapperMock);

            IActionResult result = controller.GetById(5);

            Assert.IsInstanceOf<OkObjectResult>(result);
        }

        [Test]
        public void Get_ForCorrectOrderId_ReturnsCorrectDto()
        {
            var expected = new OrderReadDto();
            var repoMock = Substitute.For<IOrdersRepository>();
            var workersRepoMock = Substitute.For<IRepository<Worker>>();
            var mapperMock = Substitute.For<IMapper>();
            repoMock.GetById(Arg.Any<int>()).Returns(new Order());
            mapperMock.Map<OrderReadDto>(Arg.Any<Order>()).Returns(expected);

            var controller = new OrdersController(repoMock, workersRepoMock, mapperMock);

            IActionResult result = controller.GetById(5);

            Assert.IsInstanceOf<OkObjectResult>(result);

            Assert.AreEqual(((ObjectResult)result).Value, expected);
        }

        [Test]
        public void Get_ForNotExistingOrderId_ReturnsNotFound()
        {
            var repoMock = Substitute.For<IOrdersRepository>();
            var workersRepoMock = Substitute.For<IRepository<Worker>>();
            var mapperMock = Substitute.For<IMapper>();
            repoMock.GetById(Arg.Any<int>()).ReturnsNull();
            mapperMock.Map<OrderReadDto>(Arg.Any<Order>()).ReturnsNull();

            var controller = new OrdersController(repoMock, workersRepoMock, mapperMock);

            IActionResult result = controller.GetById(5);

            Assert.IsInstanceOf<NotFoundResult>(result);
        }

        [Test]
        public void Get_ForAnyOrderId_MethodsCalledInOrder()
        {
            var correctId = 100;
            var correctOrder = new Order();
            var repoMock = Substitute.For<IOrdersRepository>();
            var workersRepoMock = Substitute.For<IRepository<Worker>>();
            var mapperMock = Substitute.For<IMapper>();
            repoMock.GetById(correctId).Returns(correctOrder);
            mapperMock.Map<OrderReadDto>(correctOrder).Returns(new OrderReadDto());

            var controller = new OrdersController(repoMock, workersRepoMock, mapperMock);

            IActionResult result = controller.GetById(correctId);

            Assert.IsInstanceOf<OkObjectResult>(result);

            Received.InOrder(() =>
            {
                repoMock.GetById(correctId);
                mapperMock.Map<OrderReadDto>(correctOrder);
            });
        }
    }
}
