
/// <summary>
/// 关卡信息
/// </summary>
public class Level 
{
    /// <summary>
    /// 名字
    /// </summary>
    public string Name;

    /// <summary>
    /// 背景
    /// </summary>
    public string Background;

    /// <summary>
    /// 路径
    /// </summary>
    public string Road;

    /// <summary>
    /// 初始金币
    /// </summary>
    public int InitScore;

    /// <summary>
    /// 炮塔可放置的位置
    /// </summary>
    public Point[] Holder;

    /// <summary>
    /// 怪物行走的路径
    /// </summary>
    public Point[] Path;

    /// <summary>
    /// 出怪回合信息
    /// </summary>
    public Round[] Rounds;
}
