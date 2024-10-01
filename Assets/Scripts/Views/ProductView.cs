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

    public ProductInfo Info {
        get => _info;
        set {
            _info = value;
            _productPicture.sprite = _info.ProductPricture;
            _price.text = _info.Price.ToString();
        }
    }


    private void Awake() {
        _buyButton.onClick.AddListener(OnBuyPressed);
        _equipButton.onClick.AddListener(OnEquipPressed);
    } 

    private void OnBuyPressed()
    {
        // if(_info.Avaliable)
        //     OnClick.Invoke(_info);
    }

    private void OnEquipPressed() {

    }

    private void OnDestroy() {
        _buyButton.onClick.RemoveAllListeners();
        _equipButton.onClick.RemoveAllListeners();
    } 
}
