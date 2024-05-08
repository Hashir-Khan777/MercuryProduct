using MecuryProduct.Data;
using MecuryProduct.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace MecuryProduct.Components.Driver.Pages
{
    public partial class VehicleCommentModal
    {
        [Parameter] public int VehId { get; set; }
        public List<NoteModel> notes = new List<NoteModel>();
        public NoteModel note = new NoteModel();

        [Inject]
        private NoteService NoteService { get; set; }
        [Inject]
        private AuthenticationStateProvider AuthenticationStateProvider { get; set; }

        protected override void OnInitialized()
        {
            SetUserId();
            GetNotes();
        }

        public void GetNotes()
        {
            notes = NoteService.GetNotesByVehicleId(VehId).ToList();
        }

        public async void SetUserId()
        {
            var authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
            var user = authState.User;

            if (user.Identity is not null && user.Identity.IsAuthenticated)
            {
                var userId = user.FindFirst(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

                if (userId is not null)
                {
                    note.created_by_id = userId;
                }
            }
        }

        public void AddNote()
        {
            note.veh_id = VehId;
            note.created_at = DateTime.UtcNow;
            note.updated_at = DateTime.UtcNow;
            NoteService.AddNote(note);
            dialogService.Close();
        }
    }
}
