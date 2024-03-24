using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ����ȫ��
/// </summary>
public class GameRoot : MonoBehaviour
{
    public static GameRoot Instance { get; private set; }
    /// <summary>
    /// ����������
    /// </summary>
    public SceneSystem SceneSystem { get; private set; }


    private void Awake()
    {

        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }

        SceneSystem = new SceneSystem();
        DontDestroyOnLoad(this.gameObject);
    }

    private void Start()
    {
        SceneSystem.SetScene(new StartScene());
    }
}
