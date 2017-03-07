using System.IO;
using System.Xml;
using System.Text;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 工具类
/// </summary>
public static class Tools 
{
    /// <summary>
    /// 读取关卡列表
    /// </summary>
    public static List<FileInfo> GetLevelFiles()
    {
        string[] files = Directory.GetFiles(Consts.LevelDir, "*.xml");

        List<FileInfo> list = new List<FileInfo>();
        for (int i = 0; i < files.Length; i++)
        {
            FileInfo file = new FileInfo(files[i]);
            list.Add(file);
        }
        return list;
    }

    /// <summary>
    /// 读取Level数据
    /// </summary>
    public static void FillLevel(string fileName, ref Level level)
    {
        FileInfo file = new FileInfo(fileName);
        StreamReader sr = new StreamReader(file.OpenRead(), Encoding.UTF8);

        XmlDocument doc = new XmlDocument();
        doc.Load(sr);

        level.Name = doc.SelectSingleNode("/Level/Name").InnerText;
        level.Background = doc.SelectSingleNode("/Level/Background").InnerText;
        level.Road = doc.SelectSingleNode("/Level/Road").InnerText;

        XmlNodeList nodes = doc.SelectNodes("/Level/Holder/Point");
        level.Holder = new Point[nodes.Count];
        for (int i = 0; i < nodes.Count; i++)
        {
            XmlNode node = nodes[i];
            int x = int.Parse(node.Attributes["X"].Value);
            int y = int.Parse(node.Attributes["Y"].Value);
            Point p = new Point(x, y);
            level.Holder[i] = p;
        }

        nodes = doc.SelectNodes("/Level/Path/Point");
        level.Path = new Point[nodes.Count];
        for (int i = 0; i < nodes.Count; i++)
        {
            XmlNode node = nodes[i];
            int x = int.Parse(node.Attributes["X"].Value);
            int y = int.Parse(node.Attributes["Y"].Value);
            Point p = new Point(x, y);
            level.Path[i] = p;
        }

        nodes = doc.SelectNodes("/Level/Rounds/Round");
        level.Rounds = new Round[nodes.Count];
        for (int i = 0; i < nodes.Count; i++)
        {
            XmlNode node = nodes[i];
            int monster = int.Parse(node.Attributes["Monster"].Value);
            int count = int.Parse(node.Attributes["Count"].Value);
            Round r = new Round(monster, count);
            level.Rounds[i] = r;
        }

        sr.Close();
        sr.Dispose();
    }

    /// <summary>
    /// 保存Level数据
    /// </summary>
    public static void SaveLevel(string fileName,Level level)
    {
        StringBuilder sb = new StringBuilder();
        sb.AppendLine("<?xml version=\"1.0\" encoding=\"utf - 8\"?>");
        sb.AppendLine("<Level>");

        sb.AppendLine(string.Format("<Name>{0}</Name>", level.Name));
        sb.AppendLine(string.Format("<Background>{0}</Background>", level.Background));
        sb.AppendLine(string.Format("<Road>{0}</Road>", level.Road));
        sb.AppendLine(string.Format("<InitScore>{0}</InitScore>", level.InitScore));

        sb.AppendLine("<Holder>");
        for (int i = 0; i < level.Path.Length; i++)
        {
            sb.AppendLine(string.Format("<Point X=\"{0}\" Y=\"{1}\"/>", level.Holder[i].X,level.Holder[i].Y));
        }
        sb.AppendLine("</Holder>");

        sb.AppendLine("<Path>");
        for (int i = 0; i < level.Path.Length; i++)
        {
            sb.AppendLine(string.Format("<Point X=\"{0}\" Y=\"{1}\"/>", level.Path[i].X, level.Path[i].Y));
        }
        sb.AppendLine("</Path>");

        sb.AppendLine("<Rounds>");
        for (int i = 0; i < level.Path.Length; i++)
        {
            sb.AppendLine(string.Format("<<Round Monster=\"{0}\" Count=\"{1}\"/>", level.Rounds[i].Monster, level.Rounds[i].Count));
        }
        sb.AppendLine("</Rounds>");

        sb.AppendLine("</Level>");

        string content = sb.ToString();

        StringWriter sw = new StringWriter();
        sw.Write(content);
        sw.Flush();
        sw.Dispose();
    }

    /// <summary>
    /// 加载图片
    /// </summary>
    public static IEnumerator LoadImage(string url, SpriteRenderer render)
    {
        WWW www = new WWW(url);
        while (!www.isDone)
            yield return www;

        Texture2D texture = www.texture;
        Sprite sp = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
        render.sprite = sp;
    }

    public static IEnumerator LoadImage(string url, Image image)
    {
        WWW www = new WWW(url);
        while (!www.isDone)
            yield return www;

        Texture2D texture = www.texture;
        Sprite sp = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
        image.sprite = sp;
    }
}
