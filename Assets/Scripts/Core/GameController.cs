using Cinemachine;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera _camera;
    [SerializeField] private PlayerController _player;
    [SerializeField] private Transform _spawnPoint;

    [SerializeField] private BaseTrigger _fallDeathTrigger;
    [SerializeField] private BaseTrigger _endLevelTrigger;

    private void Awake()
    {
        _fallDeathTrigger.OnTriggerEnter += OnFallDeath;
        _endLevelTrigger.OnTriggerEnter += OnEndLevel;
    }

    private void RespawnPlayer() {
        _player.SetHurtAnimation(false);
        _player.MoveTo(_spawnPoint.position);
        _camera.LookAt = _player.transform;
        _camera.Follow = _player.transform;
        InputController.EnableInput();
    }

    private void OnFallDeath() {
        InputController.DisableInput();
        _camera.LookAt = null;
        _camera.Follow = null;
        _player.SetHurtAnimation(true);
        Invoke(nameof(RespawnPlayer), 2);
    }

    private void OnEndLevel() {
        InputController.DisableInput();
         _player.SetHurtAnimation(true);
    }

    void OnDestroy()
    {
        _fallDeathTrigger.OnTriggerEnter -= OnFallDeath;
        _endLevelTrigger.OnTriggerEnter -= OnEndLevel;
    }
}
