using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIWin : View
{
    #region 常量
    #endregion

    #region 事件
    #endregion

    #region 字段
    public Text txtCurrent;
    public Text txtTotal;
    public Button btnRestart;
    public Button btnContinue;
    public Button btnSelect;
    #endregion

    #region 属性
    public override string Name
    {
        get { return Consts.V_UIWin; }
    }
    #endregion

    #region 方法
    public void Show()
    {
        gameObject.SetActive(true);
    }
    public void Hide()
    {
        gameObject.SetActive(false);
    }
    public void UpdateRoundInfo(int currentRound, int totalRound)
    {
        txtCurrent.text = currentRound.ToString("D2");//始终保留2位整数
        txtTotal.text = txtTotal.ToString();
    }
    #endregion

    #region Unity回调
    void Awake()
    {
        UpdateRoundInfo(0, 0);
    }
    #endregion

    #region 事件回调
    public void OnRestartClick()
    {

    }
    public void OnContinueClick()
    {

    }
    public override void HandleEvent(string eventName, object data)
    {

    }
    #endregion

    #region 帮助方法
    #endregion
}
