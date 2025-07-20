using Vintagestory.API.Client;

namespace HandyBook;

public class HandyBookGUI : GuiDialog
{
    private double dialogWidth = 400;
    private double dialogHeight = 400;
    
    public HandyBookGUI(ICoreClientAPI capi) : base(capi)
    {
        // Register a game tick listener to update dialog size every frame
        capi.Event.RegisterGameTickListener(OnGameTick, 0);
        //SetupDialog(400, 800);
    }

    private void SetupDialog(int screenWidth, int screenHeight)
    {
        // Center the dialog
        double posX = (screenWidth - dialogWidth) / 2.0;
        double posY = (screenHeight - dialogHeight) / 2.0;

        // Define dialog bounds
        ElementBounds dialogBounds = ElementBounds.Fixed(posX, posY, dialogWidth, dialogHeight);
        ElementBounds textBounds = ElementBounds.Fixed(10, 40, dialogWidth - 20, dialogHeight - 80);
        ElementBounds buttonBounds = ElementBounds.Fixed(dialogWidth - 80, dialogHeight - 40, 60, 30);

        // Dispose of previous composer if it exists
        SingleComposer?.Dispose();

        // Create new GUI composition
        SingleComposer = capi.Gui.CreateCompo("handybookgui", dialogBounds)
            .AddDialogBG(ElementBounds.Fixed(0, 0, dialogWidth, dialogHeight))
            .AddStaticText($"Welcome to HandyBook Mod! Size: {dialogWidth}x{dialogHeight}", CairoFont.WhiteSmallText(), textBounds)
            .AddButton("Close", OnCloseButton, buttonBounds)
            .Compose();
    }

    private void OnGameTick(float dt)
    {
        if (IsOpened())
        {
            // Get current screen resolution
            int screenWidth = capi.Render.FrameWidth;
            int screenHeight = capi.Render.FrameHeight;

            // Example: Make dialog size 50% of screen width and 30% of screen height
            dialogWidth = screenWidth * 0.5;
            dialogHeight = screenHeight * 0.3;

            // Rebuild dialog with new size
            SetupDialog(screenWidth, screenHeight);
        }
    }
    
    private bool OnCloseButton()
    {
        TryClose();
        return true;
    }

    public override string ToggleKeyCombinationCode => "handybook";
    
    public override void Dispose()
    {
        base.Dispose();
        SingleComposer?.Dispose();
    }
}