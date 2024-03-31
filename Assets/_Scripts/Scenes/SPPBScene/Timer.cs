using System;
using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using Valve.VR;



public class Timer:MonoBehaviour
{
    public static Timer instance;
    //public float totalTime = 10f; // �ܼ�ʱʱ��
    public float currentTime; // ��ǰ��ʱʱ��
    public TextMeshProUGUI timerText; // ��ʱ�����ı����

    private void Awake()
    {
        instance = this;
        Timer.instance.timerText = GameObject.Find("TimerText").GetComponent<TextMeshProUGUI>();
        timerText.enabled = false;
    }

    public void TimerDownStart(float totalTime)
    {
        timerText.enabled = true;
        StartCoroutine(UpdateTimerDown(totalTime));
    }

    private IEnumerator UpdateTimerDown(float totalTime)
    {
        currentTime = totalTime;
        while (currentTime > 0)
        {
            if(PlayerPressButtonA.instance.input.GetStateDown(SteamVR_Input_Sources.Any))
            {
                break;
            }
            currentTime -= Time.deltaTime;
            timerText.text = currentTime.ToString("F2"); // ����ʱ��ʱ����ʾΪ��λС��
            yield return null;
        }
        timerText.text = "Time's up!";
        timerText.enabled = false;
    }

    public void TimerUpStart(float totalTime)
    {
        timerText.enabled = true;
        StartCoroutine(UpdateTimerUp(totalTime));
    }
    
    private IEnumerator UpdateTimerUp(float totalTime)
    {
        currentTime = 0;
        while (currentTime < totalTime)
        {
            if(PlayerPressButtonA.instance.input.GetStateDown(SteamVR_Input_Sources.Any))
            {
                break;
            }
            currentTime += Time.deltaTime;
            timerText.text = currentTime.ToString("F2"); // ����ʱ��ʱ����ʾΪ��λС��
            yield return null;
        }
        timerText.text = "Time's up!";
        timerText.enabled = false;
    }

    public void TimerStopShowing()
    {
        timerText.enabled = false;
    }
}
