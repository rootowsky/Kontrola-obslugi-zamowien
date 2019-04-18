using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KOZ.API.Controllers.RequestParameters;
using KOZ.API.Data.DataClasses;
using KOZ.API.Data.Enums;

namespace KOZ.API.Data.Repositories
{
    public interface IOrdersRepository : IRepository<Order>
    {
        IEnumerable<Order> GetFiltered(GetOrdersParameters getOrdersParameters);
    }
}
