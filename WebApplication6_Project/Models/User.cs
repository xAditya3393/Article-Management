﻿using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication6_Project.Models
{
    public class User
    {

        private const int WorkFactor = 13;

        public static void FakeHash()
        {
            BCrypt.Net.BCrypt.HashPassword("", WorkFactor);
        }

        public virtual int Id { get; set; }
        public virtual string Username { get; set; }
        public virtual string Email { get; set; }
        public virtual string PasswordHash { get; set; }


        public virtual IList<Role> Roles { get; set; }

        public User()
        {

            Roles = new List<Role>();  
        }


        public virtual void SetPassword(string Password)
        {
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(Password, WorkFactor);
        }

        public virtual bool CheckPassword(string Password)
        {
            
            return BCrypt.Net.BCrypt.Verify(Password,PasswordHash);
        }
    }

    public class UserMap : ClassMapping<User>
    {
        public UserMap()
        {
            Table("users");

            Id(x => x.Id, x => x.Generator(Generators.Identity));

            Property(x => x.Username, x => x.NotNullable(true));
            Property(x => x.Email, x => x.NotNullable(true));

            Property(x => x.PasswordHash, x =>
            {
                x.Column("password_hash");
                x.NotNullable(true);
                
            });


            Bag(x => x.Roles, x =>
            {
                x.Table("role_users");
                x.Key(k => k.Column("user_id"));

            }, x => x.ManyToMany(k => k.Column("role_id")));

        }


    }






} 