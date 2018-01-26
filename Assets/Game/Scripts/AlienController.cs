using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackerController : MonoBehaviour {

	public float speed;
  public float rotationalSpeed;

  Renderer renderer;

  void Start() {
    renderer = GetComponent<Renderer>();
  }

  void Update() {
    float horiztontal = -Input.GetAxis("AlienHorizontal") * speed * Time.deltaTime;
    float vertical = -Input.GetAxis("AlienVertical") * speed * Time.deltaTime;
    float rotate = Input.GetAxis("AlienRotation") * rotationalSpeed * Time.deltaTime;

    transform.Translate(new Vector3(horiztontal, vertical, 0f), Space.World);
    transform.Rotate(rotate * Vector3.forward);

    if (horiztontal != 0f || vertical != 0f) {
      renderer.enabled = false;
    } else {
      renderer.enabled = true;
    }
  }
}
