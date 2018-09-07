using StardewModdingAPI;
using StardewModdingAPI.Events;
using PelicanPostalService.Config;
using PelicanPostalService.Framework.Menu;

namespace PelicanPostalService
{
    public class ModEntry : Mod
    {
        private ModConfig Config;

        public override void Entry(IModHelper helper)
        {
            Config = helper.ReadConfig<ModConfig>();
            ControlEvents.KeyPressed += ControlEvents_KeyPressed;
        }

        private void ControlEvents_KeyPressed(object sender, EventArgsKeyPressed e)
        {
            if (!Context.IsWorldReady)
            {
                return;
            }
            else if (e.KeyPressed.ToString() == Config.MenuAccessKey)
            {
                PostalService.Open();
            }
        }
    }
}
