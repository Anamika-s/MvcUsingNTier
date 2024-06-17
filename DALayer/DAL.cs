using System.Data.SqlClient;
using BELayer;
namespace DALayer
{
    public class DAL
    {
        SqlConnection connection;
        string GetConnectionString()
        {
            return "data source=ANAMIKA\\SQLSERVER;initial catalog=CompanyDb;integrated security=true";
        }
        SqlConnection GetConnection()

        {
            return new SqlConnection(GetConnectionString());

        }
        public List<Inventory> GetInventories()
        {
            List<Inventory> inventories = new List<Inventory>();
            using (SqlConnection connection = GetConnection())
            {
                using (SqlCommand command = new SqlCommand())
                {
                    command.CommandText = "select * from inventory";
                    command.Connection = connection;
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            Inventory inventory = new Inventory()
                            {
                                InventoryId = (int)reader[0],
                                ProductName = reader[1].ToString(),
                                QtyInStock = (int)reader[2],
                                ReorderLevel = (int)reader[3],
                                AddedOn = (DateTime)reader[4]
                            };
                            inventories.Add(inventory);
                        }
                        connection.Close();
                        return inventories;
                    }
                    else
                        return null;
                }

            }
        }
        public int Create(Inventory inventory)
        {
            int count;
            using (SqlConnection connection = GetConnection())
            {
                using (SqlCommand command = new SqlCommand())
                {
                    command.CommandText = "insert into inventory(productName,qtyInStock,reorderLevel,AddedOn) values(@productName,@qtyInStock,@reorderLevel,@AddedOn)";
                    command.Connection = connection;
                    command.Parameters.AddWithValue("@productName", inventory.ProductName);
                    command.Parameters.AddWithValue("@qtyInStock", inventory.QtyInStock);
                    command.Parameters.AddWithValue("@reorderLevel", inventory.ReorderLevel);
                    command.Parameters.AddWithValue("@AddedOn", inventory.AddedOn);
                    connection.Open();
                    count = command.ExecuteNonQuery();
                    connection.Close();

                }
            }
            return count;
        }

        public Inventory GetInventory(int id)
        {
            Inventory inventory = null;
            using (SqlConnection connection = GetConnection())
            {
                using (SqlCommand command = new SqlCommand())
                {
                    command.CommandText = "select * from inventory where inventory_id=@id";
                    command.Parameters.AddWithValue("@id", id);
                    command.Connection = connection;
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        reader.Read();
                        inventory = new Inventory()
                        {
                            InventoryId = (int)reader[0],
                            ProductName = reader[1].ToString(),
                            QtyInStock = (int)reader[2],
                            ReorderLevel = (int)reader[3],
                            AddedOn = (DateTime)reader[4]

                        };
                    }
                    connection.Close();
                    return inventory;
                }
            }
        }
        public void EditInventory(int id, Inventory inventory)
        {
            Inventory obj = GetInventory(id);
            if (obj != null)
            {
                using (SqlConnection connection = GetConnection())
                {
                    using (SqlCommand command = new SqlCommand())
                    {
                        command.CommandText = "update inventory set qtyInStock=@qtyInStock, reorderLevel=@reorderLevel,AddedOn=@AddedOn where inventory_id=@id";
                        command.Parameters.AddWithValue("@qtyInStock", inventory.QtyInStock);
                        command.Parameters.AddWithValue("@reorderLevel", inventory.ReorderLevel);
                        command.Parameters.AddWithValue("@AddedOn", inventory.AddedOn);
                        command.Parameters.AddWithValue("@id", id);
                        command.Connection = connection;
                        connection.Open();
                        command.ExecuteNonQuery();
                        connection.Close();

                    }
                }
            }
        }
        public void DeleteInventory(int id)
        {
            Inventory obj = GetInventory(id);
            if (obj != null)
            {
                using (SqlConnection connection = GetConnection())
                {
                    using (SqlCommand command = new SqlCommand())
                    {
                        command.CommandText = "delete from inventory where inventory_id=@id";
                        command.Parameters.AddWithValue("@id", id);
                        command.Connection = connection;
                        connection.Open();
                        command.ExecuteNonQuery();
                        connection.Close();

                    }
                }

            }
        }
    }
}