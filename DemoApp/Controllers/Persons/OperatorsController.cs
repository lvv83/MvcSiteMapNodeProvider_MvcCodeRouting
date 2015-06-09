using MvcSiteMapProvider;
using System.Web.Mvc;

namespace DemoApp.Controllers.Persons
{
	public class OperatorsController : Controller
	{
		[MvcSiteMapNode(Title = "Operators", Key = "Operators", ParentKey = "Persons")]
		public ActionResult Index()
		{
			return View();
		}
	}
}