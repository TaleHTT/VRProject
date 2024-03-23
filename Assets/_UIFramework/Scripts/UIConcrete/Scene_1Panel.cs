using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Main场景的主面板
/// </summary>
public class Scene_1Panel : BasePanel
{
    static public Scene_1Panel instanse;
    static readonly string path = "Prefabs/Panels/Scene_1Panel";


    public Scene_1Panel() : base(new UIType(path)) { }

    public override void OnEnter()
    {
        UITool.GetOrAddComponentInChildren<Button>("Back").onClick.AddListener(() =>
        {
            GameRoot.Instance.SceneSystem.SetScene(new StartScene());
        });
    }

    
}
