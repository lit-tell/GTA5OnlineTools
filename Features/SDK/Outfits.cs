using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GTA5OnlineTools.Features.SDK
{
    public class Outfits
    {
        /// <summary>
        /// 范围0~19
        /// </summary>
        public static int OutfitIndex = 0;

        public static string GetOutfitNameByIndex() { return Globals.Get_Outfit_Name_By_Index(OutfitIndex); }

        public static void SetOutfitNameByIndex(string str) { Globals.Set_Outfit_Name_By_Index(OutfitIndex, str); }

        /*********************** TOP ***********************/

        public static int TOP
        {
            get => Globals.Get_Top(OutfitIndex);
            set => Globals.Set_Top(OutfitIndex, value);
        }

        public static int TOP_TEX
        {
            get => Globals.Get_Top_Tex(OutfitIndex);
            set => Globals.Set_Top_Tex(OutfitIndex, value);
        }

        /*********************** UNDERSHIRT ***********************/

        public static int UNDERSHIRT
        {
            get => Globals.Get_Undershirt(OutfitIndex);
            set => Globals.Set_Undershirt(OutfitIndex, value);
        }

        public static int UNDERSHIRT_TEX
        {
            get => Globals.Get_Undershirt_Tex(OutfitIndex);
            set => Globals.Set_Undershirt_Tex(OutfitIndex, value);
        }

        /*********************** LEGS ***********************/

        public static int LEGS
        {
            get => Globals.Get_Legs(OutfitIndex);
            set => Globals.Set_Legs(OutfitIndex, value);
        }

        public static int LEGS_TEX
        {
            get => Globals.Get_Legs_Tex(OutfitIndex);
            set => Globals.Set_Legs_Tex(OutfitIndex, value);
        }

        /*********************** FEET ***********************/

        public static int FEET
        {
            get => Globals.Get_Feet(OutfitIndex);
            set => Globals.Set_Feet(OutfitIndex, value);
        }

        public static int FEET_TEX
        {
            get => Globals.Get_Feet_Tex(OutfitIndex);
            set => Globals.Set_Feet_Tex(OutfitIndex, value);
        }

        /*********************** ACCESSORIES ***********************/

        public static int ACCESSORIES
        {
            get => Globals.Get_Accessories(OutfitIndex);
            set => Globals.Set_Accessories(OutfitIndex, value);
        }

        public static int ACCESSORIES_TEX
        {
            get => Globals.Get_Accessories_Tex(OutfitIndex);
            set => Globals.Set_Accessories_Tex(OutfitIndex, value);
        }

        /*********************** BAGS ***********************/

        public static int BAGS
        {
            get => Globals.Get_Bags(OutfitIndex);
            set => Globals.Set_Bags(OutfitIndex, value);
        }

        public static int BAGS_TEX
        {
            get => Globals.Get_Bags_Tex(OutfitIndex);
            set => Globals.Set_Bags_Tex(OutfitIndex, value);
        }

        /*********************** GLOVES ***********************/

        public static int GLOVES
        {
            get => Globals.Get_Gloves(OutfitIndex);
            set => Globals.Set_Gloves(OutfitIndex, value);
        }

        public static int GLOVES_TEX
        {
            get => Globals.Get_Gloves_Tex(OutfitIndex);
            set => Globals.Set_Gloves_Tex(OutfitIndex, value);
        }

        /*********************** DECALS ***********************/

        public static int DECALS
        {
            get => Globals.Get_Decals(OutfitIndex);
            set => Globals.Set_Decals(OutfitIndex, value);
        }

        public static int DECALS_TEX
        {
            get => Globals.Get_Decals_Tex(OutfitIndex);
            set => Globals.Set_Decals_Tex(OutfitIndex, value);
        }

        /*********************** MASK ***********************/

        public static int MASK
        {
            get => Globals.Get_Mask(OutfitIndex);
            set => Globals.Set_Mask(OutfitIndex, value);
        }

        public static int MASK_TEX
        {
            get => Globals.Get_Mask_Tex(OutfitIndex);
            set => Globals.Set_Mask_Tex(OutfitIndex, value);
        }

        /*********************** ARMOR ***********************/

        public static int ARMOR
        {
            get => Globals.Get_Armor(OutfitIndex);
            set => Globals.Set_Armor(OutfitIndex, value);
        }

        public static int ARMOR_TEX
        {
            get => Globals.Get_Armor_Tex(OutfitIndex);
            set => Globals.Set_Armor_Tex(OutfitIndex, value);
        }

        /********************************************************************************************/
        /********************************************************************************************/
        /********************************************************************************************/

        /*********************** HATS ***********************/

        public static int HATS
        {
            get => Globals.Get_Hats(OutfitIndex);
            set => Globals.Set_Hats(OutfitIndex, value);
        }

        public static int HATS_TEX
        {
            get => Globals.Get_Hats_Tex(OutfitIndex);
            set => Globals.Set_Hats_Tex(OutfitIndex, value);
        }

        /*********************** GLASSES ***********************/

        public static int GLASSES
        {
            get => Globals.Get_Glasses(OutfitIndex);
            set => Globals.Set_Glasses(OutfitIndex, value);
        }

        public static int GLASSES_TEX
        {
            get => Globals.Get_Glasses_Tex(OutfitIndex);
            set => Globals.Set_Glasses_Tex(OutfitIndex, value);
        }

        /*********************** EARS ***********************/

        public static int EARS
        {
            get => Globals.Get_Ears(OutfitIndex);
            set => Globals.Set_Ears(OutfitIndex, value);
        }

        public static int EARS_TEX
        {
            get => Globals.Get_Ears_Tex(OutfitIndex);
            set => Globals.Set_Ears_Tex(OutfitIndex, value);
        }

        /*********************** WATCHES ***********************/

        public static int WATCHES
        {
            get => Globals.Get_Watches(OutfitIndex);
            set => Globals.Set_Watches(OutfitIndex, value);
        }

        public static int WATCHES_TEX
        {
            get => Globals.Get_Watches_Tex(OutfitIndex);
            set => Globals.Set_Watches_Tex(OutfitIndex, value);
        }

        /*********************** WRIST ***********************/

        public static int WRIST
        {
            get => Globals.Get_Wrist(OutfitIndex);
            set => Globals.Set_Wrist(OutfitIndex, value);
        }

        public static int WRIST_TEX
        {
            get => Globals.Get_Wrist_Tex(OutfitIndex);
            set => Globals.Set_Wrist_Tex(OutfitIndex, value);
        }
    }
}
