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
		if (!File.Exists("data.txt"))
		{
			FileStream fileStreamInit = new FileStream(@"data.txt", FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.None);
			string headerText = "HandId,HandConfidence,HandPalmNormalPitch,HandPalmNormalRoll,"
			 + "HandPalmDirectionRoll,HandPalmPosition,HandPalmVelocity,HandPalmNormal,"
			 + "HandDirection,HandRotation,GrabStrength,GrabAngle,PinchDistance,PinchStrength,"
			 + "PalmWidth,StabilizedPalmPosition,TimeVisible,HandWristPosition\n";
			File.AppendAllText("data.txt", headerText);
		}

		string handData = "" + hand.Id + "," + hand.Confidence + "," + hand.PalmNormal.Pitch
		+ "," + hand.PalmNormal.Roll + "," + hand.PalmNormal.Yaw + "," + hand.Direction.Roll
		+ "," + hand.PalmPosition + "," + hand.PalmVelocity + "," + hand.PalmNormal + ","
		+ hand.Direction + "," + hand.Rotation + "," + hand.GrabStrength + "," + hand.GrabAngle
		+ "," + hand.PinchDistance + "," + hand.PinchStrength + "," + hand.PalmWidth + ","
		+ hand.StabilizedPalmPosition + "," + hand.TimeVisible + "," + hand.WristPosition + "\n";
		File.AppendAllText("data.txt", handData);
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

	Dictionary<string, object> GenerateSignData(Hand hand)
	{
		Dictionary<string, object> signData = new Dictionary<string, object>();

		//Hand Data

		signData.Add("HandId", hand.Id);
		signData.Add("HandConfidence", hand.Confidence);

		signData.Add("HandPalmPitch", hand.PalmNormal.Pitch);
		signData.Add("HandPalmRoll", hand.PalmNormal.Roll);
		signData.Add("HandPalmYaw", hand.PalmNormal.Yaw);
		signData.Add("HandPalmDirectionRoll", hand.Direction.Roll);

		signData.Add("HandPalmPosition", hand.PalmPosition);
		signData.Add("HandPalmVelocity", hand.PalmVelocity);
		signData.Add("HandPalmNormal", hand.PalmNormal);
		signData.Add("HandFingersDirection", hand.Direction);

		signData.Add("HandRotation", hand.Rotation);

		signData.Add("GrabStrength", hand.GrabStrength);
		signData.Add("GrabAngle", hand.GrabAngle);

		signData.Add("PinchDistance", hand.PinchDistance);
		signData.Add("PinchStrength", hand.PinchStrength);

		signData.Add("PalmWidth", hand.PalmWidth);

		signData.Add("StabilizedPalmPosition", hand.StabilizedPalmPosition);

		signData.Add("TimeVisible", hand.TimeVisible);

		signData.Add("HandWristPosition", hand.WristPosition);

		for (int digit = 0; digit < 5; digit++)
		{
			Finger finger = hand.Fingers[digit];

			signData.Add("FingerId", finger.Id);
			signData.Add("FingerType", finger.Type);
			signData.Add("HandId", finger.HandId);

			signData.Add("TipPosition", finger.TipPosition);
			signData.Add("Direction", finger.Direction);

			signData.Add("Width", finger.Width);
			signData.Add("Length", finger.Length);

			signData.Add("IsExtended", finger.IsExtended);
			signData.Add("TimeVisible", finger.TimeVisible);

			// for (Bone.BoneType boneIndex in ){
			// 	Bone bone = finger.Bone(boneIndex);
			// }
		}

		return signData;
	}
}