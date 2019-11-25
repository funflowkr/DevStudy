using UnityEngine;

public abstract class SingletonMonoBehaviour<T> : MonoBehaviour where T : Component
{
    static T _instance;

    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType(typeof(T)) as T;

                if (_instance == null)
                {
                    var go = new GameObject(typeof(T).ToString());
                    _instance = go.AddComponent<T>();
                }
            }
            return _instance;
        }
    }

    public static T Seek
    {
        get
        {
            if (_instance == null)
                _instance = FindObjectOfType(typeof(T)) as T;
            return _instance;
        }
    }

    public static bool IsInit
    {
        get
        {
            return _instance != null;
        }
    }

    public virtual void Awake()
    {
        if (_instance == null)
        {
            _instance = this as T;
            Init();
        } else
        {
            Destroy(gameObject);
        }
    }

    public abstract void Init();

    public virtual void OnApplicationQuit()
    {
        _instance = null;
    }

    protected virtual void OnDestroy()
    {
        if (_instance == this)
        {
            _instance = null;
        }
    }
}