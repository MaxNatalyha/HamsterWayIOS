using System;
using System.Collections.Generic;
using SkinSystem;

namespace Services
{
    [Serializable]
    public class SkinsPair
    {
        public SkinCategories category;
        public List<Skin> skins;
    }
}