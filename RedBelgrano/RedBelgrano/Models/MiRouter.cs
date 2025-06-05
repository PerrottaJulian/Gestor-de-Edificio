using Microsoft.AspNetCore.Mvc;

namespace RedBelgrano.Models
{
    public class MiRouter
    {
        public static IActionResult Error(Controller controller, Exception ex)
        {
            var urlAnterior = controller?.Request?.Path.Value ?? string.Empty;
            controller.ViewBag.UrlAnterior = urlAnterior;

            // Devolvés la vista de error con la excepción como modelo
            return controller.View("Error", ex);
        }

    }
}