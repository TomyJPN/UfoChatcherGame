using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MotorController : MonoBehaviour {
  //先ほど作成したクラス
  public SerialHandler serialHandler;

  [SerializeField]
  Slider testSlider;

  void Start() {
    //信号を受信したときに、そのメッセージの処理を行う
    serialHandler.OnDataReceived += OnDataReceived;
  }

  void Update() {
    
  }

  public void SliderUpdate() {
    //文字列を送信
    Debug.Log("serial送信："+ ((int)testSlider.value).ToString());
    serialHandler.Write(((int)testSlider.value).ToString()+"\0");
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
}