using MecuryProduct.Data;
using MecuryProduct.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace MecuryProduct.Modals
{
    public partial class UpdateProductModal
    {
        [Parameter] public int ProductId { get; set; }

        public ProductModel product = new ProductModel();
        public string user_role = string.Empty;
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
        private CompanyService CompanyService { get; set; }
        [Inject]
        private DocService DocService { get; set; }
        [Inject]
        private CategoryService CategoryService { get; set; }
        [Inject]
        private ProductService ProductService { get; set; }
        [Inject]
        private UserService UserService { get; set; }
        [Inject]
        private AuthenticationStateProvider AuthenticationStateProvider { get; set; }

        protected override async void OnInitialized()
        {
            product = ProductService.GetProductById(ProductId);
            productImages = product.images;
            categories = CategoryService.GetCategories();
            GetCompanyByUserId();
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
                    var role = UserService.GetUserClaimByUserId(userId);
                    user_role = role;

                    if (role == "Manager")
                    {
                        companies = CompanyService.GetCompaniesByManagerId(userId);
                    }
                    else if (role == "Employee")
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

        public async void UpdateProduct()
        {
            product.updated_at = DateTime.UtcNow;
            ProductService.UpdateProduct(product);
            dialogService.Close();
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
                    product_id = ProductId,
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
                DocService.AddDoc(doc);
                StateHasChanged();
            }
        }
    }
}
