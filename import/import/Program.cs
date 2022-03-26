using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace import
{
    class Program
    {
        static void Main(string[] args)
        {
            // обращаемся к БД
            AutoServiceE autoservice = new AutoServiceE();

            Console.ReadKey();

            // метод для добавления данных в таблицу ProductSale
            void AddProductSale()
            {
                // циклом проходим по всем строкам данных из excel
                foreach(productSale_ product in autoservice.productSale_)
                {
                    // обьявляем данные
                    int count = (int)product.Count;
                    DateTime dateSale = ((DateTime)product.DateSale);
                    Product prod = autoservice.Products.Where(x => x.Title == product.Product).FirstOrDefault();
                    // создаем новый экземпляр класс ProductSale
                    ProductSale newProdSale = new ProductSale { Product = prod, SaleDate = dateSale, Quantity = count };
                    // добавляем его в БД
                    autoservice.ProductSales.Add(newProdSale);
                }
                // сохраняем изменения
                autoservice.SaveChanges();
            }

            // добавление продуктов
            void AddProducts()
            {
                foreach(productImport_ product in autoservice.productImport_)
                {
                    string image = @"Images\" + product.Image.Trim().Substring(product.Image.IndexOf('\\') + 1);
                    bool active = product.Active == "да" ? true : false;
                    Manufacturer manufacturer = autoservice.Manufacturers.Where(x => x.Name == product.Manufacter.Trim()).FirstOrDefault();
                    decimal cost = (decimal)product.Cost;

                    Product newProduct = new Product { Title = product.Product, MainImagePath = image, Cost = cost, IsActive = active, Manufacturer = manufacturer };
                    autoservice.Products.Add(newProduct);
                }
                autoservice.SaveChanges();
            }

            // в excel были пробелы, поэтому IsActive обработал не верно, в данном методе изменяю столбец
            void EditProducts()
            {
                foreach (productImport_ product in autoservice.productImport_)
                {
                    bool active = product.Active == " да" ? true : false;

                    Product toEdit = autoservice.Products.Where(x => x.ID == product.ID + 1).FirstOrDefault();

                    toEdit.IsActive = active;

                   
                }
                autoservice.SaveChanges();
            }
            // добавление производителя
            void addManufacturer()
            {
                foreach (manufactur_ manufactur in autoservice.manufactur_)
                {
                    Manufacturer newManu = new Manufacturer { Name = manufactur.manufactr, StartDate = manufactur.dateStart };
                    autoservice.Manufacturers.Add(newManu);
                }

                autoservice.SaveChanges();
            }
            

        }
    }
}
