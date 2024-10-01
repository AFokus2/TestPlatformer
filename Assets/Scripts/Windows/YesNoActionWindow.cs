using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class YesNoActionWindow : ActionWindow<IActionData>
{
    [SerializeField] private TextMeshProUGUI _data;
    [SerializeField] private TextMeshProUGUI _yesButtonText;
    [SerializeField] private TextMeshProUGUI _noButtonText;

    public override void SetData(IActionData data)
    {
        base.SetData(data);

        _data.text = data.Data;
        _yesButtonText.text = data.ConfirmButtonTitle;
        _noButtonText.text = data.BackButtonTitle;
    }

    public void OnConfirm() => ConfirmPopupTemplate.ConfirmButton.Invoke();

    public void OnBack() => ConfirmPopupTemplate.BackButton.Invoke();
}
