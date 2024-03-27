using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SPPBLevelManager : MonoBehaviour
{
    GameObject MovablePlayer;
    GameObject EndPoint;
    GameObject SPPBTestStartPanel;
    GameObject SPPBTestStartPanelBG;

    private void Awake()
    {
        MovablePlayer = GameObject.Find("MovablePlayer");
        EndPoint = GameObject.Find("EndPoint");
        SPPBTestStartPanel = GameObject.Find("SPPBTestStartPanel");
        SPPBTestStartPanelBG = GameObject.Find("SPPBTestStartPanelBG");

    }

    private void Update()
    {
        EndDetect();
    }

    public void EndDetect()
    {
        if(MovablePlayer.transform.position.z < EndPoint.transform.position.z)
        {
            Debug.Log("SPPB检测结束");
            SPPBTestStartPanelBG.GetComponent<Image>().color = new Color(79f/255f, 242f/255f, 28f/255f, 37f/255f);
            SPPBTestStartPanel.GetComponentInChildren<TextMeshProUGUI>().text = "SPPB已完成";
        }
    }
}
