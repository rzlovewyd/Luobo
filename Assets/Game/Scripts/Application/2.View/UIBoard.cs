﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIBoard : View
{
    #region 常量
    #endregion

    #region 事件
    #endregion

    #region 字段
    public Text txtScore;
    public Image imgRoundInfo;
    public Text txtCurrent;
    public Text txtTotal;
    public Image imgPauseInfo;
    public Button btnSpeed1;
    public Button btnSpeed2;
    public Button btnResume;
    public Button btnPause;
    public Button btnSystem;

    bool m_IsPlaying = false;
    GameSpeed m_Speed = GameSpeed.One;
    int m_Score = 0;
    #endregion

    #region 属性
    public override string Name
    {
        get { return Consts.V_UIBoard; }
    }

    public bool IsPlaying
    {
        get { return m_IsPlaying; }

        set
        {
            m_IsPlaying = value;

            imgRoundInfo.gameObject.SetActive(value);
            imgPauseInfo.gameObject.SetActive(!value);

            btnPause.gameObject.SetActive(value);
            btnResume.gameObject.SetActive(!value);
        }
    }

    public GameSpeed Speed
    {
        get { return m_Speed; }

        set
        {
            m_Speed = value;

            btnSpeed1.gameObject.SetActive(m_Speed == GameSpeed.One);
            btnSpeed2.gameObject.SetActive(m_Speed == GameSpeed.Two);
        }
    }

    public int Score
    {
        get { return m_Score; }

        set
        {
            m_Score = value;

            txtScore.text = value.ToString();
        }
    }
    #endregion

    #region 方法
    public void UpdateRoundInfo(int currentRound, int totalRound)
    {
        txtCurrent.text = currentRound.ToString("D2");//始终保留2位整数
        txtTotal.text = txtTotal.ToString();
    }
    #endregion

    #region Unity回调
    public UICountDown UICountDown;
    void Awake()
    {
        Score = 0;
        IsPlaying = true;
        Speed = GameSpeed.One;

        UICountDown.StartCountDown();
    }
    #endregion

    #region 事件回调
    public void OnSpeed1Click()
    {
        Speed = GameSpeed.Two;
    }
    public void OnSpeed2Click()
    {
        Speed = GameSpeed.One;
    }
    public void OnPauseClick()
    {
        IsPlaying = false;
    }
    public void OnResumeClick()
    {
        IsPlaying = true;
    }
    public void OnSystemClick()
    {

    }
    public override void RegisterEvents()
    {
        //注册关心的事件
        AttationEvents.Add(Consts.E_CountDownComplete);
    }
    public override void HandleEvent(string eventName, object data)
    {
        switch(eventName)
        {
            case Consts.E_CountDownComplete:

                //开始出怪

                //游侠结束
                Game.Instance.LoadScene(4);
                break;
        }
    }
    #endregion

    #region 帮助方法
    #endregion
}
