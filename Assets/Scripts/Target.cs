using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour {

    [SerializeField] private float minThrowScale;
    [SerializeField] private float maxThrowScale;
    [SerializeField] private float velocity;
    [SerializeField] private GameObject[] items;
    private List<Transform> dropItems;

	// Use this for initialization
	void Start () { 
        dropItems = new List<Transform> ();
	}
	
	// Update is called once per frame
	void Update () {
        if (dropItems.Count > 0) {
            GetComponent<Rigidbody> ().velocity = (dropItems [0].transform.position - transform.position) * velocity;
        }
        else {
            GetComponent<Rigidbody> ().velocity = Vector3.zero;
        }
	}

    public void emit(Vector3 playerPosition) {
        Vector3 dir = transform.position - playerPosition;
        foreach (GameObject item in items) {
            GameObject spawn = GameObject.Instantiate (item);
            spawn.GetComponent<Rigidbody> ().AddForce (dir * Random.Range(minThrowScale, maxThrowScale));
            dropItems.Add (spawn.transform);
        }
    }
}
