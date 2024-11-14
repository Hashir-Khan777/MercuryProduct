using MecuryProduct.Data;
using MecuryProduct.Modals;
using MecuryProduct.Services;
using Microsoft.AspNetCore.Components;
using Radzen;

namespace MecuryProduct.Components.Manager.Pages
{
    public partial class Category
    {
        public List<CategoryModel> categories = new List<CategoryModel>();

        [Inject]
        public CategoryService CategoryService { get; set; }
        [Inject]
        public DialogService DialogService { get; set; }

        protected override void OnInitialized()
        {
            base.OnInitialized();

            categories = CategoryService.GetCategories();
        }

        public async void OpenUpdateCategoryModal(int catId)
        {
            await DialogService.OpenAsync<UpdateCategoryModal>("Update Category",
                new Dictionary<string, object>() { { "catId", catId } },
                new DialogOptions() { Width = "700px", Height = "90%", Resizable = true, Draggable = true }
            );
            StateHasChanged();
        }

        public async void DeleteCategory(CategoryModel category)
        {
            bool? deleteCustomer = await DialogService.Confirm("Are you sure?", "Do you want to delete category?", new ConfirmOptions() { OkButtonText = "Yes", CancelButtonText = "No" });
            if (deleteCustomer != null && deleteCustomer == true)
            {
                CategoryService.DeleteCategory(category);
                categories = CategoryService.GetCategories();
                StateHasChanged();
            }
        }
    }
}
