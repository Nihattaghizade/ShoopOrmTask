using Microsoft.EntityFrameworkCore;
using ShoopOrmTask.Contexts;
using ShoopOrmTask.Models;


ShopContext shopContext = new ShopContext();


IEnumerable<Category> GetAllCategory()
{
    IQueryable<Category> query = shopContext.Category.Where(x => !x.IsDeleted).AsNoTracking();

    IEnumerable<Category> categories = query.ToList();

    foreach (Category category in categories)
    {
        Console.WriteLine($"Id:{category.Id} Name:{category.Name} CreatedAt:{category.CreatedAt} UpdatedAt:{category.UpdatedAt}");
    }
    
    return categories;
}

void GetByIdCategory()
{
    Console.WriteLine("Input Id:");
    int.TryParse(Console.ReadLine(), out int id);

    Category? category = shopContext.Category.Where(x => !x.IsDeleted).AsNoTracking().FirstOrDefault(x => x.Id == id);

    if (category != null)
    {
        Console.WriteLine($"Id:{category.Id} Name:{category.Name} CreatedAt:{category.CreatedAt} UpdatedAt:{category.UpdatedAt}");
    }

    else
    {
        Console.WriteLine("This category doesn't exist!");
    }
}

void CreateCategory()
{
    Console.WriteLine("Input Name:");
    string? name = Console.ReadLine();

    while (string.IsNullOrWhiteSpace(name))
    {
        Console.WriteLine("Name can't be empty!");
        name = Console.ReadLine();
    }

    Category category = new Category()
    {
        Name = name,
        CreatedAt = DateTime.UtcNow.AddHours(4)
    };

    shopContext.Category.Add(category);
    int result = shopContext.SaveChanges();

    Console.WriteLine(result == 0 ? "Failed to save changes!" : "Successfully added");
}

void UpdateCategory()
{
    Console.WriteLine("Input Id:");
    int.TryParse(Console.ReadLine(), out int Id);

    Category? category = shopContext.Category.Where(x => x.Id == Id).FirstOrDefault();

    if (category == null)
    {
        Console.WriteLine("Not found!");
        return;
    }

    Console.WriteLine("Input Name:");
    string? name = Console.ReadLine();

    while (string.IsNullOrWhiteSpace(name))
    {
        Console.WriteLine("Name can't be empty!");
        name = Console.ReadLine();
    }

    category.Name = name;
    category.UpdatedAt = DateTime.UtcNow.AddHours(4);

    shopContext.SaveChanges();
}

void RemoveCategory()
{
    Console.WriteLine("Input Id:");
    int.TryParse(Console.ReadLine(), out int Id);

    Category? category = shopContext.Category.Where(x => x.Id == Id).FirstOrDefault();

    if (category == null)
    {
        Console.WriteLine("Not found!");
        return;
    }

    category.IsDeleted = true;
    shopContext.SaveChanges();
}



void GetAllProduct()
{
    IQueryable<Product> query = shopContext.Product.Where(x => !x.IsDeleted).Include(x => x.Category).AsNoTracking()
        .Select(x => new Product
        {
            Id = x.Id,
            Name = x.Name,
            Price = x.Price,
            CategoryId = x.CategoryId,
            Category = new Category { Name = x.Category.Name },
            CreatedAt = x.CreatedAt,
            UpdatedAt = x.UpdatedAt,
        });

    IEnumerable<Product> products = query.ToList();

    foreach (Product product in products)
    {
        Console.WriteLine($"Id:{product.Id} Name:{product.Name} Price:{product.Price} CategoryId:{product.CategoryId} Category:{product.Category.Name} CreatedAt:{product.CreatedAt} UpdatedAt:{product.UpdatedAt}");
    }
}

void GetByIdProduct()
{
    Console.WriteLine("Input Id:");
    int.TryParse(Console.ReadLine(), out int id);

    Product? product = shopContext.Product.Where(x => !x.IsDeleted).Include(x => x.Category).AsNoTracking()
        .Select(x => new Product
        {
            Id = x.Id,
            Name = x.Name,
            Price = x.Price,
            CategoryId = x.CategoryId,
            Category = new Category { Name = x.Category.Name },
            CreatedAt = x.CreatedAt,
            UpdatedAt = x.UpdatedAt,
        })
        .FirstOrDefault(x => x.Id == id);

    if (product != null)
    {
        Console.WriteLine($"Id:{product.Id} Name:{product.Name} Price:{product.Price} CategoryId:{product.CategoryId} Category:{product.Category.Name} CreatedAt:{product.CreatedAt} UpdatedAt:{product.UpdatedAt}");
    }
    else
    {
        Console.WriteLine("This product doesn't exist!");
    }
}

void CreateProduct()
{
    Console.WriteLine("Input Name:");
    string? name = Console.ReadLine();

    while (string.IsNullOrWhiteSpace(name))
    {
        Console.WriteLine("Name can't be empty!");
        name = Console.ReadLine();
    }

    Console.WriteLine("Input Price:");
    double.TryParse(Console.ReadLine(), out double price);

    if (price == 0)
    {
        Console.WriteLine("Price can't be 0");
        double.TryParse(Console.ReadLine(), out price);

    }

    IEnumerable<Category> categories = GetAllCategory();

    bool isExist = false;

    Console.WriteLine("Input Product CategoryId:");
    int.TryParse(Console.ReadLine(), out int categoryId);


    while (!isExist)
    {
        foreach (Category category in categories)
        {
            if (category.Id == categoryId)
            {
                isExist = true;
            }
        }

        if (!isExist)
        {

            Console.WriteLine("Input Product CategoryId:");
            int.TryParse(Console.ReadLine(), out categoryId);
        }
    }

    Product product = new Product()
    {
        Name = name,
        Price = price,
        CategoryId = categoryId,
        CreatedAt = DateTime.UtcNow.AddHours(4)
    };

    shopContext.Product.Add(product);
    int result = shopContext.SaveChanges();

    Console.WriteLine(result == 0 ? "Failed to save changes!" : "Successfully added");
}

void UpdateProduct()
{
    Console.WriteLine("Input Id:");
    int.TryParse(Console.ReadLine(), out int Id);

    Product? product = shopContext.Product.Where(x => x.Id == Id).FirstOrDefault();

    if (product == null)
    {
        Console.WriteLine("Not found!");
        return;
    }

    Console.WriteLine("Input Name:");
    string? name = Console.ReadLine();

    while (string.IsNullOrWhiteSpace(name))
    {
        Console.WriteLine("Name can't be empty!");
        name = Console.ReadLine();
    }

    Console.WriteLine("Input Price:");
    double.TryParse(Console.ReadLine(), out double price);

    if (price == 0)
    {
        Console.WriteLine("Price can't be 0");
        double.TryParse(Console.ReadLine(), out price);

    }

    IEnumerable<Category> categories = GetAllCategory();

    bool isExist = false;

    Console.WriteLine("Input Product CategoryId:");
    int.TryParse(Console.ReadLine(), out int categoryId);


    while (!isExist)
    {
        foreach (Category category in categories)
        {
            if (category.Id == categoryId)
            {
                isExist = true;
            }
        }

        if (!isExist)
        {

            Console.WriteLine("Input Product CategoryId:");
            int.TryParse(Console.ReadLine(), out categoryId);
        }
    }

    product.Name = name;
    product.Price = price;
    product.CategoryId = categoryId;
    product.UpdatedAt = DateTime.UtcNow.AddHours(4);

    shopContext.SaveChanges();
}

void RemoveProduct()
{
    Console.WriteLine("Input Id:");
    int.TryParse(Console.ReadLine(), out int Id);

    Product? product = shopContext.Product.Where(x => x.Id == Id).FirstOrDefault();

    if (product == null)
    {
        Console.WriteLine("Not found!");
        return;
    }

    product.IsDeleted = true;
    shopContext.SaveChanges();
}



Console.WriteLine("1.GetAll Categories");
Console.WriteLine("2.GetById Category");
Console.WriteLine("3.Create Category");
Console.WriteLine("4.Update Category");
Console.WriteLine("5.Remove Category");
Console.WriteLine("*****");
Console.WriteLine("6.GetAll Products");
Console.WriteLine("7.GetById Product");
Console.WriteLine("8.Create Product");
Console.WriteLine("9.Update Product");
Console.WriteLine("10.Remove Product");
Console.WriteLine("0.Close");

int.TryParse(Console.ReadLine(), out int request);

while (request != 0)
{
    switch (request)
    {
        case 1:
            GetAllCategory();
            break;
        case 2:
            GetByIdCategory();
            break;
        case 3:
            CreateCategory();
            break;
        case 4:
            UpdateCategory();
            break;
        case 5:
            RemoveCategory();
            break;
        case 6:
            GetAllProduct();
            break;
        case 7:
            GetByIdProduct();
            break;
        case 8:
            CreateProduct();
            break;
        case 9:
            UpdateProduct();
            break;
        case 10:
            RemoveProduct();
            break;
        default:
            Console.WriteLine("Invalid request!");
            break;
    }

    Console.WriteLine("1.GetAll Categories");
    Console.WriteLine("2.GetById Category");
    Console.WriteLine("3.Create Category");
    Console.WriteLine("4.Update Category");
    Console.WriteLine("5.Remove Category");
    Console.WriteLine("*****");
    Console.WriteLine("6.GetAll Products");
    Console.WriteLine("7.GetById Product");
    Console.WriteLine("8.Create Product");
    Console.WriteLine("9.Update Product");
    Console.WriteLine("10.Remove Product");
    Console.WriteLine("0.Close");

    int.TryParse(Console.ReadLine(), out request);
}