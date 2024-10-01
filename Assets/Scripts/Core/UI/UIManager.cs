using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    [SerializeField] private Canvas _mainCanvas = default;
    [SerializeField] private List<Window> _prefabs = default;

    private Dictionary<Type, Window> _associativeMapOfPrefabs;

    private Stack<Window> _queueOfWillOpenedWindow;

    protected void Awake()
    {
        _associativeMapOfPrefabs = new Dictionary<Type, Window>();
        _queueOfWillOpenedWindow = new Stack<Window>();
        
        foreach (var prefab in _prefabs)
        {
            var window = Instantiate(prefab, _mainCanvas.transform, false);
            _associativeMapOfPrefabs.Add(window.GetType(),window);
        }
        
        if(!_associativeMapOfPrefabs.ContainsKey(typeof(GameUIWindow))) 
            return;
        
        OpenWindow<GameUIWindow>();

        DontDestroyOnLoad(gameObject);
    }

    public static Window GetWindow<T>() where T : Window => Instance._associativeMapOfPrefabs[typeof(T)];

    public static void OpenWindow<T>() where T : Window
    {
        // Closing any window before opening new
        CloseWindowsProcess();
        
        Instance._associativeMapOfPrefabs[typeof(T)].Open();
    }

    public static void OpenActionWindow<T, TData>(TData data)
        where T : ActionWindow<TData> where TData : IActionData
    {
        ((ActionWindow<TData>)Instance._associativeMapOfPrefabs[typeof(T)]).SetData(data);
        OpenWindow<T>();
    }
    
    private void Close(Window window) => window.Close();
    
    private static void CloseWindowsProcess()
    {
        foreach (var window in Instance._associativeMapOfPrefabs.Where(window => window.Value.isActiveAndEnabled))
        {
            Instance.Close(window.Value);
            Instance._queueOfWillOpenedWindow.Push(window.Value);
        }
    }

    public static void CloseWindow<T>() where T: Window
    {
        Instance.Close(Instance._associativeMapOfPrefabs[typeof(T)]);
        
        if(Instance._queueOfWillOpenedWindow.Count == 0)
            return;
        
        var window = Instance._queueOfWillOpenedWindow.Pop();
        window.Open();
    }
}
