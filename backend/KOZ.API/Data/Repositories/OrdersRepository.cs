using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KOZ.API.Controllers.RequestParameters;
using KOZ.API.Data.DataClasses;
using KOZ.API.Data.DbContexts;
using KOZ.API.Data.Enums;

namespace KOZ.API.Data.Repositories
{
    public class OrdersRepository : IOrdersRepository
    {
        private readonly OrdersContext dbContext;

        public OrdersRepository(OrdersContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public IEnumerable<Order> GetAll()
        {
            return dbContext.Orders;
        }

        public Order GetById(int entityId)
        {
            return dbContext.Orders
                .SingleOrDefault(order => order.OrderId == entityId);
        }

        public void Insert(Order entity)
        {
            dbContext.Orders.Add(entity);
        }

        public void Delete(Order entity)
        {
            dbContext.Orders.Remove(entity);
        }

        public void Update(Order entity)
        {
            dbContext.Orders.Update(entity);
        }

        public void UpdateOrInsert(Order entity)
        {
            if (!TryToUpdate(entity))
            {
                Insert(entity);
            }
        }

        public void Save()
        {
            dbContext.SaveChanges();
        }

        public IEnumerable<Order> GetFiltered(GetOrdersParameters getOrdersParameters)
        {
            var query = dbContext.Orders.AsEnumerable();

            query = FilterByStatus(query, getOrdersParameters.Status);
            query = FilterByWorkerId(query, getOrdersParameters.ProcessingWorkerId);
            
            return query;
        }

        private IEnumerable<Order> FilterByWorkerId(IEnumerable<Order> query, int? workerId)
        {
            if(workerId.HasValue)
            {
                return query.Where(order => order.ProcessingWorkerId == workerId);
            }

            return query;
        }

        private IEnumerable<Order> FilterByStatus(IEnumerable<Order> query, OrderStatus? status)
        {
            if(status.HasValue)
            {
                return query.Where(order => order.Status == status);
            }

            return query;
        }

        private bool TryToUpdate(Order order)
        {
            bool instExists = 
                dbContext.Orders.Any(o => o.OrderId == order.OrderId);

            if (instExists)
            {
                Update(order);
            }

            return instExists;
        }
    }
}
