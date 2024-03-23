using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ����UI���ĸ��࣬����UI����״̬��Ϣ
/// </summary>
public abstract class BasePanel
{
    /// <summary>
    /// UI��Ϣ
    /// </summary>
    public UIType UIType { get; private set; }
    /// <summary>
    /// UI������
    /// </summary>
    public UITool UITool { get; private set; }

    /// <summary>
    /// ��������
    /// </summary>
    public PanelManager PanelManager { get; private set; }

    /// <summary>
    /// UI������
    /// </summary>
    public UIManager UIManager { get; private set; }

    public BasePanel(UIType uiType)
    {
        UIType = uiType;
    }

    /// <summary>
    /// ��ʼ��UITool
    /// </summary>
    /// <param name="tool"></param>
    public void Initialize(UITool tool)
    {
        UITool = tool;
    }

    /// <summary>
    /// ��ʼ����������
    /// </summary>
    /// <param name="panelManager"></param>
    public void Initialize(PanelManager panelManager)
    {
        PanelManager = panelManager;
    }

    /// <summary>
    /// ��ʼ��UI������
    /// </summary>
    /// <param name="uiManager"></param>
    public void Initialize(UIManager uiManager)
    {
        UIManager = uiManager;
    }

    /// <summary>
    /// UI����ʱִ�еĲ�����ֻ��ִ��һ��
    /// </summary>
    public virtual void OnEnter() { }

    /// <summary>
    /// UI��ͣʱִ�еĲ���
    /// </summary>
    public virtual void OnPause() 
    {
        UITool.GetOrAddComponent<CanvasGroup>().blocksRaycasts = false;
    }

    /// <summary>
    /// UI����ʱִ�еĲ���
    /// </summary>
    public virtual void OnResume() 
    {
        UITool.GetOrAddComponent<CanvasGroup>().blocksRaycasts = true;
    }

    /// <summary>
    /// UI�˳�ʱִ�еĲ���
    /// </summary>
    public virtual void OnExit() 
    {
        UIManager.DestroyUI(UIType);
    }
}
