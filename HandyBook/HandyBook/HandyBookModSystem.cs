using Vintagestory.API.Client;
using Vintagestory.API.Server;
using Vintagestory.API.Config;
using Vintagestory.API.Common;

namespace HandyBook;

public class HandyBookModSystem : ModSystem
{
    private HandyBookGUI handyBookGUI;
    
    public override bool ShouldLoad(EnumAppSide side)
    {
        return side == EnumAppSide.Client; // This mod only runs on client side
    }
    
    // Called on server and client
    // Useful for registering block/entity classes on both sides
    public override void Start(ICoreAPI api)
    {
        Mod.Logger.Notification("Hello from template mod: " + api.Side);
    }

    public override void StartServerSide(ICoreServerAPI api)
    {
        Mod.Logger.Notification("Hello from template mod server side: " + Lang.Get("handybook:hello"));
    }

    public override void StartClientSide(ICoreClientAPI api)
    {
        // Register key binding
        api.Input.RegisterHotKey("handybook", "Open HandyBook GUI", GlKeys.M, HotkeyType.GUIOrOtherControls);
        api.Input.SetHotKeyHandler("handybook", OnHotKeyHandyBookGUI);
        
        handyBookGUI = new HandyBookGUI(api);
        
        Mod.Logger.Notification("Hello from template mod client side: " + Lang.Get("handybook:hello"));
    }
    
    private bool OnHotKeyHandyBookGUI(KeyCombination comb)
    {
        if (handyBookGUI.IsOpened())
        {
            handyBookGUI.TryClose();
        }
        else
        {
            handyBookGUI.TryOpen();
        }
        
        return true;
    }
}