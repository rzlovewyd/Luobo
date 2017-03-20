using UnityEngine;
using System.Collections;

public class Spawner : View
{
    #region 常量
    #endregion

    #region 事件
    #endregion

    #region 字段
    Map m_Map;
    #endregion

    #region 属性
    public override string Name
    {
        get { return Consts.V_Spawner; }
    }
    #endregion

    #region 方法
    public void Spawn(int MonsterType)
    {
        //创建怪物
        Debug.Log("地图产生了一个怪物，类型是：" + MonsterType);
    }
    #endregion

    #region Unity回调
    #endregion

    #region 事件回调
    public override void RegisterEvents()
    {
        base.RegisterEvents();

        AttationEvents.Add(Consts.E_EnterScene);
        AttationEvents.Add(Consts.E_SpawnMonster);
    }
    public override void HandleEvent(string eventName, object data)
    {
        switch (eventName)
        {
            case Consts.E_EnterScene:
                SceneArgs e0 = (SceneArgs)data;
                if (e0.SceneIndex == 3)
                {
                    m_Map = GetComponent<Map>();

                    //获取数据
                    GameModel gModel = GetModel<GameModel>();
                    m_Map.LoadLevel(gModel.PlayLevel);
                }
                break;
            case Consts.E_SpawnMonster:
                SpawnMonsterArgs e = (SpawnMonsterArgs)data;
                Spawn(e.MonsterType);
                break;
        }
    }
    #endregion

    #region 帮助方法
    #endregion
}
