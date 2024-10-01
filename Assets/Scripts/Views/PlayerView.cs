using UnityEngine;

public class PlayerView : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _playerRenderer;

    private void Start()
    {
        CharacterColorObserver.OnEquipablesUpdate += ChangeColor;
        ChangeColor();
    }

    private void ChangeColor() {
        _playerRenderer.color = CharacterColorObserver.GetCurrentCharacterColor();
    }

    void OnDestroy()
    {
        CharacterColorObserver.OnEquipablesUpdate -= ChangeColor;
    }
}
