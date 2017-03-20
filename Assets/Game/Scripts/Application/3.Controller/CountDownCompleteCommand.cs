public class CountDownCompleteCommand : Controller
{
    public override void Execute(object data)
    {
        //开始出怪
        RoundModel rModel = GetModel<RoundModel>();
        rModel.StartRound();
    }
}
