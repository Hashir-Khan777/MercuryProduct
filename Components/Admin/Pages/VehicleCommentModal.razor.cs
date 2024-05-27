using MecuryProduct.Data;
using MecuryProduct.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace MecuryProduct.Components.Admin.Pages
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

        [Inject]
        private NoteService NoteService { get; set; }
        [Inject]
        private AuthenticationStateProvider AuthenticationStateProvider { get; set; }
        [Inject]
        private CarService CarService { get; set; }
        [Inject]
        private DocService DocService { get; set; }

        protected override void OnInitialized()
        {
            SetUserId();
            GetNotes();
            GetCar();
        }

        public void GetNotes()
        {
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
                    notes = NoteService.GetNotesByVehicleId(VehId).ToList();
                }
            }
        }

        public void GetCar()
        {
            car = CarService.GetCarById(VehId);
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

        public void DeleteDoc(DocModel doc)
        {
            DocService.DeleteDoc(doc);
            docs.Remove(doc);
            StateHasChanged();
        }

        public async void changeDocs(Radzen.UploadChangeEventArgs e)
        {
            foreach (var file in e.Files)
            {
                var datetime = DateTime.UtcNow.ToString("yyyyMMddHHmmssfff");
                string filePath = @"E:\Zini Tecnologies Projects\MecuryProduct\wwwroot\uploads\" + $"stk-{car.Id}-doc-{datetime}-{file.Name}";
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

        public void AddNote()
        {
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
            if (doc_note != null && doc != null)
            {
                NoteService.AddNote(note);
            }
            dialogService.Close();
        }
    }
}
