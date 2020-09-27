using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Jobweb.Filtros
{
    [AttributeUsage(AttributeTargets.Method,AllowMultiple = false)]
    public class Autorizaciones: AuthorizeAttribute
    {

    }
}