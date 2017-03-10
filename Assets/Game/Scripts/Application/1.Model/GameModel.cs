using System;
using System.Collections.Generic;
using System.IO;

public class GameModel : Model
{
    #region 常量
    #endregion

    #region 事件
    #endregion

    #region 字段
    //所有关卡
    List<Level> m_Levels = new List<Level>();

    //游戏当前的分数
    int m_Score = 0;

    //是否在游戏中
    bool m_IsPlaying = true;

    //当前关卡索引
    int m_PlayLevelIndex = -1;

    //最大通关关卡索引
    int m_GameProgress = -1;
    #endregion

    #region 属性
    public override string Name
    {
        get { return Consts.M_GameModel; }
    }

    public List<Level> AllLevels
    {
        get { return m_Levels; }
    }

    public Level PlayLevel
    {
        get
        {
            if (PlayLevelIndex < 0 || PlayLevelIndex >= m_Levels.Count)
                throw new IndexOutOfRangeException("关卡不存在");

            return m_Levels[PlayLevelIndex];
        }
    }

    public int Score
    {
        get { return m_Score; }

        set { m_Score = value; }
    }

    public bool IsPlaying
    {
        get { return m_IsPlaying; }

        set { m_IsPlaying = value; }
    }

    public int LevelCount
    {
        get { return m_Levels.Count; }
    }

    public bool IsGamePassd
    {
        get { return GameProgress >= LevelCount - 1; }
    }

    public int GameProgress
    {
        get { return m_GameProgress; }
    }

    public int PlayLevelIndex
    {
        get { return m_PlayLevelIndex; }
    }
    #endregion

    #region 方法
    public void Initialize()
    {
        //构建Level集合
        List<FileInfo> files = Tools.GetLevelFiles();
        for (int i = 0; i < files.Count; i++)
        {
            Level level = new Level();
            Tools.FillLevel(files[i].FullName, ref level);
            m_Levels.Add(level);
        }

        //读取游戏进度
        m_GameProgress = Saver.GetProgress();
    }

    //游戏开始
    public void StartLevel(int levelIndex)
    {
        m_PlayLevelIndex = levelIndex;
        m_IsPlaying = true;
    }

    //游戏结束
    public void StopLevel(bool isWin)
    {
        if(isWin && PlayLevelIndex > GameProgress)
        {
            Saver.SetProgress(PlayLevelIndex);
        }

        IsPlaying = false;
    }

    //清档
    public void ClearProgress()
    {
        m_IsPlaying = false;
        m_GameProgress = -1;
        m_PlayLevelIndex = -1;

        Saver.SetProgress(-1);
    }
    #endregion

    #region Unity回调
    #endregion

    #region 事件回调
    #endregion

    #region 帮助方法
    #endregion
}