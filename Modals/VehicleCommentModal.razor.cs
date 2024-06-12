using MecuryProduct.Data;
using MecuryProduct.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace MecuryProduct.Modals
{
    public partial class VehicleCommentModal
    {
        [Parameter] public int VehId { get; set; }
        [Parameter] public int? SfId { get; set; } = null;
        [Parameter] public bool Docs { get; set; } = false;
        public List<NoteModel> notes = new List<NoteModel>();
        public NoteModel note = new NoteModel();
        public CarModel car = new CarModel();
        public List<DocModel> docs = new List<DocModel>();
        public DocModel doc;
        public string doc_note = string.Empty;

        /// <summary>Injects services into the class properties.</summary>
        /// <remarks>
        /// This method injects the NoteService, AuthenticationStateProvider, CarService, and DocService into the corresponding properties of the class.
        /// </remarks>
        [Inject]
        private NoteService NoteService { get; set; }
        [Inject]
        private AuthenticationStateProvider AuthenticationStateProvider { get; set; }
        [Inject]
        private CarService CarService { get; set; }
        [Inject]
        private DocService DocService { get; set; }

        /// <summary>
        /// Initializes the object by setting the user ID, retrieving notes, and getting car information.
        /// </summary>
        protected override void OnInitialized()
        {
            SetUserId();
            GetNotes();
            GetCar();
        }

        /// <summary>
        /// Retrieves notes based on certain conditions.
        /// </summary>
        /// <remarks>
        /// If <see cref="SfId"/> is not null, retrieves notes using <see cref="NoteService.GetNotesByStateFormId"/>.
        /// If <see cref="SfId"/> is null and <see cref="Docs"/> is true, retrieves documents using <see cref="DocService.GetDocsByVehId"/> with type "doc".
        /// If <see cref="SfId"/> is null and <see cref="Docs"/> is false, retrieves notes using <see cref="NoteService.GetNotesByVehicleId"/> with doc_id as null.
        /// </remarks>
        public void GetNotes()
        {
            // PP-92 & 99: notes functionality
            // Bug: Document notes are showing in vehicle comments
            // Fix: Add if statement in else and filter accrording to the type and doc_id
            if (SfId != null)
            {
                notes = NoteService.GetNotesByStateFormId(SfId);
            }
            else
            {
                if (Docs)
                {
                    docs = DocService.GetDocsByVehId(VehId).FindAll(d => d.type.ToLower() == "doc").ToList();
                }
                else
                {
                    notes = NoteService.GetNotesByVehicleId(VehId).FindAll(d => d.doc_id == null).ToList();
                }
            }
        }

        /// <summary>
        /// Retrieves a car object based on the specified vehicle ID.
        /// </summary>
        /// <remarks>
        /// This method fetches a car object from the CarService using the provided vehicle ID.
        /// </remarks>
        public void GetCar()
        {
            car = CarService.GetCarById(VehId);
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

        /// <summary>
        /// Deletes a document from the system.
        /// </summary>
        /// <param name="doc">The document to be deleted.</param>
        public void DeleteDoc(DocModel doc)
        {
            DocService.DeleteDoc(doc);
            docs.Remove(doc);
            StateHasChanged();
        }

        /// <summary>
        /// Changes the documents based on the provided upload change event arguments.
        /// </summary>
        /// <param name="e">The Radzen.UploadChangeEventArgs containing information about the uploaded files.</param>
        /// <remarks>
        /// This method processes each uploaded file, saves it to the specified directory, and updates the document information.
        /// </remarks>
        public async void changeDocs(Radzen.UploadChangeEventArgs e)
        {
            string directory = Directory.GetCurrentDirectory();
            foreach (var file in e.Files)
            {
                var datetime = DateTime.UtcNow.ToString("yyyyMMddHHmmssfff");
                string filePath = $"{directory}/wwwroot/uploads/" + $"stk-{car.Id}-doc-{datetime}-{file.Name}";
                doc = new DocModel()
                {
                    file_name = file.Name,
                    file_path = filePath,
                    type = "doc",
                    server_name = "localhost",
                    veh_id = car.Id,
                    short_path = "uploads/" + $"stk-{car.Id}-doc-{datetime}-{file.Name}",
                    created_at = DateTime.UtcNow,
                    updated_at = DateTime.UtcNow
                };
                await using (var stream = file.OpenReadStream(long.MaxValue))
                {
                    await using (var fs = new FileStream(filePath, FileMode.Create))
                    {
                        await stream.CopyToAsync(fs);
                    }
                }
                DocService.AddDoc(doc);
                docs.Add(doc);
                StateHasChanged();
            }
        }

        /// <summary>Adds a note based on the provided information.</summary>
        /// <remarks>
        /// If SfId is null, sets the veh_id of the note to VehId; otherwise, sets the sf_id to SfId.
        /// If Docs is true and doc is not null, sets the doc_id of the note to the Id of the doc.
        /// Sets the created_at and updated_at fields of the note to the current UTC time.
        /// If Docs is true and both doc_note and doc are not null, adds the note using the NoteService.
        /// If Docs is false, adds the note using the NoteService.
        /// Finally, closes the dialog.
        /// </remarks>
        public void AddNote()
        {
            // PP-82: test state form comments
            // Bug: State form comments is not adding or even not saving in database
            // Fix: I added an else statement when sfId is not null then it sets SfId to note.sf_id
            if (SfId == null)
            {
                note.veh_id = VehId;
            }
            else
            {
                note.sf_id = SfId;
            }
            if (Docs && doc != null)
            {
                note.doc_id = doc.Id;
            }
            note.created_at = DateTime.UtcNow;
            note.updated_at = DateTime.UtcNow;
            if (Docs)
            {
                if (doc_note != null && doc != null)
                {
                    NoteService.AddNote(note);
                }
            }
            else
            {
                NoteService.AddNote(note);
            }
            dialogService.Close();
        }
    }
}
