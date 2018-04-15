using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Target : MonoBehaviour {

    [SerializeField] private float minThrowScale;
    [SerializeField] private float maxThrowScale;
    [SerializeField] private float minVelocity;
    [SerializeField] private float velocityScale;
    [SerializeField] private GameObject[] items;
    private List<Transform> dropItems;

	// Use this for initialization
	void Start () { 
        dropItems = new List<Transform> ();
	}
	
	// Update is called once per frame
	void Update () {
        if (dropItems.Count > 0) {
            Vector3 dir = dropItems [0].transform.position - transform.position;
            dir = dir.normalized * Mathf.Max (dir.magnitude, minVelocity);
            GetComponent<Rigidbody> ().velocity = dir + dir * (dir.magnitude - minVelocity) * velocityScale;
        }
        else {
            GetComponent<Rigidbody> ().velocity = Vector3.zero;
        }
	}

    public void emit(Transform player) {
        Vector3 dir = player.position - transform.position;
        foreach (GameObject item in items) {
            GameObject spawn = GameObject.Instantiate (item);
            spawn.transform.position = transform.position + 3 * Vector3.up;
            Rigidbody rigid = spawn.GetComponent<Rigidbody> ();
            rigid.velocity = dir * Random.Range (minThrowScale, maxThrowScale);
            dropItems.Add (spawn.transform);
        }
    }

    void OnCollisionEnter(Collision collision) {
        if (dropItems.Count == 0 || collision.gameObject.tag != "Item")
            return;
        foreach (Transform item in new List<Transform>(dropItems)) {
            if (collision.transform == item.transform) {
                dropItems.Remove (collision.transform);
                Destroy (collision.transform.gameObject);
                return;
            }
        }
    }
}
