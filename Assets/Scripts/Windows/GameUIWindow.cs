public class GameUIWindow : Window
{
    public void BackToMenu()
    {
        var actionWindowData = new ActionData();
        actionWindowData.Data = "Your progress will not to be saved. Do you agree?";
        actionWindowData.ConfirmButtonTitle = "To Menu";
        actionWindowData.BackButtonTitle = "Back";
        actionWindowData.ConfirmButton += CloseLevel;
        actionWindowData.BackButton += UIManager.OpenWindow<GameUIWindow>;

        UIManager.OpenActionWindow<YesNoActionWindow, IActionData>(actionWindowData);
    }

    public void ToShop()
    {
        UIManager.OpenWindow<ShopWindow>();
    }

    private void CloseLevel()
    {
        GameController.ClearLevel();
        UIManager.OpenWindow<MainMenuWindow>();
    }
}
