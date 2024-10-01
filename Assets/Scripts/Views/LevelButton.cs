using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelButton : MonoBehaviour
{
    public event Action<LevelInfo> OnClick;

    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private Button _button;

    private LevelInfo _info;

    public LevelInfo Info {
        get {
            return _info;
        }

        set {
            _info = value;
            _text.text = _info.Name;
        }
    }

    private void Awake() => _button.onClick.AddListener(OnButtonPressed);

    private void OnButtonPressed() {
        if(_info.Avaliable)
            OnClick.Invoke(_info);
    } 

    private void  OnDestroy() => _button.onClick.RemoveAllListeners();
}
