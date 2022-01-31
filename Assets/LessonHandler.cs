using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leap.Unity;
using Leap;
using TMPro;

using Newtonsoft.Json;
using System.IO;
using UnityEngine.UI;

public class LessonHandler : MonoBehaviour
{
	public LeapServiceProvider leapProvider;
	Frame current;
	List<Hand> hands;
	Hand rightHand;
	Hand leftHand;
	public Slider slider;
	public Animator animator;
	public GameObject renderingSetup;
	public GameObject completedPanel;
	public GameObject textBox;
	public bool speedMode = false;
	private bool correctSign;
	private float totalTimeCorrect = 0;

	public enum Lesson
	{
		A,
		B,
		C,
		D,
		E,
		Completed,
	};

	public Lesson currentLesson = Lesson.A;
	public float lessonTolerance = 85f;

	public enum Handedness
	{
		Right,
		Left,
	};
	public Handedness handedness;
	// Start is called before the first frame update
	void Start()
	{
		textBox.GetComponent<Text>().text = currentLesson.ToString();
		// textBox = GetComponent<TMPro.TextMeshPro>();
		// print(textBox.text);
	}

	// Update is called once per frame
	void Update()
	{
		if (!leapProvider.IsConnected()) return;
		current = leapProvider.CurrentFrame;
		hands = current.Hands;
		switch (current.Hands.Count)
		{
			case 0:
				return;
			case 1:
				if (hands[0].IsRight)
					rightHand = hands[0];
				else
					leftHand = hands[0];
				break;
			case 2:
				if (hands[0].IsRight)
				{
					rightHand = hands[0];
					leftHand = hands[1];
				}
				else
				{
					leftHand = hands[0];
					rightHand = hands[1];
				}
				break;
			default:
				throw new System.Exception("Bad Hand Count");
		}


		switch (currentLesson)
		{
			case Lesson.A:
				if (rightHand != null)
				{
					slider.value = LetterAConfidence(rightHand) * 100;
					// lessonTolerance = 75f;
				}
				// if (leftHand != null)
				// {
				//     slider.value = Confidence(leftHand, "A-Left") * 100;
				// }

				break;
			case Lesson.B:
				if (rightHand != null)
				{
					slider.value = LetterBConfidence(rightHand) * 100;
					// lessonTolerance = 75f;
				}
				// if (leftHand != null)
				// {
				//     slider.value = Confidence(leftHand, "B-Left") * 100;
				// }
				break;
			case Lesson.C:
				if (rightHand != null)
				{
					slider.value = Confidence(rightHand, "C-Right") * 100;
					// lessonTolerance = 60f;
				}
				/*if (leftHand != null)
				{
					slider.value = Confidence(leftHand, "C-Left") * 100;
				}*/
				break;
			case Lesson.D:
				if (rightHand != null)
				{
					slider.value = Confidence(rightHand, "D-Right") * 100;
					// lessonTolerance = 25f;
				}
				/*if (leftHand != null)
				{
					slider.value = Confidence(leftHand, "D-Left") * 100;
				}*/
				break;
			case Lesson.E:
				if (rightHand != null)
				{
					slider.value = Confidence(rightHand, "E-Right") * 100;
					// lessonTolerance = 50f;
				}
				/*if (leftHand != null)
				{
					slider.value = Confidence(leftHand, "E-Left") * 100;
				}*/
				break;
			case Lesson.Completed:
			default:
				completedPanel.SetActive(true);
				break;
		}

		if (slider.value > lessonTolerance && correctSign)
		{
			totalTimeCorrect += Time.deltaTime;
		}
		else if (slider.value > lessonTolerance)
		{
			correctSign = true;
		}
		else
		{
			// totalTimeCorrect = 0;
			correctSign = false;
		}

		if (totalTimeCorrect > 1 && !speedMode)
		{
			currentLesson++;
			totalTimeCorrect = 0;
			slider.value = 0;
			animator.SetInteger("number of lessons", currentLesson.indexOf());
			//          renderingSetup.SetActive(false);
			// renderingSetup.SetActive(true);

		}
		else if (correctSign && speedMode)
		{
			currentLesson++;
			slider.value = 0;
			animator.SetInteger("number of lessons", currentLesson.indexOf());
		}
		textBox.GetComponent<Text>().text = currentLesson.ToString();
		// if (Input.GetKeyDown(KeyCode.A))
		// {
		// 	print("A confd: " + LetterAConfidence(rightHand).ToString());
		// 	print("B confd: " + LetterBConfidence(rightHand).ToString());
		// }




	}

	float LetterAConfidence(Hand hand)
	{
		List<Finger> fingers = hand.Fingers;
		Vector palmPosition = hand.PalmPosition;
		float[] fingerPalmDelta = new float[4];
		float totalScore = 0f, thumbScore, thumbVectorScore;
		for (int digit = 1; digit < 5; digit++)
		{
			fingerPalmDelta[digit - 1] = palmPosition.DistanceTo(fingers[digit].TipPosition);
		}
		thumbScore = fingers[0].TipPosition.DistanceTo(fingers[1].Bone(Bone.BoneType.TYPE_PROXIMAL).Center);

		thumbVectorScore = hand.Direction.Normalized.Cross(fingers[0].Bone(Bone.BoneType.TYPE_DISTAL).Direction).Magnitude;

		for (int i = 0; i < fingerPalmDelta.Length; i++)
		{
			totalScore += fingerPalmDelta[i];
		}
		totalScore += thumbScore + 0.2f * thumbVectorScore;
		// print("Total Score: " + totalScore.ToString());
		return 1 - (totalScore - 0.22f) / (0.67f - 0.22f);
		// return totalScore;
	}

	float LetterBConfidence(Hand hand)
	{
		List<Finger> fingers = hand.Fingers;
		float totalScore = 0f;
		float fingersStraight = 0f;
		float fingersTogether = 0f;

		for (int digit = 1; digit < 5; digit++)
		{
			fingersStraight = fingers[digit - 1].Bone(Bone.BoneType.TYPE_DISTAL).Direction.Dot(fingers[digit - 1].Bone(Bone.BoneType.TYPE_METACARPAL).Direction);
		}
		fingersStraight = (fingersStraight - -0.85f) / (1f - -0.85f);

		for (int digit = 2; digit < 5; digit++)
		{
			fingersTogether = fingers[digit - 1].Bone(Bone.BoneType.TYPE_DISTAL).Direction.Cross(fingers[digit].Bone(Bone.BoneType.TYPE_DISTAL).Direction).Magnitude;
		}
		fingersTogether = 1 - (fingersTogether - 0.03f) / (0.5f - 0.03f);

		Vector thumbVector = hand.Direction.Normalized.Cross(fingers[0].Bone(Bone.BoneType.TYPE_DISTAL).Direction);
		float thumbVectorScore = hand.PalmNormal.Dot(thumbVector);  //Score for thumb across palm
		thumbVectorScore = 1 - (thumbVectorScore - -0.82f) / (0.80f - -0.82f);

		totalScore += 0.50f * fingersStraight;
		totalScore += 0.15f * fingersTogether;
		totalScore += 0.35f * thumbVectorScore;
		return totalScore;
	}

	Dictionary<string, float> LoadJson(string letter)
	{
		Dictionary<string, Dictionary<string, float>> data;
		using (StreamReader r = new StreamReader("Assets/Resources/Letters.json"))
		{
			string json = r.ReadToEnd();
			data = JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, float>>>(json);
		}
		return data[letter];
	}

	public float floatTolerance = 0.1f;
	float Confidence(Hand hand, string letter)
	{
		int positiveMatchScore = 0;
		int negativeMatchScore = 0;
		float recordedFloat;

		Dictionary<string, float> confidenceData = LoadJson(letter);
		Dictionary<string, float> recordedData = GenerateSignData(hand);
		foreach (var key in confidenceData.Keys)
		{
			float confidenceFloat = confidenceData[key];
			try
			{
				recordedFloat = recordedData[key];
			}
			catch
			{
				throw new System.Exception(key);
			}


			if (confidenceFloat == 0f)
			{
				if (recordedFloat == 0f)
				{
					positiveMatchScore++;
				}
				else
				{
					negativeMatchScore++;
				}
			}
			else if (confidenceFloat == 1f)
			{
				if (recordedFloat == 1f)
				{
					positiveMatchScore++;
				}
				else
				{
					negativeMatchScore++;
				}
			}
			else if (confidenceFloat <= (recordedFloat * (1 + floatTolerance)) && (recordedFloat * (1 - floatTolerance)) <= confidenceFloat)
			{
				positiveMatchScore++;
			}
			else
			{
				negativeMatchScore++;
			}
		}

		float result = 0f;
		switch (letter)
		{
			//(positiveMatchScore - min) / (max - min)
			case "C-Right":
				result = (positiveMatchScore - 20f) / (35f - 20f);
				break;
			case "D-Right":
				result = (positiveMatchScore - 20f) / (28f - 20f);
				break;
			case "E-Right":
				result = (positiveMatchScore - 20f) / (30f - 20f);
				break;
		}
		return (result <= 1f && result >= 0) ? result : (result > 0) ? 1f : 0f;
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

		signData.Add("HandPalmNormalPitch", hand.PalmNormal.Pitch);
		signData.Add("HandPalmNormalRoll", hand.PalmNormal.Roll);
		signData.Add("HandPalmNormalYaw", hand.PalmNormal.Yaw);
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

		signData.Add("HandDirectionX", hand.Direction.x);
		signData.Add("HandDirectionY", hand.Direction.y);
		signData.Add("HandDirectionZ", hand.Direction.z);

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
