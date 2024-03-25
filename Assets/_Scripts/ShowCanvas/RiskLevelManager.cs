using TMPro;
using UnityEngine;

namespace _Scripts.ShowCanvas
{
    public class RiskLevelManager : MonoBehaviour
    {

        public GameObject rulaScoreTextBox;
        public GameObject rulaLevelTextBox;

        private TextMeshProUGUI textMeshProUGUI;
        private TextMeshProUGUI textMeshProUGUI1;

        // Start is called before the first frame update
        private void Start()
        {
            textMeshProUGUI1 = rulaLevelTextBox.GetComponent<TextMeshProUGUI>();
            textMeshProUGUI = rulaScoreTextBox.GetComponent<TextMeshProUGUI>();
            rulaScoreTextBox.GetComponent<TextMeshProUGUI>().SetText("0");
            rulaLevelTextBox.GetComponent<TextMeshProUGUI>().SetText("Undefined");
        }

        // Update is called once per frame
        private void Update()
        {
            var score = textMeshProUGUI.text;
            switch (score)
            {
                case "0":
                    textMeshProUGUI1.SetText("Undefined");
                    break;
                case "1":
                case "2":
                    textMeshProUGUI1.SetText("Negligible");
                    textMeshProUGUI.color = textMeshProUGUI1.color = Color.green;
                    break;
                case "3":
                case "4":
                    textMeshProUGUI1.SetText("Low");
                    textMeshProUGUI.color = textMeshProUGUI1.color = Color.yellow;
                    break;
                case "5":
                case "6":
                    textMeshProUGUI1.SetText("Medium");
                    textMeshProUGUI.color = textMeshProUGUI1.color = new Color(1, 0.5f, 0);
                    break;
                case "7":
                    textMeshProUGUI1.SetText("Very High");
                    textMeshProUGUI.color = textMeshProUGUI1.color = Color.red;
                    break;
            }
        }
    }
}
