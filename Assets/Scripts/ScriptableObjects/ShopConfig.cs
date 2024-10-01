using System;
using System.Collections.Generic;
using GameTypes;
using UnityEngine;

[CreateAssetMenu(fileName = "ShopConfig", menuName = "ScriptableObjects/ShopConfig", order = 4)]
public class ShopConfig : SingletonScriptableObject<ShopConfig>
{
    [SerializeField] private List<ProductInfo> _products;

    public List<ProductInfo> Products => _products;
}

[Serializable]
public class ProductInfo
{
    public ShopProductType Type;
    public Sprite ProductPricture;
    public int Price;
}
