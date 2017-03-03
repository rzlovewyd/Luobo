
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
            LevelIndex = 0
        };

        SendEvent(Consts.E_StartLevel, e);
    }
    #endregion

    #region Unity回调
    #endregion

    #region 事件回调
    public override void HandleEvent(string eventName, object obj)
    {
        
    }
    #endregion

    #region 帮助方法
    #endregion
}
