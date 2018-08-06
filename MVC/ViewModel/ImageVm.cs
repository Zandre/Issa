using System;
using BookALook.Classes;

namespace MVC.ViewModel
{
    public class ImageVm
    {
        public BaseWeddingGownItem _baseWeddingGownItem;

        public ImageVm(BaseWeddingGownItem baseWeddingGownItem)
        {
            _baseWeddingGownItem = baseWeddingGownItem;

            Id = baseWeddingGownItem.Id;
            Name = baseWeddingGownItem.Name;
            if (baseWeddingGownItem.ImageData != null)
            {
                ImageData = baseWeddingGownItem.ImageData;
            }
            else
            {
                ImageData = new Byte[64];
            }
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public byte[] ImageData { get; set; }


        //to do These 3 methods needs to be cleaned up in something slick involving an enum
        //... but this hack will work for nor
        public int ItemType
        {
            get
            {
                if (_baseWeddingGownItem is Bodice)
                {
                    return 1;
                }
                if (_baseWeddingGownItem is Skirt)
                {
                    return 2;
                }
                if (_baseWeddingGownItem is Overlay)
                {
                    return 3;
                }

                return -1;
            }
        }

        public string ItemTypeString
        {
            get
            {
                if (_baseWeddingGownItem is Bodice)
                {
                    return "Bodices";
                }
                if (_baseWeddingGownItem is Skirt)
                {
                    return "Skirts";
                }
                if (_baseWeddingGownItem is Overlay)
                {
                    return "Overlays";
                }

                return "";
            }
        }

        public string ItemTypeDetailsFormString
        {
            get
            {
                if (_baseWeddingGownItem is Bodice)
                {
                    return "BodiceDetailsForm";
                }
                if (_baseWeddingGownItem is Skirt)
                {
                    return "SkirtDetailsForm";
                }
                if (_baseWeddingGownItem is Overlay)
                {
                    return "OverlayDetailsForm";
                }

                return "";
            }
        }
    }
}