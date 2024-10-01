using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ProductView : MonoBehaviour
{
    public event Action<ProductInfo> OnBuyClick;
    public event Action<ProductInfo> OnEquipClick;

    [SerializeField] private Image _productPicture;
    [SerializeField] private TextMeshProUGUI _price;

    [Space]
    [SerializeField] private Button _buyButton;
    [SerializeField] private Button _equipButton;
    [SerializeField] private TextMeshProUGUI _equipLabel;
    [SerializeField] private string _equipText = "Equip";
    [SerializeField] private string _unequipText = "Unequip";

    private ProductInfo _info;

    public ProductInfo Info
    {
        get => _info;
        set
        {
            _info = value;
            _productPicture.sprite = _info.ProductPricture;
            _price.text = _info.Price.ToString();
            UpdateView();
        }
    }


    private void Awake()
    {
        _buyButton.onClick.AddListener(OnBuyPressed);
        _equipButton.onClick.AddListener(OnEquipPressed);

        if(_info != null)
            UpdateView();
    }

    private void OnBuyPressed()
    {
        OnBuyClick?.Invoke(_info);
    }

    private void OnEquipPressed()
    {
        OnEquipClick?.Invoke(_info);
    }

    public void UpdateView()
    {
        var isPurchased = PlayerPrefsHelper.CheckIfPurchased(_info.Type);

        _buyButton.gameObject.SetActive(!isPurchased);
        _equipButton.gameObject.SetActive(isPurchased);

        _equipLabel.text = PlayerPrefsHelper.CheckIfEquiped(_info.Type) ? _unequipText : _equipText;
    }

    private void OnDestroy()
    {
        _buyButton.onClick.RemoveAllListeners();
        _equipButton.onClick.RemoveAllListeners();
    }
}
