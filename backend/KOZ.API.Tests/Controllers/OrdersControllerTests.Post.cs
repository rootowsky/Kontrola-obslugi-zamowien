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
using System;
using System.Collections.Generic;
using System.Text;

namespace KOZ.API.Tests.Controllers
{
    [TestFixture]
    partial class OrdersControllerTests
    {
        [Test]
        public void Post_ForCorrectDto_ReturnsCreatedAtAction()
        {
            var repoMock = Substitute.For<IOrdersRepository>();
            var workersRepoMock = Substitute.For<IRepository<Worker>>();
            var mapperMock = Substitute.For<IMapper>();
            mapperMock.Map<Order>(Arg.Any<OrderInsertDto>()).Returns(new Order());
            mapperMock.Map<OrderReadDto>(Arg.Any<Order>()).Returns(new OrderReadDto());


            var controller = new OrdersController(repoMock, workersRepoMock, mapperMock);

            IActionResult result = controller.Post(new OrderInsertDto());

            Assert.IsInstanceOf<CreatedAtActionResult>(result);
        }

        [Test]
        public void Post_ForInvalidDto_ReturnsBadRequest()
        {
            var repoMock = Substitute.For<IOrdersRepository>();
            var workersRepoMock = Substitute.For<IRepository<Worker>>();
            var mapperMock = Substitute.For<IMapper>();
            mapperMock.Map<Order>(Arg.Any<OrderInsertDto>()).ReturnsNull();

            var controller = new OrdersController(repoMock, workersRepoMock, mapperMock);

            IActionResult result = controller.Post(new OrderInsertDto());

            Assert.IsInstanceOf<BadRequestResult>(result);
        }

        [Test]
        public void Post_ForCorrectDto_MethodsCalledInOrder()
        {
            var orderInsertDto = new OrderInsertDto();
            var order = new Order();
            var repoMock = Substitute.For<IOrdersRepository>();
            var workersRepoMock = Substitute.For<IRepository<Worker>>();
            var mapperMock = Substitute.For<IMapper>();
            mapperMock.Map<Order>(orderInsertDto).Returns(order);
            mapperMock.Map<OrderReadDto>(Arg.Any<Order>()).Returns(new OrderReadDto());

            var controller = new OrdersController(repoMock, workersRepoMock, mapperMock);

            IActionResult result = controller.Post(orderInsertDto);

            Received.InOrder(() =>
            {
                mapperMock.Map<Order>(orderInsertDto);
                repoMock.Insert(order);
                repoMock.Save();
                mapperMock.Map<OrderReadDto>(order);
            });
        }

        [Test]
        public void Post_ForCorrectDto_ReturnsCorrectDto()
        {
            var expected = new OrderReadDto();
            var repoMock = Substitute.For<IOrdersRepository>();
            var workersRepoMock = Substitute.For<IRepository<Worker>>();
            var mapperMock = Substitute.For<IMapper>();
            mapperMock.Map<Order>(Arg.Any<OrderInsertDto>()).Returns(new Order());
            mapperMock.Map<OrderReadDto>(Arg.Any<Order>()).Returns(expected);

            var controller = new OrdersController(repoMock, workersRepoMock, mapperMock);

            IActionResult result = controller.Post(new OrderInsertDto());

            Assert.AreEqual(((ObjectResult)result).Value, expected);
        }
    }
}
