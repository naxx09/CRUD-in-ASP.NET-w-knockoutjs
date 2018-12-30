using System.Web.Mvc;
using aspnetWKo.Interfaces;
using aspnetWKo.Repositories;
using aspnetWKo.Models;
namespace aspnetWKo.Controllers
{
    public class ProductController : Controller
    {
        static readonly IProductRepository repository = new ProductRepository();
        // GET: Product
        public ActionResult Products()
        {
            return View();
        }

        public JsonResult GetAllProducts()
        {
            return Json(repository.GetAll(), JsonRequestBehavior.AllowGet);
        }

        public JsonResult AddProduct(tblProduct item)
        {
            item = repository.Add(item);
            return Json(item, JsonRequestBehavior.AllowGet);
        }

        public JsonResult EditProduct(int id, tblProduct product)
        {
            product.Id = id;
            if (repository.Update(product))
                return Json(repository.GetAll(), JsonRequestBehavior.AllowGet);

            return Json(null);
        }

        public JsonResult DeleteProduct(int id)
        {
            if (repository.Delete(id))
                return Json(new { Status = true }, JsonRequestBehavior.AllowGet);

            return Json(new { Status = false }, JsonRequestBehavior.AllowGet);
        }
    }
}