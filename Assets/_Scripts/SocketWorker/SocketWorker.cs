using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Threading;
using Windows.Kinect;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Valve.Newtonsoft.Json;

// ReSharper disable All

namespace SocketWorker
{
    public class KinectSkeleton
    {
        public string Class;
        public byte[] Data;
    }

    public class RULAScore
    {
        public int Upper_Arm_Score;
        public int Lower_Arm_Score;
        public int Wrist_Score;
        public int Wrist_Twist_Score;
        public int Neck_Score;

        public int Trunk_Score;
        public int Leg_Score;
        public int Muscle_Use_Score_A;
        public int Force_Load_Score_A;
        public int Muscle_Use_Score_B;

        public int Force_Load_Score_B;
        public int Wrist_Arm_Score;
        public int Neck_Trunk_Leg_Score;
        public int Table_A;
        public int Table_B;

        public int RULA_Score;
    }

    public class RULAAngle
    {
        public double Upper_Arm_right_angle;
        public double Upper_Arm_left_angle;
        public double Shoulder_Raised;
        public double Arm_Abducted;
        public double Lower_Arm_right_angle;
        public double Lower_Arm_left_angle;
        public double Wrist_right_angle;

        public double Wrist_left_angle;

        // public double Wrist_Adjustments;
        public double Neck_angle;
        public double Neck_side_bending;
        public double Trunk_angle;
        public double Trunk_side_bending;
    }


    public class ManualSetting
    {
        public string Left_Wrist_Twist { set; get; }
        public string Right_Wrist_Twist { set; get; }
        public string Left_Muscle_use { set; get; }
        public string Right_Muscle_use { set; get; }
        public string Left_Arm_Load { set; get; }
        public string Right_Arm_Load { set; get; }
        public string Leg_Feet_Supported { set; get; }
        public string Muscle_use { set; get; }
        public string Leg_Load { set; get; }
        public string Left_Wrist_Deviation { set; get; }
        public string Right_Wrist_Deviation { set; get; }
    }

    public class Response
    {
        public string Class;
        public object Data;
    }

    public class ScoreResponse
    {
        public string Class;
        public RULAScore Data;
    }

    public class AngleResponse
    {
        public string Class;
        public RULAAngle Data;
    }

    public class ManualSettingResponse
    {
        public string Class;
        public ManualSetting Data;
    }

    public class SocketWorker : MonoBehaviour
    {
        private static SocketWorker instance;
        private Body[] _bodies;
        private Body _body;
        private KinectSensor _kinectSensor;
        private TcpClient _tcpClient;
        private Flag _flag;
        private BodyFrameReader _reader;

        public RULAScore Score;
        public RULAAngle Angle;
        public ManualSetting Setting;

        public GameObject Upper_Arm_Score;
        public GameObject Lower_Arm_Score;
        public GameObject Wrist_Score;
        public GameObject WristTwistScore;
        public GameObject Neck_Score;

        public GameObject Trunk_Score;
        public GameObject Leg_Score;
        public GameObject MuscleUseScoreA;
        public GameObject Force_LoadScoreA;
        public GameObject MuscleUseScoreB;

        public GameObject Force_LoadScoreB;
        public GameObject Wrist_ArmScore;
        public GameObject NTLScore;
        public GameObject TableAScore;
        public GameObject TableBScore;

        public GameObject RULA_Score;

        public GameObject Upper_Arm_right_angle;
        public GameObject Upper_Arm_left_angle;
        public GameObject Shoulder_Raised;
        public GameObject Arm_Abducted;
        public GameObject Lower_Arm_right_angle;
        public GameObject Lower_Arm_left_angle;
        public GameObject Wrist_right_angle;

        public GameObject Wrist_left_angle;

        // public GameObject Wrist_Adjustments;
        public GameObject Neck_angle;
        public GameObject Neck_side_bending;
        public GameObject Trunk_angle;
        public GameObject Trunk_side_bending;

        public GameObject StatusTipPanel;


        public static SocketWorker Instance
        {
            get { return instance; }
        }

        private bool Status { get; set; }

        private void Awake()
        {
            _kinectSensor = KinectSensor.GetDefault();
            _flag = new Flag();
            _bodies = new Body[_kinectSensor.BodyFrameSource.BodyCount];
            _tcpClient = new TcpClient();
            instance = this;
            Score = new RULAScore();
            Angle = new RULAAngle();
            Connect();


            Status = false;

            FindGameObject();
            GameObject.Find("ShowPanel_3").SetActive(false);
            // GameObject.Find("ShowPanel_1").SetActive(false);
        }


        private void Update()
        {
            Reader_FrameArrived();
            ShowPanel_1_TextGet();
            ShowPanel_3_TextGet();
            InfoStatusOK();
        }


        public void ShowPanel_1_TextGet()
        {
            Upper_Arm_Score.GetComponent<TextMeshProUGUI>().text = Score.Upper_Arm_Score.ToString();
            Lower_Arm_Score.GetComponent<TextMeshProUGUI>().text = Score.Lower_Arm_Score.ToString();
            Wrist_Score.GetComponent<TextMeshProUGUI>().text = Score.Wrist_Score.ToString();
            WristTwistScore.GetComponent<TextMeshProUGUI>().text = Score.Wrist_Twist_Score.ToString();
            Neck_Score.GetComponent<TextMeshProUGUI>().text = Score.Neck_Score.ToString();

            Trunk_Score.GetComponent<TextMeshProUGUI>().text = Score.Trunk_Score.ToString();
            Leg_Score.GetComponent<TextMeshProUGUI>().text = Score.Leg_Score.ToString();
            MuscleUseScoreA.GetComponent<TextMeshProUGUI>().text = Score.Muscle_Use_Score_A.ToString();
            Force_LoadScoreA.GetComponent<TextMeshProUGUI>().text = Score.Force_Load_Score_A.ToString();
            MuscleUseScoreB.GetComponent<TextMeshProUGUI>().text = Score.Muscle_Use_Score_B.ToString();

            Force_LoadScoreB.GetComponent<TextMeshProUGUI>().text = Score.Force_Load_Score_B.ToString();
            Wrist_ArmScore.GetComponent<TextMeshProUGUI>().text = Score.Wrist_Arm_Score.ToString();
            NTLScore.GetComponent<TextMeshProUGUI>().text = Score.Neck_Trunk_Leg_Score.ToString();
            TableAScore.GetComponent<TextMeshProUGUI>().text = Score.Table_A.ToString();
            TableBScore.GetComponent<TextMeshProUGUI>().text = Score.Table_B.ToString();

            RULA_Score.GetComponent<TextMeshProUGUI>().text = Score.RULA_Score.ToString();
        }

        public void ShowPanel_3_TextGet()
        {
            Upper_Arm_right_angle.GetComponent<TextMeshProUGUI>().text = Angle.Upper_Arm_right_angle.ToString();
            Upper_Arm_left_angle.GetComponent<TextMeshProUGUI>().text = Angle.Upper_Arm_left_angle.ToString();
            Shoulder_Raised.GetComponent<TextMeshProUGUI>().text = Angle.Shoulder_Raised.ToString();
            Arm_Abducted.GetComponent<TextMeshProUGUI>().text = Angle.Arm_Abducted.ToString();
            Lower_Arm_right_angle.GetComponent<TextMeshProUGUI>().text = Angle.Lower_Arm_right_angle.ToString();

            Lower_Arm_left_angle.GetComponent<TextMeshProUGUI>().text = Angle.Lower_Arm_left_angle.ToString();
            Wrist_right_angle.GetComponent<TextMeshProUGUI>().text = Angle.Wrist_right_angle.ToString();
            Wrist_left_angle.GetComponent<TextMeshProUGUI>().text = Angle.Wrist_left_angle.ToString();
            // Wrist_Adjustments.GetComponent<TextMeshProUGUI>().text = Angle.Wrist_Adjustments.ToString();
            Neck_angle.GetComponent<TextMeshProUGUI>().text = Angle.Neck_angle.ToString();

            Neck_side_bending.GetComponent<TextMeshProUGUI>().text = Angle.Neck_side_bending.ToString();
            Trunk_angle.GetComponent<TextMeshProUGUI>().text = Angle.Trunk_angle.ToString();
            Trunk_side_bending.GetComponent<TextMeshProUGUI>().text = Angle.Trunk_side_bending.ToString();
        }

        public void FindGameObject()
        {
            Upper_Arm_Score = GameObject.Find("UpperArmScore");
            Lower_Arm_Score = GameObject.Find("LowerArmScore");
            Wrist_Score = GameObject.Find("WristScore");
            WristTwistScore = GameObject.Find("WristTwistScore");
            Neck_Score = GameObject.Find("NeckScore");

            Trunk_Score = GameObject.Find("TrunkScore");
            Leg_Score = GameObject.Find("LegScore");
            MuscleUseScoreA = GameObject.Find("MuscleUseScoreA");
            Force_LoadScoreA = GameObject.Find("Force/LoadScoreA");
            MuscleUseScoreB = GameObject.Find("MuscleUseScoreB");

            Force_LoadScoreB = GameObject.Find("Force/LoadScoreB");
            Wrist_ArmScore = GameObject.Find("Wrist/ArmScore");
            NTLScore = GameObject.Find("NeckTrunkLegScore");
            TableAScore = GameObject.Find("TableAScore");
            TableBScore = GameObject.Find("TableBScore");

            RULA_Score = GameObject.Find("FinalScore");

            Upper_Arm_right_angle = GameObject.Find("UpperArmRightAngle");
            Upper_Arm_left_angle = GameObject.Find("UpperArmLeftAngle");
            Shoulder_Raised = GameObject.Find("ShoulderRaisedAngle");
            Arm_Abducted = GameObject.Find("ArmAbductedAngle");
            Lower_Arm_right_angle = GameObject.Find("LowerArmRightAngle");
            Lower_Arm_left_angle = GameObject.Find("LowerArmLeftAngle");
            Wrist_right_angle = GameObject.Find("WristRightAngle");
            Wrist_left_angle = GameObject.Find("WristLeftAngle");
            // Wrist_Adjustments = GameObject.Find("WristAdjustments");
            Neck_angle = GameObject.Find("NeckAngle");
            Neck_side_bending = GameObject.Find("NeckSideBending");
            Trunk_angle = GameObject.Find("TrunkAngle");
            Trunk_side_bending = GameObject.Find("TrunkSideBending");

            StatusTipPanel = GameObject.Find("ConnectionTipLabel");
        }

        private class Flag
        {
            public bool Status { get; set; } = true;
        }

        public byte[] ImageBytes { get; set; } = null;


        public void Connect()
        {
            // Debug.Log("Connected");
            _flag.Status = true;
            _reader = _kinectSensor.BodyFrameSource.OpenReader();


            new Thread(() =>
            {
                _kinectSensor.Open();

                while (true)
                {
                    ReceiveData();
                    if (!_flag.Status) break;
                }

                _kinectSensor.Close();
            }).Start();
        }


        public void Disconnect()
        {
            _flag.Status = false;
        }


        private void Reader_FrameArrived()
        {
            var dataReceived = false;

            if (_reader == null) return;

            using (var bodyFrame = _reader.AcquireLatestFrame())
            {
                if (bodyFrame != null)
                {
                    bodyFrame.GetAndRefreshBodyData(_bodies);
                    dataReceived = true;
                }
            }

            if (!dataReceived) return;

            const int head = 25 * 3 * 4;
            foreach (var body in _bodies.Where(body => body.IsTracked))
            {
                Status = true;
                _body = body;
                var dataOrigin = GetDataFromBody(body);
                var len1 = dataOrigin.Item1.Count * 3 * 4;
                var len2 = dataOrigin.Item2.Count * 4 * 4;
                var data = new byte[len1 + len2];


                for (var i = 0; i < 25; i++)
                {
                    BitConverter.GetBytes(dataOrigin.Item1[i].X).CopyTo(data, i * 3 * 4);
                    BitConverter.GetBytes(dataOrigin.Item1[i].Y).CopyTo(data, i * 3 * 4 + 4);
                    BitConverter.GetBytes(dataOrigin.Item1[i].Z).CopyTo(data, i * 3 * 4 + 8);
                }

                for (var i = 0; i < 25; i++)
                {
                    BitConverter.GetBytes(dataOrigin.Item2[i].X).CopyTo(data, head + i * 4 * 4);
                    BitConverter.GetBytes(dataOrigin.Item2[i].Y).CopyTo(data, head + i * 4 * 4 + 4);
                    BitConverter.GetBytes(dataOrigin.Item2[i].Z).CopyTo(data, head + i * 4 * 4 + 8);
                    BitConverter.GetBytes(dataOrigin.Item2[i].W).CopyTo(data, head + i * 4 * 4 + 12);
                }

                new Thread(() => SendData("kinect", data)).Start();
                return;
            }

            Status = false;
        }

        private void InfoStatusOK()
        {
            if (Status)
            {
                StatusTipPanel.GetComponent<Image>().color = new Color(0, 0.7f, 0, 0.8f);
                StatusTipPanel.GetComponentInChildren<TextMeshProUGUI>().text = "已检测到人体";
            }
            else
            {
                StatusTipPanel.GetComponent<Image>().color = new Color(0.7f, 0, 0, 0.8f);
                StatusTipPanel.GetComponentInChildren<TextMeshProUGUI>().text = "未检测到人体";
            }
        }

        public void SendData(string Class, byte[] data)
        {
            if (data != null && data.Length == 0) return;
            try
            {
                _tcpClient = new TcpClient("127.0.0.1", 14850);
                var stream = _tcpClient.GetStream();
                var jsonText = new KinectSkeleton
                {
                    Class = Class,
                    Data = data
                };
                var json = JsonConvert.SerializeObject(jsonText);
                // var json_test = JsonUtility.ToJson()
                var jsonBytes = System.Text.Encoding.UTF8.GetBytes(json);
                stream.Write(jsonBytes, 0, jsonBytes.Length);

                stream.Close();
            }
            catch
            {
                Console.WriteLine("尝试重连中...");
            }
        }

        private void ReceiveData()
        {
            _tcpClient = new TcpClient
            {
                ReceiveTimeout = 1000000
            };
            try
            {
                _tcpClient.Connect("127.0.0.1", 14849);
                var stream = _tcpClient.GetStream();
                var data = new byte[1024];
                var bytesRead = stream.Read(data, 0, data.Length);
                var json = System.Text.Encoding.UTF8.GetString(data, 0, bytesRead);

                #region Json Parser

                var jsonObject = JsonConvert.DeserializeObject<Response>(json);
                switch (jsonObject.Class)
                {
                    case "RULA":
                    {
                        var _jsonObject = JsonConvert.DeserializeObject<ScoreResponse>(json);
                        Score = _jsonObject.Data;
                        break;
                    }
                    case "Angle":
                    {
                        var _jsonObject = JsonConvert.DeserializeObject<AngleResponse>(json);
                        Angle = _jsonObject.Data;
                        break;
                    }
                    case "InitSetting":
                    {
                        var _jsonObject = JsonConvert.DeserializeObject<ManualSettingResponse>(json);
                        // Debug.Log(_jsonObject);
                        Setting = _jsonObject.Data;
                        break;
                    }
                }

                #endregion

                stream.Close();
            }
            catch (Exception)
            {
                Console.WriteLine("正在重连...");
            }
        }


        public Body getBody()
        {
            return _body;
        }

        public ManualSetting getSetting()
        {
            while (Setting == null)
            {
                SendData("InitSetting", null);
                Thread.Sleep(100);
            }

            return Setting;
        }

        private static Tuple<List<CameraSpacePoint>, List<Windows.Kinect.Vector4>> GetDataFromBody(Body body)
        {
            var jointTypes = Enum.GetValues(typeof(JointType)).Cast<JointType>().ToList();
            var jointPointList = jointTypes.Select(jointType => body.Joints[jointType].Position).ToList();
            var jointOrientationList =
                jointTypes.Select(jointType => body.JointOrientations[jointType].Orientation).ToList();
            return new Tuple<List<CameraSpacePoint>, List<Windows.Kinect.Vector4>>(jointPointList,
                jointOrientationList);
        }
    }
}