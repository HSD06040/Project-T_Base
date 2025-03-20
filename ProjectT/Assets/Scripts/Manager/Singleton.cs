using System;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : Component
{
    private static T instance;
    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                instance = (T)FindObjectOfType(typeof(T));
            }
            if (instance == null)
            {
                SetupInstance();
            }
            return instance;
        }
    }

    private void Awake()
    {
        RemoveDuplicates();
    }

    private void RemoveDuplicates()
    {
        if (instance == null)
        {
            instance = this as T;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private static void SetupInstance()
    {
        instance = (T)FindObjectOfType(typeof(T));

        if(instance == null)
        {
            GameObject newInstance = new GameObject();
            newInstance.name = typeof(T).Name;
            instance = newInstance.AddComponent<T>();
            DontDestroyOnLoad(newInstance);
        }
    }
}
