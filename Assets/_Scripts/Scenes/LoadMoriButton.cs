using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadMoriButton : MonoBehaviour
{
    private void Awake()
    {
        if (this.GetComponent<Button>() != null)
        {
            this.GetComponent<Button>().onClick.AddListener(OnButtonClick);
        }
    }

    public void OnButtonClick()
    {
        SceneManager.LoadScene("MoriScene");
    }
}
