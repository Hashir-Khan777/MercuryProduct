using MecuryProduct.Data;
using MecuryProduct.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System;
using System.Security.Claims;

namespace MecuryProduct.Components.Employee.Pages
{
    public partial class AddStateForm
    {
        public StateFormModel state_form = new StateFormModel();
        public List<DocModel> envImages = new List<DocModel>();
        public string note = string.Empty;
        public List<CompanyModel> companies = new List<CompanyModel>();
        public string user_id;

        /// <summary>Injects dependencies for the current component.</summary>
        /// <remarks>
        /// Dependencies injected:
        /// - DocService: Service for handling documents.
        /// - StateFormService: Service for managing form states.
        /// - NoteService: Service for managing notes.
        /// - NavigationManager: Manages navigation within the application.
        /// - AuthenticationStateProvider: Provides authentication state information.
        /// </remarks>
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
        [Inject]
        private CompanyService CompanyService { get; set; }
        [Inject]
        private SessionService SessionService { get; set; }

        protected override void OnInitialized()
        {
            SetUserId();
        }

        /// <summary>
        /// Creates a state form by adding a note with specified details and navigating to the inventory page.
        /// </summary>
        /// <remarks>
        /// This method sets the user ID, adds a note to the NoteService with the provided details,
        /// and then navigates to the inventory page.
        /// </remarks>
        public void CreateStateForm()
        {
            NoteService.AddNote(new NoteModel { created_by_id = user_id, sf_id = state_form.Id, note = note, created_at = DateTime.UtcNow, updated_at = DateTime.UtcNow });
            NavigationManager.NavigateTo("/employee/inventory");
        }

        /// <summary>
        /// Deletes a document and its associated data from the system.
        /// </summary>
        /// <param name="doc">The document model to be deleted.</param>
        /// <remarks>
        /// This method deletes the document from the document service, removes it from the list of environment images,
        /// deletes the associated state form, and triggers a state change to update the UI.
        /// </remarks>
        public void DeleteDoc(DocModel doc)
        {
            DocService.DeleteDoc(doc);
            envImages.Remove(doc);
            StateFormService.Delete(state_form);
            StateHasChanged();
        }

        /// <summary>
        /// Sets the user ID based on the authenticated user's information.
        /// </summary>
        /// <remarks>
        /// This method retrieves the authentication state asynchronously and extracts the user ID from the authenticated user's claims.
        /// If the user is authenticated and a user ID claim is found, it sets the user_id field to the extracted user ID.
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
                    user_id = userId;
                    companies = CompanyService.GetCompaniesByEmployeeId(userId);
                }
            }
        }

        /// <summary>
        /// Changes the documents based on the provided upload change event arguments.
        /// </summary>
        /// <param name="e">The Radzen.UploadChangeEventArgs containing information about the uploaded files.</param>
        /// <returns>Task representing the asynchronous operation.</returns>
        /// <remarks>
        /// This method performs the following actions:
        /// - Deletes the first document in the 'envImages' list if it exists.
        /// - Updates the 'created_at' and 'updated_at' properties of the 'state_form' object to the current UTC time.
        /// - Adds the 'state_form' object to the StateFormService.
        /// - Iterates through each uploaded file in the event arguments:
        ///   - Generates a unique file path based on the
        public async void changeDocs(Radzen.UploadChangeEventArgs e)
        {
            string directory = Directory.GetCurrentDirectory();
            if (envImages.Count() > 0)
            {
                DeleteDoc(envImages[0]);
            }
            state_form.created_at = DateTime.UtcNow;
            state_form.updated_at = DateTime.UtcNow;
            state_form.CompanyId = await SessionService.Get<int>("company");
            StateFormService.Add(state_form);

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
