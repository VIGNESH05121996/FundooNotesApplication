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
        string Login(LoginModel model);
        string ForgetPassword(ForgetPasswordModel model);
        bool ResetPassword(ResetPasswordModel model,string email);
    }
}
