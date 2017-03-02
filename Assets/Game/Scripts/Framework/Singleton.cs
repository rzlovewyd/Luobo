using UnityEngine;

public abstract class Singleton<T> : MonoBehaviour
    where T : MonoBehaviour
{
    private static T m_Instance = null;

    public static T Instance
    {
        get { return m_Instance; }
    }

    protected virtual void Awake()
    {
        m_Instance = this as T;
    }
}