
/// <summary>
/// 回合
/// </summary>
public class Round 
{
    /// <summary>
    /// 怪物类型ID
    /// </summary>
    public int Monster;
    /// <summary>
    /// 怪物数量
    /// </summary>
    public int Count;

    public Round(int monster,int count)
    {
        this.Monster = monster;
        this.Count = count;
    }
}
