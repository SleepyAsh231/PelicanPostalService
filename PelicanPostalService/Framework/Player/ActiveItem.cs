using StardewValley;
using System.Collections.Generic;

namespace PelicanPostalService.Framework.Player
{
    public class ActiveItem
    {
        public static Object data;

        public static bool IsLegalGift()
        {
            if (data != null)
            {
                List<string> list = new List<string> { "Bouquet", "Mermaid's Pendant", "Wedding Band" };
                foreach (string key in list)
                {
                    if (data.DisplayName.Equals(key)) {
                        return false;
                    }
                }
                return data.canBeGivenAsGift() ? true : false;
            }
            return false;
        }

        public static int FindNetValue()
        {
            return (int) (FindBaseValue() * FindDateValue() * FindQualityValue());
        }

        private static int FindBaseValue()
        {
            int flag = FriendshipData.who.getGiftTasteForThisItem(data);
            switch (flag)
            {
                case 0:
                    return 80;
                case 2:
                    return 45;
                case 4:
                    return -20;
                case 6:
                    return -40;
                case 8:
                    return 20;
                default:
                    return 0;
            }
        }

        private static int FindDateValue()
        {
            if (Game1.currentSeason.Equals("winter") && Game1.dayOfMonth == 25)
            {
                return 5;
            }
            else if (FriendshipData.isBirthday)
            {
                return 8;
            }
            else
            {
                return 1;
            }
        }

        private static float FindQualityValue()
        {
            switch (data.Quality)
            {
                case 1:
                    return 1.1f;
                case 2:
                    return 1.25f;
                case 4:
                    return 1.5f;
                default:
                    return 1f;
            }
        }
    }
}