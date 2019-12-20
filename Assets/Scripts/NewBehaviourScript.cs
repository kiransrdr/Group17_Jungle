using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewBehaviourScript : MonoBehaviour {
    public Button yoursecondButton;
    // Use this for initialization
    void Start() {
        yoursecondButton.onClick.AddListener(() => { // yourButton must be like first part
            print("hello people");//print hello people or anything in massagebar
        });
        }
	
	// Update is called once per frame
	void Update () {
        yoursecondButton.onClick.AddListener(() => { // yourButton must be like first part
            print("hello world");//print hello people or anything in massagebar
        });
       
    }
    public void btnclick() { }


}
