using UnityEngine;

public class AlienController : MonoBehaviour {

	public float speed;
  public float rotationalSpeed;
  public AlienSpawner spawner;
  public GameObject rocketPrefab;
  public GameObject radiowavePrefab;
  public GameObject laserPrefab;
  public Transform rocketSpawn;
  public Transform radiowaveSpawn;
  public Transform laserSpawn;

  Renderer renderer;

  void Start() {
    renderer = GetComponent<Renderer>();
  }

  void Update() {
    float horiztontal = -Input.GetAxis("AlienHorizontal") * speed * Time.deltaTime;
    float vertical = -Input.GetAxis("AlienVertical") * speed * Time.deltaTime;
    float rotate = Input.GetAxis("AlienRotation") * rotationalSpeed * Time.deltaTime;
    bool fire = Input.GetButtonDown("AlienFire");

    transform.Translate(new Vector3(horiztontal, vertical, 0f), Space.World);
    transform.Rotate(rotate * Vector3.forward);

    if ((horiztontal != 0f || vertical != 0f) && !fire) {
      renderer.enabled = false;
    } else {
      renderer.enabled = true;
    }

    if (fire) {
      if (Physics.Raycast(rocketSpawn.position, rocketSpawn.right, Mathf.Infinity, LayerMask.GetMask("Planet"))) {
        Debug.Log("Fire Rocket");
      } else if (Physics.Raycast(radiowaveSpawn.position, radiowaveSpawn.right, Mathf.Infinity, LayerMask.GetMask("Planet"))) {
        Debug.Log("Fire Radiowave");
      } else if (Physics.Raycast(laserSpawn.position, laserSpawn.right, Mathf.Infinity, LayerMask.GetMask("Planet"))) {
        Instantiate(laserPrefab, laserSpawn.position, laserSpawn.rotation);
      }
    }
  }

  void OnCollisionEnter(Collision collision) {
    if (collision.other.tag == "Planet") {
      spawner.DestroyCurrent();
    }
  }
}
