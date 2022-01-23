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
			float myFloat = LetterBConfidence(hands[0]);

		}
	}
	public bool wroteYet = false;

	float LetterBConfidence(Hand hand)
	{
		List<Finger> fingers = hand.Fingers;
		Vector handDirection = hand.Direction;
		Debug.Log("Hand Direction" + handDirection);
		for (int digit = 1; digit < 5; digit++)
		{
			Debug.Log("Finger " + digit + " " + fingers[digit - 1].Direction);
		}
		return 0;
	}

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

	public Dictionary<string, float> GenerateSignData(Hand hand)
	{
		Dictionary<string, float> signData = new Dictionary<string, float>();

		//Hand Data
		Vector palmPosition = hand.PalmPosition;
		Quaternion rotation = new Quaternion(hand.Rotation.x, hand.Rotation.y, hand.Rotation.z, hand.Rotation.w);
		Quaternion localRot;

		signData.Add("HandId", hand.Id);
		signData.Add("HandConfidence", hand.Confidence);

		signData.Add("HandPalmPitch", hand.PalmNormal.Pitch);
		signData.Add("HandPalmRoll", hand.PalmNormal.Roll);
		signData.Add("HandPalmYaw", hand.PalmNormal.Yaw);
		signData.Add("HandPalmDirectionRoll", hand.Direction.Roll);

		signData.Add("HandPalmPositionX", hand.PalmPosition.x - palmPosition.x);
		signData.Add("HandPalmPositionY", hand.PalmPosition.y - palmPosition.y);
		signData.Add("HandPalmPositionZ", hand.PalmPosition.z - palmPosition.z);
		signData.Add("HandPalmVelocityX", hand.PalmVelocity.x);
		signData.Add("HandPalmVelocityY", hand.PalmVelocity.y);
		signData.Add("HandPalmVelocityZ", hand.PalmVelocity.z);
		signData.Add("HandPalmNormalX", hand.PalmNormal.x - palmPosition.x);
		signData.Add("HandPalmNormalY", hand.PalmNormal.y - palmPosition.y);
		signData.Add("HandPalmNormalZ", hand.PalmNormal.z - palmPosition.z);

		localRot = (new Quaternion(hand.Rotation.x, hand.Rotation.y, hand.Rotation.z, hand.Rotation.w)) * Quaternion.Inverse(rotation);

		signData.Add("HandRotationX", localRot.x);
		signData.Add("HandRotationY", localRot.y);
		signData.Add("HandRotationZ", localRot.z);
		signData.Add("HandRotationW", localRot.w);

		signData.Add("GrabStrength", hand.GrabStrength);
		signData.Add("GrabAngle", hand.GrabAngle);

		signData.Add("PinchDistance", hand.PinchDistance);
		signData.Add("PinchStrength", hand.PinchStrength);

		signData.Add("PalmWidth", hand.PalmWidth);

		signData.Add("StabilizedPalmPositionX", hand.StabilizedPalmPosition.x - palmPosition.x);
		signData.Add("StabilizedPalmPositionY", hand.StabilizedPalmPosition.y - palmPosition.y);
		signData.Add("StabilizedPalmPositionZ", hand.StabilizedPalmPosition.z - palmPosition.z);

		signData.Add("TimeVisible", hand.TimeVisible);

		signData.Add("HandWristPositionX", hand.WristPosition.x - palmPosition.x);
		signData.Add("HandWristPositionY", hand.WristPosition.y - palmPosition.y);
		signData.Add("HandWristPositionZ", hand.WristPosition.z - palmPosition.z);

		//Thumb Data
		int digit = 0;
		Finger finger = hand.Fingers[digit];

		signData.Add("ThumbDirectionX", finger.Direction.x);
		signData.Add("ThumbDirectionY", finger.Direction.y);
		signData.Add("ThumbDirectionZ", finger.Direction.z);
		signData.Add("ThumbWidth", finger.Width);
		signData.Add("ThumbLength", finger.Length);
		signData.Add("ThumbIsExtended", finger.IsExtended ? 1f : 0f);

		signData.Add("ThumbMetacarpalLowerJointX", (finger.Bone(Bone.BoneType.TYPE_METACARPAL).PrevJoint - palmPosition).x);
		signData.Add("ThumbMetacarpalLowerJointY", (finger.Bone(Bone.BoneType.TYPE_METACARPAL).PrevJoint - palmPosition).y);
		signData.Add("ThumbMetacarpalLowerJointZ", (finger.Bone(Bone.BoneType.TYPE_METACARPAL).PrevJoint - palmPosition).z);
		signData.Add("ThumbMetacarpalUpperJointX", (finger.Bone(Bone.BoneType.TYPE_METACARPAL).NextJoint - palmPosition).x);
		signData.Add("ThumbMetacarpalUpperJointY", (finger.Bone(Bone.BoneType.TYPE_METACARPAL).NextJoint - palmPosition).y);
		signData.Add("ThumbMetacarpalUpperJointZ", (finger.Bone(Bone.BoneType.TYPE_METACARPAL).NextJoint - palmPosition).z);
		signData.Add("ThumbMetacarpalCenterX", (finger.Bone(Bone.BoneType.TYPE_METACARPAL).Center - palmPosition).x);
		signData.Add("ThumbMetacarpalCenterY", (finger.Bone(Bone.BoneType.TYPE_METACARPAL).Center - palmPosition).y);
		signData.Add("ThumbMetacarpalCenterZ", (finger.Bone(Bone.BoneType.TYPE_METACARPAL).Center - palmPosition).z);
		signData.Add("ThumbMetacarpalDirectionX", finger.Bone(Bone.BoneType.TYPE_METACARPAL).Direction.x);
		signData.Add("ThumbMetacarpalDirectionY", finger.Bone(Bone.BoneType.TYPE_METACARPAL).Direction.y);
		signData.Add("ThumbMetacarpalDirectionZ", finger.Bone(Bone.BoneType.TYPE_METACARPAL).Direction.z);
		signData.Add("ThumbMetacarpalLength", finger.Bone(Bone.BoneType.TYPE_METACARPAL).Length);
		signData.Add("ThumbMetacarpalWidth", finger.Bone(Bone.BoneType.TYPE_METACARPAL).Width);
		localRot = (new Quaternion(finger.Bone(Bone.BoneType.TYPE_METACARPAL).Rotation.x, finger.Bone(Bone.BoneType.TYPE_METACARPAL).Rotation.y, finger.Bone(Bone.BoneType.TYPE_METACARPAL).Rotation.z, finger.Bone(Bone.BoneType.TYPE_METACARPAL).Rotation.w)) * Quaternion.Inverse(rotation);
		signData.Add("ThumbMetacarpalRotationX", localRot.x);
		signData.Add("ThumbMetacarpalRotationY", localRot.y);
		signData.Add("ThumbMetacarpalRotationZ", localRot.z);
		signData.Add("ThumbMetacarpalRotationW", localRot.w);

		signData.Add("ThumbProximalLowerJointX", (finger.Bone(Bone.BoneType.TYPE_PROXIMAL).PrevJoint - palmPosition).x);
		signData.Add("ThumbProximalLowerJointY", (finger.Bone(Bone.BoneType.TYPE_PROXIMAL).PrevJoint - palmPosition).y);
		signData.Add("ThumbProximalLowerJointZ", (finger.Bone(Bone.BoneType.TYPE_PROXIMAL).PrevJoint - palmPosition).z);
		signData.Add("ThumbProximalUpperJointX", (finger.Bone(Bone.BoneType.TYPE_PROXIMAL).NextJoint - palmPosition).x);
		signData.Add("ThumbProximalUpperJointY", (finger.Bone(Bone.BoneType.TYPE_PROXIMAL).NextJoint - palmPosition).y);
		signData.Add("ThumbProximalUpperJointZ", (finger.Bone(Bone.BoneType.TYPE_PROXIMAL).NextJoint - palmPosition).z);
		signData.Add("ThumbProximalCenterX", (finger.Bone(Bone.BoneType.TYPE_PROXIMAL).Center - palmPosition).x);
		signData.Add("ThumbProximalCenterY", (finger.Bone(Bone.BoneType.TYPE_PROXIMAL).Center - palmPosition).y);
		signData.Add("ThumbProximalCenterZ", (finger.Bone(Bone.BoneType.TYPE_PROXIMAL).Center - palmPosition).z);
		signData.Add("ThumbProximalDirectionX", finger.Bone(Bone.BoneType.TYPE_PROXIMAL).Direction.x);
		signData.Add("ThumbProximalDirectionY", finger.Bone(Bone.BoneType.TYPE_PROXIMAL).Direction.y);
		signData.Add("ThumbProximalDirectionZ", finger.Bone(Bone.BoneType.TYPE_PROXIMAL).Direction.z);
		signData.Add("ThumbProximalLength", finger.Bone(Bone.BoneType.TYPE_PROXIMAL).Length);
		signData.Add("ThumbProximalWidth", finger.Bone(Bone.BoneType.TYPE_PROXIMAL).Width);
		localRot = (new Quaternion(finger.Bone(Bone.BoneType.TYPE_PROXIMAL).Rotation.x, finger.Bone(Bone.BoneType.TYPE_PROXIMAL).Rotation.y, finger.Bone(Bone.BoneType.TYPE_PROXIMAL).Rotation.z, finger.Bone(Bone.BoneType.TYPE_PROXIMAL).Rotation.w)) * Quaternion.Inverse(rotation);
		signData.Add("ThumbProximalRotationX", localRot.x);
		signData.Add("ThumbProximalRotationY", localRot.y);
		signData.Add("ThumbProximalRotationZ", localRot.z);
		signData.Add("ThumbProximalRotationW", localRot.w);

		signData.Add("ThumbIntermediateLowerJointX", (finger.Bone(Bone.BoneType.TYPE_INTERMEDIATE).PrevJoint - palmPosition).x);
		signData.Add("ThumbIntermediateLowerJointY", (finger.Bone(Bone.BoneType.TYPE_INTERMEDIATE).PrevJoint - palmPosition).y);
		signData.Add("ThumbIntermediateLowerJointZ", (finger.Bone(Bone.BoneType.TYPE_INTERMEDIATE).PrevJoint - palmPosition).z);
		signData.Add("ThumbIntermediateUpperJointX", (finger.Bone(Bone.BoneType.TYPE_INTERMEDIATE).NextJoint - palmPosition).x);
		signData.Add("ThumbIntermediateUpperJointY", (finger.Bone(Bone.BoneType.TYPE_INTERMEDIATE).NextJoint - palmPosition).y);
		signData.Add("ThumbIntermediateUpperJointZ", (finger.Bone(Bone.BoneType.TYPE_INTERMEDIATE).NextJoint - palmPosition).z);
		signData.Add("ThumbIntermediateCenterX", (finger.Bone(Bone.BoneType.TYPE_INTERMEDIATE).Center - palmPosition).x);
		signData.Add("ThumbIntermediateCenterY", (finger.Bone(Bone.BoneType.TYPE_INTERMEDIATE).Center - palmPosition).y);
		signData.Add("ThumbIntermediateCenterZ", (finger.Bone(Bone.BoneType.TYPE_INTERMEDIATE).Center - palmPosition).z);
		signData.Add("ThumbIntermediateDirectionX", finger.Bone(Bone.BoneType.TYPE_INTERMEDIATE).Direction.x);
		signData.Add("ThumbIntermediateDirectionY", finger.Bone(Bone.BoneType.TYPE_INTERMEDIATE).Direction.y);
		signData.Add("ThumbIntermediateDirectionZ", finger.Bone(Bone.BoneType.TYPE_INTERMEDIATE).Direction.z);
		signData.Add("ThumbIntermediateLength", finger.Bone(Bone.BoneType.TYPE_INTERMEDIATE).Length);
		signData.Add("ThumbIntermediateWidth", finger.Bone(Bone.BoneType.TYPE_INTERMEDIATE).Width);
		localRot = (new Quaternion(finger.Bone(Bone.BoneType.TYPE_INTERMEDIATE).Rotation.x, finger.Bone(Bone.BoneType.TYPE_INTERMEDIATE).Rotation.y, finger.Bone(Bone.BoneType.TYPE_INTERMEDIATE).Rotation.z, finger.Bone(Bone.BoneType.TYPE_INTERMEDIATE).Rotation.w)) * Quaternion.Inverse(rotation);
		signData.Add("ThumbIntermediateRotationX", localRot.x);
		signData.Add("ThumbIntermediateRotationY", localRot.y);
		signData.Add("ThumbIntermediateRotationZ", localRot.z);
		signData.Add("ThumbIntermediateRotationW", localRot.w);

		signData.Add("ThumbDistalLowerJointX", (finger.Bone(Bone.BoneType.TYPE_DISTAL).PrevJoint - palmPosition).x);
		signData.Add("ThumbDistalLowerJointY", (finger.Bone(Bone.BoneType.TYPE_DISTAL).PrevJoint - palmPosition).y);
		signData.Add("ThumbDistalLowerJointZ", (finger.Bone(Bone.BoneType.TYPE_DISTAL).PrevJoint - palmPosition).z);
		signData.Add("ThumbDistalUpperJointX", (finger.Bone(Bone.BoneType.TYPE_DISTAL).NextJoint - palmPosition).x);
		signData.Add("ThumbDistalUpperJointY", (finger.Bone(Bone.BoneType.TYPE_DISTAL).NextJoint - palmPosition).y);
		signData.Add("ThumbDistalUpperJointZ", (finger.Bone(Bone.BoneType.TYPE_DISTAL).NextJoint - palmPosition).z);
		signData.Add("ThumbDistalCenterX", (finger.Bone(Bone.BoneType.TYPE_DISTAL).Center - palmPosition).x);
		signData.Add("ThumbDistalCenterY", (finger.Bone(Bone.BoneType.TYPE_DISTAL).Center - palmPosition).y);
		signData.Add("ThumbDistalCenterZ", (finger.Bone(Bone.BoneType.TYPE_DISTAL).Center - palmPosition).z);
		signData.Add("ThumbDistalDirectionX", finger.Bone(Bone.BoneType.TYPE_DISTAL).Direction.x);
		signData.Add("ThumbDistalDirectionY", finger.Bone(Bone.BoneType.TYPE_DISTAL).Direction.y);
		signData.Add("ThumbDistalDirectionZ", finger.Bone(Bone.BoneType.TYPE_DISTAL).Direction.z);
		signData.Add("ThumbDistalLength", finger.Bone(Bone.BoneType.TYPE_DISTAL).Length);
		signData.Add("ThumbDistalWidth", finger.Bone(Bone.BoneType.TYPE_DISTAL).Width);
		localRot = (new Quaternion(finger.Bone(Bone.BoneType.TYPE_DISTAL).Rotation.x, finger.Bone(Bone.BoneType.TYPE_DISTAL).Rotation.y, finger.Bone(Bone.BoneType.TYPE_DISTAL).Rotation.z, finger.Bone(Bone.BoneType.TYPE_DISTAL).Rotation.w)) * Quaternion.Inverse(rotation);
		signData.Add("ThumbDistalRotationX", localRot.x);
		signData.Add("ThumbDistalRotationY", localRot.y);
		signData.Add("ThumbDistalRotationZ", localRot.z);
		signData.Add("ThumbDistalRotationW", localRot.w);


		//Index Data
		digit = 1;
		finger = hand.Fingers[digit];

		signData.Add("IndexDirectionX", finger.Direction.x);
		signData.Add("IndexDirectionY", finger.Direction.y);
		signData.Add("IndexDirectionZ", finger.Direction.z);
		signData.Add("IndexWidth", finger.Width);
		signData.Add("IndexLength", finger.Length);
		signData.Add("IndexIsExtended", finger.IsExtended ? 1f : 0f);

		signData.Add("IndexMetacarpalLowerJointX", (finger.Bone(Bone.BoneType.TYPE_METACARPAL).PrevJoint - palmPosition).x);
		signData.Add("IndexMetacarpalLowerJointY", (finger.Bone(Bone.BoneType.TYPE_METACARPAL).PrevJoint - palmPosition).y);
		signData.Add("IndexMetacarpalLowerJointZ", (finger.Bone(Bone.BoneType.TYPE_METACARPAL).PrevJoint - palmPosition).z);
		signData.Add("IndexMetacarpalUpperJointX", (finger.Bone(Bone.BoneType.TYPE_METACARPAL).NextJoint - palmPosition).x);
		signData.Add("IndexMetacarpalUpperJointY", (finger.Bone(Bone.BoneType.TYPE_METACARPAL).NextJoint - palmPosition).y);
		signData.Add("IndexMetacarpalUpperJointZ", (finger.Bone(Bone.BoneType.TYPE_METACARPAL).NextJoint - palmPosition).z);
		signData.Add("IndexMetacarpalCenterX", (finger.Bone(Bone.BoneType.TYPE_METACARPAL).Center - palmPosition).x);
		signData.Add("IndexMetacarpalCenterY", (finger.Bone(Bone.BoneType.TYPE_METACARPAL).Center - palmPosition).y);
		signData.Add("IndexMetacarpalCenterZ", (finger.Bone(Bone.BoneType.TYPE_METACARPAL).Center - palmPosition).z);
		signData.Add("IndexMetacarpalDirectionX", finger.Bone(Bone.BoneType.TYPE_METACARPAL).Direction.x);
		signData.Add("IndexMetacarpalDirectionY", finger.Bone(Bone.BoneType.TYPE_METACARPAL).Direction.y);
		signData.Add("IndexMetacarpalDirectionZ", finger.Bone(Bone.BoneType.TYPE_METACARPAL).Direction.z);
		signData.Add("IndexMetacarpalLength", finger.Bone(Bone.BoneType.TYPE_METACARPAL).Length);
		signData.Add("IndexMetacarpalWidth", finger.Bone(Bone.BoneType.TYPE_METACARPAL).Width);
		localRot = (new Quaternion(finger.Bone(Bone.BoneType.TYPE_METACARPAL).Rotation.x, finger.Bone(Bone.BoneType.TYPE_METACARPAL).Rotation.y, finger.Bone(Bone.BoneType.TYPE_METACARPAL).Rotation.z, finger.Bone(Bone.BoneType.TYPE_METACARPAL).Rotation.w)) * Quaternion.Inverse(rotation);
		signData.Add("IndexMetacarpalRotationX", localRot.x);
		signData.Add("IndexMetacarpalRotationY", localRot.y);
		signData.Add("IndexMetacarpalRotationZ", localRot.z);
		signData.Add("IndexMetacarpalRotationW", localRot.w);

		signData.Add("IndexProximalLowerJointX", (finger.Bone(Bone.BoneType.TYPE_PROXIMAL).PrevJoint - palmPosition).x);
		signData.Add("IndexProximalLowerJointY", (finger.Bone(Bone.BoneType.TYPE_PROXIMAL).PrevJoint - palmPosition).y);
		signData.Add("IndexProximalLowerJointZ", (finger.Bone(Bone.BoneType.TYPE_PROXIMAL).PrevJoint - palmPosition).z);
		signData.Add("IndexProximalUpperJointX", (finger.Bone(Bone.BoneType.TYPE_PROXIMAL).NextJoint - palmPosition).x);
		signData.Add("IndexProximalUpperJointY", (finger.Bone(Bone.BoneType.TYPE_PROXIMAL).NextJoint - palmPosition).y);
		signData.Add("IndexProximalUpperJointZ", (finger.Bone(Bone.BoneType.TYPE_PROXIMAL).NextJoint - palmPosition).z);
		signData.Add("IndexProximalCenterX", (finger.Bone(Bone.BoneType.TYPE_PROXIMAL).Center - palmPosition).x);
		signData.Add("IndexProximalCenterY", (finger.Bone(Bone.BoneType.TYPE_PROXIMAL).Center - palmPosition).y);
		signData.Add("IndexProximalCenterZ", (finger.Bone(Bone.BoneType.TYPE_PROXIMAL).Center - palmPosition).z);
		signData.Add("IndexProximalDirectionX", finger.Bone(Bone.BoneType.TYPE_PROXIMAL).Direction.x);
		signData.Add("IndexProximalDirectionY", finger.Bone(Bone.BoneType.TYPE_PROXIMAL).Direction.y);
		signData.Add("IndexProximalDirectionZ", finger.Bone(Bone.BoneType.TYPE_PROXIMAL).Direction.z);
		signData.Add("IndexProximalLength", finger.Bone(Bone.BoneType.TYPE_PROXIMAL).Length);
		signData.Add("IndexProximalWidth", finger.Bone(Bone.BoneType.TYPE_PROXIMAL).Width);
		localRot = (new Quaternion(finger.Bone(Bone.BoneType.TYPE_PROXIMAL).Rotation.x, finger.Bone(Bone.BoneType.TYPE_PROXIMAL).Rotation.y, finger.Bone(Bone.BoneType.TYPE_PROXIMAL).Rotation.z, finger.Bone(Bone.BoneType.TYPE_PROXIMAL).Rotation.w)) * Quaternion.Inverse(rotation);
		signData.Add("IndexProximalRotationX", localRot.x);
		signData.Add("IndexProximalRotationY", localRot.y);
		signData.Add("IndexProximalRotationZ", localRot.z);
		signData.Add("IndexProximalRotationW", localRot.w);

		signData.Add("IndexIntermediateLowerJointX", (finger.Bone(Bone.BoneType.TYPE_INTERMEDIATE).PrevJoint - palmPosition).x);
		signData.Add("IndexIntermediateLowerJointY", (finger.Bone(Bone.BoneType.TYPE_INTERMEDIATE).PrevJoint - palmPosition).y);
		signData.Add("IndexIntermediateLowerJointZ", (finger.Bone(Bone.BoneType.TYPE_INTERMEDIATE).PrevJoint - palmPosition).z);
		signData.Add("IndexIntermediateUpperJointX", (finger.Bone(Bone.BoneType.TYPE_INTERMEDIATE).NextJoint - palmPosition).x);
		signData.Add("IndexIntermediateUpperJointY", (finger.Bone(Bone.BoneType.TYPE_INTERMEDIATE).NextJoint - palmPosition).y);
		signData.Add("IndexIntermediateUpperJointZ", (finger.Bone(Bone.BoneType.TYPE_INTERMEDIATE).NextJoint - palmPosition).z);
		signData.Add("IndexIntermediateCenterX", (finger.Bone(Bone.BoneType.TYPE_INTERMEDIATE).Center - palmPosition).x);
		signData.Add("IndexIntermediateCenterY", (finger.Bone(Bone.BoneType.TYPE_INTERMEDIATE).Center - palmPosition).y);
		signData.Add("IndexIntermediateCenterZ", (finger.Bone(Bone.BoneType.TYPE_INTERMEDIATE).Center - palmPosition).z);
		signData.Add("IndexIntermediateDirectionX", finger.Bone(Bone.BoneType.TYPE_INTERMEDIATE).Direction.x);
		signData.Add("IndexIntermediateDirectionY", finger.Bone(Bone.BoneType.TYPE_INTERMEDIATE).Direction.y);
		signData.Add("IndexIntermediateDirectionZ", finger.Bone(Bone.BoneType.TYPE_INTERMEDIATE).Direction.z);
		signData.Add("IndexIntermediateLength", finger.Bone(Bone.BoneType.TYPE_INTERMEDIATE).Length);
		signData.Add("IndexIntermediateWidth", finger.Bone(Bone.BoneType.TYPE_INTERMEDIATE).Width);
		localRot = (new Quaternion(finger.Bone(Bone.BoneType.TYPE_INTERMEDIATE).Rotation.x, finger.Bone(Bone.BoneType.TYPE_INTERMEDIATE).Rotation.y, finger.Bone(Bone.BoneType.TYPE_INTERMEDIATE).Rotation.z, finger.Bone(Bone.BoneType.TYPE_INTERMEDIATE).Rotation.w)) * Quaternion.Inverse(rotation);
		signData.Add("IndexIntermediateRotationX", localRot.x);
		signData.Add("IndexIntermediateRotationY", localRot.y);
		signData.Add("IndexIntermediateRotationZ", localRot.z);
		signData.Add("IndexIntermediateRotationW", localRot.w);

		signData.Add("IndexDistalLowerJointX", (finger.Bone(Bone.BoneType.TYPE_DISTAL).PrevJoint - palmPosition).x);
		signData.Add("IndexDistalLowerJointY", (finger.Bone(Bone.BoneType.TYPE_DISTAL).PrevJoint - palmPosition).y);
		signData.Add("IndexDistalLowerJointZ", (finger.Bone(Bone.BoneType.TYPE_DISTAL).PrevJoint - palmPosition).z);
		signData.Add("IndexDistalUpperJointX", (finger.Bone(Bone.BoneType.TYPE_DISTAL).NextJoint - palmPosition).x);
		signData.Add("IndexDistalUpperJointY", (finger.Bone(Bone.BoneType.TYPE_DISTAL).NextJoint - palmPosition).y);
		signData.Add("IndexDistalUpperJointZ", (finger.Bone(Bone.BoneType.TYPE_DISTAL).NextJoint - palmPosition).z);
		signData.Add("IndexDistalCenterX", (finger.Bone(Bone.BoneType.TYPE_DISTAL).Center - palmPosition).x);
		signData.Add("IndexDistalCenterY", (finger.Bone(Bone.BoneType.TYPE_DISTAL).Center - palmPosition).y);
		signData.Add("IndexDistalCenterZ", (finger.Bone(Bone.BoneType.TYPE_DISTAL).Center - palmPosition).z);
		signData.Add("IndexDistalDirectionX", finger.Bone(Bone.BoneType.TYPE_DISTAL).Direction.x);
		signData.Add("IndexDistalDirectionY", finger.Bone(Bone.BoneType.TYPE_DISTAL).Direction.y);
		signData.Add("IndexDistalDirectionZ", finger.Bone(Bone.BoneType.TYPE_DISTAL).Direction.z);
		signData.Add("IndexDistalLength", finger.Bone(Bone.BoneType.TYPE_DISTAL).Length);
		signData.Add("IndexDistalWidth", finger.Bone(Bone.BoneType.TYPE_DISTAL).Width);
		localRot = (new Quaternion(finger.Bone(Bone.BoneType.TYPE_DISTAL).Rotation.x, finger.Bone(Bone.BoneType.TYPE_DISTAL).Rotation.y, finger.Bone(Bone.BoneType.TYPE_DISTAL).Rotation.z, finger.Bone(Bone.BoneType.TYPE_DISTAL).Rotation.w)) * Quaternion.Inverse(rotation);
		signData.Add("IndexDistalRotationX", localRot.x);
		signData.Add("IndexDistalRotationY", localRot.y);
		signData.Add("IndexDistalRotationZ", localRot.z);
		signData.Add("IndexDistalRotationW", localRot.w);








		//Middle Data
		digit = 2;
		finger = hand.Fingers[digit];

		signData.Add("MiddleDirectionX", finger.Direction.x);
		signData.Add("MiddleDirectionY", finger.Direction.y);
		signData.Add("MiddleDirectionZ", finger.Direction.z);
		signData.Add("MiddleWidth", finger.Width);
		signData.Add("MiddleLength", finger.Length);
		signData.Add("MiddleIsExtended", finger.IsExtended ? 1f : 0f);

		signData.Add("MiddleMetacarpalLowerJointX", (finger.Bone(Bone.BoneType.TYPE_METACARPAL).PrevJoint - palmPosition).x);
		signData.Add("MiddleMetacarpalLowerJointY", (finger.Bone(Bone.BoneType.TYPE_METACARPAL).PrevJoint - palmPosition).y);
		signData.Add("MiddleMetacarpalLowerJointZ", (finger.Bone(Bone.BoneType.TYPE_METACARPAL).PrevJoint - palmPosition).z);
		signData.Add("MiddleMetacarpalUpperJointX", (finger.Bone(Bone.BoneType.TYPE_METACARPAL).NextJoint - palmPosition).x);
		signData.Add("MiddleMetacarpalUpperJointY", (finger.Bone(Bone.BoneType.TYPE_METACARPAL).NextJoint - palmPosition).y);
		signData.Add("MiddleMetacarpalUpperJointZ", (finger.Bone(Bone.BoneType.TYPE_METACARPAL).NextJoint - palmPosition).z);
		signData.Add("MiddleMetacarpalCenterX", (finger.Bone(Bone.BoneType.TYPE_METACARPAL).Center - palmPosition).x);
		signData.Add("MiddleMetacarpalCenterY", (finger.Bone(Bone.BoneType.TYPE_METACARPAL).Center - palmPosition).y);
		signData.Add("MiddleMetacarpalCenterZ", (finger.Bone(Bone.BoneType.TYPE_METACARPAL).Center - palmPosition).z);
		signData.Add("MiddleMetacarpalDirectionX", finger.Bone(Bone.BoneType.TYPE_METACARPAL).Direction.x);
		signData.Add("MiddleMetacarpalDirectionY", finger.Bone(Bone.BoneType.TYPE_METACARPAL).Direction.y);
		signData.Add("MiddleMetacarpalDirectionZ", finger.Bone(Bone.BoneType.TYPE_METACARPAL).Direction.z);
		signData.Add("MiddleMetacarpalLength", finger.Bone(Bone.BoneType.TYPE_METACARPAL).Length);
		signData.Add("MiddleMetacarpalWidth", finger.Bone(Bone.BoneType.TYPE_METACARPAL).Width);
		localRot = (new Quaternion(finger.Bone(Bone.BoneType.TYPE_METACARPAL).Rotation.x, finger.Bone(Bone.BoneType.TYPE_METACARPAL).Rotation.y, finger.Bone(Bone.BoneType.TYPE_METACARPAL).Rotation.z, finger.Bone(Bone.BoneType.TYPE_METACARPAL).Rotation.w)) * Quaternion.Inverse(rotation);
		signData.Add("MiddleMetacarpalRotationX", localRot.x);
		signData.Add("MiddleMetacarpalRotationY", localRot.y);
		signData.Add("MiddleMetacarpalRotationZ", localRot.z);
		signData.Add("MiddleMetacarpalRotationW", localRot.w);

		signData.Add("MiddleProximalLowerJointX", (finger.Bone(Bone.BoneType.TYPE_PROXIMAL).PrevJoint - palmPosition).x);
		signData.Add("MiddleProximalLowerJointY", (finger.Bone(Bone.BoneType.TYPE_PROXIMAL).PrevJoint - palmPosition).y);
		signData.Add("MiddleProximalLowerJointZ", (finger.Bone(Bone.BoneType.TYPE_PROXIMAL).PrevJoint - palmPosition).z);
		signData.Add("MiddleProximalUpperJointX", (finger.Bone(Bone.BoneType.TYPE_PROXIMAL).NextJoint - palmPosition).x);
		signData.Add("MiddleProximalUpperJointY", (finger.Bone(Bone.BoneType.TYPE_PROXIMAL).NextJoint - palmPosition).y);
		signData.Add("MiddleProximalUpperJointZ", (finger.Bone(Bone.BoneType.TYPE_PROXIMAL).NextJoint - palmPosition).z);
		signData.Add("MiddleProximalCenterX", (finger.Bone(Bone.BoneType.TYPE_PROXIMAL).Center - palmPosition).x);
		signData.Add("MiddleProximalCenterY", (finger.Bone(Bone.BoneType.TYPE_PROXIMAL).Center - palmPosition).y);
		signData.Add("MiddleProximalCenterZ", (finger.Bone(Bone.BoneType.TYPE_PROXIMAL).Center - palmPosition).z);
		signData.Add("MiddleProximalDirectionX", finger.Bone(Bone.BoneType.TYPE_PROXIMAL).Direction.x);
		signData.Add("MiddleProximalDirectionY", finger.Bone(Bone.BoneType.TYPE_PROXIMAL).Direction.y);
		signData.Add("MiddleProximalDirectionZ", finger.Bone(Bone.BoneType.TYPE_PROXIMAL).Direction.z);
		signData.Add("MiddleProximalLength", finger.Bone(Bone.BoneType.TYPE_PROXIMAL).Length);
		signData.Add("MiddleProximalWidth", finger.Bone(Bone.BoneType.TYPE_PROXIMAL).Width);
		localRot = (new Quaternion(finger.Bone(Bone.BoneType.TYPE_PROXIMAL).Rotation.x, finger.Bone(Bone.BoneType.TYPE_PROXIMAL).Rotation.y, finger.Bone(Bone.BoneType.TYPE_PROXIMAL).Rotation.z, finger.Bone(Bone.BoneType.TYPE_PROXIMAL).Rotation.w)) * Quaternion.Inverse(rotation);
		signData.Add("MiddleProximalRotationX", localRot.x);
		signData.Add("MiddleProximalRotationY", localRot.y);
		signData.Add("MiddleProximalRotationZ", localRot.z);
		signData.Add("MiddleProximalRotationW", localRot.w);

		signData.Add("MiddleIntermediateLowerJointX", (finger.Bone(Bone.BoneType.TYPE_INTERMEDIATE).PrevJoint - palmPosition).x);
		signData.Add("MiddleIntermediateLowerJointY", (finger.Bone(Bone.BoneType.TYPE_INTERMEDIATE).PrevJoint - palmPosition).y);
		signData.Add("MiddleIntermediateLowerJointZ", (finger.Bone(Bone.BoneType.TYPE_INTERMEDIATE).PrevJoint - palmPosition).z);
		signData.Add("MiddleIntermediateUpperJointX", (finger.Bone(Bone.BoneType.TYPE_INTERMEDIATE).NextJoint - palmPosition).x);
		signData.Add("MiddleIntermediateUpperJointY", (finger.Bone(Bone.BoneType.TYPE_INTERMEDIATE).NextJoint - palmPosition).y);
		signData.Add("MiddleIntermediateUpperJointZ", (finger.Bone(Bone.BoneType.TYPE_INTERMEDIATE).NextJoint - palmPosition).z);
		signData.Add("MiddleIntermediateCenterX", (finger.Bone(Bone.BoneType.TYPE_INTERMEDIATE).Center - palmPosition).x);
		signData.Add("MiddleIntermediateCenterY", (finger.Bone(Bone.BoneType.TYPE_INTERMEDIATE).Center - palmPosition).y);
		signData.Add("MiddleIntermediateCenterZ", (finger.Bone(Bone.BoneType.TYPE_INTERMEDIATE).Center - palmPosition).z);
		signData.Add("MiddleIntermediateDirectionX", finger.Bone(Bone.BoneType.TYPE_INTERMEDIATE).Direction.x);
		signData.Add("MiddleIntermediateDirectionY", finger.Bone(Bone.BoneType.TYPE_INTERMEDIATE).Direction.y);
		signData.Add("MiddleIntermediateDirectionZ", finger.Bone(Bone.BoneType.TYPE_INTERMEDIATE).Direction.z);
		signData.Add("MiddleIntermediateLength", finger.Bone(Bone.BoneType.TYPE_INTERMEDIATE).Length);
		signData.Add("MiddleIntermediateWidth", finger.Bone(Bone.BoneType.TYPE_INTERMEDIATE).Width);
		localRot = (new Quaternion(finger.Bone(Bone.BoneType.TYPE_INTERMEDIATE).Rotation.x, finger.Bone(Bone.BoneType.TYPE_INTERMEDIATE).Rotation.y, finger.Bone(Bone.BoneType.TYPE_INTERMEDIATE).Rotation.z, finger.Bone(Bone.BoneType.TYPE_INTERMEDIATE).Rotation.w)) * Quaternion.Inverse(rotation);
		signData.Add("MiddleIntermediateRotationX", localRot.x);
		signData.Add("MiddleIntermediateRotationY", localRot.y);
		signData.Add("MiddleIntermediateRotationZ", localRot.z);
		signData.Add("MiddleIntermediateRotationW", localRot.w);

		signData.Add("MiddleDistalLowerJointX", (finger.Bone(Bone.BoneType.TYPE_DISTAL).PrevJoint - palmPosition).x);
		signData.Add("MiddleDistalLowerJointY", (finger.Bone(Bone.BoneType.TYPE_DISTAL).PrevJoint - palmPosition).y);
		signData.Add("MiddleDistalLowerJointZ", (finger.Bone(Bone.BoneType.TYPE_DISTAL).PrevJoint - palmPosition).z);
		signData.Add("MiddleDistalUpperJointX", (finger.Bone(Bone.BoneType.TYPE_DISTAL).NextJoint - palmPosition).x);
		signData.Add("MiddleDistalUpperJointY", (finger.Bone(Bone.BoneType.TYPE_DISTAL).NextJoint - palmPosition).y);
		signData.Add("MiddleDistalUpperJointZ", (finger.Bone(Bone.BoneType.TYPE_DISTAL).NextJoint - palmPosition).z);
		signData.Add("MiddleDistalCenterX", (finger.Bone(Bone.BoneType.TYPE_DISTAL).Center - palmPosition).x);
		signData.Add("MiddleDistalCenterY", (finger.Bone(Bone.BoneType.TYPE_DISTAL).Center - palmPosition).y);
		signData.Add("MiddleDistalCenterZ", (finger.Bone(Bone.BoneType.TYPE_DISTAL).Center - palmPosition).z);
		signData.Add("MiddleDistalDirectionX", finger.Bone(Bone.BoneType.TYPE_DISTAL).Direction.x);
		signData.Add("MiddleDistalDirectionY", finger.Bone(Bone.BoneType.TYPE_DISTAL).Direction.y);
		signData.Add("MiddleDistalDirectionZ", finger.Bone(Bone.BoneType.TYPE_DISTAL).Direction.z);
		signData.Add("MiddleDistalLength", finger.Bone(Bone.BoneType.TYPE_DISTAL).Length);
		signData.Add("MiddleDistalWidth", finger.Bone(Bone.BoneType.TYPE_DISTAL).Width);
		localRot = (new Quaternion(finger.Bone(Bone.BoneType.TYPE_DISTAL).Rotation.x, finger.Bone(Bone.BoneType.TYPE_DISTAL).Rotation.y, finger.Bone(Bone.BoneType.TYPE_DISTAL).Rotation.z, finger.Bone(Bone.BoneType.TYPE_DISTAL).Rotation.w)) * Quaternion.Inverse(rotation);
		signData.Add("MiddleDistalRotationX", localRot.x);
		signData.Add("MiddleDistalRotationY", localRot.y);
		signData.Add("MiddleDistalRotationZ", localRot.z);
		signData.Add("MiddleDistalRotationW", localRot.w);



		//Ring Data
		digit = 3;
		finger = hand.Fingers[digit];

		signData.Add("RingDirectionX", finger.Direction.x);
		signData.Add("RingDirectionY", finger.Direction.y);
		signData.Add("RingDirectionZ", finger.Direction.z);
		signData.Add("RingWidth", finger.Width);
		signData.Add("RingLength", finger.Length);
		signData.Add("RingIsExtended", finger.IsExtended ? 1f : 0f);

		signData.Add("RingMetacarpalLowerJointX", (finger.Bone(Bone.BoneType.TYPE_METACARPAL).PrevJoint - palmPosition).x);
		signData.Add("RingMetacarpalLowerJointY", (finger.Bone(Bone.BoneType.TYPE_METACARPAL).PrevJoint - palmPosition).y);
		signData.Add("RingMetacarpalLowerJointZ", (finger.Bone(Bone.BoneType.TYPE_METACARPAL).PrevJoint - palmPosition).z);
		signData.Add("RingMetacarpalUpperJointX", (finger.Bone(Bone.BoneType.TYPE_METACARPAL).NextJoint - palmPosition).x);
		signData.Add("RingMetacarpalUpperJointY", (finger.Bone(Bone.BoneType.TYPE_METACARPAL).NextJoint - palmPosition).y);
		signData.Add("RingMetacarpalUpperJointZ", (finger.Bone(Bone.BoneType.TYPE_METACARPAL).NextJoint - palmPosition).z);
		signData.Add("RingMetacarpalCenterX", (finger.Bone(Bone.BoneType.TYPE_METACARPAL).Center - palmPosition).x);
		signData.Add("RingMetacarpalCenterY", (finger.Bone(Bone.BoneType.TYPE_METACARPAL).Center - palmPosition).y);
		signData.Add("RingMetacarpalCenterZ", (finger.Bone(Bone.BoneType.TYPE_METACARPAL).Center - palmPosition).z);
		signData.Add("RingMetacarpalDirectionX", finger.Bone(Bone.BoneType.TYPE_METACARPAL).Direction.x);
		signData.Add("RingMetacarpalDirectionY", finger.Bone(Bone.BoneType.TYPE_METACARPAL).Direction.y);
		signData.Add("RingMetacarpalDirectionZ", finger.Bone(Bone.BoneType.TYPE_METACARPAL).Direction.z);
		signData.Add("RingMetacarpalLength", finger.Bone(Bone.BoneType.TYPE_METACARPAL).Length);
		signData.Add("RingMetacarpalWidth", finger.Bone(Bone.BoneType.TYPE_METACARPAL).Width);
		localRot = (new Quaternion(finger.Bone(Bone.BoneType.TYPE_METACARPAL).Rotation.x, finger.Bone(Bone.BoneType.TYPE_METACARPAL).Rotation.y, finger.Bone(Bone.BoneType.TYPE_METACARPAL).Rotation.z, finger.Bone(Bone.BoneType.TYPE_METACARPAL).Rotation.w)) * Quaternion.Inverse(rotation);
		signData.Add("RingMetacarpalRotationX", localRot.x);
		signData.Add("RingMetacarpalRotationY", localRot.y);
		signData.Add("RingMetacarpalRotationZ", localRot.z);
		signData.Add("RingMetacarpalRotationW", localRot.w);

		signData.Add("RingProximalLowerJointX", (finger.Bone(Bone.BoneType.TYPE_PROXIMAL).PrevJoint - palmPosition).x);
		signData.Add("RingProximalLowerJointY", (finger.Bone(Bone.BoneType.TYPE_PROXIMAL).PrevJoint - palmPosition).y);
		signData.Add("RingProximalLowerJointZ", (finger.Bone(Bone.BoneType.TYPE_PROXIMAL).PrevJoint - palmPosition).z);
		signData.Add("RingProximalUpperJointX", (finger.Bone(Bone.BoneType.TYPE_PROXIMAL).NextJoint - palmPosition).x);
		signData.Add("RingProximalUpperJointY", (finger.Bone(Bone.BoneType.TYPE_PROXIMAL).NextJoint - palmPosition).y);
		signData.Add("RingProximalUpperJointZ", (finger.Bone(Bone.BoneType.TYPE_PROXIMAL).NextJoint - palmPosition).z);
		signData.Add("RingProximalCenterX", (finger.Bone(Bone.BoneType.TYPE_PROXIMAL).Center - palmPosition).x);
		signData.Add("RingProximalCenterY", (finger.Bone(Bone.BoneType.TYPE_PROXIMAL).Center - palmPosition).y);
		signData.Add("RingProximalCenterZ", (finger.Bone(Bone.BoneType.TYPE_PROXIMAL).Center - palmPosition).z);
		signData.Add("RingProximalDirectionX", finger.Bone(Bone.BoneType.TYPE_PROXIMAL).Direction.x);
		signData.Add("RingProximalDirectionY", finger.Bone(Bone.BoneType.TYPE_PROXIMAL).Direction.y);
		signData.Add("RingProximalDirectionZ", finger.Bone(Bone.BoneType.TYPE_PROXIMAL).Direction.z);
		signData.Add("RingProximalLength", finger.Bone(Bone.BoneType.TYPE_PROXIMAL).Length);
		signData.Add("RingProximalWidth", finger.Bone(Bone.BoneType.TYPE_PROXIMAL).Width);
		localRot = (new Quaternion(finger.Bone(Bone.BoneType.TYPE_PROXIMAL).Rotation.x, finger.Bone(Bone.BoneType.TYPE_PROXIMAL).Rotation.y, finger.Bone(Bone.BoneType.TYPE_PROXIMAL).Rotation.z, finger.Bone(Bone.BoneType.TYPE_PROXIMAL).Rotation.w)) * Quaternion.Inverse(rotation);
		signData.Add("RingProximalRotationX", localRot.x);
		signData.Add("RingProximalRotationY", localRot.y);
		signData.Add("RingProximalRotationZ", localRot.z);
		signData.Add("RingProximalRotationW", localRot.w);

		signData.Add("RingIntermediateLowerJointX", (finger.Bone(Bone.BoneType.TYPE_INTERMEDIATE).PrevJoint - palmPosition).x);
		signData.Add("RingIntermediateLowerJointY", (finger.Bone(Bone.BoneType.TYPE_INTERMEDIATE).PrevJoint - palmPosition).y);
		signData.Add("RingIntermediateLowerJointZ", (finger.Bone(Bone.BoneType.TYPE_INTERMEDIATE).PrevJoint - palmPosition).z);
		signData.Add("RingIntermediateUpperJointX", (finger.Bone(Bone.BoneType.TYPE_INTERMEDIATE).NextJoint - palmPosition).x);
		signData.Add("RingIntermediateUpperJointY", (finger.Bone(Bone.BoneType.TYPE_INTERMEDIATE).NextJoint - palmPosition).y);
		signData.Add("RingIntermediateUpperJointZ", (finger.Bone(Bone.BoneType.TYPE_INTERMEDIATE).NextJoint - palmPosition).z);
		signData.Add("RingIntermediateCenterX", (finger.Bone(Bone.BoneType.TYPE_INTERMEDIATE).Center - palmPosition).x);
		signData.Add("RingIntermediateCenterY", (finger.Bone(Bone.BoneType.TYPE_INTERMEDIATE).Center - palmPosition).y);
		signData.Add("RingIntermediateCenterZ", (finger.Bone(Bone.BoneType.TYPE_INTERMEDIATE).Center - palmPosition).z);
		signData.Add("RingIntermediateDirectionX", finger.Bone(Bone.BoneType.TYPE_INTERMEDIATE).Direction.x);
		signData.Add("RingIntermediateDirectionY", finger.Bone(Bone.BoneType.TYPE_INTERMEDIATE).Direction.y);
		signData.Add("RingIntermediateDirectionZ", finger.Bone(Bone.BoneType.TYPE_INTERMEDIATE).Direction.z);
		signData.Add("RingIntermediateLength", finger.Bone(Bone.BoneType.TYPE_INTERMEDIATE).Length);
		signData.Add("RingIntermediateWidth", finger.Bone(Bone.BoneType.TYPE_INTERMEDIATE).Width);
		localRot = (new Quaternion(finger.Bone(Bone.BoneType.TYPE_INTERMEDIATE).Rotation.x, finger.Bone(Bone.BoneType.TYPE_INTERMEDIATE).Rotation.y, finger.Bone(Bone.BoneType.TYPE_INTERMEDIATE).Rotation.z, finger.Bone(Bone.BoneType.TYPE_INTERMEDIATE).Rotation.w)) * Quaternion.Inverse(rotation);
		signData.Add("RingIntermediateRotationX", localRot.x);
		signData.Add("RingIntermediateRotationY", localRot.y);
		signData.Add("RingIntermediateRotationZ", localRot.z);
		signData.Add("RingIntermediateRotationW", localRot.w);

		signData.Add("RingDistalLowerJointX", (finger.Bone(Bone.BoneType.TYPE_DISTAL).PrevJoint - palmPosition).x);
		signData.Add("RingDistalLowerJointY", (finger.Bone(Bone.BoneType.TYPE_DISTAL).PrevJoint - palmPosition).y);
		signData.Add("RingDistalLowerJointZ", (finger.Bone(Bone.BoneType.TYPE_DISTAL).PrevJoint - palmPosition).z);
		signData.Add("RingDistalUpperJointX", (finger.Bone(Bone.BoneType.TYPE_DISTAL).NextJoint - palmPosition).x);
		signData.Add("RingDistalUpperJointY", (finger.Bone(Bone.BoneType.TYPE_DISTAL).NextJoint - palmPosition).y);
		signData.Add("RingDistalUpperJointZ", (finger.Bone(Bone.BoneType.TYPE_DISTAL).NextJoint - palmPosition).z);
		signData.Add("RingDistalCenterX", (finger.Bone(Bone.BoneType.TYPE_DISTAL).Center - palmPosition).x);
		signData.Add("RingDistalCenterY", (finger.Bone(Bone.BoneType.TYPE_DISTAL).Center - palmPosition).y);
		signData.Add("RingDistalCenterZ", (finger.Bone(Bone.BoneType.TYPE_DISTAL).Center - palmPosition).z);
		signData.Add("RingDistalDirectionX", finger.Bone(Bone.BoneType.TYPE_DISTAL).Direction.x);
		signData.Add("RingDistalDirectionY", finger.Bone(Bone.BoneType.TYPE_DISTAL).Direction.y);
		signData.Add("RingDistalDirectionZ", finger.Bone(Bone.BoneType.TYPE_DISTAL).Direction.z);
		signData.Add("RingDistalLength", finger.Bone(Bone.BoneType.TYPE_DISTAL).Length);
		signData.Add("RingDistalWidth", finger.Bone(Bone.BoneType.TYPE_DISTAL).Width);
		localRot = (new Quaternion(finger.Bone(Bone.BoneType.TYPE_DISTAL).Rotation.x, finger.Bone(Bone.BoneType.TYPE_DISTAL).Rotation.y, finger.Bone(Bone.BoneType.TYPE_DISTAL).Rotation.z, finger.Bone(Bone.BoneType.TYPE_DISTAL).Rotation.w)) * Quaternion.Inverse(rotation);
		signData.Add("RingDistalRotationX", localRot.x);
		signData.Add("RingDistalRotationY", localRot.y);
		signData.Add("RingDistalRotationZ", localRot.z);
		signData.Add("RingDistalRotationW", localRot.w);



		//Pinky Data
		digit = 4;
		finger = hand.Fingers[digit];

		signData.Add("PinkyDirectionX", finger.Direction.x);
		signData.Add("PinkyDirectionY", finger.Direction.y);
		signData.Add("PinkyDirectionZ", finger.Direction.z);
		signData.Add("PinkyWidth", finger.Width);
		signData.Add("PinkyLength", finger.Length);
		signData.Add("PinkyIsExtended", finger.IsExtended ? 1f : 0f);

		signData.Add("PinkyMetacarpalLowerJointX", (finger.Bone(Bone.BoneType.TYPE_METACARPAL).PrevJoint - palmPosition).x);
		signData.Add("PinkyMetacarpalLowerJointY", (finger.Bone(Bone.BoneType.TYPE_METACARPAL).PrevJoint - palmPosition).y);
		signData.Add("PinkyMetacarpalLowerJointZ", (finger.Bone(Bone.BoneType.TYPE_METACARPAL).PrevJoint - palmPosition).z);
		signData.Add("PinkyMetacarpalUpperJointX", (finger.Bone(Bone.BoneType.TYPE_METACARPAL).NextJoint - palmPosition).x);
		signData.Add("PinkyMetacarpalUpperJointY", (finger.Bone(Bone.BoneType.TYPE_METACARPAL).NextJoint - palmPosition).y);
		signData.Add("PinkyMetacarpalUpperJointZ", (finger.Bone(Bone.BoneType.TYPE_METACARPAL).NextJoint - palmPosition).z);
		signData.Add("PinkyMetacarpalCenterX", (finger.Bone(Bone.BoneType.TYPE_METACARPAL).Center - palmPosition).x);
		signData.Add("PinkyMetacarpalCenterY", (finger.Bone(Bone.BoneType.TYPE_METACARPAL).Center - palmPosition).y);
		signData.Add("PinkyMetacarpalCenterZ", (finger.Bone(Bone.BoneType.TYPE_METACARPAL).Center - palmPosition).z);
		signData.Add("PinkyMetacarpalDirectionX", finger.Bone(Bone.BoneType.TYPE_METACARPAL).Direction.x);
		signData.Add("PinkyMetacarpalDirectionY", finger.Bone(Bone.BoneType.TYPE_METACARPAL).Direction.y);
		signData.Add("PinkyMetacarpalDirectionZ", finger.Bone(Bone.BoneType.TYPE_METACARPAL).Direction.z);
		signData.Add("PinkyMetacarpalLength", finger.Bone(Bone.BoneType.TYPE_METACARPAL).Length);
		signData.Add("PinkyMetacarpalWidth", finger.Bone(Bone.BoneType.TYPE_METACARPAL).Width);
		localRot = (new Quaternion(finger.Bone(Bone.BoneType.TYPE_METACARPAL).Rotation.x, finger.Bone(Bone.BoneType.TYPE_METACARPAL).Rotation.y, finger.Bone(Bone.BoneType.TYPE_METACARPAL).Rotation.z, finger.Bone(Bone.BoneType.TYPE_METACARPAL).Rotation.w)) * Quaternion.Inverse(rotation);
		signData.Add("PinkyMetacarpalRotationX", localRot.x);
		signData.Add("PinkyMetacarpalRotationY", localRot.y);
		signData.Add("PinkyMetacarpalRotationZ", localRot.z);
		signData.Add("PinkyMetacarpalRotationW", localRot.w);

		signData.Add("PinkyProximalLowerJointX", (finger.Bone(Bone.BoneType.TYPE_PROXIMAL).PrevJoint - palmPosition).x);
		signData.Add("PinkyProximalLowerJointY", (finger.Bone(Bone.BoneType.TYPE_PROXIMAL).PrevJoint - palmPosition).y);
		signData.Add("PinkyProximalLowerJointZ", (finger.Bone(Bone.BoneType.TYPE_PROXIMAL).PrevJoint - palmPosition).z);
		signData.Add("PinkyProximalUpperJointX", (finger.Bone(Bone.BoneType.TYPE_PROXIMAL).NextJoint - palmPosition).x);
		signData.Add("PinkyProximalUpperJointY", (finger.Bone(Bone.BoneType.TYPE_PROXIMAL).NextJoint - palmPosition).y);
		signData.Add("PinkyProximalUpperJointZ", (finger.Bone(Bone.BoneType.TYPE_PROXIMAL).NextJoint - palmPosition).z);
		signData.Add("PinkyProximalCenterX", (finger.Bone(Bone.BoneType.TYPE_PROXIMAL).Center - palmPosition).x);
		signData.Add("PinkyProximalCenterY", (finger.Bone(Bone.BoneType.TYPE_PROXIMAL).Center - palmPosition).y);
		signData.Add("PinkyProximalCenterZ", (finger.Bone(Bone.BoneType.TYPE_PROXIMAL).Center - palmPosition).z);
		signData.Add("PinkyProximalDirectionX", finger.Bone(Bone.BoneType.TYPE_PROXIMAL).Direction.x);
		signData.Add("PinkyProximalDirectionY", finger.Bone(Bone.BoneType.TYPE_PROXIMAL).Direction.y);
		signData.Add("PinkyProximalDirectionZ", finger.Bone(Bone.BoneType.TYPE_PROXIMAL).Direction.z);
		signData.Add("PinkyProximalLength", finger.Bone(Bone.BoneType.TYPE_PROXIMAL).Length);
		signData.Add("PinkyProximalWidth", finger.Bone(Bone.BoneType.TYPE_PROXIMAL).Width);
		localRot = (new Quaternion(finger.Bone(Bone.BoneType.TYPE_PROXIMAL).Rotation.x, finger.Bone(Bone.BoneType.TYPE_PROXIMAL).Rotation.y, finger.Bone(Bone.BoneType.TYPE_PROXIMAL).Rotation.z, finger.Bone(Bone.BoneType.TYPE_PROXIMAL).Rotation.w)) * Quaternion.Inverse(rotation);
		signData.Add("PinkyProximalRotationX", localRot.x);
		signData.Add("PinkyProximalRotationY", localRot.y);
		signData.Add("PinkyProximalRotationZ", localRot.z);
		signData.Add("PinkyProximalRotationW", localRot.w);

		signData.Add("PinkyIntermediateLowerJointX", (finger.Bone(Bone.BoneType.TYPE_INTERMEDIATE).PrevJoint - palmPosition).x);
		signData.Add("PinkyIntermediateLowerJointY", (finger.Bone(Bone.BoneType.TYPE_INTERMEDIATE).PrevJoint - palmPosition).y);
		signData.Add("PinkyIntermediateLowerJointZ", (finger.Bone(Bone.BoneType.TYPE_INTERMEDIATE).PrevJoint - palmPosition).z);
		signData.Add("PinkyIntermediateUpperJointX", (finger.Bone(Bone.BoneType.TYPE_INTERMEDIATE).NextJoint - palmPosition).x);
		signData.Add("PinkyIntermediateUpperJointY", (finger.Bone(Bone.BoneType.TYPE_INTERMEDIATE).NextJoint - palmPosition).y);
		signData.Add("PinkyIntermediateUpperJointZ", (finger.Bone(Bone.BoneType.TYPE_INTERMEDIATE).NextJoint - palmPosition).z);
		signData.Add("PinkyIntermediateCenterX", (finger.Bone(Bone.BoneType.TYPE_INTERMEDIATE).Center - palmPosition).x);
		signData.Add("PinkyIntermediateCenterY", (finger.Bone(Bone.BoneType.TYPE_INTERMEDIATE).Center - palmPosition).y);
		signData.Add("PinkyIntermediateCenterZ", (finger.Bone(Bone.BoneType.TYPE_INTERMEDIATE).Center - palmPosition).z);
		signData.Add("PinkyIntermediateDirectionX", finger.Bone(Bone.BoneType.TYPE_INTERMEDIATE).Direction.x);
		signData.Add("PinkyIntermediateDirectionY", finger.Bone(Bone.BoneType.TYPE_INTERMEDIATE).Direction.y);
		signData.Add("PinkyIntermediateDirectionZ", finger.Bone(Bone.BoneType.TYPE_INTERMEDIATE).Direction.z);
		signData.Add("PinkyIntermediateLength", finger.Bone(Bone.BoneType.TYPE_INTERMEDIATE).Length);
		signData.Add("PinkyIntermediateWidth", finger.Bone(Bone.BoneType.TYPE_INTERMEDIATE).Width);
		localRot = (new Quaternion(finger.Bone(Bone.BoneType.TYPE_INTERMEDIATE).Rotation.x, finger.Bone(Bone.BoneType.TYPE_INTERMEDIATE).Rotation.y, finger.Bone(Bone.BoneType.TYPE_INTERMEDIATE).Rotation.z, finger.Bone(Bone.BoneType.TYPE_INTERMEDIATE).Rotation.w)) * Quaternion.Inverse(rotation);
		signData.Add("PinkyIntermediateRotationX", localRot.x);
		signData.Add("PinkyIntermediateRotationY", localRot.y);
		signData.Add("PinkyIntermediateRotationZ", localRot.z);
		signData.Add("PinkyIntermediateRotationW", localRot.w);

		signData.Add("PinkyDistalLowerJointX", (finger.Bone(Bone.BoneType.TYPE_DISTAL).PrevJoint - palmPosition).x);
		signData.Add("PinkyDistalLowerJointY", (finger.Bone(Bone.BoneType.TYPE_DISTAL).PrevJoint - palmPosition).y);
		signData.Add("PinkyDistalLowerJointZ", (finger.Bone(Bone.BoneType.TYPE_DISTAL).PrevJoint - palmPosition).z);
		signData.Add("PinkyDistalUpperJointX", (finger.Bone(Bone.BoneType.TYPE_DISTAL).NextJoint - palmPosition).x);
		signData.Add("PinkyDistalUpperJointY", (finger.Bone(Bone.BoneType.TYPE_DISTAL).NextJoint - palmPosition).y);
		signData.Add("PinkyDistalUpperJointZ", (finger.Bone(Bone.BoneType.TYPE_DISTAL).NextJoint - palmPosition).z);
		signData.Add("PinkyDistalCenterX", (finger.Bone(Bone.BoneType.TYPE_DISTAL).Center - palmPosition).x);
		signData.Add("PinkyDistalCenterY", (finger.Bone(Bone.BoneType.TYPE_DISTAL).Center - palmPosition).y);
		signData.Add("PinkyDistalCenterZ", (finger.Bone(Bone.BoneType.TYPE_DISTAL).Center - palmPosition).z);
		signData.Add("PinkyDistalDirectionX", finger.Bone(Bone.BoneType.TYPE_DISTAL).Direction.x);
		signData.Add("PinkyDistalDirectionY", finger.Bone(Bone.BoneType.TYPE_DISTAL).Direction.y);
		signData.Add("PinkyDistalDirectionZ", finger.Bone(Bone.BoneType.TYPE_DISTAL).Direction.z);
		signData.Add("PinkyDistalLength", finger.Bone(Bone.BoneType.TYPE_DISTAL).Length);
		signData.Add("PinkyDistalWidth", finger.Bone(Bone.BoneType.TYPE_DISTAL).Width);
		localRot = (new Quaternion(finger.Bone(Bone.BoneType.TYPE_DISTAL).Rotation.x, finger.Bone(Bone.BoneType.TYPE_DISTAL).Rotation.y, finger.Bone(Bone.BoneType.TYPE_DISTAL).Rotation.z, finger.Bone(Bone.BoneType.TYPE_DISTAL).Rotation.w)) * Quaternion.Inverse(rotation);
		signData.Add("PinkyDistalRotationX", localRot.x);
		signData.Add("PinkyDistalRotationY", localRot.y);
		signData.Add("PinkyDistalRotationZ", localRot.z);
		signData.Add("PinkyDistalRotationW", localRot.w);



		return signData;
	}
}