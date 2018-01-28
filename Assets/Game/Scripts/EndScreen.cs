using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndScreen : MonoBehaviour {

  public static bool sos = false;

  public Text text;

  void Start() {
    if (sos) {
      text.text = "S.O.S. Successfull!";
    } else {
      text.text = "S.O.S. Failed!";
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
