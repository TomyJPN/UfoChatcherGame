using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class goal : MonoBehaviour {
  Manager manager;

  private void Start() {
    manager = GameObject.Find("Manager").GetComponent<Manager>();
  }

  private void OnTriggerEnter(Collider other) {
    if (other.transform.tag=="doll") {
      manager.GetDoll();
      Destroy(other.transform.root.gameObject);
    }
    else if (other.transform.tag == "gem") {
      manager.GetGem();
      Destroy(other.gameObject);
    }
  }
}
