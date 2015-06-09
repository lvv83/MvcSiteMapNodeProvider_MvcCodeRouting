using MvcSiteMapProvider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DemoApp.Controllers.Movies
{
	public class ComedyController : Controller
	{
		[MvcSiteMapNode(Title = "Comedy", Key = "Comedy", ParentKey = "Movies")]
		public ActionResult Index()
		{
			return View();
		}
	}
}