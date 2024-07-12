using UnityEngine;
using UnityEngine.UI;
using Unity.Robotics.ROSTCPConnector;
using System.Diagnostics;

public class ROSConnectionManager : MonoBehaviour
{
    public InputField ipInputField;
    public Button connectButton;

    private ROSConnection rosConnection;

    void Start()
    {
        rosConnection = ROSConnection.GetOrCreateInstance();
        connectButton.onClick.AddListener(UpdateROSConnection);

        string savedIpAddress = PlayerPrefs.GetString("RosIPAddress", "127.0.0.1");
        ipInputField.text = savedIpAddress;
    }

    void UpdateROSConnection()
    {
        string ipAddress = ipInputField.text;
        if (IsValidIPAddress(ipAddress))
        {
            rosConnection.RosIPAddress = ipAddress;
            rosConnection.Connect();
            PlayerPrefs.SetString("RosIPAddress", ipAddress);
            UnityEngine.Debug.Log("Connected to ROS at: " + ipAddress);
        }
        else
        {
            UnityEngine.Debug.LogError("Invalid IP Address");
        }
    }

    bool IsValidIPAddress(string ipAddress)
    {
        // Aggiungi qui una validazione IP di base (puoi rendere la validazione più robusta se necessario)
        System.Net.IPAddress temp;
        return System.Net.IPAddress.TryParse(ipAddress, out temp);
    }
}
