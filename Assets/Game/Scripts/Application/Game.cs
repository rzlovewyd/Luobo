using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;


/// <summary>
/// 游戏的起点，初始化框架
/// </summary>
[RequireComponent(typeof(Sound))]
[RequireComponent(typeof(ObjectPool))]
[RequireComponent(typeof(StaticData))]
public class Game : ApplicationBase<Game> 
{
    //全局访问的功能
    public Sound Sound = null;              //声音控制
    public ObjectPool ObjectPool = null;    //对象池
    public StaticData StaticData = null;    //静态数据
    //全局的方法


    //进入游戏
    void Start ()
	{
        DontDestroyOnLoad(gameObject);

        //全局单例赋值
        Sound = Sound.Instance;
        ObjectPool = ObjectPool.Instance;
        StaticData = StaticData.Instance;

        //注册启动命令
        RegisterController(Consts.E_StartUp, typeof(StartUpCommand));

        //启动游戏
        SendEvent(Consts.E_StartUp);
	}

    public void LoadScene(int level)
    {
        //退出旧场景

        //事件参数
        SceneArgs e = new SceneArgs(SceneManager.GetActiveScene().buildIndex);

        //发布事件
        SendEvent(Consts.E_ExitScene, e);

        //加载新场景
        SceneManager.LoadScene(level, LoadSceneMode.Single);
    }

    void OnLevelWasLoaded(int level)
    {
        Debug.Log("OnLevelWasLoaded:" + level);

        //事件参数
        SceneArgs e = new SceneArgs(level);

        SendEvent(Consts.E_EnterScene, e);
    }
}
