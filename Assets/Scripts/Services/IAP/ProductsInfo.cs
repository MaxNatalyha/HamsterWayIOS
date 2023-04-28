using System;
using UnityEngine;

namespace Services
{
    [Serializable]
    public class ProductsInfo
    {
        public string fastFoodPackID = "FastFood_Pack";
        public Sprite fastFoodPackPreview;

        [Space(10)]
        public string turnOffAdsID = "No_ADS_Forever";
        public Sprite noAdsPreview;

        [Space(10)]
        public string money5KID = "5000_Wrappers";
        public int money5КQuantity = 5000;
        public Sprite money5KPreview;

        [Space(10)]
        public string money10KID = "10000_Wrappers";
        public int money10KQuantity = 10000;
        public Sprite money10KPreview;

        [Space(10)]
        public string money15KID = "15000_Wrappers";
        public int money15KQuantity = 15000;
        public Sprite money15KPreview;
    }
}