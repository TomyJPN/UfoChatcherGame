using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MotorController : MonoBehaviour {
  //先ほど作成したクラス
  public SerialHandler serialHandler;

  [SerializeField]
  Slider testSlider;

  bool isMoving;
  int angle;

  void Start() {
    //信号を受信したときに、そのメッセージの処理を行う
    serialHandler.OnDataReceived += OnDataReceived;
    serialHandler.Write("0" + "\0");
    isMoving = false;
    angle = 0;
  }

  void Update() {
    if (isMoving) {
      angle += 2;
      Debug.Log("serial送信：" + ((int)angle).ToString());
      serialHandler.Write(((int)angle).ToString() + "\0");
    }
    if (angle > 160) {
      isMoving = false;
      angle = 0;
      Debug.Log("serial送信：" + ((int)angle).ToString());
      serialHandler.Write(((int)angle).ToString() + "\0");
    }
  }

  public void SliderUpdate() {
    //文字列を送信
    Debug.Log("serial送信："+ ((int)testSlider.value).ToString());
    serialHandler.Write(((int)testSlider.value).ToString()+"\0");
  }

  public void onLed() {
    //文字列を送信
    Debug.Log("LED ON");
    serialHandler.Write("o");
  }

  public void offLed() {
    //文字列を送信
    Debug.Log("LED OFF");
    serialHandler.Write("p");
  }

  //受信した信号(message)に対する処理
  void OnDataReceived(string message) {
    var data = message.Split(
            new string[] { "\t" }, System.StringSplitOptions.None);
    if (data.Length < 2) return;

    try {
           //...
        }
    catch (System.Exception e) {
      Debug.LogWarning(e.Message);
    }
  }

  public void MoveMotor() {
    isMoving = true;
  }

  public void Close() {
    serialHandler.Close();
  }
}