using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public abstract class BaseEquipableObserver<T> : Singleton<T> where T : BaseEquipableObserver<T>
{
    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    public static event Action OnEquipablesUpdate;

    public static void InvokeUpdate() => OnEquipablesUpdate?.Invoke();
}