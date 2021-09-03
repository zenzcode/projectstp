using System;
using UnityEngine;

public class SingletonMonoBehaviour<T> : MonoBehaviour where T : MonoBehaviour
{
    private T instance;

    public T Instance
    {
        get => instance;
    }

    protected virtual void Awake()
    {
        MakeInstance();
    }

    private void MakeInstance()
    {
        if (instance == null)
        {
            instance = this as T;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
