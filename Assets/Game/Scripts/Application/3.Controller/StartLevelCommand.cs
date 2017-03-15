public class StartLevelCommand : Controller
{
    public override void Execute(object data)
    {
        StartLevelArgs e = (StartLevelArgs)data;

        GameModel gModel = GetModel<GameModel>();
        gModel.StartLevel(e.LevelID);
        //进入游戏
        Game.Instance.LoadScene(3);
    }
}
