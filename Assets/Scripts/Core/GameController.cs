using System.Collections;
using Cinemachine;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera _camera;
    [SerializeField] private PlayerController _player;
    [SerializeField] private LevelView _currentLevel;

    private static GameController _instance;

    private static GameController Instance
    {
        get => _instance;
        set => _instance ??= value;
    }

    private void Awake()
    {
        Instance = this;
        Init(_currentLevel);
    }

    public static void Init(LevelView level) => Instance.InitInternal(level);

    public static void CollectCollectable(BaseCollectable collectable)
    {

    }

    public static void KillEnemy(BaseEnemyView enemy) => Instance.KillEnemyInternal(enemy);

    public static void KillPlayer() => Instance.KillPlayerInternal();

    private void InitInternal(LevelView level)
    {
        // if (_currentLevel)
        //     ClearLevel();

        _currentLevel = Instantiate<LevelView>(level);
        _currentLevel.FallDeathTrigger.OnTriggerEnter += OnFallDeath;
        _currentLevel.LevelFinishTrigger.OnTriggerEnter += OnEndLevel;
    }

    private void KillEnemyInternal(BaseEnemyView enemy)
    {
        StartCoroutine(KillEnemyWithDelay(2f, enemy));
    }

    private IEnumerator KillEnemyWithDelay(float delay, BaseEnemyView enemy)
    {
        yield return new WaitForSeconds(delay);
        Destroy(enemy.gameObject);
    }

    private void KillPlayerInternal()
    {
        InputController.DisableInput();
        _player.SetHurtAnimation(true);
        Invoke(nameof(RespawnPlayer), 1);
    }

    private void SpawnPlayer() => _player.MoveTo(_currentLevel.SpawnPoint.position);

    private void RespawnPlayer()
    {
        _player.SetHurtAnimation(false);
        SpawnPlayer();
        _camera.enabled = true;
        _camera.LookAt = _player.transform;
        _camera.Follow = _player.transform;
        InputController.EnableInput();
    }

    private void OnFallDeath()
    {
        InputController.DisableInput();
        _camera.LookAt = null;
        _camera.Follow = null;
        _camera.enabled = false;
        _player.SetHurtAnimation(true);
        Invoke(nameof(RespawnPlayer), 2);
    }

    private void OnEndLevel()
    {
        InputController.DisableInput();
        _player.SetHurtAnimation(true);
    }

    private void ClearLevel()
    {
        _currentLevel.FallDeathTrigger.OnTriggerEnter -= OnFallDeath;
        _currentLevel.LevelFinishTrigger.OnTriggerEnter -= OnEndLevel;
        Destroy(_currentLevel);
    }

    private void OnDestroy()
    {
        ClearLevel();
    }
}
