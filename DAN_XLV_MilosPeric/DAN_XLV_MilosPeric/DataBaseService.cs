using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAN_XLV_MilosPeric
{
    class DataBaseService
    {
        internal List<vwProduct> GetAllWarehouseItems()
        {
            try
            {
                using (WarehouseDataBaseEntities context = new WarehouseDataBaseEntities())
                {
                    List<vwProduct> list = new List<vwProduct>();
                    list = (from x in context.vwProducts select x).ToList();
                    return list;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception" + ex.Message.ToString());
                return null;
            }
        }

        internal vwProduct AddWarehouseItem(vwProduct item)
        {
            try
            {
                using (WarehouseDataBaseEntities context = new WarehouseDataBaseEntities())
                {
                    tblProduct newProduct = new tblProduct();
                    newProduct.ProductName = item.ProductName;
                    newProduct.ProductNumber = item.ProductNumber;
                    newProduct.Amount = item.Amount;
                    newProduct.Price = item.Price;
                    newProduct.InStock = item.InStock;
                    context.tblProducts.Add(newProduct);
                    context.SaveChanges();
                    item.ID = newProduct.ID;
                    return item;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception" + ex.Message.ToString());
                return null;
            }
        }

        internal vwProduct ChangeStoredStatus(vwProduct item)
        {
            try
            {
                using (WarehouseDataBaseEntities context = new WarehouseDataBaseEntities())
                {
                    tblProduct itemToEditStoredStatus = (from i in context.tblProducts where i.ID == item.ID select i).First();
                    itemToEditStoredStatus.InStock = item.InStock;
                    context.SaveChanges();
                    return item;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception" + ex.Message.ToString());
                return null;
            }
        }

        internal vwProduct EditWarehouseItem(vwProduct item)
        {
            try
            {
                using (WarehouseDataBaseEntities context = new WarehouseDataBaseEntities())
                {
                    tblProduct itemToEdit = (from i in context.tblProducts where i.ID == item.ID select i).First();
                    itemToEdit.ProductName = item.ProductName;
                    itemToEdit.ProductNumber = item.ProductNumber;
                    itemToEdit.Amount = item.Amount;
                    itemToEdit.Price = item.Price;
                    context.SaveChanges();
                    return item;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception" + ex.Message.ToString());
                return null;
            }
        }

        internal void DeleteWarehouseItem(int itemId)
        {
            try
            {
                using (WarehouseDataBaseEntities context = new WarehouseDataBaseEntities())
                {
                    tblProduct itemToDelete = (from i in context.tblProducts where i.ID == itemId select i).First();
                    context.tblProducts.Remove(itemToDelete);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception" + ex.Message.ToString());
            }
        }
    }
}
