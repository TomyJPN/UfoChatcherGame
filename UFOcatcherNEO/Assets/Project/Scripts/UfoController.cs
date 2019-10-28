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

  int state;
  enum stateNum {
    idle,
    getDown,
    catching,
    standUp,
    goR,
    goB
  }

  // Start is called before the first frame update
  void Start() {
    rb = GetComponent<Rigidbody>();
    animator = GetComponent<Animator>();
    state = 0;
  }

  // Update is called once per frame
  void Update() {
    //色リセット
    for (int i = 0; i < 4; i++) {
      arrowImages[i].color = new Color(1f, 1f, 1f);
    }

    if(state==0 && Input.GetKey("space")) {
      animator.SetTrigger("open");
      state = (int)stateNum.getDown;
      Invoke("StartCatch", 4f);
    }

    //→
    if (Input.GetKey("right")|| state == (int)stateNum.goR) {
      Move = new Vector3(3f, 0, 0);
      rb.velocity = Move;
      arrowImages[0].color = orangeColor;
    }
    //←
    else if (Input.GetKey("left")) {
      Move = new Vector3(-3f, 0, 0);
      rb.velocity = Move;
      arrowImages[1].color = orangeColor;
    }
    //↑
    else if (Input.GetKey("up")) {
      Move = new Vector3(0, 0, 3f);
      rb.velocity = Move;
      arrowImages[2].color = orangeColor;
    }
    //↓
    else if (Input.GetKey("down") ||state == (int)stateNum.goB){
      Move = new Vector3(0, 0, -3f);
      rb.velocity = Move;
      arrowImages[3].color = orangeColor;
    }
    else if (Input.GetKey("s") || (state == (int)stateNum.getDown && transform.position.y>10f)) {
      Move = new Vector3(0, -3f, 0);
      rb.velocity = Move;
    }
    else if (Input.GetKey("w") || state==(int)stateNum.standUp) {
      Move = new Vector3(0, 3f, 0);
      rb.velocity = Move;
    }
    else {
      Move = new Vector3(0, 0, 0);
      rb.velocity = Move;
      
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


  void OnCollisionEnter(Collision collision) {
    if (collision.gameObject.name == "GuideR" && state==(int)stateNum.goR) {
      setGoB();
    }
    else if (collision.gameObject.name == "GuideEnd" && state == (int)stateNum.goB) {
      End();
    }
  }
}
