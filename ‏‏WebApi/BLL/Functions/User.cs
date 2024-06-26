﻿using AutoMapper;
using BLL.Interfaces;
using DAL.Interfaces;
using DTO.Classes;
using System;
using System.Collections.Generic;

namespace BLL.Functions;

public class UserBll : IUserBll
{
    IUserDal dal;
    IMapper mapper;

    public UserBll(IUserDal dal)
    {
        this.dal = dal;

        var config = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<RecipeProfile>();
        });

        mapper = config.CreateMapper();
    }

    public List<User> GetUsers()
    {

            List<DAL.Models.User> users = dal.GetUsers();
            List<User> usersB = new List<User>();
            if (users!=null && users.Count>0)
            {
                foreach (var u in users)
                {
                usersB.Add(mapper.Map<DAL.Models.User, User>(u));
                }
            }
            return usersB;

        //return new List<User>();
    }

    public User Login(string email, string password)
    {
        DAL.Models.User user = dal.Login(email, password);
        if (user != null)
        {
            return mapper.Map<DAL.Models.User, User>(user);
        }
        return null;
    }

    public User Register(User user)
    {
        DAL.Models.User u = dal.Register(mapper.Map<User, DAL.Models.User>(user));
        if (u != null)
        {
            return mapper.Map<DAL.Models.User, User>(u);
        }
        return null;
    }
}
