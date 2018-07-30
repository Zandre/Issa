using System.Collections.Generic;
using BookALook.Classes;

namespace BookALook.MVC.ViewModel
{
    public class DressViewModel
    {
        public IEnumerable<Bodice> Bodices { get; set; }
        public IEnumerable<Skirt> Skirts { get; set; }
        public IEnumerable<Overlay> Overlays { get; set; }
    }
}