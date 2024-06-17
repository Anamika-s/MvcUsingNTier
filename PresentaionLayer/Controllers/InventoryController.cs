using Microsoft.AspNetCore.Mvc;
using BALayer;
using BELayer;

namespace PresentaionLayer.Controllers
{
    public class InventoryController : Controller
    {
        BAL bal = new BAL();
        public IActionResult Index()
        {
            List<Inventory> inventories = bal.GetInventories();
            return View(inventories);
        }

        public IActionResult Create() {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Inventory inventory)
        {
            int count = bal.Create(inventory);
            return RedirectToAction("Index");

        }

        public IActionResult Details(int id)
        {
            return View(bal.GetInventory(id));
        }

        public IActionResult Edit(int id)
        {
              Inventory inventory = bal.GetInventory(id);
               return View(inventory);
        }

        [HttpPost]
        public IActionResult Edit(int id, Inventory inventory) {
            bal.EditInventory(id, inventory);
            return RedirectToAction("index");
        }

        public IActionResult Delete(int id)
        {
            Inventory inventory = bal.GetInventory(id);
            return View(inventory);

        }

        [HttpPost]
       
        public IActionResult Deleted(int id)
        {
            bal.DeleteInventory(id);
            return RedirectToAction("index");
        }
    }
}
