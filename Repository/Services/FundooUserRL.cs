﻿using Common.Models;
using Repository.Context;
using Repository.Entities;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Services
{
    public class FundooUserRL : IFundooUserRL<FundooUser>
    {
        readonly FundooUserContext context;

        public FundooUserRL(FundooUserContext context)
        {
            this.context = context;
        }

        public void Register(RegistrationModel model)
        {
            try
            {
                FundooUser user = new()
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Email = model.LastName,
                    Password = model.Password,
                    CreatedAt = model.CreatedAt
                };
                this.context.Add(user);
                this.context.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<FundooUser> GetAllData()
        {
            try
            {
                return this.context.FundooUserTable.ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public FundooUser GetWithId(long id)
        {
            try
            {
                return this.context.FundooUserTable.FirstOrDefault(i => i.Id == id);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Update(FundooUser dbEntity, FundooUser entity)
        {
            try
            {
                dbEntity.FirstName = entity.FirstName;
                dbEntity.LastName = entity.LastName;
                dbEntity.Email = entity.Email;
                dbEntity.Password = entity.Password;
                dbEntity.ModifiedAt = entity.ModifiedAt;
                this.context.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Delete(FundooUser entity)
        {
            try
            {
                this.context.FundooUserTable.Remove(entity);
                this.context.SaveChanges();
            }
            catch(Exception)
            {
                throw;
            }
        }

        public FundooUser Login(string email, string password)
        {
            try
            {
                return this.context.FundooUserTable.FirstOrDefault(e => e.Email == email && e.Password == password);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
