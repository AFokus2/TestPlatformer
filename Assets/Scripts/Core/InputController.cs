using System;
using UnityEngine;
using UnityEngine.UI;

public class InputController : Singleton<InputController>
{
    public static event Action OnJump;

    [SerializeField] protected Joystick joystick;
    [SerializeField] protected Button jumpButton;

    private bool Enabled { get; set; } = true;

    private void Start()
    {
        jumpButton.onClick.AddListener(OnJumpInvokeInternal);
    }

    public static void EnableInput() => Instance.Enabled = true;

    public static void DisableInput() => Instance.Enabled = false;

    public static float GetHorizontalInput() => Instance.Enabled ? Instance.joystick.Horizontal : 0;

    private void OnJumpInvokeInternal()
    {
        if (Enabled)
            OnJump?.Invoke();
    }
}
