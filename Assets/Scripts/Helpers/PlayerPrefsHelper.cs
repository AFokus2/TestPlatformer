using System;
using GameTypes;
using UnityEditor;
using UnityEngine;

public static class PlayerPrefsHelper
{
    public static int GetMoneyCount() => PlayerPrefs.GetInt("Money");

    public static void AddMoney(int count) => PlayerPrefs.SetInt("Money", GetMoneyCount() + count);

    public static void TakeMoney(int count) => PlayerPrefs.SetInt("Money", GetMoneyCount() - count);

    public static bool CheckIfPurchased(ShopProductType product) => PlayerPrefs.HasKey($"{Enum.GetName(typeof(ShopProductType), product)} purchased");

    public static void SavePurchasing(ShopProductType product) => PlayerPrefs.SetString($"{Enum.GetName(typeof(ShopProductType), product)} purchased", "true");

    public static void DeletePurchasing(ShopProductType product) => PlayerPrefs.DeleteKey($"{Enum.GetName(typeof(ShopProductType), product)} purchased");

    public static bool CheckIfEquiped(ShopProductType product) => PlayerPrefs.HasKey($"{Enum.GetName(typeof(ShopProductType), product)} equiped");

    public static void SetEquiped(ShopProductType product) => PlayerPrefs.SetString($"{Enum.GetName(typeof(ShopProductType), product)} equiped", "true");

    public static void SetUnequiped(ShopProductType product) => PlayerPrefs.DeleteKey($"{Enum.GetName(typeof(ShopProductType), product)} equiped");

    [MenuItem("PlayerPrefs/Clear PlayerPrefs")]
    public static void ClearPrefs() => PlayerPrefs.DeleteAll();
}