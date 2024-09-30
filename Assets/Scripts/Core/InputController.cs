using System;
using UnityEngine;
using UnityEngine.UI;

public class InputController : MonoBehaviour
{
    public static event Action OnJump;

    [SerializeField] protected Joystick joystick;
    [SerializeField] protected Button jumpButton;

    private static InputController _instance;

    private bool Enabled {get; set;} = true;

    private void Awake()
    {
        if (!_instance)
            _instance = this;

         jumpButton.onClick.AddListener(OnJumpInvokeInternal);
    }

    public static void EnableInput() => _instance.Enabled = true;

    public static void DisableInput() => _instance.Enabled = false;

    public static float GetHorizontalInput() => _instance.Enabled ? _instance.joystick.Horizontal : 0;

    private void OnJumpInvokeInternal(){
        if(Enabled)
            OnJump?.Invoke();
    }
}
