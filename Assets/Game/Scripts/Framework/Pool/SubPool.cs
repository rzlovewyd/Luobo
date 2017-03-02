using UnityEngine;
using System.Collections.Generic;

public class SubPool 
{
    //预设
    GameObject m_Prefab;

    //集合
    List<GameObject> m_Objects = new List<GameObject>();

    //名字标识
    public  string Name { get { return m_Prefab.name; } }

    //构造
    public SubPool(GameObject prefab)
    {
        this.m_Prefab = prefab;
    }

    //取对象
    public GameObject Spawn()
    {
        GameObject go = null;

        for (int i = 0; i < m_Objects.Count; i++)
        {
            if(!m_Objects[i].activeSelf)
            {
                go = m_Objects[i];
                break;
            }
        }

        if(go == null)
        {
            go = GameObject.Instantiate<GameObject>(m_Prefab);
            m_Objects.Add(go);
        }

        go.SetActive(true);
        go.SendMessage("OnSpawn", SendMessageOptions.DontRequireReceiver);
        return go;
    }

    //回收对象
    public void Unspawn(GameObject go)
    {
        if(Contains(go))
        {
            go.SendMessage("OnUnspawn", SendMessageOptions.DontRequireReceiver);
            go.SetActive(false);
        }
    }

    //回收该池子所有对象
    public void UnspawnAll()
    {
        for (int i = 0; i < m_Objects.Count; i++)
        {
            if(m_Objects[i].activeSelf)
            {
                Unspawn(m_Objects[i]);
            }
        }
    }

    //是否包含对象
    public bool Contains(GameObject go)
    {
        return m_Objects.Contains(go);
    }
}
