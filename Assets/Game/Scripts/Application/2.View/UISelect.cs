using UnityEngine;
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
    List<Card> m_Cards = new List<Card>();
    int m_SelectIndex = -1;
    GameModel m_GameModel = null;
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
            LevelID = m_SelectIndex
        };

        SendEvent(Consts.E_StartLevel, e);
    }

    //初始化卡片
    void LoadCards()
    {
        //获取Level集合
        List<Level> levels = m_GameModel.AllLevels;

        //构建Card集合
        for (int i = 0; i < levels.Count; i++)
        {
            Card card = new Card
            {
                LevelID = i,
                CardImage = levels[i].CardImage,
                //TODO:
                IsLocked = i > m_GameModel.GameProgress
            };
            m_Cards.Add(card);
        }

        //监听卡片点击事件
        UICard[] uiCards = transform.FindChild("UICards").GetComponentsInChildren<UICard>();
        foreach (UICard uiCard in uiCards)
        {
            uiCard.OnClick += (card) =>
            {
                SelectCard(card.LevelID);
            };
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
        int left = m_SelectIndex - 1;
        int current = m_SelectIndex;
        int right = m_SelectIndex + 1;

        //绑定数据
        Transform container = transform.FindChild("UICards");

        //左边
        if (left < 0)
        {
            container.GetChild(0).gameObject.SetActive(false);
        }
        else
        {
            container.GetChild(0).gameObject.SetActive(true);
            container.GetChild(0).GetComponent<UICard>().IsTransparent = true;
            container.GetChild(0).GetComponent<UICard>().DataBind(m_Cards[left]);
        }

        //当前
        container.GetChild(1).GetComponent<UICard>().DataBind(m_Cards[current]);
        container.GetChild(1).GetComponent<UICard>().IsTransparent = false;

        //右边
        if (right >= m_Cards.Count)
        {
            container.GetChild(2).gameObject.SetActive(false);
        }
        else
        {
            container.GetChild(2).gameObject.SetActive(true);
            container.GetChild(2).GetComponent<UICard>().IsTransparent = true;
            container.GetChild(2).GetComponent<UICard>().DataBind(m_Cards[right]);
        }
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
                if (((SceneArgs)data).SceneIndex == 2)
                {
                    m_GameModel = GetModel<GameModel>();
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
