using CatalogueApp.ConsoleUi.Common;
using CatalogueApp.ConsoleUi.Controllers;
using CatalogueApp.Data.Data.Models;
using Microsoft.Extensions.DependencyInjection;


class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Hello");

        var config = ConfigurationHelper.SetupConfiguration();

        var serviceProvider = ServiceHelper.BuildServiceProvider(config);

        ProductController productController = serviceProvider.GetRequiredService<ProductController>();

        CategoryController categoryController = serviceProvider.GetRequiredService<CategoryController>();

        while (true)
        {
            Console.Clear();
            Console.WriteLine(" # # # # # Catalogue App # # # # #");
            Console.WriteLine("1 - Products | 2 - Categories |  0 - Exit");

            int menuChoice;

            Int32.TryParse(Console.ReadLine(), out menuChoice);
           
            switch (menuChoice)
            {
                case 1:
                    Console.Clear();
                    Console.WriteLine(" # # # # # Products Menu # # # # #");
                    Console.WriteLine("1 - View Products | 2 - Create Product | 3 - Delete Product | 4 - Update Product | 0 - Back");

                    int productsChoice;
                    if (!int.TryParse(Console.ReadLine(), out productsChoice))
                    {
                        Console.WriteLine("Invalid input. Please enter a valid number.");
                        continue;
                    }

                    switch (productsChoice)
                    {
                        case 1:
                            Console.WriteLine("All Products");

                            var allproducts = productController.GetAllProducts();


                            foreach (var product in allproducts)
                            {
                                Console.WriteLine($"Name:{product.Name}");

                                Console.WriteLine($"Description:{product.Description}");

                                Console.WriteLine($"Price:{product.Price}");

                                foreach (var category in product.Categories.OrderBy(c => c.Name))
                                {
                                    Console.WriteLine($"Category:{category.Name}");
                                }
                            }
                            Console.ReadKey();
                                break;

                        case 2:
                            Console.WriteLine("Add Product");

                            var allcategories = categoryController.GetAllCategories();

                            foreach (var category in allcategories)
                            {

                                Console.WriteLine($"{category.Id}-{category.Name}");

                            }
                            Console.WriteLine("Enter Id of Category");

                            int id;

                            Int32.TryParse(Console.ReadLine(), out id);

                            var potentialCategory = categoryController.GetCategoryById(id);

                            if (potentialCategory != null)
                            {

                                Product product = new Product();

                                product.Categories = new List<Category>() { potentialCategory };

                                Console.WriteLine("Enter a name");
                                product.Name = Console.ReadLine();

                                Console.WriteLine("Enter a Description");
                                product.Description = Console.ReadLine();

                                Console.WriteLine("Enter a Price");
                                product.Price = Convert.ToInt32(Console.ReadLine());

                                productController.AddProduct(product);
                            }
                            if(potentialCategory == null) 
                            {
                                Console.WriteLine("No categories");

                                Console.ReadKey();
                            }
                            break;

                        case 3:
                            Console.WriteLine("Delete a product");

                            var allproducts3 = productController.GetAllProducts();


                            foreach (var product in allproducts3)
                            {
                                Console.WriteLine(product.Id);

                                Console.WriteLine(product.Name);

                                Console.WriteLine(product.Description);

                                Console.WriteLine(product.Price);

                                foreach (var category in product.Categories.OrderBy(c => c.Name))
                                {
                                    Console.WriteLine($"{category.Name}");
                                }

                            }

                            Console.WriteLine("Enter Id");

                            int Delid;

                            Int32.TryParse(Console.ReadLine(), out Delid);

                            var potentialTrash = productController.GetProductById(Delid);


                            Console.WriteLine("1.Delete the product  2.Delete a category");

                            int deleteChoice;

                            Int32.TryParse(Console.ReadLine(), out deleteChoice);

                            if (potentialTrash != null)
                            {

                                if (deleteChoice == 1)
                                {

                                    productController.RemoveProduct(potentialTrash);

                                    

                                    Console.ReadKey();

                                }
                                if (deleteChoice == 2)
                                {
                                    Console.WriteLine("Delete a categorie");
                                    Console.WriteLine("All Categories");

                                    var allCategories = categoryController.GetAllCategories();

                                    foreach (var category in allCategories)
                                    {
                                        Console.WriteLine($"{category.Id}:{category.Name}");
                                    }
                                    Console.WriteLine("Vvedi Category");

                                    int categoryId;

                                    Int32.TryParse(Console.ReadLine(), out categoryId);

                                    var deletedCategorie = categoryController.GetCategoryById(categoryId);

                                    if (deletedCategorie != null)
                                    {
                                        potentialTrash.Categories.Remove(deletedCategorie);

                                        productController.UpdateProduct(potentialTrash);

                                    }
                                }
                            }
                            else
                            {
                                Console.WriteLine("No such product");

                                Console.ReadKey(
                                    );
                            }
                                break;

                        case 4:
                            Console.WriteLine("Update a product");


                            var allproducts4 = productController.GetAllProducts();

                            foreach (var product in allproducts4)
                            {
                                Console.WriteLine($"ID:{product.Id}");

                                Console.WriteLine($"Name:{product.Name}");

                                Console.WriteLine($"Description:{product.Description}");

                                Console.WriteLine($"Price:{product.Price}");

                                foreach (var category in product.Categories.OrderBy(c => c.Name))
                                {
                                    Console.WriteLine($"Category:{category.Name}");
                                }

                            }

                            Console.WriteLine("Choose a product");

                            int uptadeId;

                            Int32.TryParse(Console.ReadLine(), out uptadeId);

                            var exsistingProduct = productController.GetProductById(uptadeId);

                            if (exsistingProduct != null)
                            {

                                Console.WriteLine("1.Update category  2.Add Category  3.Update product");

                                int updateChoice;

                                Int32.TryParse(Console.ReadLine(), out updateChoice);

                                if (updateChoice == 1)
                                {
                                    Console.WriteLine("Update your categorie");

                                    var allcategories4 = categoryController.GetAllCategories();

                                    foreach (var category in allcategories4)
                                    {
                                        Console.WriteLine($"{category.Id}:{category.Name}");
                                    }
                                    Console.WriteLine("Enter Id");

                                    int categoryId;

                                    Int32.TryParse(Console.ReadLine(), out categoryId);


                                    Category existingCategory = categoryController.GetCategoryById(categoryId);

                                    if (existingCategory != null)
                                    {
                                        

                                        Console.WriteLine("Enter a name");
                                        existingCategory.Name = Console.ReadLine();

                                        

                                        categoryController.UpdateCategory(existingCategory);
                                    }
                                    else
                                    {
                                        Console.WriteLine("Category not found");

                                        Console.ReadKey();
                                    }
                                }
                                if (updateChoice == 2)
                                {
                                    Console.WriteLine("All Categories");

                                    var allCategories = categoryController.GetAllCategories();

                                    foreach (var category in allCategories)
                                    {
                                        Console.WriteLine($"{category.Id}:{category.Name}");
                                    }
                                    Console.WriteLine("Vvedi Category");

                                    int categoryId;

                                    Int32.TryParse(Console.ReadLine(), out categoryId);
                                    var potentialCategory4 = categoryController.GetCategoryById(categoryId);
                                    if (exsistingProduct != null)
                                    {

                                        if (potentialCategory4 != null)
                                        {
                                            exsistingProduct.Categories.Add(potentialCategory4);

                                            productController.UpdateProduct(exsistingProduct);

                                        }
                                        if (potentialCategory4 == null)
                                        {
                                            Console.WriteLine("No free categories");

                                            Console.ReadKey();

                                            // Как сделать шоб при отсутсвии фри категорий выводить сообщение
                                        }

                                    }

                                }
                                if (updateChoice == 3)
                                {
                                    Console.WriteLine("Update a product");

                                    if (exsistingProduct != null)
                                    {
                                        Console.WriteLine("Enter a new name");
                                        exsistingProduct.Name = Console.ReadLine();

                                        Console.WriteLine("Enter new description");
                                        exsistingProduct.Description = Console.ReadLine();

                                        Console.WriteLine("Enter a new price");
                                        exsistingProduct.Price = Convert.ToInt32(Console.ReadLine());

                                        productController.UpdateProduct(exsistingProduct);
                                    }
                                }

                            }
                            else if(exsistingProduct == null)
                            {
                                Console.WriteLine("No product with this id");

                                Console.ReadKey();
                            }

            break;

                        case 0:
                            break;

                        default:
                            Console.WriteLine("Invalid choice. Please try again.");
                            break;
                    }
                    break;

                case 2:
                    Console.Clear();
                    Console.WriteLine(" # # # # # Categories Menu # # # # #");
                    Console.WriteLine("1 - View Categories | 2 - Create Category | 3 - Delete Category | 4 - Update Category | 0 - Back");

                    int categoriesChoice;
                    if (!int.TryParse(Console.ReadLine(), out categoriesChoice))
                    {
                        Console.WriteLine("Invalid input. Please enter a valid number.");
                        continue;
                    }

                    switch (categoriesChoice)
                    {
                        case 1:
                            Console.WriteLine("All Categories");

                            var allCategories1 = categoryController.GetAllCategories();

                            foreach (var category in allCategories1)
                            {
                                Console.WriteLine($"Id: {category.Id}");
                                Console.WriteLine($"Name: {category.Name}");
                                Console.WriteLine($"Parent Category: {category.ParentCategory?.Name ?? "No parent category"}");

                                Console.WriteLine("Subcategories:");
                                if (category.ChildCategories != null)
                                {
                                    foreach (var subcategory in category.ChildCategories)
                                    {
                                        Console.WriteLine("- " + subcategory.Name);
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("  No subcategories");
                                }
                            }

                                Console.ReadKey();
                            
                         
                            break;


                        case 2:
                            Console.WriteLine("Create Category");

                           
                            Console.WriteLine("Choose category type:");
                            Console.WriteLine("1. Regular Category");
                            Console.WriteLine("2. Subcategory");
                            Console.WriteLine("Enter your choice:");

                            int categoryChoice;
                            Int32.TryParse(Console.ReadLine(), out categoryChoice);

                           
                            if (categoryChoice == 1)
                            {
                                
                                Category category = new Category();

                                Console.WriteLine("Enter a name");
                                category.Name = Console.ReadLine();

                                categoryController.AddCategory(category);
                                Console.WriteLine("Category added successfully!");
                            }
                            else if (categoryChoice == 2)
                            {
                               
                                Console.WriteLine("Available categories for parent category:");

                                var allCategories2 = categoryController.GetAllCategories();
                                foreach (var category in allCategories2)
                                {
                                    Console.WriteLine($"{category.Id}-{category.Name}");
                                }

                                Console.WriteLine("Enter Id of parent category:");
                                int parentCategoryId;
                                Int32.TryParse(Console.ReadLine(), out parentCategoryId);

                               
                                var parentCategory = categoryController.GetCategoryById(parentCategoryId);

                                if (parentCategory != null)
                                {
                                   
                                    Category subcategory = new Category();

                                    Console.WriteLine("Enter a name");
                                    subcategory.Name = Console.ReadLine();



                                    subcategory.ParentCategoryId = parentCategoryId;

                                    subcategory.ParentCategory = parentCategory;

                                    categoryController.AddCategory(subcategory);
                                    Console.WriteLine("Subcategory added successfully!");
                                }
                                else
                                {
                                    Console.WriteLine("Invalid parent category ID.");
                                }
                            }
                            else
                            {
                                Console.WriteLine("Invalid choice.");
                            }
                            break;

                        case 3:
                            Console.WriteLine("Delete Categorie");

                            var allCategories = categoryController.GetAllCategories();

                            foreach (var category3 in allCategories)
                            {
                                Console.WriteLine($"{category3.Id}---{category3.Name}");
                            }

                            Console.WriteLine("Enter Categorie Id");

                            int id;

                            Int32.TryParse(Console.ReadLine(), out id);

                            Category potentialDeletedCategory = categoryController.GetCategoryById(id);

                            if (potentialDeletedCategory != null)
                            {
                                categoryController.RemoveCategory(potentialDeletedCategory);

                                Console.WriteLine("Categorie deleted");
                            }
                            else
                            {
                                Console.WriteLine("Category not found");
                            }
                            break;

                        case 4:
                            Console.WriteLine("Update your categorie");

                            var allcategories = categoryController.GetAllCategories();

                            foreach (var category4 in allcategories)
                            {
                                Console.WriteLine($"{category4.Id}-{category4.Name}");
                            }
                            Console.WriteLine("Enter Id");

                            int updateId;

                            Int32.TryParse(Console.ReadLine(), out updateId);


                            Category existingCategory = categoryController.GetCategoryById(updateId);

                            if (existingCategory != null)
                            {
                                
                                Console.WriteLine("Enter a name");

                                existingCategory.Name = Console.ReadLine();

                                categoryController.UpdateCategory(existingCategory);
                            }
                            else
                            {
                                Console.WriteLine("Category not found");
                            }                
                    break;

                        case 0:
                            break;

                        default:
                            Console.WriteLine("Invalid choice. Please try again.");
                            break;
                    }
                    break;

                case 0:
                    return;

                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
        }
    }
}