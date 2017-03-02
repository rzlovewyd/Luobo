

public interface IReusable 
{
    /// <summary>
    /// 当取出时调用
    /// </summary>
    void OnSpawn();
    /// <summary>
    /// 当回收时调用
    /// </summary>
    void OnUnSpawn();
}
