using UnityEngine;

public abstract class Manager<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T instance;
    public static T Instance
    {
        get { return instance; }
        set
        {
            if(instance == null)
            {
                instance = value;
                DontDestroyOnLoad(instance);
            }
            else if(instance != value)
            {
                Destroy(value.gameObject);
            }
        }
    }

    protected virtual void Awake()
    {
        Instance = this as T;
    }
}
