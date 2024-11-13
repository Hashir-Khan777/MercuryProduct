using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace MecuryProduct.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IHttpContextAccessor HttpContextAccessor) : IdentityDbContext<ApplicationUser>(options)
    {
        private IHttpContextAccessor HttpContextAccessor = HttpContextAccessor;

        /* The above code is defining a DbContext class in C# for Entity Framework. It includes properties for
        different DbSet entities such as CustomerModel, CarModel, NoteModel, DocModel, StateFormModel,
        MasterProductionTable, MasterVehicleTable, and MasterYearTable. These properties represent tables in
        the database and allow interaction with the corresponding entities using Entity Framework's ORM
        capabilities. */
        public DbSet<CustomerModel> Customers { get; set; }
        public DbSet<PosCustomerModel> PosCustomers { get; set; }
        public DbSet<CarModel> Cars { get; set; }
        public DbSet<NoteModel> Notes { get; set; }
        public DbSet<DocModel> Docs { get; set; }
        public DbSet<StateFormModel> StateForm { get; set; }
        public DbSet<MasterProductionTable> MasterProductionTable { get; set; }
        public DbSet<MasterVehicleTable> MasterVehicleTable { get; set; }
        public DbSet<MasterYearTable> MasterYearTable { get; set; }
        public DbSet<CompanyModel> Companies { get; set; }
        public DbSet<ProductModel> Products { get; set; }
        public DbSet<CategoryModel> Categories { get; set; }
        public DbSet<InvoiceModel> Invoices { get; set; }
        public DbSet<AuditLogModel> Logs { get; set; }
        public DbSet<PaymentModel> Payments { get; set; }
        public DbSet<LocalizationModel> Localization { get; set; }
        public DbSet<ExpenseModel> Expenses { get; set; }
        public DbSet<CompanyManager> CompanyManagers { get; set; }
        public DbSet<CompanyEmployees> CompanyEmployees { get; set; }
        public DbSet<CompanyDrivers> CompanyDrivers { get; set; }
        public DbSet<ProductInvoice> ProductInvoices { get; set; }

        private void AuditChanges()
        {
            var UserId = HttpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var UserClaim = HttpContextAccessor.HttpContext?.User?.Claims?.ToList();

            if (UserClaim == null) return;

            var UserRole = "";

            try
            {
                UserRole = UserClaim[4]?.Value;
            }
            catch (ArgumentOutOfRangeException ex)
            {
                UserRole = null;
            }


            var entries = ChangeTracker.Entries()
                                        .Where(e => e.State == EntityState.Added
                                                 || e.State == EntityState.Modified
                                                 || e.State == EntityState.Deleted).ToList();

            foreach (var entry in entries)
            {
                var entityId = entry.Property("Id").CurrentValue;
                var entityTableName = entry.Metadata.GetTableName();
                var auditEntry = new AuditLogModel
                {
                    user_id = UserId,
                    user_role = UserRole,
                    entity_name = entry.Entity.GetType().Name,
                    action_type = entry.State.ToString(),
                    created_at = DateTime.UtcNow
                };

                Logs.Add(auditEntry);
            }
        }

        public override int SaveChanges()
        {
            AuditChanges();
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            AuditChanges();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        protected override async void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<ApplicationUser>()
                .Property(u => u.user_id)
                .ValueGeneratedOnAddOrUpdate();

            /* The above code is configuring relationships between different entities in a database using Entity
            Framework Core in C#. It defines various one-to-many and one-to-one relationships between entities
            such as CustomerModel, CarModel, NoteModel, DocModel, and StateFormModel. */
            builder.Entity<CustomerModel>()
                .HasIndex(c => c.cphone_number)
                .IsUnique();

            builder.Entity<CarModel>()
                .HasOne(c => c.customer)
                .WithMany(m => m.cars)
                .HasForeignKey(m => m.cid)
                .OnDelete(DeleteBehavior.ClientNoAction);

            builder.Entity<CarModel>()
                .HasOne(c => c.driver)
                .WithMany(m => m.driver_cars)
                .HasForeignKey(m => m.driver_id)
                .OnDelete(DeleteBehavior.ClientNoAction);

            builder.Entity<CarModel>()
                .HasOne(c => c.created_by)
                .WithMany(m => m.cars)
                .HasForeignKey(m => m.created_by_id)
                .OnDelete(DeleteBehavior.ClientNoAction);

            builder.Entity<NoteModel>()
                .HasOne(c => c.vehicle)
                .WithMany(m => m.notes)
                .HasForeignKey(m => m.veh_id)
                .OnDelete(DeleteBehavior.ClientNoAction);

            builder.Entity<NoteModel>()
                .HasOne(c => c.state_form)
                .WithMany(m => m.notes)
                .HasForeignKey(m => m.sf_id)
                .OnDelete(DeleteBehavior.ClientNoAction);

            builder.Entity<DocModel>()
                .HasOne(c => c.note)
                .WithOne(m => m.doc)
                .HasForeignKey<NoteModel>(m => m.doc_id)
                .OnDelete(DeleteBehavior.ClientNoAction);

            builder.Entity<NoteModel>()
                .HasOne(c => c.created_by)
                .WithMany(m => m.notes)
                .HasForeignKey(m => m.created_by_id)
                .OnDelete(DeleteBehavior.ClientNoAction);

            builder.Entity<CustomerModel>()
                .HasOne(c => c.created_by)
                .WithMany(m => m.customers)
                .HasForeignKey(m => m.created_by_id)
                .OnDelete(DeleteBehavior.ClientNoAction);

            builder.Entity<PosCustomerModel>()
                .HasOne(c => c.created_by)
                .WithMany(m => m.pos_customers)
                .HasForeignKey(m => m.created_by_id)
                .OnDelete(DeleteBehavior.ClientNoAction);

            builder.Entity<DocModel>()
                .HasOne(c => c.car)
                .WithMany(m => m.docs)
                .HasForeignKey(m => m.veh_id)
                .OnDelete(DeleteBehavior.ClientNoAction);

            builder.Entity<StateFormModel>()
                .HasOne(c => c.doc)
                .WithOne(m => m.state_form)
                .HasForeignKey<DocModel>(d => d.sf_id)
                .OnDelete(DeleteBehavior.ClientNoAction);

            builder.Entity<ProductModel>()
                .HasMany(c => c.images)
                .WithOne(m => m.product)
                .HasForeignKey(d => d.product_id)
                .OnDelete(DeleteBehavior.ClientNoAction);

            builder.Entity<ProductModel>()
                .HasOne(c => c.created_by)
                .WithMany(m => m.products)
                .HasForeignKey(d => d.created_by_id)
                .OnDelete(DeleteBehavior.ClientNoAction);

            builder.Entity<ProductModel>()
                .HasOne(c => c.category)
                .WithMany(m => m.Proucts)
                .HasForeignKey(d => d.CategoryId)
                .OnDelete(DeleteBehavior.ClientNoAction);

            builder.Entity<InvoiceModel>()
                .HasOne(c => c.created_by)
                .WithMany(m => m.invoices)
                .HasForeignKey(d => d.created_by_id)
                .OnDelete(DeleteBehavior.ClientNoAction);

            builder.Entity<InvoiceModel>()
                .HasOne(c => c.customer)
                .WithMany(m => m.invoices)
                .HasForeignKey(d => d.customer_id)
                .OnDelete(DeleteBehavior.ClientNoAction);

            builder.Entity<InvoiceModel>()
                .HasOne(c => c.company)
                .WithMany(m => m.Invoices)
                .HasForeignKey(d => d.company_id)
                .OnDelete(DeleteBehavior.ClientNoAction);

            builder.Entity<ProductInvoice>()
                .HasOne(c => c.product)
                .WithMany(e => e.productInvoice)
                .HasForeignKey(e => e.product_id)
                .OnDelete(DeleteBehavior.ClientNoAction);

            builder.Entity<ProductInvoice>()
                .HasOne(c => c.invoice)
                .WithMany(e => e.productInvoice)
                .HasForeignKey(e => e.invoice_id)
                .OnDelete(DeleteBehavior.ClientNoAction);

            // PP-55: multitenant application architecture.
            // Feature: Create a multitenant architecture for multi role user like admin, manager, employee and driver
            // Fix: Create a company model and assign company with a sub admin (Manager) and employees

            builder.Entity<CompanyManager>()
                .HasKey(pc => new { pc.manager_id, pc.company_id });
            
            builder.Entity<CompanyEmployees>()
                .HasKey(pc => new { pc.employee_id, pc.company_id });

            builder.Entity<CompanyDrivers>()
                .HasKey(pc => new { pc.driver_id, pc.company_id });

            builder.Entity<ProductInvoice>()
                .HasKey(pc => new { pc.invoice_id, pc.product_id });

            builder.Entity<CompanyManager>()
                .HasOne(c => c.manager)
                .WithMany(e => e.CompanyManagers)
                .HasForeignKey(e => e.manager_id)
                .OnDelete(DeleteBehavior.ClientNoAction);

            builder.Entity<CompanyManager>()
                .HasOne(c => c.company)
                .WithMany(e => e.CompanyManagers)
                .HasForeignKey(e => e.company_id)
                .OnDelete(DeleteBehavior.ClientNoAction);

            builder.Entity<CompanyEmployees>()
                .HasOne(c => c.employee)
                .WithMany(e => e.CompanyEmployees)
                .HasForeignKey(e => e.employee_id)
                .OnDelete(DeleteBehavior.ClientNoAction);

            builder.Entity<CompanyEmployees>()
                .HasOne(c => c.company)
                .WithMany(e => e.CompanyEmployees)
                .HasForeignKey(e => e.company_id)
                .OnDelete(DeleteBehavior.ClientNoAction);

            builder.Entity<CompanyDrivers>()
                .HasOne(c => c.driver)
                .WithMany(e => e.CompanyDrivers)
                .HasForeignKey(e => e.driver_id)
                .OnDelete(DeleteBehavior.ClientNoAction);

            builder.Entity<CompanyDrivers>()
                .HasOne(c => c.company)
                .WithMany(e => e.CompanyDrivers)
                .HasForeignKey(e => e.company_id)
                .OnDelete(DeleteBehavior.ClientNoAction);

            builder.Entity<CompanyModel>()
                .HasMany(c => c.Cars)
                .WithOne(e => e.Company)
                .HasForeignKey(e => e.CompanyId)
                .OnDelete(DeleteBehavior.ClientNoAction);

            builder.Entity<CompanyModel>()
                .HasMany(c => c.Customers)
                .WithOne(e => e.Company)
                .HasForeignKey(e => e.CompanyId)
                .OnDelete(DeleteBehavior.ClientNoAction);

            builder.Entity<CompanyModel>()
                .HasMany(c => c.StateForms)
                .WithOne(e => e.Company)
                .HasForeignKey(e => e.CompanyId)
                .OnDelete(DeleteBehavior.ClientNoAction);

            builder.Entity<CompanyModel>()
                .HasMany(c => c.Products)
                .WithOne(e => e.company)
                .HasForeignKey(e => e.company_id)
                .OnDelete(DeleteBehavior.ClientNoAction);

            builder.Entity<CompanyModel>()
                .HasMany(c => c.pos_customers)
                .WithOne(e => e.Company)
                .HasForeignKey(e => e.CompanyId)
                .OnDelete(DeleteBehavior.ClientNoAction);

            builder.Entity<CompanyModel>()
                .HasMany(c => c.Payments)
                .WithOne(e => e.company)
                .HasForeignKey(e => e.company_id)
                .OnDelete(DeleteBehavior.ClientNoAction);

            builder.Entity<CompanyModel>()
                .HasOne(c => c.Localization)
                .WithOne(e => e.company)
                .HasForeignKey<LocalizationModel>(e => e.company_id)
                .OnDelete(DeleteBehavior.ClientNoAction);

            builder.Entity<CompanyModel>()
                .HasMany(c => c.expenses)
                .WithOne(e => e.company)
                .HasForeignKey(e => e.company_id)
                .OnDelete(DeleteBehavior.ClientNoAction);

            builder.Entity<AuditLogModel>()
                .HasOne(c => c.user)
                .WithMany(e => e.logs)
                .HasForeignKey(e => e.user_id)
                .OnDelete(DeleteBehavior.ClientNoAction);

            builder.Entity<PaymentModel>()
                .HasOne(c => c.customer)
                .WithMany(e => e.payments)
                .HasForeignKey(e => e.customerId)
                .OnDelete(DeleteBehavior.ClientNoAction);

            builder.Entity<ExpenseModel>()
                .HasOne(c => c.customer)
                .WithMany(c => c.expenses)
                .HasForeignKey(c => c.cus_id)
                .OnDelete(DeleteBehavior.ClientNoAction);

            builder.Entity<ExpenseModel>()
                .HasOne(c => c.created_by)
                .WithMany(c => c.expenses)
                .HasForeignKey(c => c.created_by_id)
                .OnDelete(DeleteBehavior.ClientNoAction);

            builder.Entity<CategoryModel>().HasData(
                new CategoryModel { Id = 1, Name = "Battery" },
                new CategoryModel { Id = 2, Name = "Body" },
                new CategoryModel { Id = 3, Name = "Brakes" },
                new CategoryModel { Id = 4, Name = "Electrical" },
                new CategoryModel { Id = 5, Name = "Engine" },
                new CategoryModel { Id = 6, Name = "Engine Parts" },
                new CategoryModel { Id = 7, Name = "Glass" },
                new CategoryModel { Id = 8, Name = "Handling Fees" },
                new CategoryModel { Id = 9, Name = "Interior" },
                new CategoryModel { Id = 10, Name = "Mechanical" },
                new CategoryModel { Id = 11, Name = "Merchandise" },
                new CategoryModel { Id = 12, Name = "Specials" },
                new CategoryModel { Id = 13, Name = "Tires" },
                new CategoryModel { Id = 14, Name = "Transmission" },
                new CategoryModel { Id = 15, Name = "Transmission Parts" }
            );

            /* The code below is using Entity Framework Core to seed data into database tables named "MasterProductionTable", "MasterVehicleTable" and "MasterYearTable", It is adding multiple rows of data with different columns. Each row represents a record in the database table with the specified properties. This seeding process is typically done to populate the database with initial data when the application is first run or when the database is created. */
            builder.Entity<MasterProductionTable>().HasData(
                new MasterProductionTable { Id = 1, row = "1", section = "SP" },
                new MasterProductionTable { Id = 2, row = "2", section = "SP" },
                new MasterProductionTable { Id = 3, row = "3", section = "SP" },
                new MasterProductionTable { Id = 4, row = "4", section = "SP" },
                new MasterProductionTable { Id = 5, row = "5", section = "SP" },
                new MasterProductionTable { Id = 6, row = "6", section = "SP" },
                new MasterProductionTable { Id = 7, row = "7", section = "SP" },
                new MasterProductionTable { Id = 8, row = "8", section = "T&V" },
                new MasterProductionTable { Id = 9, row = "9", section = "T&V" },
                new MasterProductionTable { Id = 10, row = "10", section = "T&V" },
                new MasterProductionTable { Id = 11, row = "11", section = "T&V" },
                new MasterProductionTable { Id = 12, row = "12", section = "T&V" },
                new MasterProductionTable { Id = 13, row = "13", section = "T&V" },
                new MasterProductionTable { Id = 14, row = "14", section = "T&V" },
                new MasterProductionTable { Id = 15, row = "15", section = "T&V" },
                new MasterProductionTable { Id = 16, row = "16", section = "T&V" },
                new MasterProductionTable { Id = 17, row = "17", section = "T&V" },
                new MasterProductionTable { Id = 18, row = "18", section = "IMP" },
                new MasterProductionTable { Id = 19, row = "19", section = "IMP" },
                new MasterProductionTable { Id = 20, row = "20", section = "IMP" },
                new MasterProductionTable { Id = 21, row = "21", section = "IMP" },
                new MasterProductionTable { Id = 22, row = "22", section = "IMP" },
                new MasterProductionTable { Id = 23, row = "23", section = "IMP" },
                new MasterProductionTable { Id = 24, row = "24", section = "IMP" },
                new MasterProductionTable { Id = 25, row = "25", section = "IMP" },
                new MasterProductionTable { Id = 26, row = "26", section = "CH" },
                new MasterProductionTable { Id = 27, row = "27", section = "CH" },
                new MasterProductionTable { Id = 28, row = "28", section = "T&V" },
                new MasterProductionTable { Id = 29, row = "29", section = "T&V" },
                new MasterProductionTable { Id = 30, row = "30", section = "T&V" },
                new MasterProductionTable { Id = 31, row = "31", section = "T&V" },
                new MasterProductionTable { Id = 32, row = "32", section = "T&V" },
                new MasterProductionTable { Id = 33, row = "33", section = "GM" },
                new MasterProductionTable { Id = 34, row = "34", section = "GM" },
                new MasterProductionTable { Id = 35, row = "35", section = "GM" },
                new MasterProductionTable { Id = 36, row = "36", section = "GM" },
                new MasterProductionTable { Id = 37, row = "37", section = "GM" },
                new MasterProductionTable { Id = 38, row = "38", section = "GM" },
                new MasterProductionTable { Id = 39, row = "39", section = "GM" },
                new MasterProductionTable { Id = 40, row = "40", section = "GM" },
                new MasterProductionTable { Id = 41, row = "41", section = "GM" },
                new MasterProductionTable { Id = 42, row = "42", section = "GM" },
                new MasterProductionTable { Id = 43, row = "43", section = "FORD" },
                new MasterProductionTable { Id = 44, row = "44", section = "FORD" },
                new MasterProductionTable { Id = 45, row = "45", section = "FORD" },
                new MasterProductionTable { Id = 46, row = "46", section = "FORD" },
                new MasterProductionTable { Id = 47, row = "47", section = "FORD" },
                new MasterProductionTable { Id = 48, row = "48", section = "FORD" },
                new MasterProductionTable { Id = 49, row = "8B", section = "T&V" },
                new MasterProductionTable { Id = 50, row = "9B", section = "T&V" },
                new MasterProductionTable { Id = 51, row = "10B", section = "T&V" },
                new MasterProductionTable { Id = 52, row = "11B", section = "T&V" },
                new MasterProductionTable { Id = 53, row = "12B", section = "T&V" },
                new MasterProductionTable { Id = 54, row = "13B", section = "T&V" },
                new MasterProductionTable { Id = 55, row = "14B", section = "T&V" },
                new MasterProductionTable { Id = 56, row = "15B", section = "T&V" },
                new MasterProductionTable { Id = 57, row = "16B", section = "T&V" },
                new MasterProductionTable { Id = 58, row = "17B", section = "T&V" },
                new MasterProductionTable { Id = 59, row = "18B", section = "IMP" },
                new MasterProductionTable { Id = 60, row = "19B", section = "IMP" },
                new MasterProductionTable { Id = 61, row = "20B", section = "IMP" },
                new MasterProductionTable { Id = 62, row = "21B", section = "IMP" },
                new MasterProductionTable { Id = 63, row = "22B", section = "IMP" },
                new MasterProductionTable { Id = 64, row = "23B", section = "IMP" },
                new MasterProductionTable { Id = 65, row = "24B", section = "IMP" },
                new MasterProductionTable { Id = 67, row = "25B", section = "IMP" },
                new MasterProductionTable { Id = 68, row = "26B", section = "CH" },
                new MasterProductionTable { Id = 69, row = "27B", section = "CH" },
                new MasterProductionTable { Id = 70, row = "28B", section = "T&V" },
                new MasterProductionTable { Id = 71, row = "29B", section = "T&V" },
                new MasterProductionTable { Id = 72, row = "30B", section = "T&V" },
                new MasterProductionTable { Id = 73, row = "31B", section = "T&V" },
                new MasterProductionTable { Id = 74, row = "32B", section = "T&V" },
                new MasterProductionTable { Id = 75, row = "33B", section = "GM" },
                new MasterProductionTable { Id = 76, row = "34B", section = "GM" },
                new MasterProductionTable { Id = 77, row = "35B", section = "GM" },
                new MasterProductionTable { Id = 78, row = "36B", section = "GM" },
                new MasterProductionTable { Id = 79, row = "37B", section = "GM" },
                new MasterProductionTable { Id = 80, row = "38B", section = "GM" },
                new MasterProductionTable { Id = 81, row = "39B", section = "GM" },
                new MasterProductionTable { Id = 82, row = "40B", section = "GM" },
                new MasterProductionTable { Id = 83, row = "41B", section = "GM" },
                new MasterProductionTable { Id = 84, row = "42B", section = "GM" },
                new MasterProductionTable { Id = 85, row = "43B", section = "FORD" },
                new MasterProductionTable { Id = 86, row = "44B", section = "FORD" },
                new MasterProductionTable { Id = 87, row = "45B", section = "FORD" },
                new MasterProductionTable { Id = 88, row = "46B", section = "FORD" },
                new MasterProductionTable { Id = 89, row = "47B", section = "FORD" },
                new MasterProductionTable { Id = 90, row = "48B", section = "FORD" }
            );

            builder.Entity<MasterVehicleTable>().HasData(
                new MasterVehicleTable { Id = 1, model = "Other", make = "Other" },
                new MasterVehicleTable { Id = 2, model = "CL", make = "ACURA" },
                new MasterVehicleTable { Id = 3, model = "ILX", make = "ACURA" },
                new MasterVehicleTable { Id = 4, model = "ILX", make = "ACURA" },
                new MasterVehicleTable { Id = 5, model = "INTEGR", make = "ACURA" },
                new MasterVehicleTable { Id = 6, model = "MDX", make = "ACURA" },
                new MasterVehicleTable { Id = 7, model = "NSX", make = "ACURA" },
                new MasterVehicleTable { Id = 8, model = "RDX", make = "ACURA" },
                new MasterVehicleTable { Id = 9, model = "RLX", make = "ACURA" },
                new MasterVehicleTable { Id = 10, model = "RLX SPORTS", make = "ACURA" },
                new MasterVehicleTable { Id = 11, model = "RSX", make = "ACURA" },
                new MasterVehicleTable { Id = 12, model = "TL", make = "ACURA" },
                new MasterVehicleTable { Id = 13, model = "TLX", make = "ACURA" },
                new MasterVehicleTable { Id = 14, model = "TSX", make = "ACURA" },
                new MasterVehicleTable { Id = 15, model = "ZDX", make = "ACURA" },
                new MasterVehicleTable { Id = 16, model = "4C", make = "ALFA ROMEO" },
                new MasterVehicleTable { Id = 17, model = "8C", make = "ALFA ROMEO" },
                new MasterVehicleTable { Id = 18, model = "HUMMER H1", make = "AM GENERAL" },
                new MasterVehicleTable { Id = 19, model = "HUMMER H2", make = "AM GENERAL" },
                new MasterVehicleTable { Id = 20, model = "HUMMER H3", make = "AM GENERAL" },
                new MasterVehicleTable { Id = 21, model = "A3", make = "AUDI" },
                new MasterVehicleTable { Id = 22, model = "A4", make = "AUDI" },
                new MasterVehicleTable { Id = 23, model = "A5", make = "AUDI" },
                new MasterVehicleTable { Id = 24, model = "A6", make = "AUDI" },
                new MasterVehicleTable { Id = 25, model = "A7", make = "AUDI" },
                new MasterVehicleTable { Id = 26, model = "A8", make = "AUDI" },
                new MasterVehicleTable { Id = 27, model = "ALLROA", make = "AUDI" },
                new MasterVehicleTable { Id = 28, model = "Q3", make = "AUDI" },
                new MasterVehicleTable { Id = 29, model = "Q5", make = "AUDI" },
                new MasterVehicleTable { Id = 30, model = "Q7", make = "AUDI" },
                new MasterVehicleTable { Id = 31, model = "R8", make = "AUDI" },
                new MasterVehicleTable { Id = 32, model = "RS 4", make = "AUDI" },
                new MasterVehicleTable { Id = 33, model = "RS 5", make = "AUDI" },
                new MasterVehicleTable { Id = 34, model = "RS 7", make = "AUDI" },
                new MasterVehicleTable { Id = 35, model = "RS 6", make = "AUDI" },
                new MasterVehicleTable { Id = 36, model = "S3", make = "AUDI" },
                new MasterVehicleTable { Id = 37, model = "S4", make = "AUDI" },
                new MasterVehicleTable { Id = 38, model = "S5", make = "AUDI" },
                new MasterVehicleTable { Id = 39, model = "S6", make = "AUDI" },
                new MasterVehicleTable { Id = 40, model = "S7", make = "AUDI" },
                new MasterVehicleTable { Id = 41, model = "S8", make = "AUDI" },
                new MasterVehicleTable { Id = 42, model = "SQ", make = "AUDI" },
                new MasterVehicleTable { Id = 43, model = "TT", make = "AUDI" },
                new MasterVehicleTable { Id = 44, model = "TT RS", make = "AUDI" },
                new MasterVehicleTable { Id = 45, model = "1 SERIES", make = "BMW" },
                new MasterVehicleTable { Id = 46, model = "128", make = "BMW" },
                new MasterVehicleTable { Id = 47, model = "135", make = "BMW" },
                new MasterVehicleTable { Id = 48, model = "228", make = "BMW" },
                new MasterVehicleTable { Id = 49, model = "3 SERIES", make = "BMW" },
                new MasterVehicleTable { Id = 50, model = "320", make = "BMW" },
                new MasterVehicleTable { Id = 51, model = "3232", make = "BMW" },
                new MasterVehicleTable { Id = 52, model = "325", make = "BMW" },
                new MasterVehicleTable { Id = 53, model = "328", make = "BMW" },
                new MasterVehicleTable { Id = 54, model = "328 GT", make = "BMW" },
                new MasterVehicleTable { Id = 55, model = "328D", make = "BMW" },
                new MasterVehicleTable { Id = 56, model = "330", make = "BMW" },
                new MasterVehicleTable { Id = 57, model = "335", make = "BMW" },
                new MasterVehicleTable { Id = 58, model = "335 GT", make = "BMW" },
                new MasterVehicleTable { Id = 59, model = "M3", make = "BMW" },
                new MasterVehicleTable { Id = 60, model = "4 SERIES", make = "BMW" },
                new MasterVehicleTable { Id = 61, model = "428", make = "BMW" },
                new MasterVehicleTable { Id = 62, model = "428 GT", make = "BMW" },
                new MasterVehicleTable { Id = 63, model = "435", make = "BMW" },
                new MasterVehicleTable { Id = 64, model = "435 GRAN COUP", make = "BMW" },
                new MasterVehicleTable { Id = 65, model = "M4", make = "BMW" },
                new MasterVehicleTable { Id = 66, model = "5 SERIES", make = "BMW" },
                new MasterVehicleTable { Id = 67, model = "525", make = "BMW" },
                new MasterVehicleTable { Id = 68, model = "528", make = "BMW" },
                new MasterVehicleTable { Id = 69, model = "530", make = "BMW" },
                new MasterVehicleTable { Id = 70, model = "535", make = "BMW" },
                new MasterVehicleTable { Id = 71, model = "535 GT", make = "BMW" },
                new MasterVehicleTable { Id = 72, model = "535D", make = "BMW" },
                new MasterVehicleTable { Id = 73, model = "540", make = "BMW" },
                new MasterVehicleTable { Id = 74, model = "545", make = "BMW" },
                new MasterVehicleTable { Id = 75, model = "550", make = "BMW" },
                new MasterVehicleTable { Id = 76, model = "550", make = "BMW" },
                new MasterVehicleTable { Id = 77, model = "FT", make = "BMW" },
                new MasterVehicleTable { Id = 78, model = "M5", make = "BMW" },
                new MasterVehicleTable { Id = 79, model = "6 SERIES", make = "BMW" },
                new MasterVehicleTable { Id = 80, model = "640", make = "BMW" },
                new MasterVehicleTable { Id = 81, model = "640 G COUPE", make = "BMW" },
                new MasterVehicleTable { Id = 82, model = "645", make = "BMW" },
                new MasterVehicleTable { Id = 83, model = "650", make = "BMW" },
                new MasterVehicleTable { Id = 84, model = "M6", make = "BMW" },
                new MasterVehicleTable { Id = 85, model = "7 SERIES", make = "BMW" },
                new MasterVehicleTable { Id = 86, model = "740", make = "BMW" },
                new MasterVehicleTable { Id = 87, model = "745", make = "BMW" },
                new MasterVehicleTable { Id = 88, model = "750", make = "BMW" },
                new MasterVehicleTable { Id = 89, model = "760", make = "BMW" },
                new MasterVehicleTable { Id = 90, model = "HYBRID 750", make = "BMW" },
                new MasterVehicleTable { Id = 91, model = "ALPINA B7", make = "BMW" },
                new MasterVehicleTable { Id = 92, model = "HYBRID 3", make = "BMW" },
                new MasterVehicleTable { Id = 93, model = "HYBRID 5", make = "BMW" },
                new MasterVehicleTable { Id = 94, model = "HYBRID 750", make = "BMW" },
                new MasterVehicleTable { Id = 95, model = "PASSAT", make = "VOLKSWAGEN" },
                new MasterVehicleTable { Id = 96, model = "PHAETON", make = "VOLKSWAGEN" },
                new MasterVehicleTable { Id = 97, model = "R32", make = "VOLKSWAGEN" },
                new MasterVehicleTable { Id = 98, model = "RABBIT", make = "VOLKSWAGEN" },
                new MasterVehicleTable { Id = 99, model = "TIGUAN", make = "VOLKSWAGEN" },
                new MasterVehicleTable { Id = 100, model = "TOUAREG", make = "VOLKSWAGEN" },
                new MasterVehicleTable { Id = 101, model = "TOUAREG 2", make = "VOLKSWAGEN" },
                new MasterVehicleTable { Id = 102, model = "TOUAREG HYBRID", make = "VOLKSWAGEN" },
                new MasterVehicleTable { Id = 103, model = "C30", make = "VOLVO" },
                new MasterVehicleTable { Id = 104, model = "C70", make = "VOLVO" },
                new MasterVehicleTable { Id = 105, model = "S40", make = "VOLVO" },
                new MasterVehicleTable { Id = 106, model = "S60", make = "VOLVO" },
                new MasterVehicleTable { Id = 107, model = "S70", make = "VOLVO" },
                new MasterVehicleTable { Id = 108, model = "S80", make = "VOLVO" },
                new MasterVehicleTable { Id = 109, model = "V40", make = "VOLVO" },
                new MasterVehicleTable { Id = 110, model = "V50", make = "VOLVO" },
                new MasterVehicleTable { Id = 111, model = "V60", make = "VOLVO" },
                new MasterVehicleTable { Id = 112, model = "V60 CROSS COUNTRY", make = "VOLVO" },
                new MasterVehicleTable { Id = 113, model = "V70", make = "VOLVO" },
                new MasterVehicleTable { Id = 114, model = "XC60", make = "VOLVO" },
                new MasterVehicleTable { Id = 115, model = "XC70", make = "VOLVO" },
                new MasterVehicleTable { Id = 116, model = "XC90", make = "VOLVO" },
                new MasterVehicleTable { Id = 117, model = "Bulk", make = "Bulk" },
                new MasterVehicleTable { Id = 118, model = "Unlisted", make = "Unlisted" },
                new MasterVehicleTable { Id = 119, model = "ROUTAN", make = "VOLKSWAGEN" },
                new MasterVehicleTable { Id = 120, model = "TALON", make = "EAGLE" },
                new MasterVehicleTable { Id = 121, model = "88", make = "OLDSMOBILE" },
                new MasterVehicleTable { Id = 122, model = "TEMPO", make = "FORD" },
                new MasterVehicleTable { Id = 123, model = "LEBARON", make = "CHRYSLER" },
                new MasterVehicleTable { Id = 124, model = "960", make = "VOLVO" },
                new MasterVehicleTable { Id = 125, model = "AEROSTAR", make = "FORD" },
                new MasterVehicleTable { Id = 126, model = "XE", make = "NISSAN" },
                new MasterVehicleTable { Id = 127, model = "944", make = "PORSCHE" },
                new MasterVehicleTable { Id = 128, model = "DYNASTY", make = "DODGE" },
                new MasterVehicleTable { Id = 129, model = "TRAX", make = "CADILLAC" },
                new MasterVehicleTable { Id = 130, model = "SVX", make = "SUBARU" },
                new MasterVehicleTable { Id = 131, model = "test", make = "SUBARU" },
                new MasterVehicleTable { Id = 132, model = "HYBRID 740", make = "BMW" },
                new MasterVehicleTable { Id = 133, model = "ALPINA B6", make = "BMW" },
                new MasterVehicleTable { Id = 134, model = "I3", make = "BMW" },
                new MasterVehicleTable { Id = 135, model = "M SERIES", make = "BMW" },
                new MasterVehicleTable { Id = 136, model = "M", make = "BMW" },
                new MasterVehicleTable { Id = 137, model = "M235", make = "BMW" },
                new MasterVehicleTable { Id = 138, model = "M3", make = "BMW" },
                new MasterVehicleTable { Id = 139, model = "M4", make = "BMW" },
                new MasterVehicleTable { Id = 140, model = "M5", make = "BMW" },
                new MasterVehicleTable { Id = 141, model = "M6", make = "BMW" },
                new MasterVehicleTable { Id = 142, model = "M6 COUPE", make = "BMW" },
                new MasterVehicleTable { Id = 143, model = "X5", make = "BMW" },
                new MasterVehicleTable { Id = 144, model = "X6", make = "BMW" },
                new MasterVehicleTable { Id = 145, model = "Z4", make = "BMW" },
                new MasterVehicleTable { Id = 146, model = "X", make = "BMW" },
                new MasterVehicleTable { Id = 147, model = "HYBRID X6", make = "BMW" },
                new MasterVehicleTable { Id = 148, model = "X1", make = "BMW" },
                new MasterVehicleTable { Id = 149, model = "X3", make = "BMW" },
                new MasterVehicleTable { Id = 150, model = "X5", make = "BMW" },
                new MasterVehicleTable { Id = 151, model = "X6", make = "BMW" },
                new MasterVehicleTable { Id = 152, model = "X6 M", make = "BMW" },
                new MasterVehicleTable { Id = 153, model = "Z SERIES", make = "BMW" },
                new MasterVehicleTable { Id = 154, model = "Z3", make = "BMW" },
                new MasterVehicleTable { Id = 155, model = "Z4", make = "BMW" },
                new MasterVehicleTable { Id = 156, model = "Z5", make = "BMW" },
                new MasterVehicleTable { Id = 157, model = "Century", make = "BUICK" },
                new MasterVehicleTable { Id = 158, model = "Enclave", make = "BUICK" },
                new MasterVehicleTable { Id = 159, model = "Encore", make = "BUICK" },
                new MasterVehicleTable { Id = 160, model = "Lacrosse", make = "BUICK" },
                new MasterVehicleTable { Id = 161, model = "LeSabre", make = "BUICK" },
                new MasterVehicleTable { Id = 162, model = "Lucerne", make = "BUICK" },
                new MasterVehicleTable { Id = 163, model = "Park Avenue", make = "BUICK" },
                new MasterVehicleTable { Id = 164, model = "Rainier", make = "BUICK" },
                new MasterVehicleTable { Id = 165, model = "Regal", make = "BUICK" },
                new MasterVehicleTable { Id = 166, model = "Rendezvous", make = "BUICK" },
                new MasterVehicleTable { Id = 167, model = "Terraza", make = "BUICK" },
                new MasterVehicleTable { Id = 168, model = "Verano", make = "BUICK" },
                new MasterVehicleTable { Id = 169, model = "ATS", make = "CADILLAC" },
                new MasterVehicleTable { Id = 170, model = "ATS-V", make = "CADILLAC" },
                new MasterVehicleTable { Id = 171, model = "CATERA", make = "CADILLAC" },
                new MasterVehicleTable { Id = 172, model = "CTS", make = "CADILLAC" },
                new MasterVehicleTable { Id = 173, model = "DEVILLE", make = "CADILLAC" },
                new MasterVehicleTable { Id = 174, model = "DTS", make = "CADILLAC" },
                new MasterVehicleTable { Id = 175, model = "ELDORADO", make = "CADILLAC" },
                new MasterVehicleTable { Id = 176, model = "ELR", make = "CADILLAC" },
                new MasterVehicleTable { Id = 177, model = "ESCALADE", make = "CADILLAC" },
                new MasterVehicleTable { Id = 178, model = "ESCALADE ESV", make = "CADILLAC" },
                new MasterVehicleTable { Id = 179, model = "ESCALADE EXT", make = "CADILLAC" },
                new MasterVehicleTable { Id = 180, model = "ESCALADE HYBRID", make = "CADILLAC" },
                new MasterVehicleTable { Id = 181, model = "SEVILLE", make = "CADILLAC" },
                new MasterVehicleTable { Id = 182, model = "SRX", make = "CADILLAC" },
                new MasterVehicleTable { Id = 183, model = "STS", make = "CADILLAC" },
                new MasterVehicleTable { Id = 184, model = "XLR", make = "CADILLAC" },
                new MasterVehicleTable { Id = 185, model = "XTS", make = "CADILLAC" },
                new MasterVehicleTable { Id = 186, model = "2500", make = "CHEVROLET" },
                new MasterVehicleTable { Id = 187, model = "3500", make = "CHEVROLET" },
                new MasterVehicleTable { Id = 188, model = "ASTRO", make = "CHEVROLET" },
                new MasterVehicleTable { Id = 189, model = "AVALANCE", make = "CHEVROLET" },
                new MasterVehicleTable { Id = 190, model = "AVEO", make = "CHEVROLET" },
                new MasterVehicleTable { Id = 191, model = "BLAZER", make = "CHEVROLET" },
                new MasterVehicleTable { Id = 192, model = "CAMARO", make = "CHEVROLET" },
                new MasterVehicleTable { Id = 193, model = "CAPTIVA SPORT", make = "CHEVROLET" },
                new MasterVehicleTable { Id = 194, model = "CAVALIER", make = "CHEVROLET" },
                new MasterVehicleTable { Id = 195, model = "CITY ESPRESS", make = "CHEVROLET" },
                new MasterVehicleTable { Id = 196, model = "CLASSIC", make = "CHEVROLET" },
                new MasterVehicleTable { Id = 197, model = "COBALT", make = "CHEVROLET" },
                new MasterVehicleTable { Id = 198, model = "COLORADO", make = "CHEVROLET" },
                new MasterVehicleTable { Id = 199, model = "CORVETTE", make = "CHEVROLET" },
                new MasterVehicleTable { Id = 200, model = "CRUZE", make = "CHEVROLET" },
                new MasterVehicleTable { Id = 201, model = "EQUINOX", make = "CHEVROLET" },
                new MasterVehicleTable { Id = 202, model = "EXPRESS VANS", make = "CHEVROLET" },
                new MasterVehicleTable { Id = 203, model = "EXPRESS 1500", make = "CHEVROLET" },
                new MasterVehicleTable { Id = 204, model = "EXPRESS 2500", make = "CHEVROLET" },
                new MasterVehicleTable { Id = 205, model = "EXPRESS 3500", make = "CHEVROLET" },
                new MasterVehicleTable { Id = 206, model = "HHR", make = "CHEVROLET" },
                new MasterVehicleTable { Id = 207, model = "IMPALA", make = "CHEVROLET" },
                new MasterVehicleTable { Id = 208, model = "IMPALA LIMITED", make = "CHEVROLET" },
                new MasterVehicleTable { Id = 209, model = "LUMINA", make = "CHEVROLET" },
                new MasterVehicleTable { Id = 210, model = "MALIBU", make = "CHEVROLET" },
                new MasterVehicleTable { Id = 211, model = "MALIBU CLASSIC", make = "CHEVROLET" },
                new MasterVehicleTable { Id = 212, model = "MALIBU HYBRID", make = "CHEVROLET" },
                new MasterVehicleTable { Id = 213, model = "MALIBU MAXX", make = "CHEVROLET" },
                new MasterVehicleTable { Id = 214, model = "METRO", make = "CHEVROLET" },
                new MasterVehicleTable { Id = 215, model = "MONTE CARLO", make = "CHEVROLET" },
                new MasterVehicleTable { Id = 216, model = "PICKUP", make = "CHEVROLET" },
                new MasterVehicleTable { Id = 217, model = "PRIZM", make = "CHEVROLET" },
                new MasterVehicleTable { Id = 218, model = "S -10", make = "CHEVROLET" },
                new MasterVehicleTable { Id = 219, model = "SILVERADO 1500", make = "CHEVROLET" },
                new MasterVehicleTable { Id = 220, model = "SILVERADO 1500", make = "CHEVROLET" },
                new MasterVehicleTable { Id = 221, model = "SILVERADO 2500", make = "CHEVROLET" },
                new MasterVehicleTable { Id = 222, model = "SILVERADO 3500", make = "CHEVROLET" },
                new MasterVehicleTable { Id = 223, model = "SONIC", make = "CHEVROLET" },
                new MasterVehicleTable { Id = 224, model = "SPARK", make = "CHEVROLET" },
                new MasterVehicleTable { Id = 225, model = "SPARK EV", make = "CHEVROLET" },
                new MasterVehicleTable { Id = 226, model = "SS", make = "CHEVROLET" },
                new MasterVehicleTable { Id = 227, model = "SSR", make = "CHEVROLET" },
                new MasterVehicleTable { Id = 228, model = "SUBURBAN", make = "CHEVROLET" },
                new MasterVehicleTable { Id = 229, model = "TAHOE", make = "CHEVROLET" },
                new MasterVehicleTable { Id = 230, model = "TAHOE HYBIRD", make = "CHEVROLET" },
                new MasterVehicleTable { Id = 231, model = "TRACKER", make = "CHEVROLET" },
                new MasterVehicleTable { Id = 232, model = "TRAILBLAZER", make = "CHEVROLET" },
                new MasterVehicleTable { Id = 233, model = "TRAILBLAZER EXT", make = "CHEVROLET" },
                new MasterVehicleTable { Id = 234, model = "TRAVERSE", make = "CHEVROLET" },
                new MasterVehicleTable { Id = 235, model = "UPLANDER", make = "CHEVROLET" },
                new MasterVehicleTable { Id = 236, model = "VENTURE", make = "CHEVROLET" },
                new MasterVehicleTable { Id = 237, model = "VOLT", make = "CHEVROLET" },
                new MasterVehicleTable { Id = 238, model = "200", make = "CHRYSLER" },
                new MasterVehicleTable { Id = 239, model = "300", make = "CHRYSLER" },
                new MasterVehicleTable { Id = 240, model = "300C", make = "CHRYSLER" },
                new MasterVehicleTable { Id = 241, model = "300M", make = "CHRYSLER" },
                new MasterVehicleTable { Id = 242, model = "ASPEN", make = "CHRYSLER" },
                new MasterVehicleTable { Id = 243, model = "ASPEN HYBRID", make = "CHRYSLER" },
                new MasterVehicleTable { Id = 244, model = "CIRRUS", make = "CHRYSLER" },
                new MasterVehicleTable { Id = 245, model = "CONCORDE", make = "CHRYSLER" },
                new MasterVehicleTable { Id = 246, model = "CROSSFIRE", make = "CHRYSLER" },
                new MasterVehicleTable { Id = 247, model = "GRAND VOYAGER", make = "CHRYSLER" },
                new MasterVehicleTable { Id = 248, model = "LHS", make = "CHRYSLER" },
                new MasterVehicleTable { Id = 249, model = "PACIFICA", make = "CHRYSLER" },
                new MasterVehicleTable { Id = 250, model = "PROWLER", make = "CHRYSLER" },
                new MasterVehicleTable { Id = 251, model = "PT CRUISER", make = "CHRYSLER" },
                new MasterVehicleTable { Id = 252, model = "SEBRING", make = "CHRYSLER" },
                new MasterVehicleTable { Id = 253, model = "TOWN & COUNTRY", make = "CHRYSLER" },
                new MasterVehicleTable { Id = 254, model = "VOYAGER", make = "CHRYSLER" },
                new MasterVehicleTable { Id = 255, model = "LANOS", make = "DAEWOO" },
                new MasterVehicleTable { Id = 256, model = "LEGANZA", make = "DAEWOO" },
                new MasterVehicleTable { Id = 257, model = "NUBIRA", make = "DAEWOO" },
                new MasterVehicleTable { Id = 258, model = "AVENGER", make = "DODGE" },
                new MasterVehicleTable { Id = 259, model = "CALIBER", make = "DODGE" },
                new MasterVehicleTable { Id = 260, model = "CARAVAN", make = "DODGE" },
                new MasterVehicleTable { Id = 261, model = "CHALLENGER", make = "DODGE" },
                new MasterVehicleTable { Id = 262, model = "CHARGER", make = "DODGE" },
                new MasterVehicleTable { Id = 263, model = "DAKOTA", make = "DODGE" },
                new MasterVehicleTable { Id = 264, model = "DART", make = "DODGE" },
                new MasterVehicleTable { Id = 265, model = "DURANGO", make = "DODGE" },
                new MasterVehicleTable { Id = 266, model = "DURANGO HYB", make = "DODGE" },
                new MasterVehicleTable { Id = 267, model = "GRAND CARAVAN", make = "DODGE" },
                new MasterVehicleTable { Id = 268, model = "INTERPID", make = "DODGE" },
                new MasterVehicleTable { Id = 269, model = "JOURNEY", make = "DODGE" },
                new MasterVehicleTable { Id = 270, model = "MAGNUM", make = "DODGE" },
                new MasterVehicleTable { Id = 271, model = "NEON", make = "DODGE" },
                new MasterVehicleTable { Id = 272, model = "RAM ALL", make = "DODGE" },
                new MasterVehicleTable { Id = 273, model = "RAM PICKUP", make = "DODGE" },
                new MasterVehicleTable { Id = 274, model = "RAM 1500", make = "DODGE" },
                new MasterVehicleTable { Id = 275, model = "RAM 2500", make = "DODGE" },
                new MasterVehicleTable { Id = 276, model = "RAM 3500", make = "DODGE" },
                new MasterVehicleTable { Id = 277, model = "RAM VAN", make = "DODGE" },
                new MasterVehicleTable { Id = 278, model = "RAM WAGON", make = "DODGE" },
                new MasterVehicleTable { Id = 279, model = "SPRINTER", make = "DODGE" },
                new MasterVehicleTable { Id = 280, model = "SRT VIPER", make = "DODGE" },
                new MasterVehicleTable { Id = 281, model = "STRATUS", make = "DODGE" },
                new MasterVehicleTable { Id = 282, model = "VAN", make = "DODGE" },
                new MasterVehicleTable { Id = 283, model = "VIPER", make = "DODGE" },
                new MasterVehicleTable { Id = 284, model = "500", make = "FIAT" },
                new MasterVehicleTable { Id = 285, model = "500C", make = "FIAT" },
                new MasterVehicleTable { Id = 286, model = "500E", make = "FIAT" },
                new MasterVehicleTable { Id = 287, model = "500L", make = "FIAT" },
                new MasterVehicleTable { Id = 288, model = "500X", make = "FIAT" },
                new MasterVehicleTable { Id = 289, model = "C-MAX ENERGI", make = "FORD" },
                new MasterVehicleTable { Id = 290, model = "C-MAX HYBRID", make = "FORD" },
                new MasterVehicleTable { Id = 291, model = "CONTOUR", make = "FORD" },
                new MasterVehicleTable { Id = 292, model = "CROWN VICTORIA", make = "FORD" },
                new MasterVehicleTable { Id = 293, model = "ECONOLINE VANS", make = "FORD" },
                new MasterVehicleTable { Id = 294, model = "E150", make = "FORD" },
                new MasterVehicleTable { Id = 295, model = "E250", make = "FORD" },
                new MasterVehicleTable { Id = 296, model = "E350", make = "FORD" },
                new MasterVehicleTable { Id = 297, model = "E350 SUPER DUTY", make = "FORD" },
                new MasterVehicleTable { Id = 298, model = "VAN", make = "FORD" },
                new MasterVehicleTable { Id = 299, model = "EDGE", make = "FORD" },
                new MasterVehicleTable { Id = 300, model = "ESCAPE", make = "FORD" },
                new MasterVehicleTable { Id = 301, model = "EXCAPE HYBRID", make = "FORD" },
                new MasterVehicleTable { Id = 302, model = "ESCORT", make = "FORD" },
                new MasterVehicleTable { Id = 303, model = "EXCURSION", make = "FORD" },
                new MasterVehicleTable { Id = 304, model = "EXPEDITION", make = "FORD" },
                new MasterVehicleTable { Id = 305, model = "EXPEDITION", make = "FORD" },
                new MasterVehicleTable { Id = 306, model = "EXPEDITION EL", make = "FORD" },
                new MasterVehicleTable { Id = 307, model = "EXPLORER", make = "FORD" },
                new MasterVehicleTable { Id = 308, model = "EXPLORE SPORT", make = "FORD" },
                new MasterVehicleTable { Id = 309, model = "F150", make = "FORD" },
                new MasterVehicleTable { Id = 310, model = "F250", make = "FORD" },
                new MasterVehicleTable { Id = 311, model = "F350", make = "FORD" },
                new MasterVehicleTable { Id = 312, model = "F450", make = "FORD" },
                new MasterVehicleTable { Id = 313, model = "F PICKUP", make = "FORD" },
                new MasterVehicleTable { Id = 314, model = "FIESTA", make = "FORD" },
                new MasterVehicleTable { Id = 315, model = "FIVE HUNDRED", make = "FORD" },
                new MasterVehicleTable { Id = 316, model = "FLEX", make = "FORD" },
                new MasterVehicleTable { Id = 317, model = "FOCUSE", make = "FORD" },
                new MasterVehicleTable { Id = 318, model = "FOCUSE ELECTRIC", make = "FORD" },
                new MasterVehicleTable { Id = 319, model = "FOCUSE ST", make = "FORD" },
                new MasterVehicleTable { Id = 320, model = "FREESTAR", make = "FORD" },
                new MasterVehicleTable { Id = 321, model = "FREESTYLE", make = "FORD" },
                new MasterVehicleTable { Id = 322, model = "FUSION", make = "FORD" },
                new MasterVehicleTable { Id = 323, model = "FUSION ENERGI", make = "FORD" },
                new MasterVehicleTable { Id = 324, model = "FUSION HYBRID", make = "FORD" },
                new MasterVehicleTable { Id = 325, model = "FT", make = "FORD" },
                new MasterVehicleTable { Id = 326, model = "MUSTANG", make = "FORD" },
                new MasterVehicleTable { Id = 327, model = "RANGER", make = "FORD" },
                new MasterVehicleTable { Id = 328, model = "SEDAN POLICE", make = "FORD" },
                new MasterVehicleTable { Id = 329, model = "TAURUS", make = "FORD" },
                new MasterVehicleTable { Id = 330, model = "TAURUS X", make = "FORD" },
                new MasterVehicleTable { Id = 331, model = "THUNDERBIRD", make = "FORD" },
                new MasterVehicleTable { Id = 332, model = "TRANSIT CONNECT", make = "FORD" },
                new MasterVehicleTable { Id = 333, model = "TRANSIT-150", make = "FORD" },
                new MasterVehicleTable { Id = 334, model = "TRANSIT-250", make = "FORD" },
                new MasterVehicleTable { Id = 335, model = "TRANSIT-350", make = "FORD" },
                new MasterVehicleTable { Id = 336, model = "WINDSTAR", make = "FORD" },
                new MasterVehicleTable { Id = 337, model = "ZX2", make = "FORD" },
                new MasterVehicleTable { Id = 338, model = "ACADIA", make = "GMC" },
                new MasterVehicleTable { Id = 339, model = "CANYON", make = "GMC" },
                new MasterVehicleTable { Id = 340, model = "ENVOY", make = "GMC" },
                new MasterVehicleTable { Id = 341, model = "ENVOY XL", make = "GMC" },
                new MasterVehicleTable { Id = 342, model = "ENVOY XUV", make = "GMC" },
                new MasterVehicleTable { Id = 343, model = "JIMMY", make = "GMC" },
                new MasterVehicleTable { Id = 344, model = "SAFARI", make = "GMC" },
                new MasterVehicleTable { Id = 345, model = "SAVANA VANS", make = "GMC" },
                new MasterVehicleTable { Id = 346, model = "SAVANA 1500", make = "GMC" },
                new MasterVehicleTable { Id = 347, model = "SAVANA 2500", make = "GMC" },
                new MasterVehicleTable { Id = 348, model = "SAVANA 3500", make = "GMC" },
                new MasterVehicleTable { Id = 349, model = "VAN", make = "GMC" },
                new MasterVehicleTable { Id = 350, model = "SIERRA TRUCH", make = "GMC" },
                new MasterVehicleTable { Id = 351, model = "PICKUP", make = "GMC" },
                new MasterVehicleTable { Id = 352, model = "SIERRA 1500", make = "GMC" },
                new MasterVehicleTable { Id = 353, model = "SIERRA 1500 HYB", make = "GMC" },
                new MasterVehicleTable { Id = 354, model = "SIERRA 2500", make = "GMC" },
                new MasterVehicleTable { Id = 355, model = "SIERRA 3500", make = "GMC" },
                new MasterVehicleTable { Id = 356, model = "SONOMA", make = "GMC" },
                new MasterVehicleTable { Id = 357, model = "TERRAIN", make = "GMC" },
                new MasterVehicleTable { Id = 358, model = "YUKON", make = "GMC" },
                new MasterVehicleTable { Id = 359, model = "YUKON HYBIRD", make = "GMC" },
                new MasterVehicleTable { Id = 360, model = "YUKON XL", make = "GMC" },
                new MasterVehicleTable { Id = 361, model = "NV 2500 HD", make = "NISSAN" },
                new MasterVehicleTable { Id = 362, model = "NV 3500 HD", make = "NISSAN" },
                new MasterVehicleTable { Id = 363, model = "NV PASSENGER", make = "NISSAN" },
                new MasterVehicleTable { Id = 364, model = "NV 200", make = "NISSAN" },
                new MasterVehicleTable { Id = 365, model = "PATHFINDER", make = "NISSAN" },
                new MasterVehicleTable { Id = 366, model = "PATHFINDER HYBR", make = "NISSAN" },
                new MasterVehicleTable { Id = 367, model = "QUEST", make = "NISSAN" },
                new MasterVehicleTable { Id = 368, model = "ROUGE", make = "NISSAN" },
                new MasterVehicleTable { Id = 369, model = "ROUGE SELECT", make = "NISSAN" },
                new MasterVehicleTable { Id = 370, model = "SENTRA", make = "NISSAN" },
                new MasterVehicleTable { Id = 371, model = "TITAN", make = "NISSAN" },
                new MasterVehicleTable { Id = 372, model = "VERSA", make = "NISSAN" },
                new MasterVehicleTable { Id = 373, model = "VERSA NOTE", make = "NISSAN" },
                new MasterVehicleTable { Id = 374, model = "XTERRA", make = "NISSAN" },
                new MasterVehicleTable { Id = 375, model = "ALERO", make = "OLDSMOBILE" },
                new MasterVehicleTable { Id = 376, model = "AURORA", make = "OLDSMOBILE" },
                new MasterVehicleTable { Id = 377, model = "BARVADA", make = "OLDSMOBILE" },
                new MasterVehicleTable { Id = 378, model = "CUTLASS", make = "OLDSMOBILE" },
                new MasterVehicleTable { Id = 379, model = "INTRIGE", make = "OLDSMOBILE" },
                new MasterVehicleTable { Id = 380, model = "SILHOUEETE", make = "OLDSMOBILE" },
                new MasterVehicleTable { Id = 381, model = "BREEZE", make = "PLYMOUTH" },
                new MasterVehicleTable { Id = 382, model = "GRAND VOYAGER", make = "PLYMOUTH" },
                new MasterVehicleTable { Id = 383, model = "NEON", make = "PLYMOUTH" },
                new MasterVehicleTable { Id = 384, model = "PROWLER", make = "PLYMOUTH" },
                new MasterVehicleTable { Id = 385, model = "VOYAGER", make = "PLYMOUTH" },
                new MasterVehicleTable { Id = 386, model = "AZTEK", make = "PONTIAC" },
                new MasterVehicleTable { Id = 387, model = "BONNEVILLE", make = "PONTIAC" },
                new MasterVehicleTable { Id = 388, model = "FIREBIRD", make = "PONTIAC" },
                new MasterVehicleTable { Id = 389, model = "G3", make = "PONTIAC" },
                new MasterVehicleTable { Id = 390, model = "G5", make = "PONTIAC" },
                new MasterVehicleTable { Id = 391, model = "G6", make = "PONTIAC" },
                new MasterVehicleTable { Id = 392, model = "G8", make = "PONTIAC" },
                new MasterVehicleTable { Id = 393, model = "GRAND AM", make = "PONTIAC" },
                new MasterVehicleTable { Id = 394, model = "GRAND PRIX", make = "PONTIAC" },
                new MasterVehicleTable { Id = 395, model = "GTO", make = "PONTIAC" },
                new MasterVehicleTable { Id = 396, model = "MONTANA", make = "PONTIAC" },
                new MasterVehicleTable { Id = 397, model = "MONTANA SV6", make = "PONTIAC" },
                new MasterVehicleTable { Id = 398, model = "SOLSTICE", make = "PONTIAC" },
                new MasterVehicleTable { Id = 399, model = "SUNFIRE", make = "PONTIAC" },
                new MasterVehicleTable { Id = 400, model = "TORRENT", make = "PONTIAC" },
                new MasterVehicleTable { Id = 401, model = "VIBE", make = "PONTIAC" },
                new MasterVehicleTable { Id = 402, model = "1500", make = "RAM" },
                new MasterVehicleTable { Id = 403, model = "2500", make = "RAM" },
                new MasterVehicleTable { Id = 404, model = "3500", make = "RAM" },
                new MasterVehicleTable { Id = 405, model = "CARGO", make = "RAM" },
                new MasterVehicleTable { Id = 406, model = "PROMASTER 1500", make = "RAM" },
                new MasterVehicleTable { Id = 407, model = "PROMASTER 2500", make = "RAM" },
                new MasterVehicleTable { Id = 408, model = "PROMASTER 2500 W", make = "RAM" },
                new MasterVehicleTable { Id = 409, model = "PROMASTER 3500", make = "RAM" },
                new MasterVehicleTable { Id = 410, model = "PROMASTER CITY", make = "RAM" },
                new MasterVehicleTable { Id = 411, model = "9-2X", make = "SAAB" },
                new MasterVehicleTable { Id = 412, model = "42250", make = "SAAB" },
                new MasterVehicleTable { Id = 413, model = "9-3X", make = "SAAB" },
                new MasterVehicleTable { Id = 414, model = "9-4X", make = "SAAB" },
                new MasterVehicleTable { Id = 415, model = "42252", make = "SAAB" },
                new MasterVehicleTable { Id = 416, model = "9-7X", make = "SAAB" },
                new MasterVehicleTable { Id = 417, model = "ASTRA", make = "SATURN" },
                new MasterVehicleTable { Id = 418, model = "AURA", make = "SATURN" },
                new MasterVehicleTable { Id = 419, model = "AURA HYBIRD", make = "SATURN" },
                new MasterVehicleTable { Id = 420, model = "AURA GREEN LINE", make = "SATURN" },
                new MasterVehicleTable { Id = 421, model = "AURA HYBIRD", make = "SATURN" },
                new MasterVehicleTable { Id = 422, model = "ION", make = "SATURN" },
                new MasterVehicleTable { Id = 423, model = "L", make = "SATURN" },
                new MasterVehicleTable { Id = 424, model = "LS", make = "SATURN" },
                new MasterVehicleTable { Id = 425, model = "LW", make = "SATURN" },
                new MasterVehicleTable { Id = 426, model = "OUTLOOK", make = "SATURN" },
                new MasterVehicleTable { Id = 427, model = "RELAY", make = "SATURN" },
                new MasterVehicleTable { Id = 428, model = "SC", make = "SATURN" },
                new MasterVehicleTable { Id = 429, model = "SKY", make = "SATURN" },
                new MasterVehicleTable { Id = 430, model = "SL", make = "SATURN" },
                new MasterVehicleTable { Id = 431, model = "SW", make = "SATURN" },
                new MasterVehicleTable { Id = 432, model = "VUE", make = "SATURN" },
                new MasterVehicleTable { Id = 433, model = "VUE HYBRID", make = "SATURN" },
                new MasterVehicleTable { Id = 434, model = "VUE GREEN LINE", make = "SATURN" },
                new MasterVehicleTable { Id = 435, model = "VUE HYBIRD", make = "SATURN" },
                new MasterVehicleTable { Id = 436, model = "FRS", make = "SCION" },
                new MasterVehicleTable { Id = 437, model = "IQ", make = "SCION" },
                new MasterVehicleTable { Id = 438, model = "TC", make = "SCION" },
                new MasterVehicleTable { Id = 439, model = "XA", make = "SCION" },
                new MasterVehicleTable { Id = 440, model = "XB", make = "SCION" },
                new MasterVehicleTable { Id = 441, model = "XD", make = "SCION" },
                new MasterVehicleTable { Id = 442, model = "ALL", make = "SMART" },
                new MasterVehicleTable { Id = 443, model = "B9 TRIBECA", make = "SUBARU" },
                new MasterVehicleTable { Id = 444, model = "BAJA", make = "SUBARU" },
                new MasterVehicleTable { Id = 445, model = "BRZ", make = "SUBARU" },
                new MasterVehicleTable { Id = 446, model = "FORESTER", make = "SUBARU" },
                new MasterVehicleTable { Id = 447, model = "IMPREZA", make = "SUBARU" },
                new MasterVehicleTable { Id = 448, model = "IMPREZA OUTBACK", make = "SUBARU" },
                new MasterVehicleTable { Id = 449, model = "IMPERZA WRX", make = "SUBARU" },
                new MasterVehicleTable { Id = 450, model = "IMPERZA WRX STI", make = "SUBARU" },
                new MasterVehicleTable { Id = 451, model = "LEGACY", make = "SUBARU" },
                new MasterVehicleTable { Id = 452, model = "OUTBACK", make = "SUBARU" },
                new MasterVehicleTable { Id = 453, model = "TRIBECA", make = "SUBARU" },
                new MasterVehicleTable { Id = 454, model = "WRX", make = "SUBARU" },
                new MasterVehicleTable { Id = 455, model = "XV CROSSTERK", make = "SUBARU" },
                new MasterVehicleTable { Id = 456, model = "XV CROSSTREK HY", make = "SUBARU" },
                new MasterVehicleTable { Id = 457, model = "AERIO", make = "Suzuki" },
                new MasterVehicleTable { Id = 458, model = "EQUATOR", make = "Suzuki" },
                new MasterVehicleTable { Id = 459, model = "ESTEEM", make = "Suzuki" },
                new MasterVehicleTable { Id = 460, model = "FORENZA", make = "Suzuki" },
                new MasterVehicleTable { Id = 461, model = "GRAND VITRARA", make = "Suzuki" },
                new MasterVehicleTable { Id = 462, model = "KIZASHI", make = "Suzuki" },
                new MasterVehicleTable { Id = 463, model = "RENO", make = "Suzuki" },
                new MasterVehicleTable { Id = 464, model = "SWIFT", make = "Suzuki" },
                new MasterVehicleTable { Id = 465, model = "SX4", make = "Suzuki" },
                new MasterVehicleTable { Id = 466, model = "VERONA", make = "Suzuki" },
                new MasterVehicleTable { Id = 467, model = "VITARA", make = "Suzuki" },
                new MasterVehicleTable { Id = 468, model = "XL7", make = "Suzuki" },
                new MasterVehicleTable { Id = 469, model = "4RUNNER", make = "TOYOTA" },
                new MasterVehicleTable { Id = 470, model = "AVALON", make = "TOYOTA" },
                new MasterVehicleTable { Id = 471, model = "AVALON HYBRID", make = "TOYOTA" },
                new MasterVehicleTable { Id = 472, model = "CAMRY", make = "TOYOTA" },
                new MasterVehicleTable { Id = 473, model = "CAMRY HYBIRD", make = "TOYOTA" },
                new MasterVehicleTable { Id = 474, model = "CAMRY SOLARA", make = "TOYOTA" },
                new MasterVehicleTable { Id = 475, model = "CELICA", make = "TOYOTA" },
                new MasterVehicleTable { Id = 476, model = "COROLLA", make = "TOYOTA" },
                new MasterVehicleTable { Id = 477, model = "ECHO", make = "TOYOTA" },
                new MasterVehicleTable { Id = 478, model = "FJ CRUISER", make = "TOYOTA" },
                new MasterVehicleTable { Id = 479, model = "HIGHLANDER", make = "TOYOTA" },
                new MasterVehicleTable { Id = 480, model = "HIGHLANDER HYBR", make = "TOYOTA" },
                new MasterVehicleTable { Id = 481, model = "LAND CRUISER", make = "TOYOTA" },
                new MasterVehicleTable { Id = 482, model = "MATRIX", make = "TOYOTA" },
                new MasterVehicleTable { Id = 483, model = "MR2", make = "TOYOTA" },
                new MasterVehicleTable { Id = 484, model = "PRIUS", make = "TOYOTA" },
                new MasterVehicleTable { Id = 485, model = "PRIUS C", make = "TOYOTA" },
                new MasterVehicleTable { Id = 486, model = "PRIUS PLUG-IN", make = "TOYOTA" },
                new MasterVehicleTable { Id = 487, model = "PRIUS V", make = "TOYOTA" },
                new MasterVehicleTable { Id = 488, model = "RAV4", make = "TOYOTA" },
                new MasterVehicleTable { Id = 489, model = "RAV4 EV", make = "TOYOTA" },
                new MasterVehicleTable { Id = 490, model = "SEQUOIA", make = "TOYOTA" },
                new MasterVehicleTable { Id = 491, model = "SIENNA", make = "TOYOTA" },
                new MasterVehicleTable { Id = 492, model = "TACOMA", make = "TOYOTA" },
                new MasterVehicleTable { Id = 493, model = "TUNDRA", make = "TOYOTA" },
                new MasterVehicleTable { Id = 494, model = "VENXA", make = "TOYOTA" },
                new MasterVehicleTable { Id = 495, model = "YARIS", make = "TOYOTA" },
                new MasterVehicleTable { Id = 496, model = "BEETL", make = "VOLKSWAGEN" },
                new MasterVehicleTable { Id = 497, model = "CABRIO", make = "VOLKSWAGEN" },
                new MasterVehicleTable { Id = 498, model = "CC", make = "VOLKSWAGEN" },
                new MasterVehicleTable { Id = 499, model = "E-GOLD", make = "VOLKSWAGEN" },
                new MasterVehicleTable { Id = 500, model = "EOS", make = "VOLKSWAGEN" },
                new MasterVehicleTable { Id = 501, model = "EUROVAN", make = "VOLKSWAGEN" },
                new MasterVehicleTable { Id = 502, model = "GOLF", make = "VOLKSWAGEN" },
                new MasterVehicleTable { Id = 503, model = "GOLF GTI", make = "VOLKSWAGEN" },
                new MasterVehicleTable { Id = 504, model = "GOLD R", make = "VOLKSWAGEN" },
                new MasterVehicleTable { Id = 505, model = "GOLF SPORTWAGEN", make = "VOLKSWAGEN" },
                new MasterVehicleTable { Id = 506, model = "GTI", make = "VOLKSWAGEN" },
                new MasterVehicleTable { Id = 507, model = "JETTA", make = "VOLKSWAGEN" },
                new MasterVehicleTable { Id = 508, model = "JETTA HYBIRD", make = "VOLKSWAGEN" },
                new MasterVehicleTable { Id = 509, model = "JETTA SPORTWAGE", make = "VOLKSWAGEN" },
                new MasterVehicleTable { Id = 510, model = "NEW BEETLE", make = "VOLKSWAGEN" },
                new MasterVehicleTable { Id = 511, model = "ACCORD", make = "HONDA" },
                new MasterVehicleTable { Id = 512, model = "ACCORD CROSSTOUR", make = "HONDA" },
                new MasterVehicleTable { Id = 513, model = "ACCORD HYBIRD", make = "HONDA" },
                new MasterVehicleTable { Id = 514, model = "ACCORD PLUG IN", make = "HONDA" },
                new MasterVehicleTable { Id = 515, model = "CIVIC", make = "HONDA" },
                new MasterVehicleTable { Id = 516, model = "CIVIC HYBIRD", make = "HONDA" },
                new MasterVehicleTable { Id = 517, model = "CR-V", make = "HONDA" },
                new MasterVehicleTable { Id = 518, model = "CR-Z", make = "HONDA" },
                new MasterVehicleTable { Id = 519, model = "CROSSTOUR", make = "HONDA" },
                new MasterVehicleTable { Id = 520, model = "ELEMENT", make = "HONDA" },
                new MasterVehicleTable { Id = 521, model = "FIT", make = "HONDA" },
                new MasterVehicleTable { Id = 522, model = "FIT EV", make = "HONDA" },
                new MasterVehicleTable { Id = 523, model = "INSIGHT", make = "HONDA" },
                new MasterVehicleTable { Id = 524, model = "ODYSSEY", make = "HONDA" },
                new MasterVehicleTable { Id = 525, model = "PASSPORT", make = "HONDA" },
                new MasterVehicleTable { Id = 526, model = "PILOT", make = "HONDA" },
                new MasterVehicleTable { Id = 527, model = "PRELUDE", make = "HONDA" },
                new MasterVehicleTable { Id = 528, model = "RIDGELINE", make = "HONDA" },
                new MasterVehicleTable { Id = 529, model = "S2000", make = "HONDA" },
                new MasterVehicleTable { Id = 530, model = "H1", make = "HUMMER" },
                new MasterVehicleTable { Id = 531, model = "H1 ALPHA", make = "HUMMER" },
                new MasterVehicleTable { Id = 532, model = "H2", make = "HUMMER" },
                new MasterVehicleTable { Id = 533, model = "H3", make = "HUMMER" },
                new MasterVehicleTable { Id = 534, model = "H3T", make = "HUMMER" },
                new MasterVehicleTable { Id = 535, model = "ACCENT", make = "HYUNDAI" },
                new MasterVehicleTable { Id = 536, model = "AZERA", make = "HYUNDAI" },
                new MasterVehicleTable { Id = 537, model = "ELANTRA", make = "HYUNDAI" },
                new MasterVehicleTable { Id = 538, model = "ELANTRA FT", make = "HYUNDAI" },
                new MasterVehicleTable { Id = 539, model = "ELANTRA TOURING", make = "HYUNDAI" },
                new MasterVehicleTable { Id = 540, model = "ENTOURAGE", make = "HYUNDAI" },
                new MasterVehicleTable { Id = 541, model = "EQUUS", make = "HYUNDAI" },
                new MasterVehicleTable { Id = 542, model = "GENESIS", make = "HYUNDAI" },
                new MasterVehicleTable { Id = 543, model = "GENESIS COUPE", make = "HYUNDAI" },
                new MasterVehicleTable { Id = 544, model = "SANTA FE", make = "HYUNDAI" },
                new MasterVehicleTable { Id = 545, model = "SANTA FE SPORT", make = "HYUNDAI" },
                new MasterVehicleTable { Id = 546, model = "SONATA", make = "HYUNDAI" },
                new MasterVehicleTable { Id = 547, model = "SONATA HYBRID", make = "HYUNDAI" },
                new MasterVehicleTable { Id = 548, model = "TIBURON", make = "HYUNDAI" },
                new MasterVehicleTable { Id = 549, model = "TUCSON", make = "HYUNDAI" },
                new MasterVehicleTable { Id = 550, model = "VELOSTER", make = "HYUNDAI" },
                new MasterVehicleTable { Id = 551, model = "VERACRUZ", make = "HYUNDAI" },
                new MasterVehicleTable { Id = 552, model = "XG300", make = "HYUNDAI" },
                new MasterVehicleTable { Id = 553, model = "XG350", make = "HYUNDAI" },
                new MasterVehicleTable { Id = 554, model = "EX35", make = "INFINITI" },
                new MasterVehicleTable { Id = 555, model = "EX37", make = "INFINITI" },
                new MasterVehicleTable { Id = 556, model = "FX35", make = "INFINITI" },
                new MasterVehicleTable { Id = 557, model = "FX37", make = "INFINITI" },
                new MasterVehicleTable { Id = 558, model = "FX45", make = "INFINITI" },
                new MasterVehicleTable { Id = 559, model = "FX50", make = "INFINITI" },
                new MasterVehicleTable { Id = 560, model = "G20", make = "INFINITI" },
                new MasterVehicleTable { Id = 561, model = "G25", make = "INFINITI" },
                new MasterVehicleTable { Id = 562, model = "G35", make = "INFINITI" },
                new MasterVehicleTable { Id = 563, model = "G37", make = "INFINITI" },
                new MasterVehicleTable { Id = 564, model = "I30", make = "INFINITI" },
                new MasterVehicleTable { Id = 565, model = "I35", make = "INFINITI" },
                new MasterVehicleTable { Id = 566, model = "IPL G", make = "INFINITI" },
                new MasterVehicleTable { Id = 567, model = "JX35", make = "INFINITI" },
                new MasterVehicleTable { Id = 568, model = "M35", make = "INFINITI" },
                new MasterVehicleTable { Id = 569, model = "M35H", make = "INFINITI" },
                new MasterVehicleTable { Id = 570, model = "M37", make = "INFINITI" },
                new MasterVehicleTable { Id = 571, model = "M45", make = "INFINITI" },
                new MasterVehicleTable { Id = 572, model = "M56", make = "INFINITI" },
                new MasterVehicleTable { Id = 573, model = "Q45", make = "INFINITI" },
                new MasterVehicleTable { Id = 574, model = "Q50", make = "INFINITI" },
                new MasterVehicleTable { Id = 575, model = "Q5", make = "INFINITI" },
                new MasterVehicleTable { Id = 576, model = "Q60", make = "INFINITI" },
                new MasterVehicleTable { Id = 577, model = "Q60IPL", make = "INFINITI" },
                new MasterVehicleTable { Id = 578, model = "Q70", make = "INFINITI" },
                new MasterVehicleTable { Id = 579, model = "Q70H", make = "INFINITI" },
                new MasterVehicleTable { Id = 580, model = "QX", make = "INFINITI" },
                new MasterVehicleTable { Id = 581, model = "QX4", make = "INFINITI" },
                new MasterVehicleTable { Id = 582, model = "QX50", make = "INFINITI" },
                new MasterVehicleTable { Id = 583, model = "QX56", make = "INFINITI" },
                new MasterVehicleTable { Id = 584, model = "QX60", make = "INFINITI" },
                new MasterVehicleTable { Id = 585, model = "QX70", make = "INFINITI" },
                new MasterVehicleTable { Id = 586, model = "QX80", make = "INFINITI" },
                new MasterVehicleTable { Id = 587, model = "QX60", make = "INFINITI" },
                new MasterVehicleTable { Id = 588, model = "QX HYBIRD", make = "INFINITI" },
                new MasterVehicleTable { Id = 589, model = "CX", make = "INTERNATIONAL" },
                new MasterVehicleTable { Id = 590, model = "MX", make = "INTERNATIONAL" },
                new MasterVehicleTable { Id = 591, model = "MXT", make = "INTERNATIONAL" },
                new MasterVehicleTable { Id = 592, model = "RXT", make = "INTERNATIONAL" },
                new MasterVehicleTable { Id = 593, model = "AMIGO", make = "ISUZU" },
                new MasterVehicleTable { Id = 594, model = "ASCENDER", make = "ISUZU" },
                new MasterVehicleTable { Id = 595, model = "AZIOM", make = "ISUZU" },
                new MasterVehicleTable { Id = 596, model = "ISUZU TRUCK", make = "ISUZU" },
                new MasterVehicleTable { Id = 597, model = "HOMBRE", make = "ISUZU" },
                new MasterVehicleTable { Id = 598, model = "I280", make = "ISUZU" },
                new MasterVehicleTable { Id = 599, model = "I290", make = "ISUZU" },
                new MasterVehicleTable { Id = 600, model = "I350", make = "ISUZU" },
                new MasterVehicleTable { Id = 601, model = "I370", make = "ISUZU" },
                new MasterVehicleTable { Id = 602, model = "RODEO", make = "ISUZU" },
                new MasterVehicleTable { Id = 603, model = "RODEO SPORT", make = "ISUZU" },
                new MasterVehicleTable { Id = 604, model = "TROOPER", make = "ISUZU" },
                new MasterVehicleTable { Id = 605, model = "VEHICROSS", make = "ISUZU" },
                new MasterVehicleTable { Id = 606, model = "F-TYPE", make = "JAGUAR" },
                new MasterVehicleTable { Id = 607, model = "S-TYPE", make = "JAGUAR" },
                new MasterVehicleTable { Id = 608, model = "SUPER V8", make = "JAGUAR" },
                new MasterVehicleTable { Id = 609, model = "SUPER V8 PORTFOL", make = "JAGUAR" },
                new MasterVehicleTable { Id = 610, model = "VANDEN PLAS", make = "JAGUAR" },
                new MasterVehicleTable { Id = 611, model = "X-TYPE", make = "JAGUAR" },
                new MasterVehicleTable { Id = 612, model = "XF", make = "JAGUAR" },
                new MasterVehicleTable { Id = 613, model = "XJ", make = "JAGUAR" },
                new MasterVehicleTable { Id = 614, model = "XJ8", make = "JAGUAR" },
                new MasterVehicleTable { Id = 615, model = "XJR", make = "JAGUAR" },
                new MasterVehicleTable { Id = 616, model = "XK", make = "JAGUAR" },
                new MasterVehicleTable { Id = 617, model = "XK8", make = "JAGUAR" },
                new MasterVehicleTable { Id = 618, model = "XKR", make = "JAGUAR" },
                new MasterVehicleTable { Id = 619, model = "CHEROKEE", make = "JEEP" },
                new MasterVehicleTable { Id = 620, model = "COMMANDER", make = "JEEP" },
                new MasterVehicleTable { Id = 621, model = "COMPASS", make = "JEEP" },
                new MasterVehicleTable { Id = 622, model = "GRAND CHEROKEE", make = "JEEP" },
                new MasterVehicleTable { Id = 623, model = "LIBERTY", make = "JEEP" },
                new MasterVehicleTable { Id = 624, model = "PATRIOT", make = "JEEP" },
                new MasterVehicleTable { Id = 625, model = "WRANGLER", make = "JEEP" },
                new MasterVehicleTable { Id = 626, model = "WRANGLER UNLIMIT", make = "JEEP" },
                new MasterVehicleTable { Id = 627, model = "AMANTI", make = "KIA" },
                new MasterVehicleTable { Id = 628, model = "BORREGO", make = "KIA" },
                new MasterVehicleTable { Id = 629, model = "CADENZA", make = "KIA" },
                new MasterVehicleTable { Id = 630, model = "FORTE", make = "KIA" },
                new MasterVehicleTable { Id = 631, model = "FORTE KOUP", make = "KIA" },
                new MasterVehicleTable { Id = 632, model = "K900", make = "KIA" },
                new MasterVehicleTable { Id = 633, model = "OPTIMA", make = "KIA" },
                new MasterVehicleTable { Id = 634, model = "OPTIMA HYBRID", make = "KIA" },
                new MasterVehicleTable { Id = 635, model = "RIO", make = "KIA" },
                new MasterVehicleTable { Id = 636, model = "RIO5", make = "KIA" },
                new MasterVehicleTable { Id = 637, model = "RONDO", make = "KIA" },
                new MasterVehicleTable { Id = 638, model = "SEDONA", make = "KIA" },
                new MasterVehicleTable { Id = 639, model = "SEPHIA", make = "KIA" },
                new MasterVehicleTable { Id = 640, model = "SORENTO", make = "KIA" },
                new MasterVehicleTable { Id = 641, model = "SOUL", make = "KIA" },
                new MasterVehicleTable { Id = 642, model = "SPECTRA", make = "KIA" },
                new MasterVehicleTable { Id = 643, model = "SPECTRA5", make = "KIA" },
                new MasterVehicleTable { Id = 644, model = "SPORTAGE", make = "KIA" },
                new MasterVehicleTable { Id = 645, model = "DISCOVERY", make = "LAND ROVER" },
                new MasterVehicleTable { Id = 646, model = "DISCOVERY SPORT", make = "LAND ROVER" },
                new MasterVehicleTable { Id = 647, model = "FREELANDER", make = "LAND ROVER" },
                new MasterVehicleTable { Id = 648, model = "LR2", make = "LAND ROVER" },
                new MasterVehicleTable { Id = 649, model = "LR3", make = "LAND ROVER" },
                new MasterVehicleTable { Id = 650, model = "LR4", make = "LAND ROVER" },
                new MasterVehicleTable { Id = 651, model = "RANGE ROVER", make = "LAND ROVER" },
                new MasterVehicleTable { Id = 652, model = "RANGE ROVER EVOQ", make = "LAND ROVER" },
                new MasterVehicleTable { Id = 653, model = "RANGE ROVER SPORT", make = "LAND ROVER" },
                new MasterVehicleTable { Id = 654, model = "CT 200H", make = "LEXUS" },
                new MasterVehicleTable { Id = 655, model = "ES 300", make = "LEXUS" },
                new MasterVehicleTable { Id = 656, model = "ES 300H", make = "LEXUS" },
                new MasterVehicleTable { Id = 657, model = "ES 330", make = "LEXUS" },
                new MasterVehicleTable { Id = 658, model = "ES 350", make = "LEXUS" },
                new MasterVehicleTable { Id = 659, model = "GS 300", make = "LEXUS" },
                new MasterVehicleTable { Id = 660, model = "GS 350", make = "LEXUS" },
                new MasterVehicleTable { Id = 661, model = "GS 400", make = "LEXUS" },
                new MasterVehicleTable { Id = 662, model = "GS 430", make = "LEXUS" },
                new MasterVehicleTable { Id = 663, model = "GS 450H", make = "LEXUS" },
                new MasterVehicleTable { Id = 664, model = "GS 460", make = "LEXUS" },
                new MasterVehicleTable { Id = 665, model = "GX 460", make = "LEXUS" },
                new MasterVehicleTable { Id = 666, model = "GX 470", make = "LEXUS" },
                new MasterVehicleTable { Id = 667, model = "HS 250H", make = "LEXUS" },
                new MasterVehicleTable { Id = 668, model = "IS 250", make = "LEXUS" },
                new MasterVehicleTable { Id = 669, model = "IS 250C", make = "LEXUS" },
                new MasterVehicleTable { Id = 670, model = "IS 300", make = "LEXUS" },
                new MasterVehicleTable { Id = 671, model = "IS 350", make = "LEXUS" },
                new MasterVehicleTable { Id = 672, model = "IS 350C", make = "LEXUS" },
                new MasterVehicleTable { Id = 673, model = "IS F", make = "LEXUS" },
                new MasterVehicleTable { Id = 674, model = "LS 400", make = "LEXUS" },
                new MasterVehicleTable { Id = 675, model = "LS 430", make = "LEXUS" },
                new MasterVehicleTable { Id = 676, model = "LS 460", make = "LEXUS" },
                new MasterVehicleTable { Id = 677, model = "LS 600HL", make = "LEXUS" },
                new MasterVehicleTable { Id = 678, model = "LX 470", make = "LEXUS" },
                new MasterVehicleTable { Id = 679, model = "LX 570", make = "LEXUS" },
                new MasterVehicleTable { Id = 680, model = "NX 200T", make = "LEXUS" },
                new MasterVehicleTable { Id = 681, model = "NX 300H", make = "LEXUS" },
                new MasterVehicleTable { Id = 682, model = "RC 350", make = "LEXUS" },
                new MasterVehicleTable { Id = 683, model = "RC F", make = "LEXUS" },
                new MasterVehicleTable { Id = 684, model = "RX 300", make = "LEXUS" },
                new MasterVehicleTable { Id = 685, model = "RX 330", make = "LEXUS" },
                new MasterVehicleTable { Id = 686, model = "RX 350", make = "LEXUS" },
                new MasterVehicleTable { Id = 687, model = "RX 400H", make = "LEXUS" },
                new MasterVehicleTable { Id = 688, model = "RX 450H", make = "LEXUS" },
                new MasterVehicleTable { Id = 689, model = "SC 300", make = "LEXUS" },
                new MasterVehicleTable { Id = 690, model = "SC 400", make = "LEXUS" },
                new MasterVehicleTable { Id = 691, model = "SC 430", make = "LEXUS" },
                new MasterVehicleTable { Id = 692, model = "AVIATOR", make = "LINCOLN" },
                new MasterVehicleTable { Id = 693, model = "BLACKWOOD", make = "LINCOLN" },
                new MasterVehicleTable { Id = 694, model = "CONTINENTAL", make = "LINCOLN" },
                new MasterVehicleTable { Id = 695, model = "LS", make = "LINCOLN" },
                new MasterVehicleTable { Id = 696, model = "MARK LT", make = "LINCOLN" },
                new MasterVehicleTable { Id = 697, model = "MKC", make = "LINCOLN" },
                new MasterVehicleTable { Id = 698, model = "MKS", make = "LINCOLN" },
                new MasterVehicleTable { Id = 699, model = "MKT", make = "LINCOLN" },
                new MasterVehicleTable { Id = 700, model = "MKX", make = "LINCOLN" },
                new MasterVehicleTable { Id = 701, model = "MKZ", make = "LINCOLN" },
                new MasterVehicleTable { Id = 702, model = "MKZ HYBRID", make = "LINCOLN" },
                new MasterVehicleTable { Id = 703, model = "NAVIGATOR", make = "LINCOLN" },
                new MasterVehicleTable { Id = 704, model = "TOWN CAR", make = "LINCOLN" },
                new MasterVehicleTable { Id = 705, model = "ZEPHYR", make = "LINCOLN" },
                new MasterVehicleTable { Id = 706, model = "628", make = "MAZDA" },
                new MasterVehicleTable { Id = 707, model = "B2300", make = "MAZDA" },
                new MasterVehicleTable { Id = 708, model = "B2500", make = "MAZDA" },
                new MasterVehicleTable { Id = 709, model = "B3000", make = "MAZDA" },
                new MasterVehicleTable { Id = 710, model = "B4000", make = "MAZDA" },
                new MasterVehicleTable { Id = 711, model = "PICKUP", make = "MAZDA" },
                new MasterVehicleTable { Id = 712, model = "CX-5", make = "MAZDA" },
                new MasterVehicleTable { Id = 713, model = "CX-7", make = "MAZDA" },
                new MasterVehicleTable { Id = 714, model = "CX-9", make = "MAZDA" },
                new MasterVehicleTable { Id = 715, model = "MAZDA2", make = "MAZDA" },
                new MasterVehicleTable { Id = 716, model = "MAZDA3", make = "MAZDA" },
                new MasterVehicleTable { Id = 717, model = "MAZDA5", make = "MAZDA" },
                new MasterVehicleTable { Id = 718, model = "MAZDA6", make = "MAZDA" },
                new MasterVehicleTable { Id = 719, model = "MAZDASPEED MIAT", make = "MAZDA" },
                new MasterVehicleTable { Id = 720, model = "MAZDASPEED PORT", make = "MAZDA" },
                new MasterVehicleTable { Id = 721, model = "MAZDASPEED3", make = "MAZDA" },
                new MasterVehicleTable { Id = 722, model = "MAZDASPEED6", make = "MAZDA" },
                new MasterVehicleTable { Id = 723, model = "MIATA MX5", make = "MAZDA" },
                new MasterVehicleTable { Id = 724, model = "MILLENIA", make = "MAZDA" },
                new MasterVehicleTable { Id = 725, model = "MPV", make = "MAZDA" },
                new MasterVehicleTable { Id = 726, model = "PROTEGE", make = "MAZDA" },
                new MasterVehicleTable { Id = 727, model = "PROTEGE5", make = "MAZDA" },
                new MasterVehicleTable { Id = 728, model = "RX-8", make = "MAZDA" },
                new MasterVehicleTable { Id = 729, model = "TRIBUTE", make = "MAZDA" },
                new MasterVehicleTable { Id = 730, model = "TRIBUTE HYBRID", make = "MAZDA" },
                new MasterVehicleTable { Id = 731, model = "AMG GT", make = "MERCEDES-BENZ" },
                new MasterVehicleTable { Id = 732, model = "B ELECTRIC", make = "MERCEDES-BENZ" },
                new MasterVehicleTable { Id = 733, model = "DRIVE", make = "MERCEDES-BENZ" },
                new MasterVehicleTable { Id = 734, model = "C CLASS", make = "MERCEDES-BENZ" },
                new MasterVehicleTable { Id = 735, model = "CL CLASS", make = "MERCEDES-BENZ" },
                new MasterVehicleTable { Id = 736, model = "CLA CLASS", make = "MERCEDES-BENZ" },
                new MasterVehicleTable { Id = 737, model = "CLK CLASS", make = "MERCEDES-BENZ" },
                new MasterVehicleTable { Id = 738, model = "CLS CLASS", make = "MERCEDES-BENZ" },
                new MasterVehicleTable { Id = 739, model = "E CLASS", make = "MERCEDES-BENZ" },
                new MasterVehicleTable { Id = 740, model = "G CLASS", make = "MERCEDES-BENZ" },
                new MasterVehicleTable { Id = 741, model = "GL CLASS", make = "MERCEDES-BENZ" },
                new MasterVehicleTable { Id = 742, model = "CLA CLASS", make = "MERCEDES-BENZ" },
                new MasterVehicleTable { Id = 743, model = "CLK CLASS", make = "MERCEDES-BENZ" },
                new MasterVehicleTable { Id = 744, model = "M CLASS", make = "MERCEDES-BENZ" },
                new MasterVehicleTable { Id = 745, model = "MAYBACH S", make = "MERCEDES-BENZ" },
                new MasterVehicleTable { Id = 746, model = "R CLASS", make = "MERCEDES-BENZ" },
                new MasterVehicleTable { Id = 747, model = "S CLASS", make = "MERCEDES-BENZ" },
                new MasterVehicleTable { Id = 748, model = "SL CLASS", make = "MERCEDES-BENZ" },
                new MasterVehicleTable { Id = 749, model = "CLK CLASS", make = "MERCEDES-BENZ" },
                new MasterVehicleTable { Id = 750, model = "SLR MCLAREN", make = "MERCEDES-BENZ" },
                new MasterVehicleTable { Id = 751, model = "SLS AMG", make = "MERCEDES-BENZ" },
                new MasterVehicleTable { Id = 752, model = "SLS AMG BLACK S", make = "MERCEDES-BENZ" },
                new MasterVehicleTable { Id = 753, model = "SPRINTER", make = "MERCEDES-BENZ" },
                new MasterVehicleTable { Id = 754, model = "COUGER", make = "MERCURY" },
                new MasterVehicleTable { Id = 755, model = "GRAND MARQUIS", make = "MERCURY" },
                new MasterVehicleTable { Id = 756, model = "MARAUDER", make = "MERCURY" },
                new MasterVehicleTable { Id = 757, model = "MARINER", make = "MERCURY" },
                new MasterVehicleTable { Id = 758, model = "MARINER HYBIRD", make = "MERCURY" },
                new MasterVehicleTable { Id = 759, model = "MILAN", make = "MERCURY" },
                new MasterVehicleTable { Id = 760, model = "MILAN HYBIRD", make = "MERCURY" },
                new MasterVehicleTable { Id = 761, model = "MONTEGO", make = "MERCURY" },
                new MasterVehicleTable { Id = 762, model = "MONTEREY", make = "MERCURY" },
                new MasterVehicleTable { Id = 763, model = "MOUNTAINEER", make = "MERCURY" },
                new MasterVehicleTable { Id = 764, model = "MYSTIQUE", make = "MERCURY" },
                new MasterVehicleTable { Id = 765, model = "SABLE", make = "MERCURY" },
                new MasterVehicleTable { Id = 766, model = "VILLAGER", make = "MERCURY" },
                new MasterVehicleTable { Id = 767, model = "CLUBMAN", make = "MINI" },
                new MasterVehicleTable { Id = 768, model = "COOPER CLUBMAN", make = "MINI" },
                new MasterVehicleTable { Id = 769, model = "COOPER SCLUBMAN", make = "MINI" },
                new MasterVehicleTable { Id = 770, model = "CONVERTIBLE", make = "MINI" },
                new MasterVehicleTable { Id = 771, model = "COOPER", make = "MINI" },
                new MasterVehicleTable { Id = 772, model = "COOPER S", make = "MINI" },
                new MasterVehicleTable { Id = 773, model = "COUNTRYMAN", make = "MINI" },
                new MasterVehicleTable { Id = 774, model = "COOPER COUNTRYMAN", make = "MINI" },
                new MasterVehicleTable { Id = 775, model = "COOPER S COUNTRY", make = "MINI" },
                new MasterVehicleTable { Id = 776, model = "COUNTRYMAN", make = "MINI" },
                new MasterVehicleTable { Id = 777, model = "COUPE", make = "MINI" },
                new MasterVehicleTable { Id = 778, model = "HARDTOP", make = "MINI" },
                new MasterVehicleTable { Id = 779, model = "PACEMAN", make = "MINI" },
                new MasterVehicleTable { Id = 780, model = "ROADSTER", make = "MINI" },
                new MasterVehicleTable { Id = 781, model = "DIAMANTE", make = "MITSUBISHI" },
                new MasterVehicleTable { Id = 782, model = "ECLIPSE", make = "MITSUBISHI" },
                new MasterVehicleTable { Id = 783, model = "ENDAVOR", make = "MITSUBISHI" },
                new MasterVehicleTable { Id = 784, model = "GLANT", make = "MITSUBISHI" },
                new MasterVehicleTable { Id = 785, model = "I-MIEV", make = "MITSUBISHI" },
                new MasterVehicleTable { Id = 786, model = "LANCER", make = "MITSUBISHI" },
                new MasterVehicleTable { Id = 787, model = "LANCER EVOLUTION", make = "MITSUBISHI" },
                new MasterVehicleTable { Id = 788, model = "LANCER SPORTBACK", make = "MITSUBISHI" },
                new MasterVehicleTable { Id = 789, model = "MIRAGE", make = "MITSUBISHI" },
                new MasterVehicleTable { Id = 790, model = "MONTERO", make = "MITSUBISHI" },
                new MasterVehicleTable { Id = 791, model = "MONTERO SPORT", make = "MITSUBISHI" },
                new MasterVehicleTable { Id = 792, model = "OUTLANDER", make = "MITSUBISHI" },
                new MasterVehicleTable { Id = 793, model = "OUTLANDER SPORT", make = "MITSUBISHI" },
                new MasterVehicleTable { Id = 794, model = "RAIDER", make = "MITSUBISHI" },
                new MasterVehicleTable { Id = 795, model = "350Z", make = "NISSAN" },
                new MasterVehicleTable { Id = 796, model = "370Z", make = "NISSAN" },
                new MasterVehicleTable { Id = 797, model = "ALTIMA", make = "NISSAN" },
                new MasterVehicleTable { Id = 798, model = "ALTIMA HYBRID", make = "NISSAN" },
                new MasterVehicleTable { Id = 799, model = "ARMADA", make = "NISSAN" },
                new MasterVehicleTable { Id = 800, model = "CUBE", make = "NISSAN" },
                new MasterVehicleTable { Id = 801, model = "FRONTIER", make = "NISSAN" },
                new MasterVehicleTable { Id = 802, model = "GT-R", make = "NISSAN" },
                new MasterVehicleTable { Id = 803, model = "JUKE", make = "NISSAN" },
                new MasterVehicleTable { Id = 804, model = "MAXIMA", make = "NISSAN" },
                new MasterVehicleTable { Id = 805, model = "MURANO", make = "NISSAN" },
                new MasterVehicleTable { Id = 806, model = "MURANO CROSSCAB", make = "NISSAN" },
                new MasterVehicleTable { Id = 807, model = "NV CARGO", make = "NISSAN" },
                new MasterVehicleTable { Id = 808, model = "NV 1500", make = "NISSAN" }
            );

            builder.Entity<MasterYearTable>().HasData(
                new MasterYearTable { Id = 1, Year = 2018 },
                new MasterYearTable { Id = 2, Year = 2017 },
                new MasterYearTable { Id = 3, Year = 2016 },
                new MasterYearTable { Id = 4, Year = 2015 },
                new MasterYearTable { Id = 5, Year = 2014 },
                new MasterYearTable { Id = 6, Year = 2013 },
                new MasterYearTable { Id = 7, Year = 2012 },
                new MasterYearTable { Id = 8, Year = 2011 },
                new MasterYearTable { Id = 9, Year = 2010 },
                new MasterYearTable { Id = 10, Year = 2009 },
                new MasterYearTable { Id = 11, Year = 2008 },
                new MasterYearTable { Id = 12, Year = 2007 },
                new MasterYearTable { Id = 13, Year = 2006 },
                new MasterYearTable { Id = 14, Year = 2005 },
                new MasterYearTable { Id = 15, Year = 2004 },
                new MasterYearTable { Id = 16, Year = 2003 },
                new MasterYearTable { Id = 17, Year = 2002 },
                new MasterYearTable { Id = 18, Year = 2001 },
                new MasterYearTable { Id = 19, Year = 2000 },
                new MasterYearTable { Id = 20, Year = 1999 },
                new MasterYearTable { Id = 21, Year = 1998 },
                new MasterYearTable { Id = 22, Year = 1997 },
                new MasterYearTable { Id = 23, Year = 1996 },
                new MasterYearTable { Id = 24, Year = 1995 },
                new MasterYearTable { Id = 25, Year = 1994 },
                new MasterYearTable { Id = 26, Year = 1993 },
                new MasterYearTable { Id = 27, Year = 1992 },
                new MasterYearTable { Id = 28, Year = 1991 },
                new MasterYearTable { Id = 29, Year = 1990 },
                new MasterYearTable { Id = 30, Year = 1989 },
                new MasterYearTable { Id = 31, Year = 1988 },
                new MasterYearTable { Id = 32, Year = 1987 },
                new MasterYearTable { Id = 33, Year = 1986 },
                new MasterYearTable { Id = 34, Year = 1985 },
                new MasterYearTable { Id = 35, Year = 1984 },
                new MasterYearTable { Id = 36, Year = 1983 },
                new MasterYearTable { Id = 37, Year = 1982 },
                new MasterYearTable { Id = 38, Year = 1981 },
                new MasterYearTable { Id = 39, Year = 1980 },
                new MasterYearTable { Id = 40, Year = 1979 },
                new MasterYearTable { Id = 41, Year = 1978 },
                new MasterYearTable { Id = 42, Year = 1977 },
                new MasterYearTable { Id = 43, Year = 1976 },
                new MasterYearTable { Id = 44, Year = 1975 },
                new MasterYearTable { Id = 45, Year = 1974 },
                new MasterYearTable { Id = 46, Year = 1973 },
                new MasterYearTable { Id = 47, Year = 1972 },
                new MasterYearTable { Id = 48, Year = 1971 },
                new MasterYearTable { Id = 49, Year = 1970 },
                new MasterYearTable { Id = 50, Year = 1969 },
                new MasterYearTable { Id = 51, Year = 1968 },
                new MasterYearTable { Id = 52, Year = 1967 },
                new MasterYearTable { Id = 53, Year = 1966 },
                new MasterYearTable { Id = 55, Year = 1965 },
                new MasterYearTable { Id = 56, Year = 1964 },
                new MasterYearTable { Id = 57, Year = 1963 },
                new MasterYearTable { Id = 58, Year = 1962 },
                new MasterYearTable { Id = 59, Year = 1961 },
                new MasterYearTable { Id = 60, Year = 1960 },
                new MasterYearTable { Id = 61, Year = 1959 },
                new MasterYearTable { Id = 62, Year = 1958 },
                new MasterYearTable { Id = 63, Year = 1957 },
                new MasterYearTable { Id = 64, Year = 1956 },
                new MasterYearTable { Id = 65, Year = 1955 },
                new MasterYearTable { Id = 66, Year = 1954 },
                new MasterYearTable { Id = 67, Year = 1953 },
                new MasterYearTable { Id = 68, Year = 1952 },
                new MasterYearTable { Id = 69, Year = 1951 },
                new MasterYearTable { Id = 70, Year = 1950 },
                new MasterYearTable { Id = 71, Year = 1949 },
                new MasterYearTable { Id = 72, Year = 1948 },
                new MasterYearTable { Id = 73, Year = 1947 },
                new MasterYearTable { Id = 74, Year = 1946 },
                new MasterYearTable { Id = 75, Year = 1945 },
                new MasterYearTable { Id = 76, Year = 1944 },
                new MasterYearTable { Id = 77, Year = 1943 },
                new MasterYearTable { Id = 78, Year = 1942 },
                new MasterYearTable { Id = 79, Year = 1941 },
                new MasterYearTable { Id = 80, Year = 1940 },
                new MasterYearTable { Id = 81, Year = 1939 },
                new MasterYearTable { Id = 82, Year = 1938 },
                new MasterYearTable { Id = 83, Year = 1937 },
                new MasterYearTable { Id = 84, Year = 1936 },
                new MasterYearTable { Id = 85, Year = 1935 },
                new MasterYearTable { Id = 86, Year = 1934 },
                new MasterYearTable { Id = 87, Year = 1933 },
                new MasterYearTable { Id = 88, Year = 1932 },
                new MasterYearTable { Id = 89, Year = 1931 },
                new MasterYearTable { Id = 90, Year = 1930 }
            );
        }
    }
}
