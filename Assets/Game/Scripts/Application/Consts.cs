using UnityEngine;

public static class Consts 
{
    /// <summary>
    /// 关卡目录
    /// </summary>
    public static readonly string LevelDir = Application.dataPath + @"/Game/Res/Levels/";
    public static readonly string MapDir = Application.dataPath + @"/Game/Res/Maps/";
    public static readonly string CardDir = Application.dataPath + @"/Game/Res/Cards/";

    //视图
    public const string V_Start = "V_Start";
    public const string V_Select = "V_Select";
    public const string V_UIBoard = "V_UIBoard";
    public const string V_UICountDown = "V_UICountDown";
    public const string V_UIWin = "V_UIWin";
    public const string V_UILost = "V_UILost";
    public const string V_UISystem = "V_UISystem";
    public const string V_Complete = "V_Complete";

    //事件
    public const string E_StartUp = "E_StartUp";

    public const string E_EnterScene = "E_EnterScene";  //SceneArgs
    public const string E_ExitScene = "E_ExitScene";    //SceneArgs

    public const string E_StartLevel = "E_StartLevel";  //StartLevelArgs
    public const string E_EndLevel = "E_EndLevel";      //EndtLevelArgs

    public const string E_CountDownComplete = "E_CountDownComplete";

    #region 常量
    #endregion

    #region 事件
    #endregion

    #region 字段
    #endregion

    #region 属性
    #endregion

    #region 方法
    #endregion

    #region Unity回调
    #endregion

    #region 事件回调
    #endregion

    #region 帮助方法
    #endregion
}

public enum GameSpeed
{
    One,
    Two
}
