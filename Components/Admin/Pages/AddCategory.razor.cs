using MecuryProduct.Data;
using MecuryProduct.Services;
using Microsoft.AspNetCore.Components;

namespace MecuryProduct.Components.Admin.Pages
{
    public partial class AddCategory
    {
        public CategoryModel category = new CategoryModel();

        [Inject]
        public CategoryService CategoryService { get; set; }
        [Inject]
        public NavigationManager NavigationManager { get; set; }

        public void CreateCategory()
        {
            CategoryService.AddCategory(category);
            NavigationManager.NavigateTo("/admin/category");
        }
    }
}
