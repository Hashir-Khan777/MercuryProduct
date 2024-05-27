using MecuryProduct.Data;
using Microsoft.EntityFrameworkCore;

namespace MecuryProduct.Services
{
    public class StateFormService
    {
        private readonly ApplicationDbContext db;

        public StateFormService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public void Add(StateFormModel state_form)
        {
            db.StateForm.Add(state_form);
            db.SaveChanges();
        }

        public List<StateFormModel> Get()
        {
            return db.StateForm.Include(s => s.doc).ToList();
        }

        public StateFormModel? GetById(int id)
        {
            return db.StateForm.FirstOrDefault(c => c.Id == id);
        }

        public void Delete(StateFormModel state_form)
        {
            db.StateForm.Remove(state_form);
            db.SaveChanges();
        }

        public void Update(StateFormModel state_form)
        {
            db.StateForm.Update(state_form);
            db.SaveChanges();
        }
    }
}
