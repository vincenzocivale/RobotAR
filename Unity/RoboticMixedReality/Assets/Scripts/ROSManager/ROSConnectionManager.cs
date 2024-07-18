using UnityEngine;
using UnityEngine.UI;
using Unity.Robotics.ROSTCPConnector;
using System.Diagnostics;

public class ROSConnectionManager : MonoBehaviour
{
    private string ipAddress = "127.0.0.1";
    private bool connected = false;

    private ROSConnection rosConnection;

    void Start()
    {
        rosConnection = ROSConnection.GetOrCreateInstance();
        ipAddress = PlayerPrefs.GetString("RosIPAddress", "127.0.0.1");
    }

    void OnGUI()
    {
        ipAddress = GUI.TextField(new Rect(10, 30, 200, 30), ipAddress, 25);

        string buttonText = connected ? "Disconnect" : "Connect";

        if (GUI.Button(new Rect(220, 30, 100, 30), buttonText))
        {
            if (connected)
            {
                Disconnect();
            }
            else
            {
                UpdateROSConnection();
            }

            UnityEngine.Debug.Log(buttonText);
        }
    }

    void UpdateROSConnection()
    {
        if (IsValidIPAddress(ipAddress))
        {
            rosConnection.RosIPAddress = ipAddress;
            rosConnection.Connect();
            PlayerPrefs.SetString("RosIPAddress", ipAddress);
            connected = true;
            UnityEngine.Debug.Log("Connected to ROS at: " + ipAddress);
        }
        else
        {
            UnityEngine.Debug.LogError("Invalid IP Address");
        }
    }

    void Disconnect()
    {
        rosConnection.Disconnect();
        connected = false;
        UnityEngine.Debug.Log("Disconnected from ROS");
    }

    bool IsValidIPAddress(string ipAddress)
    {
        System.Net.IPAddress temp;
        return System.Net.IPAddress.TryParse(ipAddress, out temp);
    }
}
