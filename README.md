# ASP.NETCoreSD46IsmailiaD08

# 🔷 ASP.NET Core MVC – Day 08 (.NET 9)

## Dependency Injection & Repository Pattern

Day 08 contains **two projects**:

1️⃣ Console Application – Dependency Injection & Loose Coupling  
2️⃣ ASP.NET Core MVC Application – Repository Pattern + DI + Service Lifetime  

---

# 📦 Project 1 – Console App  
## Dependency Injection (Loose Coupling)

This project demonstrates:

- Tight Coupling vs Loose Coupling
- Dependency Inversion Principle (DIP)
- Constructor Injection
- Interface-based design
- Testability improvement

---

## ❌ Tight Coupling (Bad Practice)

```csharp
var paymentService = new PaymentService();
var orderRepository = new OrderRepository();
var emailService = new EmailService();
```

Problems:

- Hard to test
- Hard to replace implementation
- Business logic mixed with object creation
- Violates SOLID principles

---

## ✅ Loose Coupling (Using Interfaces)

```csharp
private readonly IPaymentService _paymentService;
private readonly IOrderRepository _orderRepository;
private readonly IEmailService _emailService;
```

Injected via constructor:

```csharp
public OrderService(
    IPaymentService paymentService,
    IOrderRepository orderRepository,
    IEmailService emailService)
{
    _paymentService = paymentService;
    _orderRepository = orderRepository;
    _emailService = emailService;
}
```

---

## 🔄 Order Flow

```csharp
public void PlaceOrder(string orderId, string customerId, decimal amount)
{
    _paymentService.ProcessPayment(orderId, amount);
    _orderRepository.SaveOrder(orderId, customerId, amount);
    _emailService.SendConfirmationEmail(customerId, orderId);
}
```

---

## 🧠 What We Achieved

✔ Separation of Concerns  
✔ Easy Unit Testing  
✔ Replaceable Implementations  
✔ Follows SOLID (Dependency Inversion Principle)  

---

# 📦 Project 2 – ASP.NET Core MVC

## Concepts Covered

- Repository Pattern
- Dependency Injection
- EF Core
- Service Lifetime (Scoped)
- IConfiguration
- Injecting services into Views
- Multiple Implementations
- Clean Architecture

---

# 🗂 Repository Pattern

Instead of using `AppDbContext` directly in controllers:

```csharp
private readonly IDepartmentRepository _departmentRepository;
```

---

## IDepartmentRepository

```csharp
public interface IDepartmentRepository
{
    IEnumerable<Department> GetAll();
    Department? GetById(int departmentID);
    void Insert(Department department);
    void Update(Department department);
    void Delete(Department department);
    int Save();
}
```

---

## DepartmentRepository Implementation

```csharp
public class DepartmentRepository : IDepartmentRepository
{
    private readonly AppDbContext _context;

    public DepartmentRepository(AppDbContext context)
    {
        _context = context;
    }

    public IEnumerable<Department> GetAll()
        => _context.Departments.ToList();

    public Department? GetById(int id)
        => _context.Departments.Find(id);

    public void Insert(Department department)
        => _context.Departments.Add(department);

    public void Delete(Department department)
        => _context.Departments.Remove(department);

    public int Save()
        => _context.SaveChanges();
}
```

---

# 🧱 Why Repository Pattern?

✔ Decouples Controllers from EF Core  
✔ Easier to Unit Test  
✔ Clean Architecture  
✔ Replace database without changing controller  

---

# 🏗 Dependency Injection Registration

Inside `Program.cs`:

```csharp
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("ASPNETCoreD08"));
});

builder.Services.AddScoped<IPrint, Print>();
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();
```

---

# 🔁 Service Lifetime Explained

### Scoped

```csharp
builder.Services.AddScoped<IPrint, Print>();
```

- One instance per HTTP request
- Shared within same request
- New instance on new request

---

# 🖨 Custom Service – IPrint

## Interface

```csharp
public interface IPrint
{
    Guid Id { get; set; }
    void PrintDateTime();
}
```

---

## Implementation

```csharp
public class Print : IPrint
{
    public Guid Id { get; set; }

    public Print()
    {
        Id = Guid.NewGuid();
    }

    public void PrintDateTime()
    {
        Console.WriteLine($"Print Service {DateTime.UtcNow}");
    }
}
```

---

# 🎮 Injecting into Controller

```csharp
public class TestController : Controller
{
    private readonly IPrint _print;
    private readonly IConfiguration _configuration;

    public TestController(IPrint print, IConfiguration configuration)
    {
        _print = print;
        _configuration = configuration;
    }
}
```

---

# 🖥 Injecting Service into View

```csharp
@inject IPrint _print

<h1>@_print.Id</h1>
```

---

# ⚙ Reading From Configuration

```csharp
var appName = _configuration.GetSection("AppName").Value;
```

Reads value from:

```
appsettings.json
```

---

# 🏛 MVC With Repository Pattern

## DepartmentController

Instead of:

```csharp
private readonly AppDbContext db;
```

We use:

```csharp
private readonly IDepartmentRepository _departmentRepository;
```

---

## EmployeeController

Uses two repositories:

```csharp
private readonly IEmployeeRepository _employeeRepository;
private readonly IDepartmentRepository _departmentRepository;
```

Clean & maintainable architecture.

---

# 🆚 Direct EF Controller (Scaffolded)

Also included:

```csharp
DepartmentsController
```

Created using:

```
Add → Controller → MVC Controller with Views using EF Core
```

This shows:

- Async EF Core
- Model Binding
- Concurrency Handling
- Overposting Protection

Example:

```csharp
[Bind("Id,Name")] Department department
```

Prevents overposting attacks.

---

# 🔥 DI Flow in ASP.NET Core

1. Register service in Program.cs  
2. ASP.NET stores it inside IoC container  
3. Framework injects it automatically  
4. Controller uses it  

---

# 📂 Project Structure

```
/Controllers
    DepartmentController.cs
    EmployeeController.cs
    TestController.cs
    DepartmentsController.cs (Scaffolded)

/Repositories
    DepartmentRepository
    EmployeeRepository

/Helper
    IPrint.cs
    Print.cs
    PrintV02.cs
    PrintV03.cs

/Data
    AppDbContext.cs
```

---

# 🎯 Key Learning Outcomes

✔ Understand Dependency Injection deeply  
✔ Apply Repository Pattern  
✔ Use Service Lifetimes correctly  
✔ Inject services into Views  
✔ Read from IConfiguration  
✔ Compare Scaffolded vs Clean Architecture  
✔ Improve Testability  
✔ Follow SOLID Principles  

---

# 🧠 Interview Topics From This Day

- What is Dependency Injection?
- What is IoC Container?
- What is Scoped lifetime?
- Difference between AddScoped / AddTransient / AddSingleton?
- What is Repository Pattern?
- Why not inject DbContext directly?
- What is Overposting?
- What is Constructor Injection?

---

# ⭐ Summary

Day 08 focused on:

- Professional architecture
- Clean separation of concerns
- Real-world scalable design
- Writing maintainable enterprise-level code

---

# 👨‍💻 Author

Mohamed Hatem  
Software Engineer

---
