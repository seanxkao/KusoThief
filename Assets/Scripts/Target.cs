using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Rigidbody))]
public class Target : MonoBehaviour {

    [SerializeField] private float minThrowScale;
    [SerializeField] private float maxThrowScale;
    [SerializeField] private float minVelocity;
    [SerializeField] private float velocityScale;
    [SerializeField] private GameObject[] items;
	[SerializeField] private bool isRandomMove = true;
    private NavMeshAgent agent;
    private bool[] isDropped;
    private GameObject[] dropItems;
    private List<GameObject> dropItemList;
	private Coroutine randomMoveCoroutine;

    public void setItems(GameObject[] newItems) {
        items = newItems;
    }

	// Use this for initialization
	void Start () {
        agent = this.GetComponent<NavMeshAgent>();
        dropItems = new GameObject[items.Length];
        isDropped = new bool[items.Length];
        dropItemList = new List<GameObject> ();
        for (int i = 0; i < isDropped.Length; i++) {
            isDropped [i] = false;
        }
		randomMoveCoroutine = null;
	}
	
	// Update is called once per frame
	void Update () {
		if (dropItemList.Count > 0) {
			if (randomMoveCoroutine != null) {
				StopCoroutine (randomMoveCoroutine);
				randomMoveCoroutine = null;
			}

			agent.SetDestination (dropItemList [0].transform.position);
        }
        else {
            //GetComponent<Rigidbody> ().velocity = Vector3.zero;
            if (isRandomMove) {
				randomMoveCoroutine = StartCoroutine(RandomMove());
            }
        }
	}

    public void emit(Transform player) {
        Vector3 dir = player.position - transform.position;
        for (int i = 0; i < items.Length; i++) {
            if (isDropped [i])
                continue;
            GameObject item = items [i];
            GameObject spawn = GameObject.Instantiate (item);
            spawn.transform.position = transform.position + 3 * Vector3.up;
            Rigidbody rigid = spawn.GetComponent<Rigidbody> ();
            rigid.velocity = dir * Random.Range (minThrowScale, maxThrowScale);
            dropItems[i] = spawn;
            dropItemList.Add (spawn);
            isDropped [i] = true;
        }
    }

    void OnCollisionEnter(Collision collision) {
        if (dropItemList.Count == 0 || collision.gameObject.tag != "Item")
            return;
        for (int i = 0; i < items.Length; i++) {
            if (dropItems [i] == null)
                continue;
            if (collision.transform == dropItems [i].transform) {
                isDropped [i] = false;
                dropItemList.Remove (dropItems[i]);
                Destroy (dropItems [i]);
                dropItems [i] = null;
                break;
            }
        }
    }
		
    private IEnumerator RandomMove () {
        isRandomMove = false;
        Vector3 goal = new Vector3(Random.Range(-10f, 10f), 0, Random.Range(-10f, 10f));
        agent.SetDestination(goal);
        while (Vector3.Magnitude(transform.position - goal) >= 0.5f) {
            yield return null;
        }
        isRandomMove = true;
        yield break;
    }

}
