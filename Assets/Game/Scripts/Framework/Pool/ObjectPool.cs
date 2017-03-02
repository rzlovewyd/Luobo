using UnityEngine;
using System.Collections.Generic;

public class ObjectPool : Singleton<ObjectPool>
{
    public string ResourceDir = "";

    Dictionary<string, SubPool> m_Pools = new Dictionary<string, SubPool>();

    //取对象
    public GameObject Spawn(string name)
    {
        if (!m_Pools.ContainsKey(name))
            RegisterNew(name);

        SubPool pool = m_Pools[name];
        return pool.Spawn();
    }

    //回收对象
    public void Unspawn(GameObject go)
    {
        foreach (SubPool p in m_Pools.Values)
        {
            if (p.Contains(go))
            {
                p.Unspawn(go);
                break;
            }
        }
    }

    //回收所有对象
    public void UnspawnAll()
    {
        foreach (SubPool p in m_Pools.Values)
        {
            p.UnspawnAll();
        }
    }

    //创建新子池子
    void RegisterNew(string name)
    {
        //预设路径
        string path = string.IsNullOrEmpty(ResourceDir) ? name : ResourceDir + "/" + name;

        //加载预设
        GameObject prefab = Resources.Load<GameObject>(path);

        //创建子对象池
        SubPool pool = new SubPool(prefab);
        m_Pools.Add(pool.Name, pool);
    }
}
