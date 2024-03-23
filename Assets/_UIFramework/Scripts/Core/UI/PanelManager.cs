using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ������������ջ���洢UI
/// </summary>
public class PanelManager
{
    /// <summary>
    /// �洢UI����ջ
    /// </summary>
    private Stack<BasePanel> stackPanel;
    /// <summary>
    /// UI������
    /// </summary>
    private UIManager uiManager;
    private BasePanel panel;

    public PanelManager()
    {
        stackPanel = new Stack<BasePanel>();
        uiManager = new UIManager();
    }

    /// <summary>
    /// UI����ջ�������˲�������ʾһ�����
    /// </summary>
    /// <param name="nextPanel">Ҫ��ʾ�����</param>
    public void Push(BasePanel nextPanel)
    {
        if(stackPanel.Count > 0)
        {
            panel = stackPanel.Peek();
            panel.OnPause();
        }
        stackPanel.Push(nextPanel);
        GameObject panelGO = uiManager.GetSingleUI(nextPanel.UIType);
        nextPanel.Initialize(new UITool(panelGO));
        nextPanel.Initialize(this);
        nextPanel.Initialize(uiManager);
        nextPanel.OnEnter();

    }

    /// <summary>
    /// ִ������ջ�������˲�����ִ������OnExit����
    /// </summary>
    public void Pop()
    {
        if (stackPanel.Count > 0)
        {
            stackPanel.Peek().OnExit();
            stackPanel.Pop();
        }
        if(stackPanel.Count > 0)
            stackPanel.Peek().OnResume();
    }

    /// <summary>
    /// ִ����������OnExit()
    /// </summary>
    public void PopAll()
    {
        while(stackPanel.Count > 0)
        {
            stackPanel.Pop().OnExit();
        }
    }
}
