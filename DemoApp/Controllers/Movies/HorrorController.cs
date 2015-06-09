using MvcSiteMapProvider;
using System.Web.Mvc;

namespace DemoApp.Controllers.Movies
{
	public class HorrorController : Controller
	{
		[MvcSiteMapNode(Title = "Horror", Key = "Horror", ParentKey = "Movies")]
		public ActionResult Index()
		{
			return View();
		}
	}
}