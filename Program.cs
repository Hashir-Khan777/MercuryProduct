using MecuryProduct.Components;
using MecuryProduct.Components.Account;
using MecuryProduct.Data;
using MecuryProduct.Services;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Radzen;

var builder = WebApplication.CreateBuilder(args);

/* This block of code is configuring Razor Components and Server-Side Blazor in a C# ASP.NET Core
application. */
builder.Services.AddRazorComponents().AddInteractiveServerComponents();
builder.Services.AddServerSideBlazor().AddHubOptions(o =>
{
    o.MaximumReceiveMessageSize = 10 * 1024 * 1024;
});

/* This block of code is configuring session management in a C# ASP.NET Core application. */
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

/* This block of code is configuring various services and dependencies for authentication and
user-related functionalities in a C# ASP.NET Core application. */
builder.Services.AddCascadingAuthenticationState();
builder.Services.AddScoped<IdentityUserAccessor>();
builder.Services.AddScoped<IdentityRedirectManager>();
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<CustomerService>();
builder.Services.AddScoped<CarService>();
builder.Services.AddScoped<DialogService>();
builder.Services.AddScoped<NoteService>();
builder.Services.AddScoped<SessionService>();
builder.Services.AddScoped<DocService>();
builder.Services.AddScoped<StateFormService>();
builder.Services.AddScoped<ProductionService>();
builder.Services.AddScoped<NotificationService>();
builder.Services.AddScoped<HelperService>();
builder.Services.AddScoped<CompanyService>();
builder.Services.AddScoped<LogService>();
builder.Services.AddScoped<ProductService>();
builder.Services.AddScoped<InvoiceService>();
builder.Services.AddScoped<CategoryService>();
builder.Services.AddScoped<PaymentService>();
builder.Services.AddScoped<LocalizationService>();
builder.Services.AddScoped<ExpenseService>();
builder.Services.AddScoped<PosCustomerService>();
builder.Services.AddScoped<AuthenticationStateProvider, ServerAuthenticationStateProvider>();

/* `builder.Services.AddHttpClient<ApiService>();` is registering the `ApiService` class with the
dependency injection container as a transient service that can make HTTP requests. This allows
instances of `ApiService` to be injected into other classes or components that require making HTTP
calls to external APIs. The `AddHttpClient` method sets up the HttpClient factory to create
instances of `HttpClient` for `ApiService` instances, providing better performance and resource
utilization compared to creating new `HttpClient` instances manually. */
builder.Services.AddHttpClient<ApiService>();

/* This block of code is configuring authorization policies in a C# ASP.NET Core application. It is
defining four policies: "Admin", "Manager", "Employee", and "Driver". Each policy is associated with
a specific role claim requirement. For example, the "Admin" policy requires the user to have a role
claim with the value "Admin" in order to access resources protected by that policy. Similarly, the
other policies have role claim requirements for "Manager", "Employee", and "Driver" roles
respectively. These policies help control access to different parts of the application based on the
roles assigned to users. */
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Admin", p => p.RequireClaim("Role", "Admin"));
    options.AddPolicy("Manager", p => p.RequireClaim("Role", "Manager"));
    options.AddPolicy("Employee", p => p.RequireClaim("Role", "Employee"));
    options.AddPolicy("Driver", p => p.RequireClaim("Role", "Driver"));
});
builder.Services.AddAuthentication(options =>
    {
        options.DefaultScheme = IdentityConstants.ApplicationScheme;
        options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
    })
    .AddIdentityCookies();

/* This block of code is retrieving the connection string named "DefaultConnection" from the
configuration settings. If the connection string is not found, it throws an
`InvalidOperationException` with a message indicating that the connection string was not found. */
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

/* This block of code is configuring the identity system in a C# ASP.NET Core application. Here's a
breakdown of what each method call is doing: */
builder.Services.AddIdentityCore<ApplicationUser>(options =>
{
    options.SignIn.RequireConfirmedAccount = true;
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);
    options.Lockout.MaxFailedAccessAttempts = 3;
    options.Lockout.AllowedForNewUsers = true;
})
.AddEntityFrameworkStores<ApplicationDbContext>()
.AddSignInManager()
.AddDefaultTokenProviders();


/* `builder.Services.AddSingleton<IEmailSender<ApplicationUser>, EmailSender>();` is registering the
`EmailSender` class as a singleton service in the dependency injection container with a specific
generic type `IEmailSender<ApplicationUser>`. */
builder.Services.AddSingleton<IEmailSender<ApplicationUser>, EmailSender>();

/* This block of code is configuring the password requirements for the identity system in a C# ASP.NET
Core application. It is using the `Configure` method to set specific options for the
`IdentityOptions` class related to password policies. */
builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = true;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();
app.UseSession();

app.MapRazorComponents<App>().AddInteractiveServerRenderMode();

// Add additional endpoints required by the Identity /Account Razor components.
app.MapAdditionalIdentityEndpoints();

app.Run();
