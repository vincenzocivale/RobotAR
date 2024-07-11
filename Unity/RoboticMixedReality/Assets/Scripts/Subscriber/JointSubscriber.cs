using UnityEngine;
using Unity.Robotics.ROSTCPConnector;
using RosMessageTypes.Sensor;

public class JointSubscriber : MonoBehaviour
{
    public float[] jointStatesDeg;

    [SerializeField]
    private int numberOfJoints;

    [SerializeField]
    private string jointTopic;

    void Awake()
    {
        jointStatesDeg = new float[numberOfJoints];
    }

    // Start is called before the first frame update
    void Start()
    {
        ROSConnection.GetOrCreateInstance().Subscribe<JointStateMsg>(jointTopic, Updatejoints);
    }

    void Updatejoints(JointStateMsg jointState)
    {
        if (jointState.position.Length < numberOfJoints)
        {
            Debug.LogError($"Received joint state message with fewer positions than expected. Expected: {numberOfJoints}, Received: {jointState.position.Length}");
            return;
        }

        try
        {
            for (int i = 0; i < numberOfJoints; i++)
            {
                jointStatesDeg[i] = (float)(jointState.position[i] * Mathf.Rad2Deg);
            }
        }
        catch (System.IndexOutOfRangeException e)
        {
            Debug.LogError($"IndexOutOfRangeException: {e.Message}\nJointState message length: {jointState.position.Length}, numberOfJoints: {numberOfJoints}");
        }
    }
}

