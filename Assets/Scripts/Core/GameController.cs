using System.Collections;
using Cinemachine;
using UnityEngine;

public class GameController : Singleton<GameController>
{
    [SerializeField] private CinemachineVirtualCamera _camera;
    [SerializeField] private PlayerController _player;
    private LevelView _currentLevel;

    private IActionData _congratulationsData;

    private void Awake()
    {
        _congratulationsData = new ActionData();
        _congratulationsData.Data = "Congratulations!";
        _congratulationsData.ConfirmButtonTitle = "Yeeey";
        _congratulationsData.ConfirmButton += ClearLevel;
        _congratulationsData.ConfirmButton += UIManager.OpenWindow<MainMenuWindow>;
        DontDestroyOnLoad(gameObject);
    }

    public static void Init(LevelView level) => Instance.InitInternal(level);

    public static void ClearLevel() => Instance.ClearLevelInternal();

    public static void CollectCollectable(BaseCollectableView collectable) => Instance.CollectCollectableInternal(collectable);

    public static void KillEnemy(BaseEnemyView enemy) => Instance.KillEnemyInternal(enemy);

    public static void KillPlayer() => Instance.KillPlayerInternal();

    private void InitInternal(LevelView level)
    {
        if (_currentLevel)
            ClearLevel();

        _currentLevel = Instantiate<LevelView>(level, transform);
        _currentLevel.FallDeathTrigger.OnTriggerEnter += OnFallDeath;
        _currentLevel.LevelFinishTrigger.OnTriggerEnter += OnEndLevel;
        _player.gameObject.SetActive(true);
        SpawnPlayer();
    }

    private void CollectCollectableInternal(BaseCollectableView collectable)
    {
        var collectableValue = CollectablesConfig.Instance.Collectables.Find((collectableInfo) => collectableInfo.Type == collectable.Type).Value;
        PlayerPrefsHelper.AddMoney(collectableValue);
    }

    private void KillEnemyInternal(BaseEnemyView enemy)
    {
        var enemyKillCost = EnemiesConfig.Instance.Enemies.Find((enemyInfo) => enemyInfo.Type == enemy.Type).EnemyKillCost;
        PlayerPrefsHelper.AddMoney(enemyKillCost);

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
        UIManager.OpenActionWindow<CongratulationsActionWindow, IActionData>(_congratulationsData);

        InputController.DisableInput();
        _player.SetHurtAnimation(true);
    }

    private void ClearLevelInternal()
    {
        if (_currentLevel)
        {
            _currentLevel.FallDeathTrigger.OnTriggerEnter -= OnFallDeath;
            _currentLevel.LevelFinishTrigger.OnTriggerEnter -= OnEndLevel;
            Destroy(_currentLevel.gameObject);
        }
        
        _player.gameObject.SetActive(false);
    }
}
