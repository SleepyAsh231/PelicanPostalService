using StardewValley;
using System.Collections.Generic;

namespace PelicanPostalService.Framework.Player
{
    public class FriendshipData
    {
        public static bool isBirthday;
        public static NPC who;
        private static int affectionLevel;
        private static int giftsThisDay;
        private static int giftsThisWeek;
        private static bool isSpouse;
        private static WorldDate lastGiftDate;        

        public FriendshipData(string name)
        {
            who = Game1.getCharacterFromName(name, true);
            affectionLevel = Game1.player.friendshipData[name].Points;
            giftsThisDay = Game1.player.friendshipData[name].GiftsToday;
            giftsThisWeek = Game1.player.friendshipData[name].GiftsThisWeek;
            isBirthday = Game1.currentSeason.Equals(who.Birthday_Season) && Game1.dayOfMonth == who.Birthday_Day;
            isSpouse = Game1.player.friendshipData[name].IsMarried();
            lastGiftDate = Game1.player.friendshipData[name].LastGiftDate;
        }

        public static bool CanReceiveGiftToday()
        {
            bool normalScenario = giftsThisDay == 0 && giftsThisWeek < 2;
            bool birthdayScenario = giftsThisDay == 0 && giftsThisWeek == 2 && isBirthday;
            bool marriageScenario = giftsThisDay == 0;

            return normalScenario || birthdayScenario || marriageScenario ? true : false;
        }

        public static List<string> Find()
        {
            HashSet<string> table = new HashSet<string>();
            foreach (string key in Game1.player.friendshipData.Keys)
            {
                if (Game1.player.friendshipData.ContainsKey(key))
                {
                    table.Add(key);
                }
            }

            return table.Count > 0 ? Sort(table) : null;
        }

        // Stardew Valley 1.3.29
        // Game statistics can only be modified when accessed directly
        public static void Update(int points)
        {
            Game1.player.friendshipData[who.displayName].Points += points;
            ++Game1.player.friendshipData[who.displayName].GiftsToday;
            ++Game1.player.friendshipData[who.displayName].GiftsThisWeek;
            Game1.player.friendshipData[who.displayName].LastGiftDate = Game1.Date;
        }

        private static List<string> Sort(HashSet<string> table)
        {
            List<string> list = new List<string>();
            foreach (string key in table)
            {
                list.Add(key);
            }

            list.Sort();
            return list;
        }
    }
}
