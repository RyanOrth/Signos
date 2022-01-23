using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leap;
using Leap.Unity;

using System.IO;

public class TestHandDataPrint : MonoBehaviour
{
	Controller controller;
	void Start()
	{

	}

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Space))
		{
			controller = new Controller();
			Frame frame = controller.Frame();
			List<Hand> hands = frame.Hands;

			// for (int i = 0; i < frame.Hands.Count; i++)
			// {
			//   string handName = hands[i].IsRight ? "Right Hand" : "Left Hand";
			//   PrintHandData(hands[i], handName);
			// }
			RecordData(hands[0]);

		}
	}
	public bool wroteYet = false;
	void RecordData(Hand hand)
	{
		Debug.Log("Record Data Function");
		if (!wroteYet)
		{
			//FileStream fileStreamInit = new FileStream(@"data.txt", FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.None);
			string headerText = "HandId,HandConfidence,HandPalmNormalPitch,HandPalmNormalRoll,"
			+ "HandPalmNormalYaw,HandPalmDirectionRoll,HandPalmPositionX,HandPalmPositionY,HandPalmPositionZ,HandPalmVelocityX,"
			+ "HandPalmVelocityY,HandPalmVelocityZ,HandPalmNormalX,HandPalmNormalY,"
			+ "HandPalmNormalZ,HandDirectionX,HandDirectionY,HandDirectionZ,HandRotationX,"
			+ "HandRotationY,HandRotationZ,HandRotationW,GrabStrength,GrabAngle,PinchDistance,PinchStrength,"
			+ "PalmWidth,StabilizedPalmPositionX,StabilizedPalmPositionY,StabilizedPalmPositionZ,TimeVisible,"
			+ "HandWristPositionX,HandWristPositionY,HandWristPositionZ,"
			+ "ThumbDirectionX,ThumbDirectionY,ThumbDirectionZ,ThumbWidth,ThumbLength,ThumbIsExtended,"
			+ "ThumbMetacarpalLowerJointX,ThumbMetacarpalLowerJointY,ThumbMetacarpalLowerJointZ,"
			+ "ThumbMetacarpalUpperJointX,ThumbMetacarpalUpperJointY,ThumbMetacarpalUpperJointZ,"
			+ "ThumbMetacarpalCenterX,ThumbMetacarpalCenterY,ThumbMetacarpalCenterZ,"
			+ "ThumbMetacarpalDirectionX,ThumbMetacarpalDirectionY,ThumbMetacarpalDirectionZ,"
			+ "ThumbMetacarpalLength,ThumbMetacarpalWidth,ThumbMetacarpalRotationX,ThumbMetacarpalRotationY,"
			+ "ThumbMetacarpalRotationZ,ThumbMetacarpalRotationW,"
			+ "ThumbProximalLowerJointX,ThumbProximalLowerJointY,ThumbProximalLowerJointZ,"
			+ "ThumbProximalUpperJointX,ThumbProximalUpperJointY,ThumbProximalUpperJointZ,"
			+ "ThumbProximalCenterX,ThumbProximalCenterY,ThumbProximalCenterZ,"
			+ "ThumbProximalDirectionX,ThumbProximalDirectionY,ThumbProximalDirectionZ,"
			+ "ThumbProximalLength,ThumbProximalWidth,ThumbProximalRotationX,ThumbProximalRotationY,"
			+ "ThumbProximalRotationZ,ThumbProximalRotationW,"
			+ "ThumbIntermediateLowerJointX,ThumbIntermediateLowerJointY,ThumbIntermediateLowerJointZ,"
			+ "ThumbIntermediateUpperJointX,ThumbIntermediateUpperJointY,ThumbIntermediateUpperJointZ,"
			+ "ThumbIntermediateCenterX,ThumbIntermediateCenterY,ThumbIntermediateCenterZ,"
			+ "ThumbIntermediateDirectionX,ThumbIntermediateDirectionY,ThumbIntermediateDirectionZ,"
			+ "ThumbIntermediateLength,ThumbIntermediateWidth,ThumbIntermediateRotationX,ThumbIntermediateRotationY,"
			+ "ThumbIntermediateRotationZ,ThumbIntermediateRotationW,"
			+ "ThumbDistalLowerJointX,ThumbDistalLowerJointY,ThumbDistalLowerJointZ,"
			+ "ThumbDistalUpperJointX,ThumbDistalUpperJointY,ThumbDistalUpperJointZ,"
			+ "ThumbDistalCenterX,ThumbDistalCenterY,ThumbDistalCenterZ,"
			+ "ThumbDistalDirectionX,ThumbDistalDirectionY,ThumbDistalDirectionZ,"
			+ "ThumbDistalLength,ThumbDistalWidth,ThumbDistalRotationX,ThumbDistalRotationY,"
			+ "ThumbDistalRotationZ,ThumbDistalRotationW,"
			+ "IndexDirectionX,IndexDirectionY,IndexDirectionZ,IndexWidth,IndexLength,IndexIsExtended,"
			+ "IndexMetacarpalLowerJointX,IndexMetacarpalLowerJointY,IndexMetacarpalLowerJointZ,"
			+ "IndexMetacarpalUpperJointX,IndexMetacarpalUpperJointY,IndexMetacarpalUpperJointZ,"
			+ "IndexMetacarpalCenterX,IndexMetacarpalCenterY,IndexMetacarpalCenterZ,"
			+ "IndexMetacarpalDirectionX,IndexMetacarpalDirectionY,IndexMetacarpalDirectionZ,"
			+ "IndexMetacarpalLength,IndexMetacarpalWidth,IndexMetacarpalRotationX,IndexMetacarpalRotationY,"
			+ "IndexMetacarpalRotationZ,IndexMetacarpalRotationW,"
			+ "IndexProximalLowerJointX,IndexProximalLowerJointY,IndexProximalLowerJointZ,"
			+ "IndexProximalUpperJointX,IndexProximalUpperJointY,IndexProximalUpperJointZ,"
			+ "IndexProximalCenterX,IndexProximalCenterY,IndexProximalCenterZ,"
			+ "IndexProximalDirectionX,IndexProximalDirectionY,IndexProximalDirectionZ,"
			+ "IndexProximalLength,IndexProximalWidth,IndexProximalRotationX,IndexProximalRotationY,"
			+ "IndexProximalRotationZ,IndexProximalRotationW,"
			+ "IndexIntermediateLowerJointX,IndexIntermediateLowerJointY,IndexIntermediateLowerJointZ,"
			+ "IndexIntermediateUpperJointX,IndexIntermediateUpperJointY,IndexIntermediateUpperJointZ,"
			+ "IndexIntermediateCenterX,IndexIntermediateCenterY,IndexIntermediateCenterZ,"
			+ "IndexIntermediateDirectionX,IndexIntermediateDirectionY,IndexIntermediateDirectionZ,"
			+ "IndexIntermediateLength,IndexIntermediateWidth,IndexIntermediateRotationX,IndexIntermediateRotationY,"
			+ "IndexIntermediateRotationZ,IndexIntermediateRotationW,"
			+ "IndexDistalLowerJointX,IndexDistalLowerJointY,IndexDistalLowerJointZ,"
			+ "IndexDistalUpperJointX,IndexDistalUpperJointY,IndexDistalUpperJointZ,"
			+ "IndexDistalCenterX,IndexDistalCenterY,IndexDistalCenterZ,"
			+ "IndexDistalDirectionX,IndexDistalDirectionY,IndexDistalDirectionZ,"
			+ "IndexDistalLength,IndexDistalWidth,IndexDistalRotationX,IndexDistalRotationY,"
			+ "IndexDistalRotationZ,IndexDistalRotationW,"
			+ "MiddleDirectionX,MiddleDirectionY,MiddleDirectionZ,MiddleWidth,MiddleLength,MiddleIsExtended,"
			+ "MiddleMetacarpalLowerJointX,MiddleMetacarpalLowerJointY,MiddleMetacarpalLowerJointZ,"
			+ "MiddleMetacarpalUpperJointX,MiddleMetacarpalUpperJointY,MiddleMetacarpalUpperJointZ,"
			+ "MiddleMetacarpalCenterX,MiddleMetacarpalCenterY,MiddleMetacarpalCenterZ,"
			+ "MiddleMetacarpalDirectionX,MiddleMetacarpalDirectionY,MiddleMetacarpalDirectionZ,"
			+ "MiddleMetacarpalLength,MiddleMetacarpalWidth,MiddleMetacarpalRotationX,MiddleMetacarpalRotationY,"
			+ "MiddleMetacarpalRotationZ,MiddleMetacarpalRotationW,"
			+ "MiddleProximalLowerJointX,MiddleProximalLowerJointY,MiddleProximalLowerJointZ,"
			+ "MiddleProximalUpperJointX,MiddleProximalUpperJointY,MiddleProximalUpperJointZ,"
			+ "MiddleProximalCenterX,MiddleProximalCenterY,MiddleProximalCenterZ,"
			+ "MiddleProximalDirectionX,MiddleProximalDirectionY,MiddleProximalDirectionZ,"
			+ "MiddleProximalLength,MiddleProximalWidth,MiddleProximalRotationX,MiddleProximalRotationY,"
			+ "MiddleProximalRotationZ,MiddleProximalRotationW,"
			+ "MiddleIntermediateLowerJointX,MiddleIntermediateLowerJointY,MiddleIntermediateLowerJointZ,"
			+ "MiddleIntermediateUpperJointX,MiddleIntermediateUpperJointY,MiddleIntermediateUpperJointZ,"
			+ "MiddleIntermediateCenterX,MiddleIntermediateCenterY,MiddleIntermediateCenterZ,"
			+ "MiddleIntermediateDirectionX,MiddleIntermediateDirectionY,MiddleIntermediateDirectionZ,"
			+ "MiddleIntermediateLength,MiddleIntermediateWidth,MiddleIntermediateRotationX,MiddleIntermediateRotationY,"
			+ "MiddleIntermediateRotationZ,MiddleIntermediateRotationW,"
			+ "MiddleDistalLowerJointX,MiddleDistalLowerJointY,MiddleDistalLowerJointZ,"
			+ "MiddleDistalUpperJointX,MiddleDistalUpperJointY,MiddleDistalUpperJointZ,"
			+ "MiddleDistalCenterX,MiddleDistalCenterY,MiddleDistalCenterZ,"
			+ "MiddleDistalDirectionX,MiddleDistalDirectionY,MiddleDistalDirectionZ,"
			+ "MiddleDistalLength,MiddleDistalWidth,MiddleDistalRotationX,MiddleDistalRotationY,"
			+ "MiddleDistalRotationZ,MiddleDistalRotationW,"
			+ "RingDirectionX,RingDirectionY,RingDirectionZ,RingWidth,RingLength,RingIsExtended,"
			+ "RingMetacarpalLowerJointX,RingMetacarpalLowerJointY,RingMetacarpalLowerJointZ,"
			+ "RingMetacarpalUpperJointX,RingMetacarpalUpperJointY,RingMetacarpalUpperJointZ,"
			+ "RingMetacarpalCenterX,RingMetacarpalCenterY,RingMetacarpalCenterZ,"
			+ "RingMetacarpalDirectionX,RingMetacarpalDirectionY,RingMetacarpalDirectionZ,"
			+ "RingMetacarpalLength,RingMetacarpalWidth,RingMetacarpalRotationX,RingMetacarpalRotationY,"
			+ "RingMetacarpalRotationZ,RingMetacarpalRotationW,"
			+ "RingProximalLowerJointX,RingProximalLowerJointY,RingProximalLowerJointZ,"
			+ "RingProximalUpperJointX,RingProximalUpperJointY,RingProximalUpperJointZ,"
			+ "RingProximalCenterX,RingProximalCenterY,RingProximalCenterZ,"
			+ "RingProximalDirectionX,RingProximalDirectionY,RingProximalDirectionZ,"
			+ "RingProximalLength,RingProximalWidth,RingProximalRotationX,RingProximalRotationY,"
			+ "RingProximalRotationZ,RingProximalRotationW,"
			+ "RingIntermediateLowerJointX,RingIntermediateLowerJointY,RingIntermediateLowerJointZ,"
			+ "RingIntermediateUpperJointX,RingIntermediateUpperJointY,RingIntermediateUpperJointZ,"
			+ "RingIntermediateCenterX,RingIntermediateCenterY,RingIntermediateCenterZ,"
			+ "RingIntermediateDirectionX,RingIntermediateDirectionY,RingIntermediateDirectionZ,"
			+ "RingIntermediateLength,RingIntermediateWidth,RingIntermediateRotationX,RingIntermediateRotationY,"
			+ "RingIntermediateRotationZ,RingIntermediateRotationW,"
			+ "RingDistalLowerJointX,RingDistalLowerJointY,RingDistalLowerJointZ,"
			+ "RingDistalUpperJointX,RingDistalUpperJointY,RingDistalUpperJointZ,"
			+ "RingDistalCenterX,RingDistalCenterY,RingDistalCenterZ,"
			+ "RingDistalDirectionX,RingDistalDirectionY,RingDistalDirectionZ,"
			+ "RingDistalLength,RingDistalWidth,RingDistalRotationX,RingDistalRotationY,"
			+ "RingDistalRotationZ,RingDistalRotationW,"
			+ "PinkyDirectionX,PinkyDirectionY,PinkyDirectionZ,PinkyWidth,PinkyLength,PinkyIsExtended,"
			+ "PinkyMetacarpalLowerJointX,PinkyMetacarpalLowerJointY,PinkyMetacarpalLowerJointZ,"
			+ "PinkyMetacarpalUpperJointX,PinkyMetacarpalUpperJointY,PinkyMetacarpalUpperJointZ,"
			+ "PinkyMetacarpalCenterX,PinkyMetacarpalCenterY,PinkyMetacarpalCenterZ,"
			+ "PinkyMetacarpalDirectionX,PinkyMetacarpalDirectionY,PinkyMetacarpalDirectionZ,"
			+ "PinkyMetacarpalLength,PinkyMetacarpalWidth,PinkyMetacarpalRotationX,PinkyMetacarpalRotationY,"
			+ "PinkyMetacarpalRotationZ,PinkyMetacarpalRotationW,"
			+ "PinkyProximalLowerJointX,PinkyProximalLowerJointY,PinkyProximalLowerJointZ,"
			+ "PinkyProximalUpperJointX,PinkyProximalUpperJointY,PinkyProximalUpperJointZ,"
			+ "PinkyProximalCenterX,PinkyProximalCenterY,PinkyProximalCenterZ,"
			+ "PinkyProximalDirectionX,PinkyProximalDirectionY,PinkyProximalDirectionZ,"
			+ "PinkyProximalLength,PinkyProximalWidth,PinkyProximalRotationX,PinkyProximalRotationY,"
			+ "PinkyProximalRotationZ,PinkyProximalRotationW,"
			+ "PinkyIntermediateLowerJointX,PinkyIntermediateLowerJointY,PinkyIntermediateLowerJointZ,"
			+ "PinkyIntermediateUpperJointX,PinkyIntermediateUpperJointY,PinkyIntermediateUpperJointZ,"
			+ "PinkyIntermediateCenterX,PinkyIntermediateCenterY,PinkyIntermediateCenterZ,"
			+ "PinkyIntermediateDirectionX,PinkyIntermediateDirectionY,PinkyIntermediateDirectionZ,"
			+ "PinkyIntermediateLength,PinkyIntermediateWidth,PinkyIntermediateRotationX,PinkyIntermediateRotationY,"
			+ "PinkyIntermediateRotationZ,PinkyIntermediateRotationW,"
			+ "PinkyDistalLowerJointX,PinkyDistalLowerJointY,PinkyDistalLowerJointZ,"
			+ "PinkyDistalUpperJointX,PinkyDistalUpperJointY,PinkyDistalUpperJointZ,"
			+ "PinkyDistalCenterX,PinkyDistalCenterY,PinkyDistalCenterZ,"
			+ "PinkyDistalDirectionX,PinkyDistalDirectionY,PinkyDistalDirectionZ,"
			+ "PinkyDistalLength,PinkyDistalWidth,PinkyDistalRotationX,PinkyDistalRotationY,"
			+ "PinkyDistalRotationZ,PinkyDistalRotationW\n";

			File.AppendAllText("data.txt", headerText);
			wroteYet = true;
		}

		Vector palmPosition = hand.PalmPosition;
		Quaternion rotation = new Quaternion(hand.Rotation.x, hand.Rotation.y, hand.Rotation.z, hand.Rotation.w);

		string handData = "" + hand.Id + "," + hand.Confidence + "," + hand.PalmNormal.Pitch
		+ "," + hand.PalmNormal.Roll + "," + hand.PalmNormal.Yaw + "," + hand.Direction.Roll
		+ "," + (hand.PalmPosition - palmPosition) + "," + hand.PalmVelocity + "," + hand.PalmNormal + ","
		+ hand.Direction + ","
		+ ((new Quaternion(hand.Rotation.x, hand.Rotation.y, hand.Rotation.z, hand.Rotation.w)) * Quaternion.Inverse(rotation))
		+ "," + hand.GrabStrength + "," + hand.GrabAngle
		+ "," + hand.PinchDistance + "," + hand.PinchStrength + "," + hand.PalmWidth + ","
		+ hand.StabilizedPalmPosition + "," + hand.TimeVisible + "," + (hand.WristPosition - palmPosition) + ",";

		List<Finger> fingers = hand.Fingers;
		Finger thumb = fingers[0];
		Finger index = fingers[1];
		Finger middle = fingers[2];
		Finger ring = fingers[3];
		Finger pinky = fingers[4];

		handData += thumb.Direction + "," + thumb.Width + "," + thumb.Length + "," + thumb.IsExtended + ",";
		handData += (thumb.Bone(Bone.BoneType.TYPE_METACARPAL).PrevJoint - palmPosition) + "," + (thumb.Bone(Bone.BoneType.TYPE_METACARPAL).NextJoint - palmPosition) + ",";
		handData += (thumb.Bone(Bone.BoneType.TYPE_METACARPAL).Center - palmPosition) + "," + thumb.Bone(Bone.BoneType.TYPE_METACARPAL).Direction + ",";
		handData += thumb.Bone(Bone.BoneType.TYPE_METACARPAL).Length + "," + thumb.Bone(Bone.BoneType.TYPE_METACARPAL).Width + ",";
		handData += ((new Quaternion(thumb.Bone(Bone.BoneType.TYPE_METACARPAL).Rotation.x, thumb.Bone(Bone.BoneType.TYPE_METACARPAL).Rotation.y, thumb.Bone(Bone.BoneType.TYPE_METACARPAL).Rotation.z, thumb.Bone(Bone.BoneType.TYPE_METACARPAL).Rotation.w)) * Quaternion.Inverse(rotation)) + ",";
		handData += (thumb.Bone(Bone.BoneType.TYPE_PROXIMAL).PrevJoint - palmPosition) + "," + (thumb.Bone(Bone.BoneType.TYPE_PROXIMAL).NextJoint - palmPosition) + ",";
		handData += (thumb.Bone(Bone.BoneType.TYPE_PROXIMAL).Center - palmPosition) + "," + thumb.Bone(Bone.BoneType.TYPE_PROXIMAL).Direction + ",";
		handData += thumb.Bone(Bone.BoneType.TYPE_PROXIMAL).Length + "," + thumb.Bone(Bone.BoneType.TYPE_PROXIMAL).Width + ",";
		handData += ((new Quaternion(thumb.Bone(Bone.BoneType.TYPE_PROXIMAL).Rotation.x, thumb.Bone(Bone.BoneType.TYPE_PROXIMAL).Rotation.y, thumb.Bone(Bone.BoneType.TYPE_PROXIMAL).Rotation.z, thumb.Bone(Bone.BoneType.TYPE_PROXIMAL).Rotation.w)) * Quaternion.Inverse(rotation)) + ",";
		handData += (thumb.Bone(Bone.BoneType.TYPE_INTERMEDIATE).PrevJoint - palmPosition) + "," + (thumb.Bone(Bone.BoneType.TYPE_INTERMEDIATE).NextJoint - palmPosition) + ",";
		handData += (thumb.Bone(Bone.BoneType.TYPE_INTERMEDIATE).Center - palmPosition) + "," + thumb.Bone(Bone.BoneType.TYPE_INTERMEDIATE).Direction + ",";
		handData += thumb.Bone(Bone.BoneType.TYPE_INTERMEDIATE).Length + "," + thumb.Bone(Bone.BoneType.TYPE_INTERMEDIATE).Width + ",";
		handData += ((new Quaternion(thumb.Bone(Bone.BoneType.TYPE_INTERMEDIATE).Rotation.x, thumb.Bone(Bone.BoneType.TYPE_INTERMEDIATE).Rotation.y, thumb.Bone(Bone.BoneType.TYPE_INTERMEDIATE).Rotation.z, thumb.Bone(Bone.BoneType.TYPE_INTERMEDIATE).Rotation.w)) * Quaternion.Inverse(rotation)) + ",";
		handData += (thumb.Bone(Bone.BoneType.TYPE_DISTAL).PrevJoint - palmPosition) + "," + (thumb.Bone(Bone.BoneType.TYPE_DISTAL).NextJoint - palmPosition) + ",";
		handData += (thumb.Bone(Bone.BoneType.TYPE_DISTAL).Center - palmPosition) + "," + thumb.Bone(Bone.BoneType.TYPE_DISTAL).Direction + ",";
		handData += thumb.Bone(Bone.BoneType.TYPE_DISTAL).Length + "," + thumb.Bone(Bone.BoneType.TYPE_DISTAL).Width + ",";
		handData += ((new Quaternion(thumb.Bone(Bone.BoneType.TYPE_DISTAL).Rotation.x, thumb.Bone(Bone.BoneType.TYPE_DISTAL).Rotation.y, thumb.Bone(Bone.BoneType.TYPE_DISTAL).Rotation.z, thumb.Bone(Bone.BoneType.TYPE_DISTAL).Rotation.w)) * Quaternion.Inverse(rotation)) + ",";

		handData += index.Direction + "," + index.Width + "," + index.Length + "," + index.IsExtended + ",";
		handData += (index.Bone(Bone.BoneType.TYPE_METACARPAL).PrevJoint - palmPosition) + "," + (index.Bone(Bone.BoneType.TYPE_METACARPAL).NextJoint - palmPosition) + ",";
		handData += (index.Bone(Bone.BoneType.TYPE_METACARPAL).Center - palmPosition) + "," + index.Bone(Bone.BoneType.TYPE_METACARPAL).Direction + ",";
		handData += index.Bone(Bone.BoneType.TYPE_METACARPAL).Length + "," + index.Bone(Bone.BoneType.TYPE_METACARPAL).Width + ",";
		handData += ((new Quaternion(index.Bone(Bone.BoneType.TYPE_METACARPAL).Rotation.x, index.Bone(Bone.BoneType.TYPE_METACARPAL).Rotation.y, index.Bone(Bone.BoneType.TYPE_METACARPAL).Rotation.z, index.Bone(Bone.BoneType.TYPE_METACARPAL).Rotation.w)) * Quaternion.Inverse(rotation)) + ",";
		handData += (index.Bone(Bone.BoneType.TYPE_PROXIMAL).PrevJoint - palmPosition) + "," + (index.Bone(Bone.BoneType.TYPE_PROXIMAL).NextJoint - palmPosition) + ",";
		handData += (index.Bone(Bone.BoneType.TYPE_PROXIMAL).Center - palmPosition) + "," + index.Bone(Bone.BoneType.TYPE_PROXIMAL).Direction + ",";
		handData += index.Bone(Bone.BoneType.TYPE_PROXIMAL).Length + "," + index.Bone(Bone.BoneType.TYPE_PROXIMAL).Width + ",";
		handData += ((new Quaternion(index.Bone(Bone.BoneType.TYPE_PROXIMAL).Rotation.x, index.Bone(Bone.BoneType.TYPE_PROXIMAL).Rotation.y, index.Bone(Bone.BoneType.TYPE_PROXIMAL).Rotation.z, index.Bone(Bone.BoneType.TYPE_PROXIMAL).Rotation.w)) * Quaternion.Inverse(rotation)) + ",";
		handData += (index.Bone(Bone.BoneType.TYPE_INTERMEDIATE).PrevJoint - palmPosition) + "," + (index.Bone(Bone.BoneType.TYPE_INTERMEDIATE).NextJoint - palmPosition) + ",";
		handData += (index.Bone(Bone.BoneType.TYPE_INTERMEDIATE).Center - palmPosition) + "," + index.Bone(Bone.BoneType.TYPE_INTERMEDIATE).Direction + ",";
		handData += index.Bone(Bone.BoneType.TYPE_INTERMEDIATE).Length + "," + index.Bone(Bone.BoneType.TYPE_INTERMEDIATE).Width + ",";
		handData += ((new Quaternion(index.Bone(Bone.BoneType.TYPE_INTERMEDIATE).Rotation.x, index.Bone(Bone.BoneType.TYPE_INTERMEDIATE).Rotation.y, index.Bone(Bone.BoneType.TYPE_INTERMEDIATE).Rotation.z, index.Bone(Bone.BoneType.TYPE_INTERMEDIATE).Rotation.w)) * Quaternion.Inverse(rotation)) + ",";
		handData += (index.Bone(Bone.BoneType.TYPE_DISTAL).PrevJoint - palmPosition) + "," + (index.Bone(Bone.BoneType.TYPE_DISTAL).NextJoint - palmPosition) + ",";
		handData += (index.Bone(Bone.BoneType.TYPE_DISTAL).Center - palmPosition) + "," + index.Bone(Bone.BoneType.TYPE_DISTAL).Direction + ",";
		handData += index.Bone(Bone.BoneType.TYPE_DISTAL).Length + "," + index.Bone(Bone.BoneType.TYPE_DISTAL).Width + ",";
		handData += ((new Quaternion(index.Bone(Bone.BoneType.TYPE_DISTAL).Rotation.x, index.Bone(Bone.BoneType.TYPE_DISTAL).Rotation.y, index.Bone(Bone.BoneType.TYPE_DISTAL).Rotation.z, index.Bone(Bone.BoneType.TYPE_DISTAL).Rotation.w)) * Quaternion.Inverse(rotation)) + ",";

		handData += middle.Direction + "," + middle.Width + "," + middle.Length + "," + middle.IsExtended + ",";
		handData += (middle.Bone(Bone.BoneType.TYPE_METACARPAL).PrevJoint - palmPosition) + "," + (middle.Bone(Bone.BoneType.TYPE_METACARPAL).NextJoint - palmPosition) + ",";
		handData += (middle.Bone(Bone.BoneType.TYPE_METACARPAL).Center - palmPosition) + "," + middle.Bone(Bone.BoneType.TYPE_METACARPAL).Direction + ",";
		handData += middle.Bone(Bone.BoneType.TYPE_METACARPAL).Length + "," + middle.Bone(Bone.BoneType.TYPE_METACARPAL).Width + ",";
		handData += ((new Quaternion(middle.Bone(Bone.BoneType.TYPE_METACARPAL).Rotation.x, middle.Bone(Bone.BoneType.TYPE_METACARPAL).Rotation.y, middle.Bone(Bone.BoneType.TYPE_METACARPAL).Rotation.z, middle.Bone(Bone.BoneType.TYPE_METACARPAL).Rotation.w)) * Quaternion.Inverse(rotation)) + ",";
		handData += (middle.Bone(Bone.BoneType.TYPE_PROXIMAL).PrevJoint - palmPosition) + "," + (middle.Bone(Bone.BoneType.TYPE_PROXIMAL).NextJoint - palmPosition) + ",";
		handData += (middle.Bone(Bone.BoneType.TYPE_PROXIMAL).Center - palmPosition) + "," + middle.Bone(Bone.BoneType.TYPE_PROXIMAL).Direction + ",";
		handData += middle.Bone(Bone.BoneType.TYPE_PROXIMAL).Length + "," + middle.Bone(Bone.BoneType.TYPE_PROXIMAL).Width + ",";
		handData += ((new Quaternion(middle.Bone(Bone.BoneType.TYPE_PROXIMAL).Rotation.x, middle.Bone(Bone.BoneType.TYPE_PROXIMAL).Rotation.y, middle.Bone(Bone.BoneType.TYPE_PROXIMAL).Rotation.z, middle.Bone(Bone.BoneType.TYPE_PROXIMAL).Rotation.w)) * Quaternion.Inverse(rotation)) + ",";
		handData += (middle.Bone(Bone.BoneType.TYPE_INTERMEDIATE).PrevJoint - palmPosition) + "," + (middle.Bone(Bone.BoneType.TYPE_INTERMEDIATE).NextJoint - palmPosition) + ",";
		handData += (middle.Bone(Bone.BoneType.TYPE_INTERMEDIATE).Center - palmPosition) + "," + middle.Bone(Bone.BoneType.TYPE_INTERMEDIATE).Direction + ",";
		handData += middle.Bone(Bone.BoneType.TYPE_INTERMEDIATE).Length + "," + middle.Bone(Bone.BoneType.TYPE_INTERMEDIATE).Width + ",";
		handData += ((new Quaternion(middle.Bone(Bone.BoneType.TYPE_INTERMEDIATE).Rotation.x, middle.Bone(Bone.BoneType.TYPE_INTERMEDIATE).Rotation.y, middle.Bone(Bone.BoneType.TYPE_INTERMEDIATE).Rotation.z, middle.Bone(Bone.BoneType.TYPE_INTERMEDIATE).Rotation.w)) * Quaternion.Inverse(rotation)) + ",";
		handData += (middle.Bone(Bone.BoneType.TYPE_DISTAL).PrevJoint - palmPosition) + "," + (middle.Bone(Bone.BoneType.TYPE_DISTAL).NextJoint - palmPosition) + ",";
		handData += (middle.Bone(Bone.BoneType.TYPE_DISTAL).Center - palmPosition) + "," + middle.Bone(Bone.BoneType.TYPE_DISTAL).Direction + ",";
		handData += middle.Bone(Bone.BoneType.TYPE_DISTAL).Length + "," + middle.Bone(Bone.BoneType.TYPE_DISTAL).Width + ",";
		handData += ((new Quaternion(middle.Bone(Bone.BoneType.TYPE_DISTAL).Rotation.x, middle.Bone(Bone.BoneType.TYPE_DISTAL).Rotation.y, middle.Bone(Bone.BoneType.TYPE_DISTAL).Rotation.z, middle.Bone(Bone.BoneType.TYPE_DISTAL).Rotation.w)) * Quaternion.Inverse(rotation)) + ",";

		handData += ring.Direction + "," + ring.Width + "," + ring.Length + "," + ring.IsExtended + ",";
		handData += (ring.Bone(Bone.BoneType.TYPE_METACARPAL).PrevJoint - palmPosition) + "," + (ring.Bone(Bone.BoneType.TYPE_METACARPAL).NextJoint - palmPosition) + ",";
		handData += (ring.Bone(Bone.BoneType.TYPE_METACARPAL).Center - palmPosition) + "," + ring.Bone(Bone.BoneType.TYPE_METACARPAL).Direction + ",";
		handData += ring.Bone(Bone.BoneType.TYPE_METACARPAL).Length + "," + ring.Bone(Bone.BoneType.TYPE_METACARPAL).Width + ",";
		handData += ((new Quaternion(ring.Bone(Bone.BoneType.TYPE_METACARPAL).Rotation.x, ring.Bone(Bone.BoneType.TYPE_METACARPAL).Rotation.y, ring.Bone(Bone.BoneType.TYPE_METACARPAL).Rotation.z, ring.Bone(Bone.BoneType.TYPE_METACARPAL).Rotation.w)) * Quaternion.Inverse(rotation)) + ",";
		handData += (ring.Bone(Bone.BoneType.TYPE_PROXIMAL).PrevJoint - palmPosition) + "," + (ring.Bone(Bone.BoneType.TYPE_PROXIMAL).NextJoint - palmPosition) + ",";
		handData += (ring.Bone(Bone.BoneType.TYPE_PROXIMAL).Center - palmPosition) + "," + ring.Bone(Bone.BoneType.TYPE_PROXIMAL).Direction + ",";
		handData += ring.Bone(Bone.BoneType.TYPE_PROXIMAL).Length + "," + ring.Bone(Bone.BoneType.TYPE_PROXIMAL).Width + ",";
		handData += ((new Quaternion(ring.Bone(Bone.BoneType.TYPE_PROXIMAL).Rotation.x, ring.Bone(Bone.BoneType.TYPE_PROXIMAL).Rotation.y, ring.Bone(Bone.BoneType.TYPE_PROXIMAL).Rotation.z, ring.Bone(Bone.BoneType.TYPE_PROXIMAL).Rotation.w)) * Quaternion.Inverse(rotation)) + ",";
		handData += (ring.Bone(Bone.BoneType.TYPE_INTERMEDIATE).PrevJoint - palmPosition) + "," + (ring.Bone(Bone.BoneType.TYPE_INTERMEDIATE).NextJoint - palmPosition) + ",";
		handData += (ring.Bone(Bone.BoneType.TYPE_INTERMEDIATE).Center - palmPosition) + "," + ring.Bone(Bone.BoneType.TYPE_INTERMEDIATE).Direction + ",";
		handData += ring.Bone(Bone.BoneType.TYPE_INTERMEDIATE).Length + "," + ring.Bone(Bone.BoneType.TYPE_INTERMEDIATE).Width + ",";
		handData += ((new Quaternion(ring.Bone(Bone.BoneType.TYPE_INTERMEDIATE).Rotation.x, ring.Bone(Bone.BoneType.TYPE_INTERMEDIATE).Rotation.y, ring.Bone(Bone.BoneType.TYPE_INTERMEDIATE).Rotation.z, ring.Bone(Bone.BoneType.TYPE_INTERMEDIATE).Rotation.w)) * Quaternion.Inverse(rotation)) + ",";
		handData += (ring.Bone(Bone.BoneType.TYPE_DISTAL).PrevJoint - palmPosition) + "," + (ring.Bone(Bone.BoneType.TYPE_DISTAL).NextJoint - palmPosition) + ",";
		handData += (ring.Bone(Bone.BoneType.TYPE_DISTAL).Center - palmPosition) + "," + ring.Bone(Bone.BoneType.TYPE_DISTAL).Direction + ",";
		handData += ring.Bone(Bone.BoneType.TYPE_DISTAL).Length + "," + ring.Bone(Bone.BoneType.TYPE_DISTAL).Width + ",";
		handData += ((new Quaternion(ring.Bone(Bone.BoneType.TYPE_DISTAL).Rotation.x, ring.Bone(Bone.BoneType.TYPE_DISTAL).Rotation.y, ring.Bone(Bone.BoneType.TYPE_DISTAL).Rotation.z, ring.Bone(Bone.BoneType.TYPE_DISTAL).Rotation.w)) * Quaternion.Inverse(rotation)) + ",";

		handData += pinky.Direction + "," + pinky.Width + "," + pinky.Length + "," + pinky.IsExtended + ",";
		handData += (pinky.Bone(Bone.BoneType.TYPE_METACARPAL).PrevJoint - palmPosition) + "," + (pinky.Bone(Bone.BoneType.TYPE_METACARPAL).NextJoint - palmPosition) + ",";
		handData += (pinky.Bone(Bone.BoneType.TYPE_METACARPAL).Center - palmPosition) + "," + pinky.Bone(Bone.BoneType.TYPE_METACARPAL).Direction + ",";
		handData += pinky.Bone(Bone.BoneType.TYPE_METACARPAL).Length + "," + pinky.Bone(Bone.BoneType.TYPE_METACARPAL).Width + ",";
		handData += ((new Quaternion(pinky.Bone(Bone.BoneType.TYPE_METACARPAL).Rotation.x, pinky.Bone(Bone.BoneType.TYPE_METACARPAL).Rotation.y, pinky.Bone(Bone.BoneType.TYPE_METACARPAL).Rotation.z, pinky.Bone(Bone.BoneType.TYPE_METACARPAL).Rotation.w)) * Quaternion.Inverse(rotation)) + ",";
		handData += (pinky.Bone(Bone.BoneType.TYPE_PROXIMAL).PrevJoint - palmPosition) + "," + (pinky.Bone(Bone.BoneType.TYPE_PROXIMAL).NextJoint - palmPosition) + ",";
		handData += (pinky.Bone(Bone.BoneType.TYPE_PROXIMAL).Center - palmPosition) + "," + pinky.Bone(Bone.BoneType.TYPE_PROXIMAL).Direction + ",";
		handData += pinky.Bone(Bone.BoneType.TYPE_PROXIMAL).Length + "," + pinky.Bone(Bone.BoneType.TYPE_PROXIMAL).Width + ",";
		handData += ((new Quaternion(pinky.Bone(Bone.BoneType.TYPE_PROXIMAL).Rotation.x, pinky.Bone(Bone.BoneType.TYPE_PROXIMAL).Rotation.y, pinky.Bone(Bone.BoneType.TYPE_PROXIMAL).Rotation.z, pinky.Bone(Bone.BoneType.TYPE_PROXIMAL).Rotation.w)) * Quaternion.Inverse(rotation)) + ",";
		handData += (pinky.Bone(Bone.BoneType.TYPE_INTERMEDIATE).PrevJoint - palmPosition) + "," + (pinky.Bone(Bone.BoneType.TYPE_INTERMEDIATE).NextJoint - palmPosition) + ",";
		handData += (pinky.Bone(Bone.BoneType.TYPE_INTERMEDIATE).Center - palmPosition) + "," + pinky.Bone(Bone.BoneType.TYPE_INTERMEDIATE).Direction + ",";
		handData += pinky.Bone(Bone.BoneType.TYPE_INTERMEDIATE).Length + "," + pinky.Bone(Bone.BoneType.TYPE_INTERMEDIATE).Width + ",";
		handData += ((new Quaternion(pinky.Bone(Bone.BoneType.TYPE_INTERMEDIATE).Rotation.x, pinky.Bone(Bone.BoneType.TYPE_INTERMEDIATE).Rotation.y, pinky.Bone(Bone.BoneType.TYPE_INTERMEDIATE).Rotation.z, pinky.Bone(Bone.BoneType.TYPE_INTERMEDIATE).Rotation.w)) * Quaternion.Inverse(rotation)) + ",";
		handData += (pinky.Bone(Bone.BoneType.TYPE_DISTAL).PrevJoint - palmPosition) + "," + (pinky.Bone(Bone.BoneType.TYPE_DISTAL).NextJoint - palmPosition) + ",";
		handData += (pinky.Bone(Bone.BoneType.TYPE_DISTAL).Center - palmPosition) + "," + pinky.Bone(Bone.BoneType.TYPE_DISTAL).Direction + ",";
		handData += pinky.Bone(Bone.BoneType.TYPE_DISTAL).Length + "," + pinky.Bone(Bone.BoneType.TYPE_DISTAL).Width + ",";
		handData += ((new Quaternion(pinky.Bone(Bone.BoneType.TYPE_DISTAL).Rotation.x, pinky.Bone(Bone.BoneType.TYPE_DISTAL).Rotation.y, pinky.Bone(Bone.BoneType.TYPE_DISTAL).Rotation.z, pinky.Bone(Bone.BoneType.TYPE_DISTAL).Rotation.w)) * Quaternion.Inverse(rotation)) + "\n";
		File.AppendAllText("data.txt", handData.Replace("(", "").Replace(")", "").Replace(" ", ""));
	}

	void PrintHandData(Hand hand, string handName)
	{
		int HandId = hand.Id;
		float confidence = hand.Confidence;

		List<Finger> Fingers = hand.Fingers;

		float HandPalmPitch = hand.PalmNormal.Pitch;
		float HandPalmRoll = hand.PalmNormal.Roll;
		float HandPalmYaw = hand.PalmNormal.Yaw;
		float HandPalmDirectionRoll = hand.Direction.Roll;

		Vector HandPalmPosition = hand.PalmPosition;
		Vector HandPalmVelocity = hand.PalmVelocity;
		Vector HandPalmNormal = hand.PalmNormal;
		Vector HandFingersDirection = hand.Direction;

		LeapQuaternion HandRotation = hand.Rotation;

		float GrabStrength = hand.GrabStrength;
		float GrabAngle = hand.GrabAngle;

		float PinchDistance = hand.PinchDistance;
		float PinchStrength = hand.PinchStrength;

		float PalmWidth = hand.PalmWidth;

		Vector StabilizedPalmPosition = hand.StabilizedPalmPosition;

		float TimeVisible = hand.TimeVisible;

		Vector HandWristPos = hand.WristPosition;

		Debug.Log(handName + " Hand ID: " + HandId);
		Debug.Log(handName + " Confidence: " + confidence);
		Debug.Log(handName + " Fingers: " + Fingers);
		Debug.Log(handName + " Pitch: " + HandPalmPitch);
		Debug.Log(handName + " Roll: " + HandPalmRoll);
		Debug.Log(handName + " Yaw: " + HandPalmYaw);
		Debug.Log(handName + " PalmDirectionRoll: " + HandPalmDirectionRoll);

		Debug.Log(handName + " PalmPosition: " + HandPalmPosition);
		Debug.Log(handName + " PalmVelocity: " + HandPalmVelocity);
		Debug.Log(handName + " PalmNormal: " + HandPalmNormal);
		Debug.Log(handName + " FingersDirection: " + HandFingersDirection);

		Debug.Log(handName + " HandRotation: " + HandRotation);

		Debug.Log(handName + " GrabStrength: " + GrabStrength);
		Debug.Log(handName + " GrabAngle: " + GrabAngle);

		Debug.Log(handName + " Pinch Distance: " + PinchDistance);
		Debug.Log(handName + " PinchStrength: " + PinchStrength);

		Debug.Log(handName + " PalmWidth: " + PalmWidth);

		Debug.Log(handName + " StabilizedPalmPosition: " + StabilizedPalmPosition);

		Debug.Log(handName + " TimeVisible: " + TimeVisible);

		Debug.Log(handName + " WristPos: " + HandWristPos);
	}

	void PrintFingerData(Finger finger)
	{
		float FingerId = finger.Id;
		Finger.FingerType FingerType = finger.Type;
		int HandId = finger.HandId;

		Vector TipPosition = finger.TipPosition;
		Vector Direction = finger.Direction;

		float Width = finger.Width;
		float Length = finger.Length;

		bool IsExtended = finger.IsExtended;
		float TimeVisible = finger.TimeVisible;

		Debug.Log("Finger ID: " + FingerId);
		Debug.Log("Finger Type: " + FingerType);
		Debug.Log("Hand ID: " + HandId);
		Debug.Log("Tip Position: " + TipPosition);
		Debug.Log("Direction: " + Direction);
		Debug.Log("Width: " + Width);
		Debug.Log("Length: " + Length);
		Debug.Log("IsExtended: " + IsExtended);
		Debug.Log("TimeVisible: " + TimeVisible);

	}

	void PrintBoneData(Bone bone)
	{
		Vector PrevJoint = bone.PrevJoint;  //base of the bone closest to wrist
		Vector BoneMidpoint = bone.Center;
		Vector NextJoint = bone.NextJoint;  //end of the bone, closest to finger tip
		Vector Direction = bone.Direction;

		float Length = bone.Length;
		float Width = bone.Width;
		// BoneType boneType = bone.Type;

		LeapQuaternion Rotation = bone.Rotation;
		LeapTransform basis = bone.Basis;

		Debug.Log("PrevJoint: " + PrevJoint);
		Debug.Log("BoneMidpoint: " + BoneMidpoint);
		Debug.Log("NextJoint: " + NextJoint);
		Debug.Log("Direction: " + Direction);

		Debug.Log("Length: " + Length);
		Debug.Log("Width: " + Width);

		Debug.Log("Rotation: " + Rotation);
		Debug.Log("Basis: " + basis);

	}
}