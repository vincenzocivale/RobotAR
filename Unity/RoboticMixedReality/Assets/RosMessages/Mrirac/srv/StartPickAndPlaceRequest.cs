//Do not edit! This file was generated by Unity-ROS MessageGeneration.
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Unity.Robotics.ROSTCPConnector.MessageGeneration;

namespace RosMessageTypes.Mrirac
{
    [Serializable]
    public class StartPickAndPlaceRequest : Message
    {
        public const string k_RosMessageName = "mrirac_msgs/StartPickAndPlace";
        public override string RosMessageName => k_RosMessageName;

        public Geometry.PoseMsg pre_grasp_pose;
        public Geometry.PoseMsg grasp_pose;
        public Geometry.PoseMsg pre_place_pose;
        public Geometry.PoseMsg place_pose;

        public StartPickAndPlaceRequest()
        {
            this.pre_grasp_pose = new Geometry.PoseMsg();
            this.grasp_pose = new Geometry.PoseMsg();
            this.pre_place_pose = new Geometry.PoseMsg();
            this.place_pose = new Geometry.PoseMsg();
        }

        public StartPickAndPlaceRequest(Geometry.PoseMsg pre_grasp_pose, Geometry.PoseMsg grasp_pose, Geometry.PoseMsg pre_place_pose, Geometry.PoseMsg place_pose)
        {
            this.pre_grasp_pose = pre_grasp_pose;
            this.grasp_pose = grasp_pose;
            this.pre_place_pose = pre_place_pose;
            this.place_pose = place_pose;
        }

        public static StartPickAndPlaceRequest Deserialize(MessageDeserializer deserializer) => new StartPickAndPlaceRequest(deserializer);

        private StartPickAndPlaceRequest(MessageDeserializer deserializer)
        {
            this.pre_grasp_pose = Geometry.PoseMsg.Deserialize(deserializer);
            this.grasp_pose = Geometry.PoseMsg.Deserialize(deserializer);
            this.pre_place_pose = Geometry.PoseMsg.Deserialize(deserializer);
            this.place_pose = Geometry.PoseMsg.Deserialize(deserializer);
        }

        public override void SerializeTo(MessageSerializer serializer)
        {
            serializer.Write(this.pre_grasp_pose);
            serializer.Write(this.grasp_pose);
            serializer.Write(this.pre_place_pose);
            serializer.Write(this.place_pose);
        }

        public override string ToString()
        {
            return "StartPickAndPlaceRequest: " +
            "\npre_grasp_pose: " + pre_grasp_pose.ToString() +
            "\ngrasp_pose: " + grasp_pose.ToString() +
            "\npre_place_pose: " + pre_place_pose.ToString() +
            "\nplace_pose: " + place_pose.ToString();
        }

#if UNITY_EDITOR
        [UnityEditor.InitializeOnLoadMethod]
#else
        [UnityEngine.RuntimeInitializeOnLoadMethod]
#endif
        public static void Register()
        {
            MessageRegistry.Register(k_RosMessageName, Deserialize);
        }
    }
}
