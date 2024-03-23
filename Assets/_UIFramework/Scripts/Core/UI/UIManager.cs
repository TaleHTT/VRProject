using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �洢����UI��Ϣ�������Դ�����������UI
/// </summary>
public class UIManager
{
    /// <summary>
    /// �洢����UI��Ϣ���ֵ䣬ÿһ��UI��Ϣ�����Ӧһ��GameObject
    /// </summary>
    private Dictionary<UIType, GameObject> dicUI;

    public UIManager() 
    {
        dicUI = new Dictionary<UIType, GameObject>();
    }

    /// <summary>
    /// ��ȡһ��UI����
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    public GameObject GetSingleUI(UIType type)
    {
        GameObject parent = GameObject.Find("Canvas");
        if (!parent)
        {
            Debug.LogError("Canvas�����ڣ���������޸ö���");
            return null; 
        }
        if(dicUI.ContainsKey(type)) 
            return dicUI[type];
        GameObject ui = GameObject.Instantiate(Resources.Load<GameObject>(type.Path), parent.transform);
        ui.name = type.Name;
        dicUI.Add(type, ui);
        return ui;
    }

    /// <summary>
    /// ����һ��UI����
    /// </summary>
    /// <param name="tyep">UI��Ϣ</param>
    public void DestroyUI(UIType type)
    {
        if (dicUI.ContainsKey(type))
        {
            GameObject.Destroy(dicUI[type]);
            dicUI.Remove(type);
        }
    }
}
