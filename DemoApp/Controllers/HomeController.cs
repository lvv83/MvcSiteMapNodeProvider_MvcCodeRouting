using MvcSiteMapProvider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DemoApp.Controllers
{
	public class HomeController : Controller
	{
		[MvcSiteMapNode(Title = "Home", Key = "Home")]
		public ActionResult Index()
		{
			return View();
		}

		[MvcSiteMapNode(Title = "More", Key = "More", ParentKey="Home", Order=1000, Url="http://example.com/movies")]
		public ActionResult External()
		{
			return new EmptyResult();
		}
	}
}