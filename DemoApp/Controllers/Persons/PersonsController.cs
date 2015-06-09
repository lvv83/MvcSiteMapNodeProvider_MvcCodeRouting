using MvcSiteMapProvider;
using System.Web.Mvc;

namespace DemoApp.Controllers.Persons
{
	public class PersonsController : Controller
	{
		[MvcSiteMapNode(Title = "Persons", Key = "Persons", ParentKey = "Home")]
		public ActionResult Index()
		{
			return View();
		}
	}
}