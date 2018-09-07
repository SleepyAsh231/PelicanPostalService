using PelicanPostalService.Framework.Player;
using StardewValley;
using StardewValley.Menus;
using System.Collections.Generic;

namespace PelicanPostalService.Framework.Menu
{
    public class PostalService
    {
        public static void Open()
        {
            ActiveItem.data = Game1.player.ActiveObject;
            if (ActiveItem.IsLegalGift() && Game1.activeClickableMenu == null)
            {
                List<string> options = FriendshipData.Find();
                if (options != null)
                {
                    Game1.activeClickableMenu = new ChooseFromListMenu(options, OnSelectOption, false);
                }
            }
        }

        private static void OnSelectOption(string name)
        {
            FriendshipData fd = new FriendshipData(name);

            if (FriendshipData.who != null)
            {
                if (FriendshipData.CanReceiveGiftToday())
                {
                    int points = ActiveItem.FindNetValue();
                    FriendshipData.Update(points);
                    Game1.player.removeFirstOfThisItemFromInventory(ActiveItem.data.ParentSheetIndex);
                    Game1.activeClickableMenu.exitThisMenu();
                }
            }
        }
    }
}
