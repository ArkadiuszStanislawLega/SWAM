using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Windows;
using SWAM.Enumerators;
using SWAM.Models;

namespace SWAM
{
    public class Testable
    {
        public static void Test()
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {

                Product product = new Product() { Id = 1, Name = "Cegła" };
                Product product2 = new Product() { Id = 2, Name = "Worek cementu" };
                Product product3 = new Product() { Id = 3, Name = "Młotek" };

                var adress = new Address
                {
                    ApartmentNumber = "10",
                    Id = 2,
                    City = "Poznań",
                    HouseNumber = "1",
                    Country = "Poland",
                    Street = "Gutentagowa",
                    PostCode = "11010"
                };

                var warehouse = new Warehouse()
                {
                    Name = "Najlepszy magazyn na swiecie",
                    Address = adress
                };

                var warehouseOrder = new WarehouseOrder()
                {
                    Id = 1,
                    WarehouseOrderStatus = WarehouseOrderStatus.Delivered,
                    OrderDate = DateTime.Now,
                    Warehouse = warehouse
                };

                var warehouseOrderPosition = new WarehouseOrderPosition()
                {
                    Id = 1,
                    Price = 10,
                    Product = product,
                    Quantity = 10,
                    WarehouseOrder = warehouseOrder
                };

                var warehouseOrderPosition2 = new WarehouseOrderPosition()
                {
                    Id = 2,
                    Price = 100,
                    Product = product2,
                    WarehouseOrder = warehouseOrder,
                    Quantity = 1000
                };

                var warehouseOrderPosition3 = new WarehouseOrderPosition()
                {
                    Id = 3,
                    Price = 150,
                    Product = product3,
                    Quantity = 500,
                    WarehouseOrder = warehouseOrder
                };

                //context.Addresses.Add(adress);
                //context.Products.Add(product);
                //context.Products.Add(product2);
                //context.Products.Add(product3);
                //context.Warehouses.Add(warehouse);
                //context.WarehouseOrders.Add(warehouseOrder);
                //context.WarehouseOrderPositions.Add(warehouseOrderPosition);
                //context.WarehouseOrderPositions.Add(warehouseOrderPosition2);
                //context.WarehouseOrderPositions.Add(warehouseOrderPosition3);

                //context.SaveChanges();


                List<WarehouseOrderPosition> warehousesPositions = context.WarehouseOrderPositions.Include(w => w.WarehouseOrder.Warehouse).Include(w => w.Product).ToList();

                Dictionary<string, uint> allProductsFromWarehouse = new Dictionary<string, uint>();


                string score = "";
                foreach (WarehouseOrderPosition w in warehousesPositions)
                {
                    if (w.WarehouseOrder.Warehouse.Id == 1)
                    {
                        allProductsFromWarehouse.Add(w.Product.Name, w.Quantity);
                    }
                }

                foreach (KeyValuePair<string, uint> entry in allProductsFromWarehouse)
                    score += entry.Key + " " + entry.Value + ", ";


                MainWindow win = (MainWindow)Application.Current.MainWindow;

                if (warehousesPositions != null)
                    win.message.Text = "" + score;
                else
                    win.message.Text = "null";
            }
        }
    }
}
