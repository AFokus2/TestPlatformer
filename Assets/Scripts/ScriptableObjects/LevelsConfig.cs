using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelsConfig", menuName = "ScriptableObjects/LevelsConfig", order = 1)]
public class LevelsConfig : SingletonScriptableObject<LevelsConfig>
{
    [SerializeField] private List<LevelInfo> _levels;

    public List<LevelInfo> Levels => _levels;
}

[Serializable]
public class LevelInfo {
    public string Name;
    public LevelView View;
    public bool Avaliable;
}

public enum Levels {
    Level1,
    ComingSoon
}
