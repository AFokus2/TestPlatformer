using System;
using UnityEngine;

public class BaseTrigger : MonoBehaviour
{
    public event Action OnTriggerEnter;
    public event Action OnTriggerStay;
    public event Action OnTriggerExit;

    protected virtual void OnTriggerEnter2D(Collider2D other) => OnTriggerEnter?.Invoke();

    protected virtual void OnTriggerStay2D(Collider2D other) => OnTriggerStay?.Invoke();

    protected virtual void OnTriggerExit2D(Collider2D other) => OnTriggerExit?.Invoke();
}
