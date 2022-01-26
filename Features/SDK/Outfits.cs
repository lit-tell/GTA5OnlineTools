using static GTA5OnlineTools.Features.SDK.Hacks;

namespace GTA5OnlineTools.Features.SDK
{
    public class Outfits
    {
        /// <summary>
        /// Outfit Editor Globals
        /// </summary>
        private const int oWardrobeG = 2359296;
        private const int oWPointA = 5559;
        private const int oWPointB = 675;
        private const int oWComponent = 1333;
        private const int oWComponentTex = 1607;
        private const int oWProp = 1881;
        private const int oWPropTex = 2092;

        /// <summary>
        /// 范围0~19
        /// </summary>
        public static int OutfitIndex = 0;

        public static string GetOutfitNameByIndex()
        {
            return ReadGAString(oWardrobeG + (0 * oWPointA) + oWPointB + oWComponent + (OutfitIndex * 13) + 1126 - (OutfitIndex * 5));
        }

        public static void SetOutfitNameByIndex(string str)
        {
            WriteGAString(oWardrobeG + (0 * oWPointA) + oWPointB + oWComponent + (OutfitIndex * 13) + 1126 - (OutfitIndex * 5), str);
        }

        /*********************** TOP ***********************/

        public static int TOP
        {
            get => ReadGA<int>(oWardrobeG + (0 * oWPointA) + oWPointB + oWComponent + (OutfitIndex * 13) + 14);
            set => WriteGA<int>(oWardrobeG + (0 * oWPointA) + oWPointB + oWComponent + (OutfitIndex * 13) + 14, value);
        }

        public static int TOP_TEX
        {
            get => ReadGA<int>(oWardrobeG + (0 * oWPointA) + oWPointB + oWComponentTex + (OutfitIndex * 13) + 14);
            set => WriteGA<int>(oWardrobeG + (0 * oWPointA) + oWPointB + oWComponentTex + (OutfitIndex * 13) + 14, value);
        }

        /*********************** UNDERSHIRT ***********************/

        public static int UNDERSHIRT
        {
            get => ReadGA<int>(oWardrobeG + (0 * oWPointA) + oWPointB + oWComponent + (OutfitIndex * 13) + 11);
            set => WriteGA<int>(oWardrobeG + (0 * oWPointA) + oWPointB + oWComponent + (OutfitIndex * 13) + 11, value);
        }

        public static int UNDERSHIRT_TEX
        {
            get => ReadGA<int>(oWardrobeG + (0 * oWPointA) + oWPointB + oWComponentTex + (OutfitIndex * 13) + 11);
            set => WriteGA<int>(oWardrobeG + (0 * oWPointA) + oWPointB + oWComponentTex + (OutfitIndex * 13) + 11, value);
        }

        /*********************** LEGS ***********************/

        public static int LEGS
        {
            get => ReadGA<int>(oWardrobeG + (0 * oWPointA) + oWPointB + oWComponent + (OutfitIndex * 13) + 7);
            set => WriteGA<int>(oWardrobeG + (0 * oWPointA) + oWPointB + oWComponent + (OutfitIndex * 13) + 7, value);
        }

        public static int LEGS_TEX
        {
            get => ReadGA<int>(oWardrobeG + (0 * oWPointA) + oWPointB + oWComponentTex + (OutfitIndex * 13) + 7);
            set => WriteGA<int>(oWardrobeG + (0 * oWPointA) + oWPointB + oWComponentTex + (OutfitIndex * 13) + 7, value);
        }

        /*********************** FEET ***********************/

        public static int FEET
        {
            get => ReadGA<int>(oWardrobeG + (0 * oWPointA) + oWPointB + oWComponent + (OutfitIndex * 13) + 9);
            set => WriteGA<int>(oWardrobeG + (0 * oWPointA) + oWPointB + oWComponent + (OutfitIndex * 13) + 9, value);
        }

        public static int FEET_TEX
        {
            get => ReadGA<int>(oWardrobeG + (0 * oWPointA) + oWPointB + oWComponentTex + (OutfitIndex * 13) + 9);
            set => WriteGA<int>(oWardrobeG + (0 * oWPointA) + oWPointB + oWComponentTex + (OutfitIndex * 13) + 9, value);
        }

        /*********************** ACCESSORIES ***********************/

        public static int ACCESSORIES
        {
            get => ReadGA<int>(oWardrobeG + (0 * oWPointA) + oWPointB + oWComponent + (OutfitIndex * 13) + 10);
            set => WriteGA<int>(oWardrobeG + (0 * oWPointA) + oWPointB + oWComponent + (OutfitIndex * 13) + 10, value);
        }

        public static int ACCESSORIES_TEX
        {
            get => ReadGA<int>(oWardrobeG + (0 * oWPointA) + oWPointB + oWComponentTex + (OutfitIndex * 13) + 10);
            set => WriteGA<int>(oWardrobeG + (0 * oWPointA) + oWPointB + oWComponentTex + (OutfitIndex * 13) + 10, value);
        }

        /*********************** BAGS ***********************/

        public static int BAGS
        {
            get => ReadGA<int>(oWardrobeG + (0 * oWPointA) + oWPointB + oWComponent + (OutfitIndex * 13) + 8);
            set => WriteGA<int>(oWardrobeG + (0 * oWPointA) + oWPointB + oWComponent + (OutfitIndex * 13) + 8, value);
        }

        public static int BAGS_TEX
        {
            get => ReadGA<int>(oWardrobeG + (0 * oWPointA) + oWPointB + oWComponentTex + (OutfitIndex * 13) + 8);
            set => WriteGA<int>(oWardrobeG + (0 * oWPointA) + oWPointB + oWComponentTex + (OutfitIndex * 13) + 8, value);
        }

        /*********************** GLOVES ***********************/

        public static int GLOVES
        {
            get => ReadGA<int>(oWardrobeG + (0 * oWPointA) + oWPointB + oWComponent + (OutfitIndex * 13) + 6);
            set => WriteGA<int>(oWardrobeG + (0 * oWPointA) + oWPointB + oWComponent + (OutfitIndex * 13) + 6, value);
        }

        public static int GLOVES_TEX
        {
            get => ReadGA<int>(oWardrobeG + (0 * oWPointA) + oWPointB + oWComponentTex + (OutfitIndex * 13) + 6);
            set => WriteGA<int>(oWardrobeG + (0 * oWPointA) + oWPointB + oWComponentTex + (OutfitIndex * 13) + 6, value);
        }

        /*********************** DECALS ***********************/

        public static int DECALS
        {
            get => ReadGA<int>(oWardrobeG + (0 * oWPointA) + oWPointB + oWComponent + (OutfitIndex * 13) + 13);
            set => WriteGA<int>(oWardrobeG + (0 * oWPointA) + oWPointB + oWComponent + (OutfitIndex * 13) + 13, value);
        }

        public static int DECALS_TEX
        {
            get => ReadGA<int>(oWardrobeG + (0 * oWPointA) + oWPointB + oWComponentTex + (OutfitIndex * 13) + 13);
            set => WriteGA<int>(oWardrobeG + (0 * oWPointA) + oWPointB + oWComponentTex + (OutfitIndex * 13) + 13, value);
        }

        /*********************** MASK ***********************/

        public static int MASK
        {
            get => ReadGA<int>(oWardrobeG + (0 * oWPointA) + oWPointB + oWComponent + (OutfitIndex * 13) + 4);
            set => WriteGA<int>(oWardrobeG + (0 * oWPointA) + oWPointB + oWComponent + (OutfitIndex * 13) + 4, value);
        }

        public static int MASK_TEX
        {
            get => ReadGA<int>(oWardrobeG + (0 * oWPointA) + oWPointB + oWComponentTex + (OutfitIndex * 13) + 4);
            set => WriteGA<int>(oWardrobeG + (0 * oWPointA) + oWPointB + oWComponentTex + (OutfitIndex * 13) + 4, value);
        }

        /*********************** ARMOR ***********************/

        public static int ARMOR
        {
            get => ReadGA<int>(oWardrobeG + (0 * oWPointA) + oWPointB + oWComponent + (OutfitIndex * 13) + 12);
            set => WriteGA<int>(oWardrobeG + (0 * oWPointA) + oWPointB + oWComponent + (OutfitIndex * 13) + 12, value);
        }

        public static int ARMOR_TEX
        {
            get => ReadGA<int>(oWardrobeG + (0 * oWPointA) + oWPointB + oWComponentTex + (OutfitIndex * 13) + 12);
            set => WriteGA<int>(oWardrobeG + (0 * oWPointA) + oWPointB + oWComponentTex + (OutfitIndex * 13) + 12, value);
        }

        /********************************************************************************************/
        /********************************************************************************************/
        /********************************************************************************************/

        /*********************** HATS ***********************/

        public static int HATS
        {
            get => ReadGA<int>(oWardrobeG + (0 * oWPointA) + oWPointB + oWProp + (OutfitIndex * 10) + 3);
            set => WriteGA<int>(oWardrobeG + (0 * oWPointA) + oWPointB + oWProp + (OutfitIndex * 10) + 3, value);
        }

        public static int HATS_TEX
        {
            get => ReadGA<int>(oWardrobeG + (0 * oWPointA) + oWPointB + oWPropTex + (OutfitIndex * 10) + 3);
            set => WriteGA<int>(oWardrobeG + (0 * oWPointA) + oWPointB + oWPropTex + (OutfitIndex * 10) + 3, value);
        }

        /*********************** GLASSES ***********************/

        public static int GLASSES
        {
            get => ReadGA<int>(oWardrobeG + (0 * oWPointA) + oWPointB + oWProp + (OutfitIndex * 10) + 4);
            set => WriteGA<int>(oWardrobeG + (0 * oWPointA) + oWPointB + oWProp + (OutfitIndex * 10) + 4, value);
        }

        public static int GLASSES_TEX
        {
            get => ReadGA<int>(oWardrobeG + (0 * oWPointA) + oWPointB + oWPropTex + (OutfitIndex * 10) + 4);
            set => WriteGA<int>(oWardrobeG + (0 * oWPointA) + oWPointB + oWPropTex + (OutfitIndex * 10) + 4, value);
        }

        /*********************** EARS ***********************/

        public static int EARS
        {
            get => ReadGA<int>(oWardrobeG + (0 * oWPointA) + oWPointB + oWProp + (OutfitIndex * 10) + 5);
            set => WriteGA<int>(oWardrobeG + (0 * oWPointA) + oWPointB + oWProp + (OutfitIndex * 10) + 5, value);
        }

        public static int EARS_TEX
        {
            get => ReadGA<int>(oWardrobeG + (0 * oWPointA) + oWPointB + oWPropTex + (OutfitIndex * 10) + 5);
            set => WriteGA<int>(oWardrobeG + (0 * oWPointA) + oWPointB + oWPropTex + (OutfitIndex * 10) + 5, value);
        }

        /*********************** WATCHES ***********************/

        public static int WATCHES
        {
            get => ReadGA<int>(oWardrobeG + (0 * oWPointA) + oWPointB + oWProp + (OutfitIndex * 10) + 9);
            set => WriteGA<int>(oWardrobeG + (0 * oWPointA) + oWPointB + oWProp + (OutfitIndex * 10) + 9, value);
        }

        public static int WATCHES_TEX
        {
            get => ReadGA<int>(oWardrobeG + (0 * oWPointA) + oWPointB + oWPropTex + (OutfitIndex * 10) + 9);
            set => WriteGA<int>(oWardrobeG + (0 * oWPointA) + oWPointB + oWPropTex + (OutfitIndex * 10) + 9, value);
        }

        /*********************** WRIST ***********************/

        public static int WRIST
        {
            get => ReadGA<int>(oWardrobeG + (0 * oWPointA) + oWPointB + oWProp + (OutfitIndex * 10) + 10);
            set => WriteGA<int>(oWardrobeG + (0 * oWPointA) + oWPointB + oWProp + (OutfitIndex * 10) + 10, value);
        }

        public static int WRIST_TEX
        {
            get => ReadGA<int>(oWardrobeG + (0 * oWPointA) + oWPointB + oWPropTex + (OutfitIndex * 10) + 10);
            set => WriteGA<int>(oWardrobeG + (0 * oWPointA) + oWPointB + oWPropTex + (OutfitIndex * 10) + 10, value);
        }
    }
}
