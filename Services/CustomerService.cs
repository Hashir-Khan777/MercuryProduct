using MecuryProduct.Data;
using Microsoft.EntityFrameworkCore;

namespace MecuryProduct.Services
{
    public class CustomerService
    {
        private readonly ApplicationDbContext db;

        public CustomerService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public void AddCustomer(CustomerModel customer)
        {
            db.Customers.Add(customer);
            db.SaveChanges();
        }

        public List<CustomerModel> GetCustomers()
        {
            return db.Customers.Include(c => c.created_by).Include(c => c.cars).OrderByDescending(c => c.created_at).ToList();
        }

        public CustomerModel? GetCustomerById(int CusId)
        {
            return db.Customers.Include(c => c.created_by).Include(c => c.cars).ThenInclude(c => c.driver).Include(u => u.cars).ThenInclude(c => c.created_by).FirstOrDefault(c => c.Id == CusId);
        }

        public void UpdateCustomer(CustomerModel customer)
        {
            db.Customers.Update(customer);
            db.SaveChanges();
        }

        public void DeleteCustomer(CustomerModel customer)
        {
            db.Customers.Remove(customer);
            db.SaveChanges();
        }
    }
}
