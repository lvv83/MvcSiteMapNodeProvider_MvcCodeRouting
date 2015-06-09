using MvcSiteMapProvider;
using System.Web.Mvc;

namespace DemoApp.Controllers.Movies
{
	public class DocumentaryController : Controller
	{
		[MvcSiteMapNode(Title = "Documentary", Key = "Documentary", ParentKey = "Movies")]
		public ActionResult Index()
		{
			return View();
		}
	}
}