using MvcSiteMapProvider;
using System.Web.Mvc;

namespace DemoApp.Controllers.Persons
{
	public class ActorsController : Controller
	{
		[MvcSiteMapNode(Title = "Actors", Key = "Actors", ParentKey = "Persons")]
		public ActionResult Index()
		{
			return View();
		}
	}
}