using KOZ.API.Data.DataClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KOZ.API.Data.DbContexts;
using KOZ.API.Data.Enums;

namespace KOZ.API.Data.Repositories
{
    public class WorkersRepository : IRepository<Worker>
    {
        private readonly OrdersContext dbContext;

        public WorkersRepository(OrdersContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public IEnumerable<Worker> GetAll()
        {
            return dbContext.Workers;
        }

        public Worker GetById(int entityId)
        {
            return dbContext.Workers
                .SingleOrDefault(worker => worker.WorkerId == entityId);
        }

        public void Insert(Worker entity)
        {
            dbContext.Workers.Add(entity);
        }

        public void Delete(Worker entity)
        {
            dbContext.Workers.Remove(entity);
        }

        public void Update(Worker entity)
        {
            dbContext.Workers.Update(entity);
        }

        public void UpdateOrInsert(Worker entity)
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


        private bool TryToUpdate(Worker worker)
        {
            bool instExists =
                dbContext.Workers.Any(w => w.WorkerId== worker.OrderId);

            if (instExists)
            {
                Update(worker);
            }

            return instExists;
        }
    }
}
