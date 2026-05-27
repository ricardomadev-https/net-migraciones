using System;

namespace CrudNet4
{
    internal static class Program
    {
        private static readonly ProductService Service = new ProductService();

        private static void Main()
        {
            bool exit = false;

            while (!exit)
            {
                ShowMenu();
                Console.Write("Selecciona una opcion: ");
                string option = Console.ReadLine();
                Console.WriteLine();

                switch (option)
                {
                    case "1":
                        CreateProduct();
                        break;
                    case "2":
                        ListProducts();
                        break;
                    case "3":
                        UpdateProduct();
                        break;
                    case "4":
                        DeleteProduct();
                        break;
                    case "0":
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Opcion invalida.");
                        break;
                }

                if (!exit)
                {
                    Console.WriteLine();
                    Console.WriteLine("Presiona una tecla para continuar...");
                    Console.ReadKey();
                    Console.Clear();
                }
            }
        }

        private static void ShowMenu()
        {
            Console.WriteLine("=== CRUD Basico .NET Framework 4.8 ===");
            Console.WriteLine("1. Crear producto");
            Console.WriteLine("2. Listar productos");
            Console.WriteLine("3. Actualizar producto");
            Console.WriteLine("4. Eliminar producto");
            Console.WriteLine("0. Salir");
            Console.WriteLine();
        }

        private static void CreateProduct()
        {
            Console.Write("Nombre: ");
            string name = Console.ReadLine();

            Console.Write("Precio: ");
            decimal price;
            if (!decimal.TryParse(Console.ReadLine(), out price))
            {
                Console.WriteLine("Precio invalido.");
                return;
            }

            Product product = Service.Create(name, price);
            Console.WriteLine("Producto creado con ID: {0}", product.Id);
        }

        private static void ListProducts()
        {
            var products = Service.GetAll();

            if (products.Count == 0)
            {
                Console.WriteLine("No hay productos registrados.");
                return;
            }

            Console.WriteLine("ID\tNombre\t\tPrecio");
            Console.WriteLine("---------------------------------");
            foreach (var product in products)
            {
                Console.WriteLine("{0}\t{1}\t\t{2:C}", product.Id, product.Name, product.Price);
            }
        }

        private static void UpdateProduct()
        {
            Console.Write("ID del producto a actualizar: ");
            int id;
            if (!int.TryParse(Console.ReadLine(), out id))
            {
                Console.WriteLine("ID invalido.");
                return;
            }

            Product existing = Service.GetById(id);
            if (existing == null)
            {
                Console.WriteLine("Producto no encontrado.");
                return;
            }

            Console.Write("Nuevo nombre ({0}): ", existing.Name);
            string newName = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(newName))
            {
                newName = existing.Name;
            }

            Console.Write("Nuevo precio ({0}): ", existing.Price);
            string priceInput = Console.ReadLine();
            decimal newPrice;
            if (string.IsNullOrWhiteSpace(priceInput))
            {
                newPrice = existing.Price;
            }
            else if (!decimal.TryParse(priceInput, out newPrice))
            {
                Console.WriteLine("Precio invalido.");
                return;
            }

            bool updated = Service.Update(id, newName, newPrice);
            Console.WriteLine(updated ? "Producto actualizado." : "No se pudo actualizar.");
        }

        private static void DeleteProduct()
        {
            Console.Write("ID del producto a eliminar: ");
            int id;
            if (!int.TryParse(Console.ReadLine(), out id))
            {
                Console.WriteLine("ID invalido.");
                return;
            }

            bool deleted = Service.Delete(id);
            Console.WriteLine(deleted ? "Producto eliminado." : "Producto no encontrado.");
        }
    }
}
