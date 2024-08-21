using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Door : MonoBehaviour {
	public GameObject Text;
	public AudioSource audioSource;
	public AudioClip clipOpen, clipClose;
	public string PlayerTag;
	public Animation AnimationComponent;
	public string AnimNameOpen, AnimNameClose;
	public int Rezhim, count;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(Rezhim == 1)
		{
			if(count == 0)
			if(Input.GetKeyUp(KeyCode.E))
			{
				audioSource.PlayOneShot(clipOpen);
				AnimationComponent.CrossFade(AnimNameOpen);
				Text.GetComponent<Text>().text = "Е Open/Close";
				count = 1;
				print("Opened");
			}
			if(count == 1)
			if(Input.GetKeyDown(KeyCode.E))
				count = 2;

			if(count == 2)
				if(Input.GetKeyUp(KeyCode.E))
			{
				audioSource.PlayOneShot(clipClose);
				AnimationComponent.CrossFade(AnimNameClose);
				Text.GetComponent<Text>().text = "Е Open/Close";
				count = 0;
				print("Closed");
			}
		}
	}

	void OnTriggerEnter (Collider other) {
		if(other.gameObject.tag == PlayerTag)
		{
			Text.SetActive(true);
			Rezhim = 1;
			if(count == 0)
				Text.GetComponent<Text>().text = "Е Open/Close";
			if(count == 1)
				Text.GetComponent<Text>().text = "Е Open/Close";
		}
	}
	void OnTriggerExit (Collider other) {
		if(other.gameObject.tag == PlayerTag)
		{
			Text.SetActive(false);
			Rezhim = 0;
		}
	}
}
