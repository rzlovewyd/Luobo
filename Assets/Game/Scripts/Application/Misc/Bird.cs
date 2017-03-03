using UnityEngine;

public class Bird : MonoBehaviour 
{
    public float Time = 1;      //一次循环所需要的时间
    public float OffsetY = 8;   //Y方向浮动便宜

    void Start () 
	{
        iTween.MoveBy(gameObject, iTween.Hash
        (
            "y", OffsetY, 
            "easeType", iTween.EaseType.easeInOutSine, 
            "loopType", iTween.LoopType.pingPong, 
            "time", Time)
        );
	}
}
