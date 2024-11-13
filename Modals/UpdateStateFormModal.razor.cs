using MecuryProduct.Data;
using MecuryProduct.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System.Data;
using System.Security.Claims;

namespace MecuryProduct.Modals
{
    public partial class UpdateStateFormModal
    {
        [Parameter] public int SfId { get; set; }
        public StateFormModel state_form = new StateFormModel();
        public List<DocModel> envImages = new List<DocModel>();
        public List<CompanyModel> companies = new List<CompanyModel>();
        public string user_role = string.Empty;

        /// <summary>Injects the DocService and StateFormService dependencies.</summary>
        [Inject]
        private DocService DocService { get; set; }
        [Inject]
        private StateFormService StateFormService { get; set; }
        [Inject]
        private CompanyService CompanyService { get; set; }
        [Inject]
        private AuthenticationStateProvider AuthenticationStateProvider { get; set; }
        [Inject]
        private UserService UserService { get; set; }

        /// <summary>
        /// This method is called when the element is initialized.
        /// It retrieves the state form by its identifier.
        /// </summary>
        protected override void OnInitialized()
        {
            GetStateFormById();
            GetCompanyByUserId();
        }

        /// <summary>
        /// Updates the state form by setting the updated timestamp to the current UTC time,
        /// then calls the StateFormService to update the state form and closes the dialog.
        /// </summary>
        public void UpdateStateForm()
        {
            state_form.updated_at = DateTime.UtcNow;
            StateFormService.Update(state_form);
            dialogService.Close();
        }

        public async void GetCompanyByUserId()
        {
            var authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
            var user = authState.User;

            if (user.Identity is not null && user.Identity.IsAuthenticated)
            {
                var userId = user.FindFirst(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

                if (userId is not null)
                {
                    user_role = UserService.GetUserClaimByUserId(userId);

                    if (user_role == "Manager")
                    {
                        companies = CompanyService.GetCompaniesByManagerId(userId);
                    }
                    else if (user_role == "Employee")
                    {
                        companies = CompanyService.GetCompaniesByEmployeeId(userId);
                    }
                    else
                    {
                        companies = CompanyService.GetCompanies();
                    }
                }
            }
        }

        /// <summary>
        /// Retrieves a state form by its ID and updates the state_form and envImages properties accordingly.
        /// </summary>
        /// <remarks>
        /// This method fetches a StateFormModel object by its ID using the StateFormService. If a valid result is obtained,
        /// it updates the state_form property with the retrieved StateFormModel and assigns the document to the envImages list.
        /// </remarks>
        public async void GetStateFormById()
        {
            StateFormModel? result = StateFormService.GetById(SfId);
            if (result is not null)
            {
                state_form = result;
                envImages = [result.doc];
            }
        }

        /// <summary>
        /// Deletes a document and its associated images from the environment.
        /// </summary>
        /// <param name="doc">The document to be deleted.</param>
        public void DeleteDoc(DocModel doc)
        {
            DocService.DeleteDoc(doc);
            envImages.Remove(doc);
        }

        /// <summary>
        /// Changes the documents based on the uploaded files.
        /// </summary>
        /// <param name="e">The event arguments containing the uploaded files.</param>
        /// <remarks>
        /// This method updates the documents based on the files uploaded. It deletes the existing document if any,
        /// and then processes each uploaded file to save it to the specified directory.
        /// </remarks>
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
