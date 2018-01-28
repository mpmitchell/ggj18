using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartScreen : MonoBehaviour {

  public GameObject image1;
  public GameObject image2;
  public GameObject image3;
  public GameObject image4;

  int stage = 0;

  void Update() {
    if (Input.anyKeyDown) {
      if (stage == 0) {
        image1.SetActive(false);
        image2.SetActive(true);
        stage = 1;
      } else if (stage == 1) {
        image2.SetActive(false);
        image3.SetActive(true);
        stage = 2;
      } else if (stage == 2) {
        image3.SetActive(false);
        image4.SetActive(true);
        stage = 3;
      } else if (stage == 3) {
        SceneManager.LoadScene(2);
      }
    }
  }
}
