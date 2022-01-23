using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leap.Unity;
using Leap;
using TMPro;
using Newtonsoft.Json;
using System.IO;


public class CheckLetter : MonoBehaviour
{
	public LeapServiceProvider leapProvider;
	public TestHandDataPrint dataPrinter;
	Frame current;
	List<Hand> hands;
	Hand rightHand;
	Hand leftHand;
	public TMP_Text textBox;
	public enum Handedness
	{
		Right,
		Left,
	};
	public Handedness handedness;
	// Start is called before the first frame update
	void Start()
	{
		// textBox = GetComponent<TMPro.TextMeshPro>();
		print(textBox.text);
		foreach (KeyValuePair<string, float> item in LoadJson("C-Right"))
		{
			print(item.Key + " = " + item.Value);
		}


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

		switch (handedness)
		{
			case Handedness.Right:
				if (rightHand != null)
				{
					//textBox.text = "" + Confidence(rightHand, "C-Right");
				}
				// textBox.text = (new Quaternion(rightHand.Rotation.x, rightHand.Rotation.y, rightHand.Rotation.z, rightHand.Rotation.w)).eulerAngles + "\n"
				// + (new Quaternion(rightHand.Fingers[1].Bone(Bone.BoneType.TYPE_PROXIMAL).Rotation.x,
				// 									rightHand.Fingers[1].Bone(Bone.BoneType.TYPE_PROXIMAL).Rotation.y,
				// 									rightHand.Fingers[1].Bone(Bone.BoneType.TYPE_PROXIMAL).Rotation.z,
				// 									rightHand.Fingers[1].Bone(Bone.BoneType.TYPE_PROXIMAL).Rotation.w)).eulerAngles
				// + "\n" + ((new Quaternion(rightHand.Rotation.x, rightHand.Rotation.y, rightHand.Rotation.z, rightHand.Rotation.w)) *
				// Quaternion.Inverse((new Quaternion(rightHand.Fingers[1].Bone(Bone.BoneType.TYPE_PROXIMAL).Rotation.x,
				// 									rightHand.Fingers[1].Bone(Bone.BoneType.TYPE_PROXIMAL).Rotation.y,
				// 									rightHand.Fingers[1].Bone(Bone.BoneType.TYPE_PROXIMAL).Rotation.z,
				// 									rightHand.Fingers[1].Bone(Bone.BoneType.TYPE_PROXIMAL).Rotation.w)))).eulerAngles;


				break;
			case Handedness.Left:
				//if (leftHand != null)
				//textBox.text = LetterAConfidence(leftHand).ToString();
				break;
			default:
				break;
		}
		if (Input.GetKeyDown(KeyCode.A))
		{
			print("A confd: " + LetterAConfidence(rightHand).ToString());
			print("B confd: " + LetterBConfidence(rightHand).ToString());
		}




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

	public float floatTolerance = 0.1f;
	public float max = 40f;
	public float min = 10f;

	float Confidence(Hand hand, string letter)
	{
		int positiveMatchScore = 0;
		int negativeMatchScore = 0;
		float recordedFloat;

		Dictionary<string, float> confidenceData = LoadJson(letter);
		Dictionary<string, float> recordedData = dataPrinter.GenerateSignData(hand);
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
		switch (letter)
		{

			case "C-Right":
				return (positiveMatchScore - min) / (max - min);
			case "D-Right":
				return (positiveMatchScore - 15) / (40 - 15);
			case "E-Right":
				return (positiveMatchScore - 10) / (10 - 10);
		}

	}

}
