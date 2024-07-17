using UnityEngine;
using Unity.Robotics.ROSTCPConnector;
using RosMessageTypes.Geometry;
using System.Collections;
using System.Diagnostics;

public class TargetSubscriber : MonoBehaviour
{
    private ROSConnection ros;

    [SerializeField]
    private string targetTopicName = "/target_position";

    [SerializeField]
    private GameObject targetGameObject;

    private bool messageReceived = false;
    private float timeout = 5.0f; // Timeout in seconds to wait for a message

    void Start()
    {
        // Get or create the ROSConnection instance
        ros = ROSConnection.GetOrCreateInstance();

        // Subscribe to the target topic with a callback function
        ros.Subscribe<PoseMsg>(targetTopicName, UpdateTargetPosition);

        // Start the timeout coroutine
        StartCoroutine(TopicTimeoutChecker());
    }

    // Callback function to update the target position and orientation
    void UpdateTargetPosition(PoseMsg pose)
    {
        // Extract position and orientation from the PoseMsg
        Vector3 position = new Vector3((float)pose.position.x, (float)pose.position.y, (float)pose.position.z);
        Quaternion orientation = new Quaternion((float)pose.orientation.x, (float)pose.orientation.y, (float)pose.orientation.z, (float)pose.orientation.w);

        // Update the target GameObject's transform
        targetGameObject.transform.position = position;
        targetGameObject.transform.rotation = orientation;

        // Set messageReceived to true as we have received a message
        messageReceived = true;
    }

    // Coroutine to check for timeout
    private IEnumerator TopicTimeoutChecker()
    {
        yield return new WaitForSeconds(timeout);

        if (!messageReceived)
        {
            // Use UnityEngine.Debug
            UnityEngine.Debug.Log($"No messages received on topic '{targetTopicName}' within {timeout} seconds. Please check if the topic exists and is being published.");
        }
    }
}

