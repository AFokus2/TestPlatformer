using UnityEngine;

public class SingletonScriptableObject<T> : ScriptableObject where T : SingletonScriptableObject<T>
{
    private static T _instance;

    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = Resources.Load<T>($"Scriptables\\{typeof(T).Name}");
                if (_instance == null)
                {
                    Debug.LogError($"{typeof(T).Name} instance not found in Resources!");
                }
            }
            return _instance;
        }
    }
}