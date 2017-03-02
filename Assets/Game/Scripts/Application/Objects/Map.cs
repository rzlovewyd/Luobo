using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class TileClickEventArgs : EventArgs
{
    public Tile Tile;
    public int MouseButton; //0左键，1右键
    public TileClickEventArgs(int mouseButton, Tile tile)
    {
        this.Tile = tile;
        this.MouseButton = mouseButton;
    }
}


/// <summary>
/// 用于描述一个关卡地图的状态
/// </summary>
public class Map : MonoBehaviour 
{
    #region 常量
    public const int RowCount = 8;      //行数
    public const int ColumnCount = 12;  //列数
    #endregion

    #region 事件
    public event EventHandler<TileClickEventArgs> OnTileClick;
    #endregion

    #region 字段
    float MapWidth;     //地图宽
    float MapHeight;    //地图高

    float TileWidth;    //格子高
    float TileHeight;   //格子高

    List<Tile> m_Grid = new List<Tile>();   //格子集合
    List<Tile> m_Road = new List<Tile>();   //路经集合

    Level m_Level;      //关卡数据

    public bool DrawGizmos = true;  //是否绘制网格
    #endregion

    #region 属性
    public Level Level
    {
        get { return m_Level; }
    }
    public string BackgroundImage
    {
        set
        {
            SpriteRenderer render = transform.FindChild("Background").GetComponent<SpriteRenderer>();
            StartCoroutine(Tools.LoadImage(value, render));
        }
    }

    public string RoadImage
    {
        set
        {
            SpriteRenderer render = transform.FindChild("Road").GetComponent<SpriteRenderer>();
            StartCoroutine(Tools.LoadImage(value, render));
        }
    }

    public List<Tile> Grid
    {
        get { return m_Grid; }
    }

    public List<Tile> Road
    {
        get { return m_Road; }
    }

    /// <summary>
    /// 怪物的寻路路径集合
    /// </summary>
    public Vector3[] Path
    {
        get
        {
            List<Vector3> m_Path = new List<Vector3>();
            for (int i = 0; i < m_Road.Count; i++)
            {
                Tile t = m_Road[i];
                Vector3 point = GetPosition(t);
                m_Path.Add(point);
            }
            return m_Path.ToArray();
        }
    }
    #endregion

    #region 方法
    public void LoadLevel(Level level)
    {
        if (level == null)
        {
            Debug.LogError("Load Level Error!");
            return;
        }

        //清空当前状态
        Clear();

        this.m_Level = level;

        //加载图片
        this.BackgroundImage = "file://" + Consts.MapDir + level.Background;
        this.RoadImage = "file://" + Consts.MapDir + level.Road;

        //寻路路径
        for (int i = 0; i < level.Path.Length; i++)
        {
            Point p = level.Path[i];
            Tile t = GetTile(p.X, p.Y);
            m_Road.Add(t);
        }

        //炮台空地
        for (int i = 0; i < m_Level.Holder.Length; i++)
        {
            Point p = level.Holder[i];
            Tile t = GetTile(p.X, p.Y);
            t.CanHold = true;
        }
    }

    /// <summary>
    /// 清除塔位信息
    /// </summary>
    public void ClearHolder()
    {
        for (int i = 0; i < m_Grid.Count; i++)
        {
            if (m_Grid[i].CanHold)
                m_Grid[i].CanHold = false;
        }
    }

    /// <summary>
    /// 清除寻路格子集合
    /// </summary>
    public void ClearRoad()
    {
        m_Road.Clear();
    }

    public void Clear()
    {
        m_Level = null;
        ClearHolder();
        ClearRoad();
    }
    #endregion

    #region Unity回调
    void Awake()
    {
        //计算地图和格子大小
        CalculateSize();

        //创建所有的格子
        for (int i = 0; i < RowCount; i++)
            for (int j = 0; j < ColumnCount; j++)
                m_Grid.Add(new Tile(j, i));

        //监听鼠标点击事件
        OnTileClick += Map_OnTileClick;
    }

    void Update()
    {
        //鼠标左键检测
        if(Input.GetMouseButtonDown(0))
        {
            Tile t = GetTileUnderMouse();

            if (t != null)
            {
                //触发鼠标左键点击事件
                TileClickEventArgs e = new TileClickEventArgs(0, t);
                if (OnTileClick != null)
                {
                    OnTileClick(this, e);
                }
            }
        }

        //鼠标右键键检测
        if (Input.GetMouseButtonDown(1))
        {
            Tile t = GetTileUnderMouse();

            if (t != null)
            {
                //触发鼠标右键点击事件
                TileClickEventArgs e = new TileClickEventArgs(1, t);
                if (OnTileClick != null)
                {
                    OnTileClick(this, e);
                }
            }
        }
    }

    void OnDrawGizmos()
    {
        if (!DrawGizmos)
            return;

        //计算地图和格子大小
        CalculateSize();

        //绘制格子
        Gizmos.color = Color.green;

        //绘制行
        for (int row = 0; row <= RowCount; row++)
        {
            Vector2 from = new Vector2(-MapWidth / 2, -MapHeight / 2 + row * TileHeight);
            Vector2 to = new Vector2(-MapWidth / 2 + MapWidth, -MapHeight / 2 + row * TileHeight);
            Gizmos.DrawLine(from, to);
        }

        //绘制列
        for (int col = 0; col <= ColumnCount; col++)
        {
            Vector2 from = new Vector2(-MapWidth / 2 + col * TileWidth, -MapHeight / 2);
            Vector2 to = new Vector2(-MapWidth / 2 + col * TileWidth, -MapHeight / 2 + MapHeight);
            Gizmos.DrawLine(from, to);
        }

        for (int i = 0; i < m_Grid.Count; i++)
        {
            if(m_Grid[i].CanHold)
            {
                Vector3 pos = GetPosition(m_Grid[i]);
                Gizmos.DrawIcon(pos, "holder.png", true);
            }
        }

        Gizmos.color = Color.red;
        for (int i = 0; i < m_Road.Count; i++)
        {
            //起点
            if (i == 0)
            {
                Gizmos.DrawIcon(GetPosition(m_Road[i]), "start.png", true);
            }

            if(m_Road.Count > 1 && i == m_Road.Count - 1)
            {
                Gizmos.DrawIcon(GetPosition(m_Road[i]), "end.png", true);
            }

            //红色的连线
            if(m_Road.Count > 1 && i != 0)
            {
                Vector2 from = GetPosition(m_Road[i - 1]);
                Vector2 to = GetPosition(m_Road[i]);
                Gizmos.DrawLine(from, to);
            }
        }
    }
    #endregion

    #region 事件回调
    private void Map_OnTileClick(object sender, TileClickEventArgs e)
    {
        if (Level == null)
            return;

        //处理放塔操作
        if(e.MouseButton == 0 && !m_Road.Contains(e.Tile))
        {
            e.Tile.CanHold = !e.Tile.CanHold;
        }

        //处理寻路点操作
        if(e.MouseButton == 1 && !e.Tile.CanHold)
        {
            if (m_Road.Contains(e.Tile))
                m_Road.Remove(e.Tile);
            else
                m_Road.Add(e.Tile);
        }
    }
    #endregion

    #region 帮助函数
    //计算地图大小，格子大小
    void CalculateSize()
    {
        Vector3 p1 = Camera.main.ViewportToWorldPoint(new Vector3(0, 0));//左下
        Vector3 p2 = Camera.main.ViewportToWorldPoint(new Vector3(1, 1));//右上

        MapWidth = (p2.x - p1.x);
        MapHeight = (p2.y - p1.y);

        TileWidth = MapWidth / ColumnCount;
        TileHeight = MapHeight / RowCount;
    }

    //获取格子中心点所在的世界坐标
    Vector3 GetPosition(Tile t)
    {
        return new Vector3(-MapWidth / 2 + (t.X + 0.5f) * TileWidth, -MapHeight / 2 + (t.Y + 0.5f) * TileHeight, 0);
    }

    //根据格子索引号获得格子
    Tile GetTile(int tileX, int tileY)
    {
        int index = tileX + tileY * ColumnCount;

        if (index < 0 || index >= m_Grid.Count)
            return null;

        return m_Grid[index];
    }

    //获取鼠标位置的格子
    Tile GetTileUnderMouse()
    {
        Vector2 worldPos = GetMouseWorldPosition();

        int col = (int)((worldPos.x + MapWidth / 2.0f) / TileWidth);
        int row = (int)((worldPos.y + MapHeight / 2.0f) / TileHeight);

        return GetTile(col, row);
    }

    //获取鼠标的世界坐标
    Vector3 GetMouseWorldPosition()
    {
        Vector3 viewPos = Camera.main.ScreenToViewportPoint(Input.mousePosition);
        Vector3 worldPos = Camera.main.ViewportToWorldPoint(viewPos);
        return worldPos;
    }
    #endregion
}
