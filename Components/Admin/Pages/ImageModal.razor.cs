using MecuryProduct.Data;
using Microsoft.AspNetCore.Components;

namespace MecuryProduct.Components.Admin.Pages
{
    public partial class ImageModal
    {
        [Parameter] public List<DocModel> Paths { get; set; }
        [Parameter] public bool Slider { get; set; }

        public int index = 0;

        public void Next()
        {
            if (index + 1 >= Paths.Count())
            {
                index = 0;
            }
            else
            {
                index++;
            }
        }

        public void Previous()
        {
            if (index <= 0)
            {
                index = Paths.Count() - 1;
            }
            else
            {
                index--;
            }
        }
    }
}
