using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Item : MonoBehaviour {

    public bool isPickup;
    public int ID;
    [SerializeField] private Sprite img;

    private void OnEnable () {
        //Debug.Log("On");
        isPickup = false;
        StartCoroutine(CountDownIsPickup());
    }

    public Sprite getImage() {
        return img;
    }

    private IEnumerator CountDownIsPickup () {
        yield return new WaitForSeconds(3f);
        isPickup = true;
        yield break;
    }

}
