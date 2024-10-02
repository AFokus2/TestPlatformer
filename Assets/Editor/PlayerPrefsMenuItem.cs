using UnityEditor;
using UnityEngine;

public static class PlayerPrefsMenuItem
{
   [MenuItem("PlayerPrefs/Clear PlayerPrefs")]
    public static void ClearPrefs() => PlayerPrefs.DeleteAll();
}
