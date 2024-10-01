using GameTypes;
using UnityEngine;

public class CharacterColorObserver : BaseEquipableObserver<CharacterColorObserver>
{
    public static Color GetCurrentCharacterColor()
    {

        foreach (var colorInfo in PlayerColorConfig.Instance.Colors)
        {
            if (PlayerPrefsHelper.CheckIfEquiped((ShopProductType)colorInfo.Type))
                return colorInfo.Color;
        }

        return Color.white;
    }
}
