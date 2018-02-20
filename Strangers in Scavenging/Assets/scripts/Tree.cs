using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : MonoBehaviour {

	public GameObject sectionsample;
	public GameObject endsample;

	public Sprite[] bases;
	public Sprite[] sections;
	public Sprite[] ends;

	public int maxrecursion;
	public float segmentsize;
	public float backdistance;
	public float angledev;
	public float splitangle;
	public float sizeloss;
	public float sizedev;
	public float endchance;
	public float splitchance;
	public float splitendchance;

	// Use this for initialization
	void Start () {
		Generate ();
	}
	
	// Update is called once per frame
	void Update () {
		/*if (Input.GetKeyDown (KeyCode.Space)) {
			Generate ();
		}*/
	}

	void Generate(){
		foreach (Transform child in gameObject.transform) {
			Destroy (child.gameObject);
		}
		Sprite chosen = bases[Random.Range(0,bases.Length)];
		gameObject.GetComponent<SpriteRenderer> ().sprite = chosen;
		Addlayer (gameObject.transform);
		float size = 1 + Random.Range (-sizedev, sizedev);
		gameObject.transform.localScale = new Vector3 (size, size, 0);
	}

	void Addlayer(Transform parent){
		if ((Random.Range (0f, 1f) < endchance) || (parent.localScale.x < Mathf.Pow(sizeloss, maxrecursion-1))) { //Branch ends here
			float angle = Random.Range (-angledev, angledev);
			float newsize = sizeloss; // + Random.Range (-sizedev, sizedev);
			GameObject end = GameObject.Instantiate (endsample, parent);
			end.transform.localScale = new Vector3 (newsize, newsize, 1);
			end.transform.localPosition = new Vector3 (0f, segmentsize, backdistance);
			end.transform.localEulerAngles = new Vector3 (0f, 0f, angle);
			Sprite chosen = ends[Random.Range(0,ends.Length)];
			end.GetComponent<SpriteRenderer> ().sprite = chosen;
			return;
		}
		if (Random.Range (0f, 1f) < splitchance) { //Branch splits into two
			float baseangle = -splitangle; //first iteration
			for (int i = 0; i < 2; i++) {
				if (Random.Range (0f, 1f) < splitendchance) { //splitted part ends
					float angledif = Random.Range (-angledev, angledev);
					float newsize = sizeloss; // + Random.Range (-sizedev, sizedev);
					GameObject end = GameObject.Instantiate (endsample, parent);
					end.transform.localScale = new Vector3 (newsize, newsize, 1);
					end.transform.localPosition = new Vector3 (0f, segmentsize, backdistance);
					end.transform.localEulerAngles = new Vector3 (0f, 0f, baseangle + angledif);
					Sprite chosen = ends [Random.Range (0, ends.Length)];
					end.GetComponent<SpriteRenderer> ().sprite = chosen;
				} else {         //splitted part continues
					float angledif = Random.Range (-angledev, angledev);
					float newsize = sizeloss; // + Random.Range (-sizedev, sizedev);
					GameObject section = GameObject.Instantiate (sectionsample, parent);
					section.transform.localScale = new Vector3 (newsize, newsize, 1);
					section.transform.localPosition = new Vector3 (0f, segmentsize, backdistance);
					section.transform.localEulerAngles = new Vector3 (0f, 0f, baseangle + angledif);
					Sprite chosen = sections [Random.Range (0, sections.Length)];
					section.GetComponent<SpriteRenderer> ().sprite = chosen;
					Addlayer (section.transform);
				}
				baseangle = splitangle; //second iteration
			}
		} else {// Branch continues ahead
			float angle = Random.Range (-angledev, angledev);
			float newsize = sizeloss; // + Random.Range (-sizedev, sizedev);
			GameObject section = GameObject.Instantiate (sectionsample, parent);
			section.transform.localScale = new Vector3 (newsize, newsize, 1);
			section.transform.localPosition = new Vector3 (0f, segmentsize, backdistance);
			section.transform.localEulerAngles = new Vector3 (0f, 0f, angle);
			Sprite chosen = sections [Random.Range (0, sections.Length)];
			section.GetComponent<SpriteRenderer> ().sprite = chosen;
			Addlayer (section.transform);
		}
	}
}