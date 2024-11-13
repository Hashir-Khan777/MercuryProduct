using MecuryProduct.Data;
using Microsoft.AspNetCore.Components;

namespace MecuryProduct.Modals
{
    public partial class ImageModal
    {
        [Parameter] public List<DocModel> Paths { get; set; }
        [Parameter] public bool Slider { get; set; }

        public int index = 0;

        /// <summary>Move to the next item in the collection of paths.</summary>
        /// <remarks>
        /// If the current index is at the end of the collection, it wraps around to the beginning.
        /// </remarks>
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

        /// <summary>
        /// Moves to the previous item in the collection.
        /// </summary>
        /// <remarks>
        /// If the current index is at the beginning of the collection, it wraps around to the end.
        /// </remarks>
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
