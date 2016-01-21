using HangFireManager;
using System.Web.Mvc;

namespace WebUI.Controllers
{
    public class HomeController : Controller
    {
        private Manager _managerJobs = new Manager();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult AddJob(string tipo)
        {
            if (tipo.Equals("FireForget"))
                _managerJobs.AddJob(() => ExecutarNada());
            else if (tipo.Equals("Delayed"))
                _managerJobs.AddJob(() => ExecutarNada(), 1);
            else if (tipo.Equals("Recurring"))
                _managerJobs.AddJob(() => ExecutarNada(), Period.Hourly);

            return View("Index");
        }

        public void ExecutarNada()
        {
            //haha nada de metodo.
        }
    }
}