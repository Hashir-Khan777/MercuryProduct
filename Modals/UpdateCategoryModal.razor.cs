using MecuryProduct.Data;
using MecuryProduct.Services;
using Microsoft.AspNetCore.Components;

namespace MecuryProduct.Modals
{
    public partial class UpdateCategoryModal
    {
        [Parameter] public int catId { get; set; }
        public CategoryModel category = new CategoryModel();

        [Inject]
        public CategoryService CategoryService { get; set; }
        [Inject]
        public NavigationManager NavigationManager { get; set; }

        protected override void OnInitialized()
        {
            base.OnInitialized();

            category = CategoryService.GetCategoryById(catId);
        }

        public void UpdateCategory()
        {
            CategoryService.UpdateCategory(category);
            dialogService.Close();
        }
    }
}
