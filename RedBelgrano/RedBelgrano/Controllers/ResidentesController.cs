using Microsoft.AspNetCore.Mvc;
using RedBelgrano.Context;

namespace RedBelgrano.Controllers
{
    public class ResidentesController : Controller
    {
        public AppDBContext db;

        public ResidentesController(AppDBContext _context)
        {
            db = _context;
        }

        public IActionResult Index()
        {
            return View();
        }

    }
}
