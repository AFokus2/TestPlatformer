using UnityEngine;

[RequireComponent(typeof(BaseTrigger))]
public class BaseCollectable : MonoBehaviour
{
    [SerializeField] private float _value;

    private BaseTrigger _trigger;

    private void Awake()
    {
        _trigger = GetComponent<BaseTrigger>();
        _trigger.OnTriggerEnter += OnTrigger;
    }

    private void OnTrigger() {
        gameObject.SetActive(false);
        GameController.CollectCollectable(this);
    }

    private void OnDestroy()
    {
        _trigger.OnTriggerEnter -= OnTrigger;
    }
}
