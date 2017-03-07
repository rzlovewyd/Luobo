
using System.Collections.Generic;
using System.IO;
/// <summary>
/// 选择关卡界面
/// </summary>
public class UISelect : View 
{
    #region 常量
    #endregion

    #region 事件
    #endregion

    #region 字段
    List<Card> m_Card = new List<Card>();
    int m_SelectIndex = -1;
    #endregion

    #region 属性
    public override string Name
    {
        get { return Consts.V_Select; }
    }
    #endregion

    #region 方法
    //返回开始界面
    public void GoBack()
    {
        Game.Instance.LoadScene(1);
    }

    //选中关卡游戏
    public void ChooseLevel()
    {
        StartLevelArgs e = new StartLevelArgs()
        {
            LevelID = 0
        };

        SendEvent(Consts.E_StartLevel, e);
    }

    //初始化卡片
    void LoadCards()
    {
        //构建Level集合
        List<FileInfo> files = Tools.GetLevelFiles();
        List<Level> levels = new List<Level>();
        for (int i = 0; i < files.Count; i++)
        {
            Level level = new Level();
            Tools.FillLevel(files[i].FullName, ref level);
        }

        //构建Card集合
        for (int i = 0; i < levels.Count; i++)
        {
            Card card = new Card
            {
                LevelID = i,
                CardImage = levels[i].CardImage,
                //TODO:
                IsLocked = true
            };
            m_Card.Add(card);
        }

        //默认选择第1个卡片
        SelectCard(0);
    }

    //选择卡片
    void SelectCard(int index)
    {
        if (m_SelectIndex == index)
            return;

        m_SelectIndex = index;

        //计算索引
    }
    #endregion

    #region Unity回调
    #endregion

    #region 事件回调
    public override void RegisterEvents()
    {
        base.RegisterEvents();
        AttationEvents.Add(Consts.E_EnterScene);
    }
    public override void HandleEvent(string eventName, object data)
    {
        switch (eventName)
        {
            case Consts.E_EnterScene:
                if(((SceneArgs)data).SceneIndex == 2)
                {
                    //初始化卡片
                    LoadCards();
                }
                break;
        }
    }
    #endregion

    #region 帮助方法
    #endregion
}
