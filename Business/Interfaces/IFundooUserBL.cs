using Common.Models;
using Common.UserModels;
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
        RegistrationResponse Register(RegistrationModel model);
        string Login(LoginModel model);
        string ForgetPassword(ForgetPasswordModel model);
        bool ResetPassword(ResetPasswordModel model,string email);
        List<FundooUser> RedisUser();
    }
}
