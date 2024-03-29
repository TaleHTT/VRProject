using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SPPBLevelManager : MonoBehaviour
{
    public static SPPBLevelManager instance;
    GameObject VRCamera;
    GameObject EndPoint;
    GameObject SPPBTestStartPanel;
    GameObject SPPBTestStartPanelBG;
    private GameObject VRCanvas;
    public float testBeginTime;
    public float testEndTime;

    private void Awake()
    {
        instance = this;
        VRCanvas = GameObject.Find("VRCanvas");
        VRCamera = GameObject.Find("VRCamera");
        EndPoint = GameObject.Find("EndPoint");
        SPPBTestStartPanel = GameObject.Find("SPPBTestStartPanel");
        SPPBTestStartPanelBG = GameObject.Find("SPPBTestStartPanelBG");

    }

    private void Update()
    {
        EndDetect();
    }

    private bool passOnce = false;
    public void EndDetect()
    {
        if(VRCamera.transform.position.z < EndPoint.transform.position.z && !passOnce)
        {
            passOnce = true;
            Debug.Log("SPPB已完成");
            testEndTime = Time.time;    
            SPPBTestStartPanelBG.GetComponent<Image>().color = new Color(79f/255f, 242f/255f, 28f/255f, 37f/255f);
            SPPBTestStartPanel.GetComponentInChildren<TextMeshProUGUI>().text = "SPPB测试未进行";
            StartCoroutine(ShowCanvas());
            CalPoint();
        }
    }
    
    IEnumerator ShowCanvas()
    {
        VRCanvas.SetActive(true);
        VRCanvas.GetComponentInChildren<TextMeshProUGUI>().text = "测试完成";
        yield return new WaitForSeconds(3);
        VRCanvas.SetActive(false);
    }

    public void CalPoint()
    {
        Debug.Log("CalPoint Start");
    }
}
