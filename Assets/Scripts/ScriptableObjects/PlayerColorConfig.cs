using System;
using System.Collections.Generic;
using GameTypes;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerColorConfig", menuName = "ScriptableObjects/PlayerColorConfig", order = 5)]
public class PlayerColorConfig : SingletonScriptableObject<PlayerColorConfig>
{
    [SerializeField] private List<CharacterColorInfo> _colors;

    public List<CharacterColorInfo> Colors => _colors;
}

[Serializable]
public class CharacterColorInfo
{
    public CharacterColorType Type;
    public Color Color;
}