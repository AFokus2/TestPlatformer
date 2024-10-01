using System;
using System.Collections.Generic;
using GameTypes;
using UnityEngine;

public class ShopWindow : Window
{
    [SerializeField] private Transform _goodsContent;
    [SerializeField] private ProductView _productPrefab;

    private List<ProductView> _goods = new();

    private void Start()
    {
        var goods = ShopConfig.Instance.Products;

        foreach (var productInfo in goods)
        {
            var view = Instantiate(_productPrefab, _goodsContent);
            view.Info = productInfo;
            view.OnBuyClick += OnBuyButtonClick;
            view.OnEquipClick += OnEquipButtonClick;
            _goods.Add(view);
        }
    }

    public void OnBack() => UIManager.CloseWindow<ShopWindow>();

    private void ClearButtons()
    {
        foreach (var products in _goods)
            Destroy(products.gameObject);

        _goods.Clear();
    }

    private void OnBuyButtonClick(ProductInfo info)
    {
        if (PlayerPrefsHelper.GetMoneyCount() < info.Price)
            return;
            
        PlayerPrefsHelper.SavePurchasing(info.Type);
        PlayerPrefsHelper.TakeMoney(info.Price);

        foreach (var product in _goods)
            product.UpdateView();
    }

    private void OnEquipButtonClick(ProductInfo info)
    {
        var isEquiped = PlayerPrefsHelper.CheckIfEquiped(info.Type);

        if (isEquiped)
            PlayerPrefsHelper.SetUnequiped(info.Type);
        else
        {
            foreach (var product in ShopConfig.Instance.Products)
            {
                if (product.Type == info.Type)
                    continue;

                PlayerPrefsHelper.SetUnequiped(product.Type);
            }

            PlayerPrefsHelper.SetEquiped(info.Type);
        }

        foreach (var product in _goods)
            product.UpdateView();
    }
}
