using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackerController : MonoBehaviour {

	public float speed = 90f;

  Renderer renderer;

  void Start() {
    renderer = GetComponent<Renderer>();
  }

  void Update() {
    float horiztontal = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
    float vertical = Input.GetAxis("Vertical") * speed * Time.deltaTime;

    transform.Translate(new Vector3(horiztontal, vertical, 0f));

    if (horiztontal != 0f || vertical != 0f) {
      renderer.enabled = false;
    } else {
      renderer.enabled = true;
    }
  }
}
