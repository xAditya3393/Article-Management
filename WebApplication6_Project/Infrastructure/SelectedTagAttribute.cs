using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication6_Project.Infrastructure
{

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class SelectedTagAttribute : ActionFilterAttribute
    {

        private readonly string _selectedTag;

        public SelectedTagAttribute(string selectedTab)
        {
            _selectedTag = selectedTab;
        }

        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            filterContext.Controller.ViewBag.SelectedTab = _selectedTag;
        }

    }
}