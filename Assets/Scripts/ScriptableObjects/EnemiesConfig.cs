using System;
using System.Collections.Generic;
using GameTypes;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemiesConfig", menuName = "ScriptableObjects/EnemiesConfig", order = 3)]
public class EnemiesConfig : SingletonScriptableObject<EnemiesConfig>
{
    [SerializeField] private List<EnemyInfo> _enemies;

    public List<EnemyInfo> Enemies => _enemies;
}

[Serializable]
public class EnemyInfo
{
    public EnemyType Type;
    public int EnemyKillCost;
}