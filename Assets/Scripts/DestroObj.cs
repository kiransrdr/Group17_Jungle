using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroObj : MonoBehaviour {
    public GameObject destroyablecube;
	// Use this for initialization
	void Start () {
        GameObject.Instantiate(destroyablecube,transform.position,transform.rotation);
        Destroy(gameObject);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
