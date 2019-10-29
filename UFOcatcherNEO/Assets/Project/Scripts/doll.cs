using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class doll : MonoBehaviour {
  Manager manager;

  private void Start() {
    manager = GameObject.Find("Manager").GetComponent<Manager>();
  }

  private void OnTriggerEnter(Collider other) {
    Debug.Log(other.gameObject.name+"と衝突");
    if (other.gameObject.name == "goal") {
      manager.GetDoll();
    }
  }
}
