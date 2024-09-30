using UnityEngine;

public class LevelView : MonoBehaviour
{
    [SerializeField] private BaseTrigger _fallDeathTrigger;
    [SerializeField] private BaseTrigger _levelFinishTrigger;
    [SerializeField] private Transform _spawnPoint;

    public BaseTrigger FallDeathTrigger => _fallDeathTrigger;
    public BaseTrigger LevelFinishTrigger => _levelFinishTrigger;
    public Transform SpawnPoint => _spawnPoint;
}
