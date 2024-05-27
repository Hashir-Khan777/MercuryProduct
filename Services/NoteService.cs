using MecuryProduct.Data;
using Microsoft.EntityFrameworkCore;

namespace MecuryProduct.Services
{
    public class NoteService
    {
        private readonly ApplicationDbContext db;

        public NoteService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public List<NoteModel> GetNotesByVehicleId(int VehId)
        {
            return db.Notes.Where(n => n.veh_id == VehId).Include(n => n.created_by).ToList();
        }

        public List<NoteModel> GetNotesByStateFormId(int? sf_id)
        {
            return db.Notes.Where(n => n.sf_id == sf_id).Include(n => n.created_by).ToList();
        }

        public void AddNote(NoteModel note)
        {
            db.Notes.Add(note);
            db.SaveChanges();
        }
    }
}
