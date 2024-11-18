using MecuryProduct.Data;
using Microsoft.EntityFrameworkCore;
using Radzen;
using System.Text.Json;

namespace MecuryProduct.Services
{
    public class CustomerService
    {
        private readonly ApplicationDbContext db;
        private readonly NotificationService notificationService;
        private readonly HelperService helperService;

        /* The `CustomerService` class in the provided C# code snippet has a constructor method `public
        CustomerService(ApplicationDbContext db, NotificationService notificationService)`. This constructor
        initializes a new instance of the `CustomerService` class with two parameters: an
        `ApplicationDbContext` object named `db` and a `NotificationService` object named
        `notificationService`. */
        public CustomerService(ApplicationDbContext db, NotificationService notificationService, HelperService helperService)
        {
            this.db = db;
            this.notificationService = notificationService;
            this.helperService = helperService;
        }

        /// <summary>
        /// The AddCustomer function in C# adds a customer to a database and generates a search string based on
        /// the customer's name and phone number, handling any exceptions with a notification message.
        /// </summary>
        /// <param name="CustomerModel">CustomerModel is a model class that represents a customer entity with
        /// properties such as cfname (customer first name), clname (customer last name), cphone_number
        /// (customer phone number), and search (a concatenated string of customer's name and phone number for
        /// searching purposes).</param>
        public void AddCustomer(CustomerModel customer)
        {
            try
            {
                customer.search = $"{customer.cfname} {customer.clname} {customer.cphone_number}";
                db.Customers.Add(customer);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                helperService.WriteLog(exception: $"{ex}");
                var notificationMessage = new NotificationMessage { Severity = NotificationSeverity.Error, Detail = ex.Message, Duration = 4000 };
                notificationService.Notify(notificationMessage);
            }
        }

        /// <summary>
        /// The function `GetCustomers` retrieves a list of customers from a database, including related
        /// entities, and handles exceptions by notifying with an error message.
        /// </summary>
        /// <returns>
        /// A `List<CustomerModel>` is being returned from the `GetCustomers` method. If an exception occurs, a
        /// notification message is created and the method returns `null`.
        /// </returns>
        public List<CustomerModel>? GetCustomers()
        {
            try
            {
                return db.Customers.Include(c => c.created_by).Include(c => c.cars).Include(c => c.Company).OrderByDescending(c => c.created_at).ToList();
            }
            catch (Exception ex)
            {
                helperService.WriteLog(exception: $"{ex}");
                var notificationMessage = new NotificationMessage { Severity = NotificationSeverity.Error, Detail = ex.Message, Duration = 4000 };
                notificationService.Notify(notificationMessage);
                return null;
            }
        }

        public List<CustomerModel>? GetCustomersByManagerId(string ManagerId)
        {
            try
            {
                return db.Customers.Include(c => c.created_by).Include(c => c.cars).Include(c => c.Company).ThenInclude(c => c.CompanyManagers).Where(c => c.Company.CompanyManagers.Any(x => x.manager_id == ManagerId)).OrderByDescending(c => c.created_at).ToList();
            }
            catch (Exception ex)
            {
                helperService.WriteLog(exception: $"{ex}");
                var notificationMessage = new NotificationMessage { Severity = NotificationSeverity.Error, Detail = ex.Message, Duration = 4000 };
                notificationService.Notify(notificationMessage);
                return null;
            }
        }

        public List<CustomerModel>? GetAllCustomersByCompanyId(int company_id)
        {
            try
            {
                return db.Customers.Include(c => c.created_by).Include(c => c.cars).Include(c => c.Company).ThenInclude(c => c.CompanyManagers).Where(c => c.CompanyId == company_id).OrderByDescending(c => c.created_at).ToList();
            }
            catch (Exception ex)
            {
                helperService.WriteLog(exception: $"{ex}");
                var notificationMessage = new NotificationMessage { Severity = NotificationSeverity.Error, Detail = ex.Message, Duration = 4000 };
                notificationService.Notify(notificationMessage);
                return null;
            }
        }

        public List<CustomerModel>? GetCustomersByCompanyId(int company_id)
        {
            try
            {
                return db.Customers.Where(x => x.deleted == false).Include(c => c.created_by).Include(c => c.cars).Include(c => c.Company).ThenInclude(c => c.CompanyManagers).Where(c => c.CompanyId == company_id).OrderByDescending(c => c.created_at).ToList();
            }
            catch (Exception ex)
            {
                helperService.WriteLog(exception: $"{ex}");
                var notificationMessage = new NotificationMessage { Severity = NotificationSeverity.Error, Detail = ex.Message, Duration = 4000 };
                notificationService.Notify(notificationMessage);
                return null;
            }
        }

        public List<CustomerModel>? GetCustomersByEmployeeId(string EmployeeId)
        {
            try
            {
                return db.Customers.Include(c => c.created_by).Include(c => c.cars).Include(c => c.Company).ThenInclude(c => c.CompanyManagers).Where(c => c.Company.CompanyEmployees.Any(x => x.employee_id == EmployeeId)).OrderByDescending(c => c.created_at).ToList();
            }
            catch (Exception ex)
            {
                helperService.WriteLog(exception: $"{ex}");
                var notificationMessage = new NotificationMessage { Severity = NotificationSeverity.Error, Detail = ex.Message, Duration = 4000 };
                notificationService.Notify(notificationMessage);
                return null;
            }
        }

        public List<CustomerModel>? GetCustomersByCompanyId(int? CompanyId)
        {
            try
            {
                return db.Customers.Where(c => c.CompanyId == CompanyId).Include(c => c.created_by).Include(c => c.cars).Include(c => c.Company).OrderByDescending(c => c.created_at).ToList();
            }
            catch (Exception ex)
            {
                helperService.WriteLog(exception: $"{ex}");
                var notificationMessage = new NotificationMessage { Severity = NotificationSeverity.Error, Detail = ex.Message, Duration = 4000 };
                notificationService.Notify(notificationMessage);
                return null;
            }
        }

        /// <summary>
        /// The function `GetCustomerById` retrieves a customer by their ID from a database, handling exceptions
        /// and returning a nullable `CustomerModel`.
        /// </summary>
        /// <param name="CusId">The `CusId` parameter is an integer representing the unique identifier of the
        /// customer you want to retrieve from the database.</param>
        /// <returns>
        /// The `GetCustomerById` method is returning a `CustomerModel` object if the customer with the
        /// specified `CusId` is found in the database. If an exception occurs during the database query, it
        /// catches the exception, creates a notification message with the error details, notifies the
        /// `notificationService`, and then returns `null`.
        /// </returns>
        public CustomerModel? GetCustomerById(int CusId)
        {
            try
            {
                return db.Customers.Include(c => c.created_by).Include(c => c.cars).ThenInclude(c => c.driver).Include(u => u.cars).ThenInclude(c => c.created_by).FirstOrDefault(c => c.Id == CusId);
            }
            catch (Exception ex)
            {
                helperService.WriteLog(exception: $"{ex}");
                var notificationMessage = new NotificationMessage { Severity = NotificationSeverity.Error, Detail = ex.Message, Duration = 4000 };
                notificationService.Notify(notificationMessage);
                return null;
            }
        }

        /// <summary>
        /// The function `GetCustomerByPhoneNumber` retrieves a customer by their phone number from a database,
        /// handling exceptions and returning a nullable `CustomerModel`.
        /// </summary>
        /// <param name="phone_number">The `GetCustomerByPhoneNumber` method is designed to retrieve a
        /// `CustomerModel` object based on the provided `phone_number`. The method attempts to fetch the
        /// customer from the database using Entity Framework's `Include` method to eagerly load related
        /// entities like `created_by`, `cars`, and `driver</param>
        /// <returns>
        /// The method `GetCustomerByPhoneNumber` is returning a `CustomerModel` object that corresponds to the
        /// customer with the provided phone number. If an exception occurs during the database query, it
        /// catches the exception, creates a notification message with the error details, notifies the user
        /// using the `notificationService`, and then returns `null`.
        /// </returns>
        public CustomerModel? GetCustomerByPhoneNumber(string phone_number)
        {
            try
            {
                return db.Customers.Include(c => c.created_by).Include(c => c.cars).ThenInclude(c => c.driver).Include(u => u.cars).ThenInclude(c => c.created_by).FirstOrDefault(c => c.cphone_number == phone_number);
            }
            catch (Exception ex)
            {
                helperService.WriteLog(exception: $"{ex}");
                var notificationMessage = new NotificationMessage { Severity = NotificationSeverity.Error, Detail = ex.Message, Duration = 4000 };
                notificationService.Notify(notificationMessage);
                return null;
            }
        }

        /// <summary>
        /// The UpdateCustomer function updates a customer record in a database and notifies the user of any
        /// errors that occur.
        /// </summary>
        /// <param name="CustomerModel">CustomerModel is a model class that represents a customer entity. It
        /// likely contains properties such as cfname (customer first name), clname (customer last name),
        /// cphone_number (customer phone number), and possibly other customer-related information.</param>
        public void UpdateCustomer(CustomerModel customer)
        {
            try
            {
                customer.search = $"{customer.cfname} {customer.clname} {customer.cphone_number}";
                db.Customers.Update(customer);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                helperService.WriteLog(exception: $"{ex}");
                var notificationMessage = new NotificationMessage { Severity = NotificationSeverity.Error, Detail = ex.Message, Duration = 4000 };
                notificationService.Notify(notificationMessage);
            }
        }

        /// <summary>
        /// The DeleteCustomer function removes a customer from the database and handles any exceptions by
        /// notifying with an error message.
        /// </summary>
        /// <param name="CustomerModel">CustomerModel is a class that represents a customer entity in the
        /// application. It likely contains properties such as customer ID, name, address, contact information,
        /// etc. The DeleteCustomer method takes an instance of CustomerModel as a parameter and attempts to
        /// remove that customer from the database. If an exception occurs during</param>
        public void DeleteCustomer(CustomerModel customer)
        {
            try
            {
                customer.deleted = true;
                db.Customers.Update(customer);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                helperService.WriteLog(exception: $"{ex}");
                var notificationMessage = new NotificationMessage { Severity = NotificationSeverity.Error, Detail = ex.Message, Duration = 4000 };
                notificationService.Notify(notificationMessage);
            }
        }

        public List<CustomerModel>? SearchCustomerListByPhoneNumber(string phone_number)
        {
            try
            {
                return db.Customers.Where(c => c.cphone_number.Contains(phone_number)).ToList();
            }
            catch (Exception ex)
            {
                helperService.WriteLog(exception: $"{ex}");
                var notificationMessage = new NotificationMessage { Severity = NotificationSeverity.Error, Detail = ex.Message, Duration = 4000 };
                notificationService.Notify(notificationMessage);
                return null;
            }
        }
    }
}
