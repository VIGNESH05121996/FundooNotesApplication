using Common.LableModel;
using Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Interfaces
{
    public interface ILableBL
    {
        LableResponseModel CreateLable(long notesId, long jwtUserId, LableModel model);
        void DeleteLable(FundooLable lable,long jwtUserId);
        LableResponseModel GetLableWithId(long lableId, long jwtUserId);
        LableResponseModel GetAllLable(long jwtUserId);
        LableResponseModel UpdateLable(FundooLable updateLable, UpdateLableModel model, long jwtUserId);
        FundooLable GetLablesWithId(long lableId, long jwtUserId);
        LableResponseModel AddLable(LableModel model, long jwtUserId);
    }
}
