public class MainMenuWindow : Window
{
    public void OnPlay() => UIManager.OpenWindow<SelectLevelWindow>();

    public void OnShop() => UIManager.OpenWindow<ShopWindow>();
}
