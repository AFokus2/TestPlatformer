using System.Collections.Generic;
using UnityEngine;

public class SelectLevelWindow : Window
{
    [SerializeField] private Transform _levelContent;
    [SerializeField] private LevelButton _buttonPrefab;

    private List<LevelButton> _buttons = new();

    private void Start()
    {
        var levels = LevelsConfig.Instance.Levels;

        foreach(var levelInfo in levels) {
            var button = Instantiate(_buttonPrefab, _levelContent);
            button.Info = levelInfo;
            button.OnClick += OnLevelButtonClick;
            _buttons.Add(button);
        }
    }

    public void OnBack() => UIManager.OpenWindow<MainMenuWindow>();

    private void ClearButtons() {
        foreach(var button in _buttons)
            Destroy(button.gameObject);

        _buttons.Clear();
    }

    private void OnLevelButtonClick(LevelInfo info) {
        GameController.Init(info.View);
        UIManager.OpenWindow<GameUIWindow>();
    }
}
