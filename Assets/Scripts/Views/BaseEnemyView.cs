using UnityEngine;

public class BaseEnemyView : MonoBehaviour
{
    public enum EnemyType {
        Beatle
    }

    [SerializeField] private EnemyType _type;
    [SerializeField] private LayerMask _playerLayer;
    [SerializeField] private BaseTrigger _deathTrigger;

    private HorizontalEnemyController _controller;

    public EnemyType Type => _type;

    private void Start()
    {
        _controller = GetComponent<HorizontalEnemyController>();
        _deathTrigger.OnTriggerEnter += OnDeathTrigger;
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if((_playerLayer & (1 << other.gameObject.layer)) != 0) {
            _controller.Flip();
            GameController.KillPlayer();
        }
    }

    private void OnDeathTrigger() {
        _deathTrigger.OnTriggerEnter -= OnDeathTrigger;
        GameController.KillEnemy(this);
        _controller.MovementEnabled = false;
        _controller.Bounce();
        
        var colliders = GetComponents<Collider2D>();

        foreach(var collider in colliders)
            collider.enabled = false;
    }
}
