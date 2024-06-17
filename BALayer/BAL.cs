using BELayer;
using DALayer;
namespace BALayer
{
    public class BAL
    {
        DAL dal = new DAL();
        public List<Inventory> GetInventories()
        {
            return dal.GetInventories();
        }

        public int Create(Inventory inventory)
        {
            return dal.Create(inventory);
        }

        public void EditInventory(int id, Inventory inventory)
        {
            dal.EditInventory(id, inventory);
        }

        public void DeleteInventory(int id)
        {
            dal.DeleteInventory(id);
        }
        public Inventory GetInventory(int id)
        {
            return dal.GetInventory(id);
        }
    }
}