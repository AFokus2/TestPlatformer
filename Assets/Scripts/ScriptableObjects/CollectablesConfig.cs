using System;
using System.Collections.Generic;
using GameTypes;
using UnityEngine;

[CreateAssetMenu(fileName = "CollectablesConfig", menuName = "ScriptableObjects/CollectablesConfig", order = 2)]
public class CollectablesConfig : SingletonScriptableObject<CollectablesConfig>
{
    [SerializeField] private List<CollectableInfo> _collectables;

    public List<CollectableInfo> Collectables => _collectables;
}

[Serializable]
public class CollectableInfo
{
    public CollectableTypes Type;
    public int Value;
}
