using UnityEngine;

public class Audio : MonoBehaviour {

	public static AudioSource source;

  void Start() {
    source = GetComponent<AudioSource>();
  }
}
