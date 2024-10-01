// Copyright (c) 2012-2021 FuryLion Group. All Rights Reserved.

using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T _instance;

    public static T Instance
    {
        get
        {
            if (_instance == null)
                _instance = FindObjectOfType<T>();

            if (_instance == null)
            {
                var singleton = new GameObject();
                _instance = singleton.AddComponent<T>();
            }

            return _instance;
        }
    }
}