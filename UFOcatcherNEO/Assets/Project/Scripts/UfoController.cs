using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UfoController : MonoBehaviour {
  Rigidbody rb;
  Vector3 Move;

  Color orangeColor=new Color(1.0f, 0.7533273f, 0.0f, 1.0f);

  [SerializeField]
  List<Image> arrowImages;

  Animator animator;

  [SerializeField]
  bool debugMode;

  int state;
  enum stateNum {
    idle,
    getDown,
    catching,
    standUp,
    goR,
    goB
  }

  [SerializeField]
  List<Text> cameraUI = new List<Text>();

  [SerializeField]
  GameObject cameraRoot;
  int cameraState;
  enum CameraStateNum {
    stop,
    front,
    right,
    left
  }

  // Start is called before the first frame update
  void Start() {
    rb = GetComponent<Rigidbody>();
    animator = GetComponent<Animator>();
    state = 0;
    cameraState = 0;
  }

  // Update is called once per frame
  void Update() {
    //色リセット
    for (int i = 0; i < 4; i++) {
      arrowImages[i].color = new Color(1f, 1f, 1f);
    }

    GetKey();
    RotateCam();
    
;  }

  void GetKey() {


    if (state == 0 && Input.GetKey("space")) {
      animator.SetTrigger("open");
      state = (int)stateNum.getDown;
      Invoke("StartCatch", 4f);
    }

    //→
    if ((Input.GetKey("right")&& state==0 )|| state == (int)stateNum.goR) {
      Move = new Vector3(5f, 0, 0);
      rb.velocity = Move;
      arrowImages[0].color = orangeColor;
    }
    //←
    else if (Input.GetKey("left") && state == 0) {
      Move = new Vector3(-5f, 0, 0);
      rb.velocity = Move;
      arrowImages[1].color = orangeColor;
    }
    //↑
    else if (Input.GetKey("up") && state == 0) {
      Move = new Vector3(0, 0, 5f);
      rb.velocity = Move;
      arrowImages[2].color = orangeColor;
    }
    //↓
    else if ((Input.GetKey("down") && state == 0) || state == (int)stateNum.goB) {
      Move = new Vector3(0, 0, -5f);
      rb.velocity = Move;
      arrowImages[3].color = orangeColor;
    }
    else if ((Input.GetKey("s")&&debugMode) || (state == (int)stateNum.getDown && transform.position.y > 10f)) {
      Move = new Vector3(0, -3f, 0);
      rb.velocity = Move;
    }
    else if ((Input.GetKey("w")&&debugMode )|| state == (int)stateNum.standUp) {
      Move = new Vector3(0, 3f, 0);
      rb.velocity = Move;
    }
    else {
      Move = new Vector3(0, 0, 0);
      rb.velocity = Move;
    }

    if (Input.GetKey("2")) {
      cameraState = (int)CameraStateNum.front;
      cameraUI[1].color = orangeColor;
      cameraUI[0].color = new Color(1f, 1f, 1f);
      cameraUI[2].color = new Color(1f, 1f, 1f);
    }
    else if (Input.GetKey("3")) {
      cameraState = (int)CameraStateNum.right;
      cameraUI[2].color = orangeColor;
      cameraUI[0].color = new Color(1f, 1f, 1f);
      cameraUI[1].color = new Color(1f, 1f, 1f);
    }
    else if (Input.GetKey("1")) {
      cameraState = (int)CameraStateNum.left;
      cameraUI[0].color = orangeColor;
      cameraUI[1].color = new Color(1f, 1f, 1f);
      cameraUI[2].color = new Color(1f, 1f, 1f);
    }
  }

  void RotateCam() {
    if (cameraState == (int)CameraStateNum.stop) {
      cameraRoot.transform.Rotate(new Vector3(0, 0, 0));
      return;
    }

    if (cameraState == (int)CameraStateNum.front) {
      if (cameraRoot.transform.rotation.y > -0.001f && cameraRoot.transform.rotation.y < 0.001f) {
        cameraState = (int)CameraStateNum.stop;
        Debug.Log("Stop" + cameraRoot.transform.rotation.y);
        return;
      }
      if (cameraRoot.transform.rotation.y > 0) {
        cameraRoot.transform.Rotate(new Vector3(0, -2f, 0));
      }
      else {
        cameraRoot.transform.Rotate(new Vector3(0, 2f, 0));
      }
      
    }
    else if (cameraState == (int)CameraStateNum.right) {
      if (cameraRoot.transform.rotation.y <= -0.7f) {
        cameraState = (int)CameraStateNum.stop;
        Debug.Log("Stop" + cameraRoot.transform.rotation.y);
        return;
      }
      cameraRoot.transform.Rotate(new Vector3(0, -2f, 0));
      
    }
    else{
      if (cameraRoot.transform.rotation.y >= 0.7f) {
        cameraState = (int)CameraStateNum.stop;
        Debug.Log("Stop" + cameraRoot.transform.rotation.y);
        return;
      }
      cameraRoot.transform.Rotate(new Vector3(0, 2f, 0));
    }
  }


  //　スペースキー押す
  //  ↓↓↓
  //  UFO降りる
  //  ↓↓↓
  //  キャッチ
  void StartCatch() {
    animator.SetTrigger("close");
    Invoke("StartStandup", 2f);
  }
  //  ↓↓↓
  //  UFO上げる
  void StartStandup() {
    state = (int)stateNum.standUp;
    Invoke("setGoR", 3f);
  }
  //  ↓↓↓
  //  UFO右に
  void setGoR() {
    state = (int)stateNum.goR;
  }
  //  ↓↓↓
  //  右端についたら
  public void setGoB() {
    state = (int)stateNum.goB;
  }
  //  ↓↓↓
  //  手前についたら
  public void End() {
    animator.SetTrigger("open");
    Invoke("setIdle", 2f);
  }
  //  ↓↓↓
  //  終了
  void setIdle() {
    animator.SetTrigger("close");
    state = (int)stateNum.idle;
  }


  void OnCollisionStay(Collision collision) {
    if (collision.gameObject.name == "GuideR" && state==(int)stateNum.goR) {
      setGoB();
    }
    else if (collision.gameObject.name == "GuideEnd" && state == (int)stateNum.goB) {
      state = -1;
      End();
    }
  }
}
