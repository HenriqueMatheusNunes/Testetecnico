using Microsoft.AspNetCore.Mvc;

namespace GestaoFornecedores.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return RedirectToAction("Index", "Fornecedores");
        }
    }
}
