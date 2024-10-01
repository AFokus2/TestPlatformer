using UnityEngine;

public class Window : MonoBehaviour
{
    private bool _isActive;

    private void Awake()
    {
        gameObject.SetActive(false);
    }

    public virtual void Open()
    {
        if (_isActive)
            return;

        gameObject.SetActive(true);
        _isActive = true;
    }

    public virtual void Close()
    {
        gameObject.SetActive(false);
        _isActive = false;
    }
}

public delegate void Action();

public interface IActionData
{
    string Data { get; set; }
    string ConfirmButtonTitle { get; set; }
    string BackButtonTitle { get; set; }
    Action ConfirmButton { get; set; }
    Action BackButton { get; set; }
}

public struct ActionData : IActionData
{
    public string Data { get; set; }
    public string ConfirmButtonTitle { get; set; }
    public string BackButtonTitle { get; set; }
    public Action ConfirmButton { get; set; }
    public Action BackButton { get; set; }
}

public class ActionWindow<TData> : Window where TData : IActionData
{
    protected TData ConfirmPopupTemplate { get; private set; }

    public virtual void SetData(TData data)
    {
        ConfirmPopupTemplate = data;
    }
}