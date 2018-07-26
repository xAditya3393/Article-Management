using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication6_Project.Infrastructure;

namespace WebApplication6_Project.App_Start
{
    public class FilterConfig
    {

        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {

            filters.Add(new TransactionFilter());
            filters.Add(new HandleErrorAttribute());

        }

    }
}