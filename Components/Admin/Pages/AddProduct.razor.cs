using MecuryProduct.Data;
using MecuryProduct.Services;
using System.Text.Json;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace MecuryProduct.Components.Admin.Pages
{
    public partial class AddProduct
    {
        /// Create a new instance of ProductModel and its sub - models. This is used to determine which fields are affected
        public ProductModel product = new ProductModel();
        public LocalizationModel localization = new LocalizationModel();
        public List<CategoryModel> categories = new List<CategoryModel>();
        public List<string> grades = new List<string>
        {
            "A",
            "B",
            "C",
            "X",
        };
        public List<CompanyModel> companies = new List<CompanyModel>();
        public List<DocModel> productImages = new List<DocModel>();
        public bool taxable = true;

        [Inject]
        private SessionService SessionService { get; set; }
        [Inject]
        private CompanyService CompanyService { get; set; }
        [Inject]
        private DocService DocService { get; set; }
        [Inject]
        private ProductService ProductService { get; set; }
        [Inject]
        private CategoryService CategoryService { get; set; }
        [Inject]
        private NavigationManager NavigationManager { get; set; }
        [Inject]
        private AuthenticationStateProvider AuthenticationStateProvider { get; set; }

        /// @brief Called when [ initialized ]. Initializes the instance by getting the data from
        protected override async void OnInitialized()
        {
            var result = await SessionService.Get<ProductModel>("product_form");
            var local_result = await SessionService.Get<LocalizationModel>("localization");
            companies = CompanyService.GetCompanies().ToList();
            categories = CategoryService.GetCategories().ToList();

            SetUserId();

            var session_vehicleImages = await SessionService.Get<List<DocModel>>("product_images");

            /// Set product product images for the vehicle
            if (session_vehicleImages is not null)
            {
                productImages = session_vehicleImages;
            }

            if (local_result is not null)
            {
                localization = local_result;
            }

            /// Set the product of the result.
            if (result != null)
            {
                product = result;
            }
        }

        /// @brief Creates and navigates to the product page after setting the created_at and updated
        public async void CreateProduct()
        {
            product.created_at = DateTime.UtcNow;
            product.updated_at = DateTime.UtcNow;
            product.company_id = await SessionService.Get<int>("company");
            if (taxable)
            {
                product.vat = localization.salesTax;
            }
            ProductService.AddProduct(product);
            /// Add all the product images to the collection
            if (productImages.Count() > 0)
            {
                foreach (var item in productImages)
                {
                    item.product_id = product.Id;
                    DocService.AddDoc(item);
                }
                productImages = new List<DocModel>();
            }
            await SessionService.Clear("product_form");
            await SessionService.Clear("product_images");
            NavigationManager.NavigateTo("/admin/products");
        }

        /// Sets the in session. This is called when the user clicks the submit button
        public async void SetInSession()
        {
            await SessionService.Set("product_form", JsonSerializer.Serialize(product));
        }

        /// Deletes the specified document from the list of documents. This is useful when you want to remove a document from the list and don't want to show the image that was used to create it.
        /// 
        /// @param doc - The document to delete from the list of documents
        public void DeleteDoc(DocModel doc)
        {
            productImages.Remove(doc);
            StateHasChanged();
        }

        /// Adds or updates product images. This is called when files are uploaded to the web server
        /// 
        /// @param e - The instance containing the event data
        public async void changeProductImages(Radzen.UploadChangeEventArgs e)
        {
            string directory = Directory.GetCurrentDirectory();
            foreach (var file in e.Files)
            {
                var datetime = DateTime.UtcNow.ToString("yyyyMMddHHmmssfff");
                string filePath = $"{directory}/wwwroot/uploads/" + $"stk-{product.Id}-product-{datetime}-{file.Name}";
                DocModel doc = new DocModel()
                {
                    file_name = file.Name,
                    file_path = filePath,
                    type = "product",
                    server_name = "localhost",
                    short_path = "uploads/" + $"stk-{product.Id}-product-{datetime}-{file.Name}",
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
                productImages.Add(doc);
                await SessionService.Set("product_images", JsonSerializer.Serialize(productImages));
                StateHasChanged();
            }
        }

        /// Sets the user ID if logged in and the user is authenticated. This is used to create the
        public async void SetUserId()
        {
            var authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
            var user = authState.User;

            /// If the user is authenticated set product. created_by_id to product. created_by_id
            if (user.Identity is not null && user.Identity.IsAuthenticated)
            {
                var userId = user.FindFirst(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

                /// Set the user id of the product created by this user
                if (userId is not null)
                {
                    product.created_by_id = userId;
                }
            }
        }
    }
}
