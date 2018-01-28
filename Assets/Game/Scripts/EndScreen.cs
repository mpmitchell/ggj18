using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndScreen : MonoBehaviour {

  public static bool sos = false;

  public GameObject humanityWins;
  public GameObject aliensWins;

  void Start() {
    if (sos) {
      humanityWins.SetActive(true);
    } else {
      aliensWins.SetActive(true);
    }
  }

  void Update() {
    if (Input.GetKeyDown("escape")) {
      Application.Quit();
    } else if (Input.anyKeyDown) {
      SceneManager.LoadScene(1);
    }
  }
}
