using MecuryProduct.Data;
using MecuryProduct.Services;
using System.Text.Json;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace MecuryProduct.Components.Manager.Pages
{
    public partial class AddProduct
    {
        public ProductModel product = new ProductModel();
        public List<string> departments = new List<string>
        {
            "Department 1",
            "Department 2",
            "Department 3",
            "Department 4",
        };
        public List<string> grades = new List<string>
        {
            "A",
            "B",
            "C",
            "X",
        };
        public List<CompanyModel> companies = new List<CompanyModel>();
        public List<DocModel> productImages = new List<DocModel>();

        [Inject]
        private SessionService SessionService { get; set; }
        [Inject]
        private CompanyService CompanyService { get; set; }
        [Inject]
        private DocService DocService { get; set; }
        [Inject]
        private ProductService ProductService { get; set; }
        [Inject]
        private NavigationManager NavigationManager { get; set; }
        [Inject]
        private AuthenticationStateProvider AuthenticationStateProvider { get; set; }

        protected override async void OnInitialized()
        {
            var result = await SessionService.Get<ProductModel>("product_form");

            SetUserId();

            var session_vehicleImages = await SessionService.Get<List<DocModel>>("product_images");

            if (session_vehicleImages is not null)
            {
                productImages = session_vehicleImages;
            }

            if (result != null)
            {
                product = result;
            }
        }

        public async void CreateProduct()
        {
            product.created_at = DateTime.UtcNow;
            product.updated_at = DateTime.UtcNow;
            ProductService.AddProduct(product);
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
            NavigationManager.NavigateTo("/manager/products");
        }

        public async void SetInSession()
        {
            await SessionService.Set("product_form", JsonSerializer.Serialize(product));
        }

        public void DeleteDoc(DocModel doc)
        {
            productImages.Remove(doc);
            StateHasChanged();
        }

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

        public async void SetUserId()
        {
            var authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
            var user = authState.User;

            if (user.Identity is not null && user.Identity.IsAuthenticated)
            {
                var userId = user.FindFirst(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

                if (userId is not null)
                {
                    product.created_by_id = userId;
                    companies = CompanyService.GetCompaniesByManagerId(userId);
                }
            }
        }
    }
}
