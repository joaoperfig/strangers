using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForestGenerator : MonoBehaviour {

	public GameObject[] trees;
	public int treecount;
	public float radius;

	private GameObject[] instances = new GameObject[0];

	// Use this for initialization
	void Start () {
		Generate ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Space)) {
			Generate ();
		}
	}

	void Generate(){
		foreach (GameObject oldtree in instances) {
			Destroy (oldtree);
		}
		instances = new GameObject[treecount];
		for (int i = 0; i < treecount; i++) {
			Vector2 pos2 = Random.insideUnitCircle * radius;
			Vector3 pos3 = new Vector3 (pos2.x, 0, pos2.y);
			GameObject tree = trees [Random.Range (0, trees.Length)];
			instances[i] = GameObject.Instantiate (tree, pos3, Quaternion.identity, gameObject.transform);
		}
	}
}
