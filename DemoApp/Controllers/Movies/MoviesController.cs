using MvcSiteMapProvider;
using System.Web.Mvc;

namespace DemoApp.Controllers.Movies
{
	public class MoviesController : Controller
	{
		[MvcSiteMapNode(Title = "Movies", Key = "Movies", ParentKey="Home")]
		public ActionResult Index()
		{
			return View();
		}
	}
}