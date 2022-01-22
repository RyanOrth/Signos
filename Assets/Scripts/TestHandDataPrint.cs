using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leap;
using Leap.Unity;

public class TestHandDataPrint : MonoBehaviour
{
  Controller controller;
  float HandPalmPitch;
  float HandPalmYaw;
  float HandPalmRoll;
  float HandWristRot;
  float HandDirectionPitch;
  float pinch;
  float HandPalmDirectionRoll;
  float LeftHand;
  float RightHand;
  float DistanceBetweenHands;
  Vector VectorHandPosRight;
  Vector VectorHandPosLeft;

  // Start is called before the first frame update
  void Start()
  {

  }

  // Update is called once per frame
  void Update()
  {
    if (Input.GetKeyDown(KeyCode.Space))
    {
      controller = new Controller();
      Frame frame = controller.Frame();
      List<Hand> hands = frame.Hands;

      if (frame.Hands.Count > 0)
      {
        Hand firstHand = hands[0];
        Hand secondHand = hands[1];
      }

      HandPalmPitch = hands[0].PalmNormal.Pitch;
      HandPalmRoll = hands[0].PalmNormal.Roll;
      HandPalmYaw = hands[0].PalmNormal.Yaw;

      VectorHandPosRight = hands[0].PalmPosition; //position right hand
      VectorHandPosLeft = hands[1].PalmPosition; // position left hand

      DistanceBetweenHands = VectorHandPosLeft.DistanceTo(VectorHandPosRight); //distance position between hands as float

      RightHand = hands[0].PalmNormal.Roll; //clap right hand
      LeftHand = hands[1].PalmNormal.Roll; // clap left hand

      HandPalmDirectionRoll = hands[0].Direction.Roll; //stroke

      pinch = hands[0].PinchStrength; //turn radio on


      Debug.Log("RightHand: " + RightHand);
      Debug.Log("LeftHand: " + LeftHand);
      Debug.Log("Pitch: " + HandPalmPitch);
      Debug.Log("Roll: " + HandPalmRoll);
      Debug.Log("Yaw: " + HandPalmYaw);
      Debug.Log("Wrist: " + HandWristRot);
      Debug.Log("Pinch: " + pinch);
      Debug.Log("PalmdirectionRoll: " + HandPalmDirectionRoll);
      Debug.Log("RightHandPos: " + VectorHandPosRight);
      Debug.Log("LeftHandPos: " + VectorHandPosLeft);
      Debug.Log("DistanceBetween: " + DistanceBetweenHands);
    }
  }
}