using SPTarkov.DI.Annotations;
using SPTarkov.Server.Core.DI;
using SPTarkov.Server.Core.Helpers;
using SPTarkov.Server.Core.Models.Spt.Mod;
using SPTarkov.Server.Core.Models.Utils;
using SPTarkov.Server.Core.Models.Eft.Hideout;
using System.Reflection;
using WTTServerCommonLib;

namespace DoorBreacher
{
    public record ModMetadata : AbstractModMetadata
    {
        public override string ModGuid { get; init; } = "com.dvize.doorbreacher";
        public override string Name { get; init; } = "DoorBreacher";
        public override string Author { get; init; } = "dvize";
        public override List<string>? Contributors { get; init; } = new List<string> { "Props", "Tron", "MakerMacher" };
        public override SemanticVersioning.Version Version { get; init; } = new("2.0.0");
        public override SemanticVersioning.Range SptVersion { get; init; } = new("~4.0.0");
        public override List<string>? Incompatibilities { get; init; }
        public override Dictionary<string, SemanticVersioning.Range>? ModDependencies { get; init; } = new()
        {
            ["com.wtt.commonlib"] = new(">=1.0.0")
        };
        public override string? Url { get; init; } = "https://github.com/dvize/BackdoorBandit";
        public override bool? IsBundleMod { get; init; } = true;
        public override string License { get; init; } = "MIT";
    }

    [Injectable(TypePriority = OnLoadOrder.PostDBModLoader + 1)]
    public class DoorBreacherMain(
        ISptLogger<DoorBreacherMain> logger,
        ModHelper modHelper,
        WTTServerCommonLib.WTTServerCommonLib wttCommon) : IOnLoad
    {
        private readonly ISptLogger<DoorBreacherMain> _logger = logger;
        private readonly ModHelper _modHelper = modHelper;
        private readonly WTTServerCommonLib.WTTServerCommonLib _wttCommon = wttCommon;

        public async Task OnLoad()
        {
            try
            {
                var assembly = Assembly.GetExecutingAssembly();

                _logger.Info("Loading DoorBreacher custom items...");

                // Use WTT library to create custom items (handles items + traders automatically)
                await _wttCommon.CustomItemServiceExtended.CreateCustomItems(assembly);

                // Use WTT library to add hideout recipes (from db/CustomHideoutRecipes by default)
                await _wttCommon.CustomHideoutRecipeService.CreateHideoutRecipes(assembly);

                _logger.Success("DoorBreacher mod loaded successfully!");
                _logger.Info("Custom items added: Door Breacher Round, Door Breacher Box, C4 Explosive");
                _logger.Info("Check Mechanic trader for breach rounds and C4");
                _logger.Info("C4 can be crafted at Workbench Level 2");
            }
            catch (Exception ex)
            {
                _logger.Error($"Error loading DoorBreacher mod: {ex.Message}");
                _logger.Error($"Stack trace: {ex.StackTrace}");
                throw;
            }
        }

    }
}
