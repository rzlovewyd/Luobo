public class StartLevelCommand : Controller
{
    public override void Execute(object data)
    {
        StartLevelArgs e = (StartLevelArgs)data;

        //第一步
        GameModel gModel = GetModel<GameModel>();
        gModel.StartLevel(e.LevelID);

        //第二步
        RoundModel rModel = GetModel<RoundModel>();
        rModel.LoadLevel(gModel.PlayLevel);

        //进入游戏
        Game.Instance.LoadScene(3);
    }
}
