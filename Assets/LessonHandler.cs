using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leap.Unity;
using Leap;
using TMPro;

using Newtonsoft.Json;
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
                }
                // if (leftHand != null)
                // {
                //     slider.value = LetterAConfidence(leftHand) * 100;
                // }

                break;
			case Lesson.B:
                if (rightHand != null)
                {
                    slider.value = LetterBConfidence(rightHand) * 100;
                }
                // else if (leftHand != null)
                // {
                //     slider.value = LetterBConfidence(leftHand) * 100;
                // }
				break;
			case Lesson.C:
			case Lesson.D:
			case Lesson.E:
			case Lesson.Completed:
            default:
                //open panel thingy
				break;
        }

        if (slider.value > 85.0f && correctSign)
        {
            totalTimeCorrect += Time.deltaTime;
        }
        else if (slider.value > 85.0f)
        {
            correctSign = true;
        }
        else
        {
            totalTimeCorrect = 0;
            correctSign = false;
        }

		if ( totalTimeCorrect > 3 && !speedMode)
        {
            currentLesson++;
            totalTimeCorrect = 0;
            slider.value = 0;
			animator.SetInteger("Lesson", currentLesson.indexOf());
            renderingSetup.SetActive(false);
			renderingSetup.SetActive(true);

        } else if (correctSign && speedMode)
        {
            currentLesson++;
            slider.value = 0;
            animator.SetInteger("Lesson", currentLesson.indexOf());
		}

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
}