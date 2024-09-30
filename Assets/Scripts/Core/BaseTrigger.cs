using System;
using UnityEngine;

[ExecuteAlways]
[RequireComponent(typeof(Collider2D))]
public class BaseTrigger : MonoBehaviour
{
    public event Action OnTriggerEnter;
    public event Action OnTriggerStay;
    public event Action OnTriggerExit;

    private Collider2D _collider;

    private void Awake()
    {
        _collider = GetComponent<Collider2D>();
        _collider.isTrigger = true;
    }

    protected virtual void OnTriggerEnter2D(Collider2D other) => OnTriggerEnter?.Invoke();

    protected virtual void OnTriggerStay2D(Collider2D other) => OnTriggerStay?.Invoke();

    protected virtual void OnTriggerExit2D(Collider2D other) => OnTriggerExit?.Invoke();
}
