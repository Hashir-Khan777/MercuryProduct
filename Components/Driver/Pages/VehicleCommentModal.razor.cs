using MecuryProduct.Data;
using MecuryProduct.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace MecuryProduct.Components.Driver.Pages
{
    public partial class VehicleCommentModal
    {
        /// <summary>Gets or sets the vehicle ID.</summary>
        /// <value>The vehicle ID.</value>
        [Parameter] public int VehId { get; set; }

        public List<NoteModel> notes = new List<NoteModel>();
        public NoteModel note = new NoteModel();

        /// <summary>Injects the NoteService and AuthenticationStateProvider dependencies.</summary>
        [Inject]
        private NoteService NoteService { get; set; }
        [Inject]
        private AuthenticationStateProvider AuthenticationStateProvider { get; set; }

        /// <summary>
        /// Initializes the object by setting the user ID and retrieving notes.
        /// </summary>
        protected override void OnInitialized()
        {
            SetUserId();
            GetNotes();
        }

        /// <summary>
        /// Retrieves notes associated with a specific vehicle.
        /// </summary>
        /// <remarks>
        /// This method fetches notes related to a vehicle based on its identifier and stores them in a list.
        /// </remarks>
        public void GetNotes()
        {
            notes = NoteService.GetNotesByVehicleId(VehId).ToList();
        }

        /// <summary>
        /// Sets the user ID based on the authenticated user's information.
        /// </summary>
        /// <remarks>
        /// This method retrieves the authentication state of the user and sets the user ID if the user is authenticated.
        /// </remarks>
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

        /// <summary>Adds a note to the system.</summary>
        /// <remarks>
        /// This method assigns the vehicle ID, sets the creation and update timestamps to the current UTC time,
        /// adds the note using the NoteService, and then closes the dialog window using the DialogService.
        /// </remarks>
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
