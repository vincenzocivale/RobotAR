using UnityEngine;

public class RobotManager : MonoBehaviour
{
    [SerializeField]
    private string[] linkTags;
    RobotJoint[] robotJoints;
    GameObject[] joints;

    float[] prevAnglesDeg;
    public float[] PreviousJointAnglesDeg { get => prevAnglesDeg; private set { prevAnglesDeg = value; } }

    void Awake()
    {
        robotJoints = new RobotJoint[linkTags.Length];
        joints = new GameObject[linkTags.Length];
        prevAnglesDeg = new float[linkTags.Length];

        for (int jointIdx = 0; jointIdx < linkTags.Length; jointIdx++)
        {
            joints[jointIdx] = GameObject.FindGameObjectWithTag(linkTags[jointIdx]);

            if (joints[jointIdx] == null)
            {
                Debug.LogError($"GameObject with tag '{linkTags[jointIdx]}' not found");
            }
            else
            {
                robotJoints[jointIdx] = joints[jointIdx].GetComponent<RobotJoint>();

                if (robotJoints[jointIdx] == null)
                {
                    Debug.LogError($"RobotJoint component not found on GameObject with tag '{linkTags[jointIdx]}'");
                }
            }
        }
    }

    public void SetJointAngles(float[] angles)
    {
        if (angles.Length != joints.Length)
        {
            Debug.LogError("Angles array length does not match joints array length");
            return;
        }

        for (int jointAngleIdx = 0; jointAngleIdx < angles.Length; jointAngleIdx++)
        {
            if (joints[jointAngleIdx] == null)
            {
                Debug.LogError($"Joint at index {jointAngleIdx} is null");
                continue;
            }

            if (robotJoints[jointAngleIdx] == null)
            {
                Debug.LogError($"RobotJoint at index {jointAngleIdx} is null");
                continue;
            }

            joints[jointAngleIdx].transform.localRotation = joints[jointAngleIdx].transform.localRotation * Quaternion.AngleAxis(angles[jointAngleIdx] - prevAnglesDeg[jointAngleIdx], robotJoints[jointAngleIdx].axis);
            prevAnglesDeg[jointAngleIdx] = angles[jointAngleIdx];
        }
    }
}
