using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using System.IO;

[CustomEditor(typeof(Map))]
public class MapEditor : Editor
{
    [HideInInspector]
    public Map m_Map = null;

    //关卡列表
    List<FileInfo> m_Files = new List<FileInfo>();

    //当前编辑的关卡索引号
    int m_SelectIndex = -1;

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if (Application.isPlaying)
        {
            //关联的Mono脚本组件
            m_Map = target as Map;

            EditorGUILayout.BeginHorizontal();
            int currentIndex = EditorGUILayout.Popup(m_SelectIndex, GetNames(m_Files));
            if (currentIndex != m_SelectIndex)
            {
                m_SelectIndex = currentIndex;
                LoadLevel();
            }
            if (GUILayout.Button("读取列表"))
            {
                //读取关卡列表
                LoadLevelFiles();
            }
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("清楚塔点"))
            {
                m_Map.ClearHolder();
            }
            if (GUILayout.Button("清楚路径"))
            {
                m_Map.ClearRoad();
            }
            EditorGUILayout.EndHorizontal();

            if (GUILayout.Button("保存数据"))
            {
                SaveLevel();
            }
        }

        if (GUI.changed)
            EditorUtility.SetDirty(target);
    }

    //读取关卡列表
    void LoadLevelFiles()
    {
        //清除状态
        Clear();

        //加载列表
        m_Files = Tools.GetLevelFiles();

        //默认加载第一个关卡
        if(m_Files.Count > 0)
        {
            m_SelectIndex = 0;
            LoadLevel();
        }
    }

    void Clear()
    {
        m_Files.Clear();
        m_SelectIndex = -1;
    }

    //读取关卡
    void LoadLevel()
    {
        FileInfo file = m_Files[m_SelectIndex];

        Level level = new Level();
        Tools.FillLevel(file.FullName, ref level);

        m_Map.LoadLevel(level);
    }

    //保存关卡
    void SaveLevel()
    {
        Level level = m_Map.Level;

        List<Point> list = new List<Point>();
        //收集放塔点
        for (int i = 0; i < m_Map.Grid.Count; i++)
        {
            Tile t = m_Map.Grid[i];
            if (t.CanHold)
            {
                Point p = new Point(t.X, t.Y);
                list.Add(p);
            }
        }
        level.Holder = list.ToArray();

        //收集寻路点
        for (int i = 0; i < m_Map.Road.Count; i++)
        {
            Tile t = m_Map.Road[i];
            Point p = new Point(t.X, t.Y);
            list.Add(p);
        }
        level.Path = list.ToArray();

        //保存关卡
        string fileName = m_Files[m_SelectIndex].FullName;
        Tools.SaveLevel(fileName, level);

        //弹框提示
        EditorUtility.DisplayDialog("保存关卡数据", "保存成功", "确定");
    }

    string[] GetNames(List<FileInfo> files)
    {
        List<string> names = new List<string>();
        for (int i = 0; i < files.Count; i++)
        {
            names.Add(files[i].Name);
        }
        return names.ToArray();
    }
}
