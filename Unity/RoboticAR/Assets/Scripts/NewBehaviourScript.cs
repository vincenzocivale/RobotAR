using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Robotics.ROSTCPConnector;
using RosMessageTypes.Geometry;
using RosMessageTypes.Trajectory;

public class ContinuousTrajectoryExecutor : MonoBehaviour
{
    private ROSConnection ros;
    public string trajectoryTopicName;
    public GameObject robot;

    private RobotManager robotManager;
    private UpdateRobot robotUpdater;

    void Awake()
    {
        robotUpdater = robot.GetComponent<UpdateRobot>();
        robotManager = robot.GetComponent<RobotManager>();
    }

    void Start()
    {
        ros = ROSConnection.GetOrCreateInstance();
        ros.Subscribe<JointTrajectoryMsg>(trajectoryTopicName, ExecuteTrajectory);
    }

    void ExecuteTrajectory(JointTrajectoryMsg trajectory)
    {
        StartCoroutine(ShowTrajectory(trajectory.points));
    }

    IEnumerator ShowTrajectory(JointTrajectoryPointMsg[] points)
    {
        float[] prevJointAnglesDeg = robotManager.PreviousJointAnglesDeg;

        for (int pointIdx = 0; pointIdx < points.Length; pointIdx++)
        {
            JointTrajectoryPointMsg point = points[pointIdx];
            float[] jointAnglesDeg = new float[point.positions.Length];

            for (int i = 0; i < point.positions.Length; i++)
            {
                jointAnglesDeg[i] = (float)point.positions[i] * Mathf.Rad2Deg;
            }

            robotManager.SetJointAngles(jointAnglesDeg);
            yield return new WaitForSeconds(0.1f);  // Adjust the delay to control the speed of the trajectory execution
        }
    }
}
