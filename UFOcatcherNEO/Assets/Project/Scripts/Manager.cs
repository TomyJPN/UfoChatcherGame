using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Manager : MonoBehaviour {
  int point;

  [SerializeField]
  GameObject clearUI;

  AudioSource sound_get;
  AudioSource sound_miniget;

  [SerializeField]
  MotorController motor;

  bool leding;  //Lチカチカ

  void Start() {
    AudioSource[] audioSources = GetComponents<AudioSource>();
    sound_get = audioSources[1];
    sound_miniget = audioSources[2];
  }

  void Update() {

  }

  public void GetDoll() {
    Debug.Log("ユニティちゃん");
    clearUI.SetActive(true);  //再生
    Invoke("unableClearUI", 3f);
    sound_get.PlayOneShot(sound_get.clip);
    leding = true;
    LED1();
    motor.MoveMotor();
    Invoke("off", 5f);
  }

  public void GetGem() {
    Debug.Log("じぇむ");
    sound_miniget.PlayOneShot(sound_miniget.clip);
  }

  void unableClearUI() {
    clearUI.SetActive(false);
  }

  void LED1() {
    if (!leding) return;
    motor.onLed();
    Invoke("LED2", 0.25f);
  }
  void LED2() {
    if (!leding) return;
    motor.offLed();
    Invoke("LED1", 0.25f);
  }

  void off() {
    motor.offLed();
    leding = false;
  }

  public void LoadTitle() {
    motor.Close();
    SceneManager.LoadScene("Title");
  }
}
