using MvcSiteMapProvider;
using System.Web.Mvc;

namespace DemoApp.Controllers.Persons
{
	public class DirectorsController : Controller
	{
		[MvcSiteMapNode(Title = "Directors", Key = "Directors", ParentKey = "Persons")]
		public ActionResult Index()
		{
			return View();
		}
	}
}