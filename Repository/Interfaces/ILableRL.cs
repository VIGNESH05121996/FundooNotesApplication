using Common.LableModel;
using Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interfaces
{
    public interface ILableRL
    {
        LableResponseModel CreateLable(long notesId, long jwtUserId, LableModel model);
        LableResponseModel GetLableWithId(long lableId, long jwtUserId);
        void DeleteLable(FundooLable lable, long jwtUserId);
        LableResponseModel GetAllLable(long jwtUserId);
        void UpdateLable(FundooLable updateLable, UpdateLableModel model, long jwtUserId);
        FundooLable GetLablesWithId(long lableId, long jwtUserId);
        FundooLable AddLable(LableModel model, long jwtUserId);
    }
}
