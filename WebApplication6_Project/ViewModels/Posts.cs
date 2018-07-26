using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication6_Project.Infrastructure;
using WebApplication6_Project.Models;

namespace WebApplication6_Project.ViewModels
{
    public class PostsIndex
    {
        public PagedData<Post> Posts { get; set; }
    }

    public class PostsShow
    {
        public Post Post { get; set; }
    }

    public class PostsTag
    {
        public Tag Tag { get; set; }
        public PagedData<Post> Posts { get; set; }
    }
}