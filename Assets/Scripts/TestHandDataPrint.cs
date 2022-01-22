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
}