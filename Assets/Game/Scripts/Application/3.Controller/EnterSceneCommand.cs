using UnityEngine;

public class EnterSceneCommand : Controller 
{
    public override void Execute(object data)
    {
        SceneArgs e = (SceneArgs)data;

        //注册视图（View）
        switch(e.SceneIndex)
        {
            case 0://Init

                break;
            case 1://Start
                RegisterView(GameObject.Find("UIStart").GetComponent<UIStart>());
                break;
            case 2://Select
                RegisterView(GameObject.Find("UISelect").GetComponent<UISelect>());
                break;
            case 3://Level
                RegisterView(GameObject.Find("UIBoard").GetComponent<UIBoard>());
                RegisterView(GameObject.Find("Canvas").transform.FindChild("UICountDown").GetComponent<UICountDown>());
                RegisterView(GameObject.Find("Canvas").transform.FindChild("UIWin").GetComponent<UIWin>());
                RegisterView(GameObject.Find("Canvas").transform.FindChild("UILost").GetComponent<UILost>());
                RegisterView(GameObject.Find("Canvas").transform.FindChild("UISystem").GetComponent<UISystem>());
                break;
            case 4://Complete
                RegisterView(GameObject.Find("UIComplete").GetComponent<UIComplete>());
                break;
        }
    }
}
