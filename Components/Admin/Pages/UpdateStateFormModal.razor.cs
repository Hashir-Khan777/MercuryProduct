using MecuryProduct.Data;
using MecuryProduct.Services;
using Microsoft.AspNetCore.Components;

namespace MecuryProduct.Components.Admin.Pages
{
    public partial class UpdateStateFormModal
    {
        [Parameter] public int SfId {  get; set; }
        public StateFormModel state_form = new StateFormModel();
        public List<DocModel> envImages = new List<DocModel>();

        [Inject]
        private DocService DocService { get; set; }
        [Inject]
        private StateFormService StateFormService { get; set; }

        protected override void OnInitialized()
        {
            GetStateFormById();
        }

        public void UpdateStateForm()
        {
            state_form.updated_at = DateTime.UtcNow;
            StateFormService.Update(state_form);
            dialogService.Close();
        }

        public async void GetStateFormById()
        {
            StateFormModel? result = StateFormService.GetById(SfId);
            if (result is not null)
            {
                state_form = result;
                envImages = [result.doc];
            }
        }

        public void DeleteDoc(DocModel doc)
        {
            DocService.DeleteDoc(doc);
            envImages.Remove(doc);
            state_form = new StateFormModel();
        }

        public async void changeDocs(Radzen.UploadChangeEventArgs e)
        {
            string directory = Directory.GetCurrentDirectory();
            if (envImages.Count() > 0)
            {
                DeleteDoc(envImages[0]);
            }
            foreach (var file in e.Files)
            {
                var datetime = DateTime.UtcNow.ToString("yyyyMMddHHmmssfff");
                string filePath = $"{directory}/wwwroot/uploads/" + $"env-{state_form.Id}-env-{datetime}-{file.Name}";
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
