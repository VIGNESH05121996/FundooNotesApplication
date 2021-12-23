using Common.Models;
using Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Interfaces
{
    public interface IFundooUserBL <TEntity>
    {
        void Register(RegistrationModel model);
        IEnumerable<TEntity> GetAllData();
        TEntity GetWithId(long id);
        void Update(TEntity dbEntity, TEntity entity);
        void Delete(TEntity entity);
        FundooUser Login(string email,string password);
    }
}
