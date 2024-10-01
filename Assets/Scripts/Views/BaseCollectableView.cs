using GameTypes;
using UnityEngine;

[RequireComponent(typeof(BaseTrigger))]
public class BaseCollectableView : MonoBehaviour
{
    [SerializeField] private CollectableTypes _type;

    private BaseTrigger _trigger;

    public CollectableTypes Type => _type;

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
