using TMPro;
using UnityEngine;

public class CongratulationsActionWindow : ActionWindow<IActionData>
{
    [SerializeField] private TextMeshProUGUI _data;
    [SerializeField] private TextMeshProUGUI _yesButtonText;

    public override void SetData(IActionData data)
    {
        base.SetData(data);

        _data.text = data.Data;
        _yesButtonText.text = data.ConfirmButtonTitle;
    }

    public void OnConfirm() => ConfirmPopupTemplate.ConfirmButton.Invoke();
}
