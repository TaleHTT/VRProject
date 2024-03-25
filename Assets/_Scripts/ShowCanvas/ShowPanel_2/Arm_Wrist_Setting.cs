using UnityEngine;
using UnityEngine.UI;

namespace _Scripts.ShowCanvas.ShowPanel_2
{
    public class UniversalSettingManager : MonoBehaviour
    {
        private Toggle[] toggles;

        private void Start()
        {
            // 获取Toggle Group中的所有Toggle
            toggles = GetComponentsInChildren<Toggle>();
            var setting = SocketWorker.SocketWorker.Instance.getSetting();
            var propertyInfo = setting.GetType().GetProperty(gameObject.name);
            var value = propertyInfo?.GetValue(setting, null);
            // 遍历每个Toggle
            foreach (var toggle in toggles)
                if (toggle.name == value?.ToString())
                    toggle.isOn = true;

            foreach (var toggle in toggles)
            {
                toggle.onValueChanged.AddListener(isOn => OnToggleValueChanged(toggle, isOn));
            }
        }

        private void OnToggleValueChanged(Object toggle, bool isOn)
        {
            if (!isOn) return;
            var settingText = "{\"" + gameObject.name + "\":\"" + toggle.name + "\"}";
            var jsonBytes = System.Text.Encoding.UTF8.GetBytes(settingText);
            SocketWorker.SocketWorker.Instance.SendData("SettingChange", jsonBytes);
        }
    }
}