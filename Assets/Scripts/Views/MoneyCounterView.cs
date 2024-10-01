using TMPro;
using UnityEngine;

public class MoneyCounterView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _count;

    private void FixedUpdate() => _count.text = PlayerPrefsHelper.GetMoneyCount().ToString();
}
