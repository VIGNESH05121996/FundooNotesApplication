using Common.Models;
using Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interfaces
{
    public interface IFundooUserRL<TEntity>
    {
        void Register(RegistrationModel model);
        IEnumerable<TEntity> GetAllData();
        TEntity GetWithId(long id);
        void Update(TEntity dbEntity, TEntity entity);
        void Delete(TEntity entity);
    }
}
