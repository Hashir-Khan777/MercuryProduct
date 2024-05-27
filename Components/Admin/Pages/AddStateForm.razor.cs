using MecuryProduct.Data;
using MecuryProduct.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System;
using System.Security.Claims;

namespace MecuryProduct.Components.Admin.Pages
{
    public partial class AddStateForm
    {
        public StateFormModel state_form = new StateFormModel();
        public List<DocModel> envImages = new List<DocModel>();
        public string note = string.Empty;
        public string user_id;

        [Inject]
        private DocService DocService { get; set; }
        [Inject]
        private StateFormService StateFormService { get; set; }
        [Inject]
        private NoteService NoteService { get; set; }
        [Inject]
        private NavigationManager NavigationManager { get; set; }
        [Inject]
        private AuthenticationStateProvider AuthenticationStateProvider { get; set; }

        public void CreateStateForm()
        {
            SetUserId();
            NoteService.AddNote(new NoteModel { created_by_id = user_id, sf_id = state_form.Id, note = note, created_at = DateTime.UtcNow, updated_at = DateTime.UtcNow });
            NavigationManager.NavigateTo("/admin/inventory");
        }

        public void DeleteDoc(DocModel doc)
        {
            DocService.DeleteDoc(doc);
            envImages.Remove(doc);
            StateFormService.Delete(state_form);
            StateHasChanged();
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
                    user_id = userId;
                }
            }
        }

        public async void changeDocs(Radzen.UploadChangeEventArgs e)
        {
            if (envImages.Count() > 0)
            {
                DeleteDoc(envImages[0]);
            }
            state_form.created_at = DateTime.UtcNow;
            state_form.updated_at = DateTime.UtcNow;
            StateFormService.Add(state_form);

            foreach (var file in e.Files)
            {
                var datetime = DateTime.UtcNow.ToString("yyyyMMddHHmmssfff");
                string filePath = @"E:\Zini Tecnologies Projects\MecuryProduct\wwwroot\uploads\" + $"env-{state_form.Id}-env-{datetime}-{file.Name}";
                DocModel doc = new DocModel()
                {
                    file_name = file.Name,
                    file_path = filePath,
                    type = "env",
                    sf_id = state_form.Id,
                    server_name = "localhost",
                    short_path = "uploads/" + $"env-{state_form.Id}-env-{datetime}-{file.Name}",
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
                envImages = [doc];
                StateHasChanged();
            }
        }
    }
}
