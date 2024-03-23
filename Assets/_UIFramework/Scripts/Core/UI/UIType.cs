using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 存储单个UI的信息，名字，路径，Unity的Prefab中的文件位置不可以随意更改，因为是根据路径获取名字的
/// </summary>
public class UIType
{
    /// <summary>
    /// UI名字
    /// </summary>
    public string Name { get; private set; }

    /// <summary>
    /// UI路径
    /// </summary>
    public string Path { get; private set; }

    public UIType(string path)
    {
        Path = path;
        Name = path.Substring(path.LastIndexOf('/') + 1);
    }
}
