using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leap;
using Leap.Unity;

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

      for (int i = 0; i < frame.Hands.Count; i++)
      {
        string handName = hands[i].IsRight ? "Right Hand" : "Left Hand";
        PrintHandData(hands[i], handName);
      }
    }
  }

  void PrintHandData(Hand hand, string handName)
  {
    float confidence = hand.Confidence;

    float HandPalmPitch = hand.PalmNormal.Pitch;
    float HandPalmRoll = hand.PalmNormal.Roll;
    float HandPalmYaw = hand.PalmNormal.Yaw;
    float HandPalmDirectionRoll = hand.Direction.Roll;

    Vector HandPalmPosition = hand.PalmPosition;
    Vector HandPalmNormal = hand.PalmNormal;
    Vector HandFingersDirection = hand.Direction;


    float pinch = hand.PinchStrength;
    Vector HandWristPos = hand.WristPosition;

    Debug.Log(handName + " Confidence: " + confidence);
    Debug.Log(handName + " Pitch: " + HandPalmPitch);
    Debug.Log(handName + " Roll: " + HandPalmRoll);
    Debug.Log(handName + " Yaw: " + HandPalmYaw);
    Debug.Log(handName + " PalmDirectionRoll: " + HandPalmDirectionRoll);

    Debug.Log(handName + " PalmPosition: " + HandPalmPosition);
    Debug.Log(handName + " PalmNormal: " + HandPalmNormal);
    Debug.Log(handName + " FingersDirection: " + HandFingersDirection);

    Debug.Log(handName + " Pinch: " + pinch);
    Debug.Log(handName + " WristPos: " + HandWristPos);
  }
}