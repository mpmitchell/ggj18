using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScreen : MonoBehaviour {

  void Update() {
    if (Input.anyKeyDown) {
      SceneManager.LoadScene(2);
    }
  }
}
