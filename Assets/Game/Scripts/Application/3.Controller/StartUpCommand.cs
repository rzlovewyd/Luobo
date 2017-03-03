public class StartUpCommand : Controller
{
    public override void Execute(object obj)
    {
        //注册模型（Model）

        //注册命令（Conroller）
        RegisterController(Consts.E_EnterScene, typeof(EnterSceneCommand));
        RegisterController(Consts.E_ExitScene, typeof(ExitCommand));

        //进入开始界面
        Game.Instance.LoadScene(1);
    }
}
