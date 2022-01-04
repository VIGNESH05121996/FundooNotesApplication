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
        string Login(LoginModel model);
        Task<string> ForgetPassword(ForgetPasswordModel model);
    }
}
