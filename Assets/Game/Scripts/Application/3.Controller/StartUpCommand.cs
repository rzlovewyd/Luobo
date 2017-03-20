public class StartUpCommand : Controller
{
    public override void Execute(object data)
    {
        //注册模型（Model）
        RegisterModel(new GameModel());
        RegisterModel(new RoundModel());

        //注册命令（Conroller）
        RegisterController(Consts.E_EnterScene, typeof(EnterSceneCommand));
        RegisterController(Consts.E_ExitScene, typeof(ExitCommand));
        RegisterController(Consts.E_StartLevel, typeof(StartLevelCommand));
        RegisterController(Consts.E_EndLevel, typeof(EndLevelCommand));
        RegisterController(Consts.E_CountDownComplete, typeof(CountDownCompleteCommand));

        //初始化
        GameModel gm = GetModel<GameModel>();
        gm.Initialize();
        
        //进入开始界面
        Game.Instance.LoadScene(1);
    }
}
