using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using SocketWorker;
using TMPro;
using UnityEngine;
using Valve.Newtonsoft.Json;

namespace _Scripts
{
    
    public class HsscModule : MonoBehaviour
    {
        private TcpClient _tcpClient;
        private byte[] buffer = new byte[1024];
        
        public GameObject pulse;
        public GameObject oxy;
        private TextMeshProUGUI textMeshProUGUI;
        private TextMeshProUGUI textMeshProUGUI1;

        private int HeartRate { set; get; }
        private int Oxygen { set; get; }

        // Start is called before the first frame update
        private void Start()
        {
            textMeshProUGUI1 = oxy.GetComponent<TextMeshProUGUI>();
            textMeshProUGUI = pulse.GetComponent<TextMeshProUGUI>();
        }

        void Awake()
        {
            _tcpClient = new TcpClient();
            Connect();
        }

        private void Connect()
        {
            new Thread(() =>
            {
                while (true)
                {
                    Thread.Sleep(1000);
                    StartHSSBServer();
                }
            }).Start();
        }
        
        void StartHSSBServer()
        {
            _tcpClient = new TcpClient
            {
                ReceiveTimeout = 1000
            };
            try
            {
                _tcpClient.Connect("172.31.100.86", 14850);
                // post
                
                var stream = _tcpClient.GetStream();
                // Write GET header

                // 构建HTTP GET请求
                var request = "GET / HTTP/1.1\r\n" +
                                 "Host: 172.31.100.86\r\n" +
                                 "Connection: close\r\n\r\n" +
                                 "Content-Type: application/json\r\n";

                // 将请求数据转换为字节数组
                var requestData = Encoding.UTF8.GetBytes(request);
                // 发送请求数据
                stream.Write(requestData, 0, requestData.Length);
                
                
                var responseBuilder = new StringBuilder();
                var buffer = new byte[1024];
                int bytesRead;

                do
                {
                    bytesRead = stream.Read(buffer, 0, buffer.Length);
                    responseBuilder.Append(Encoding.UTF8.GetString(buffer, 0, bytesRead));
                } while (bytesRead > 0);

                var response = responseBuilder.ToString();
                var json = response.Split("\r\n\r\n")[1];

                #region Json Parser

                var jsonObject = JsonConvert.DeserializeObject<Response>(json);
                if (jsonObject.Class == "HSSC")
                {
                    var _jsonObject = JsonConvert.DeserializeObject<HSSCResponse>(json);
                    var Data = _jsonObject.Data;
                    Oxygen = Data.spo2;
                    HeartRate = Data.heartrate;
                }

                #endregion

                stream.Close();
            }
            catch (Exception e)
            {
                Debug.Log("Error" + e.Message);
                // _tcpClient.Close();
            }
        }

        // Update is called once per frame
        void Update()
        {
            textMeshProUGUI.text = HeartRate.ToString();
            textMeshProUGUI1.text = Oxygen.ToString();
        }
    }
}