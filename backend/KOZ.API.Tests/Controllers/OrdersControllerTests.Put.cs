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
        public void Put_ForCorrectDto_ReturnsOk()
        {
            var repoMock = Substitute.For<IOrdersRepository>();
            var workersRepoMock = Substitute.For<IRepository<Worker>>();
            var mapperMock = Substitute.For<IMapper>();
            repoMock.GetById(Arg.Any<int>()).Returns(new Order());
            mapperMock.Map(Arg.Any<OrderUpdateDto>(),Arg.Any<Order>()).Returns(new Order());
            mapperMock.Map<OrderReadDto>(Arg.Any<Order>()).Returns(new OrderReadDto());

            var controller = new OrdersController(repoMock, workersRepoMock, mapperMock);

            IActionResult result = controller.Put(new OrderUpdateDto());

            Assert.IsInstanceOf<OkObjectResult>(result);
        }

        [Test]
        public void Put_ForInvalidOrderId_ReturnsNotFound()
        {
            var repoMock = Substitute.For<IOrdersRepository>();
            var workersRepoMock = Substitute.For<IRepository<Worker>>();
            var mapperMock = Substitute.For<IMapper>();
            repoMock.GetById(Arg.Any<int>()).ReturnsNull();

            var controller = new OrdersController(repoMock, workersRepoMock, mapperMock);

            IActionResult result = controller.Put(new OrderUpdateDto());

            Assert.IsInstanceOf<NotFoundResult>(result);
        }

        [Test]
        public void Put_ForCorrectDto_MethodsCalledInOrder()
        {
            var orderId = 10;
            var order = new Order();
            var updatedOrder = new Order();
            var orderUpdateDto = new OrderUpdateDto { OrderId = orderId };
            var repoMock = Substitute.For<IOrdersRepository>();
            var workersRepoMock = Substitute.For<IRepository<Worker>>();
            var mapperMock = Substitute.For<IMapper>();
            repoMock.GetById(orderId).Returns(order);
            mapperMock.Map(orderUpdateDto,order).Returns(updatedOrder);
            mapperMock.Map<OrderReadDto>(updatedOrder).Returns(new OrderReadDto());

            var controller = new OrdersController(repoMock, workersRepoMock, mapperMock);

            IActionResult result = controller.Put(orderUpdateDto);

            Received.InOrder(() =>
            {
                repoMock.GetById(orderId);
                mapperMock.Map(orderUpdateDto, order);
                repoMock.Update(updatedOrder);
                repoMock.Save();
                mapperMock.Map<OrderReadDto>(updatedOrder);
            });
        }

        [Test]
        public void Put_ForCorrectDto_ReturnsCorrectDto()
        {
            var expected = new OrderReadDto();
            var repoMock = Substitute.For<IOrdersRepository>();
            var workersRepoMock = Substitute.For<IRepository<Worker>>();
            var mapperMock = Substitute.For<IMapper>();
            repoMock.GetById(Arg.Any<int>()).Returns(new Order());
            mapperMock.Map(Arg.Any<OrderUpdateDto>(), Arg.Any<Order>()).Returns(new Order());
            mapperMock.Map<OrderReadDto>(Arg.Any<Order>()).Returns(expected);

            var controller = new OrdersController(repoMock, workersRepoMock, mapperMock);

            IActionResult result = controller.Put(new OrderUpdateDto());

            Assert.AreEqual(((ObjectResult)result).Value, expected);
        }
    }
}
