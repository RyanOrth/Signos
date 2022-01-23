using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leap.Unity;
using Leap;
using TMPro;



public class CheckLetter : MonoBehaviour
{
	public LeapServiceProvider leapProvider;
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
					textBox.text = LetterAConfidence(rightHand).ToString();
				break;
			case Handedness.Left:
				if (leftHand != null)
					textBox.text = LetterAConfidence(leftHand).ToString();
				break;
		}

		// if (Input.GetKeyDown(KeyCode.KeypadEnter))
		// print("A confd: " + LetterAConfidence(rightHand).ToString());



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


	// float LetterCConfidence(Hand hand)
	// {
	// 	List<Finger> fingers = hand.Fingers;
	// 	float[] tipToKnuckle = new float[4];
	// 	float[][] 
	// 	for (int digit = 1; digit < tipToKnuckle.Length; digit++)
	// 	{
	// 		tipToKnuckle[digit - 1] = fingers[digit].TipPosition.DistanceTo(fingers[digit].Bone(Bone.BoneType.TYPE_METACARPAL).NextJoint);
	// 	}

	// }

}
