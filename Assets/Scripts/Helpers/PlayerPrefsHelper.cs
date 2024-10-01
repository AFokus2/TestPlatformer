using UnityEngine;

public static class PlayerPrefsHelper
{
    public static int GetMoneyCount() => PlayerPrefs.GetInt("Money");

    public static void AddMoney(int count) => PlayerPrefs.SetInt("Money", GetMoneyCount() + count);
}