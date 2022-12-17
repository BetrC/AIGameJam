using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MonoSingleton<T> : MonoBehaviour where T : MonoSingleton<T>
{
    private static T m_instance;

    private static bool isInit = false;

    public static T Instance
    {
        get
        {
            if (m_instance == null)
            {
                m_instance = GameObject.FindObjectOfType<T>();
                if (m_instance == null)
                    m_instance = new GameObject("Mono_Sigleton" + typeof(T).Name, typeof(T)).GetComponent<T>();
            }

            if (!isInit)
            {
                isInit = true;
                m_instance.Init();
            }

            return m_instance;
        }
    }

    protected virtual void Init()
    {
        DontDestroyOnLoad(m_instance.gameObject);
    }

    private void Awake()
    {
        if (m_instance == null)
        {
            m_instance = this as T;
        }
        else if (m_instance != this)
        {
            DestroyImmediate(this.gameObject);
            return;
        }

        if (!isInit)
        {
            DontDestroyOnLoad(gameObject);
            isInit = true;
            m_instance.Init();
        }
    }
}