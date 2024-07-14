using UnityEngine;
using Unity.Robotics.ROSTCPConnector;
using RosMessageTypes.Geometry;

public class targetSubscriber : MonoBehaviour
{
    [Tooltip("Name of the topic where the target configuration is published")]
    public string topicName = "/end_effector_pose";

    [Tooltip("Unity scene element representing the desired target")]
    public Transform targetTransform;
    private ROSConnection ros;
    private bool isInitialPoseReceived = false;
    private Vector3 previousPosition;
    private Vector3 previousEulerAngles;

    void Start()
    {
        ros = ROSConnection.GetOrCreateInstance();
        ros.Subscribe<PoseStampedMsg>(topicName, ReceiveMessage);
    }

    void ReceiveMessage(PoseStampedMsg message)
    {
        // Extract position and orientation from received messages
        Vector3 receivedPosition = new Vector3(
            (float)message.pose.position.x,
            (float)message.pose.position.y,
            (float)message.pose.position.z);

        // Assume the orientation is given as roll, pitch, yaw in radians
        float roll = (float)message.pose.orientation.x;
        float pitch = (float)message.pose.orientation.y;
        float yaw = (float)message.pose.orientation.z;

        Vector3 receivedEulerAngles = new Vector3(pitch * Mathf.Rad2Deg, yaw * Mathf.Rad2Deg, roll * Mathf.Rad2Deg);

        if (!isInitialPoseReceived)
        {
            previousPosition = receivedPosition;
            previousEulerAngles = receivedEulerAngles;
            isInitialPoseReceived = true;
        }
        else
        {
            // Calculate differences from initial position
            Vector3 positionDifference = receivedPosition - previousPosition;
            Vector3 rotationDifference = receivedEulerAngles - previousEulerAngles;

            // Convert rotation difference to a quaternion
            Quaternion rotationDifferenceQuat = Quaternion.Euler(rotationDifference);

            // Update GameObject position and orientation by applying proportional movements/rotations from the ROS target
            targetTransform.position += positionDifference;
            targetTransform.rotation *= rotationDifferenceQuat;
        }

        // Update previous positions with received values
        previousPosition = receivedPosition;
        previousEulerAngles = receivedEulerAngles;
    }
}
