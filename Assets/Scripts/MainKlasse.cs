using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainKlasse : MonoBehaviour {

    public Verhalten verhalten1;

    MySelector selektor1;
    MyParallelTask parallel1;
    MySequence sequence1;

    Seek seek1;
    Wander wander1;

    public GameObject seeker;
    public GameObject target;

	// Use this for initialization
	void Start () {

        //selektor1 = new MySelector(verhalten1);
        parallel1 = new MyParallelTask(verhalten1);
        //sequence1 = new MySequence(verhalten1);

        seek1 = new Seek(seeker.GetComponent<Rigidbody>(), seeker.GetComponent<Transform>(), target.GetComponent<Transform>(), verhalten1, parallel1);
        wander1 = new Wander(target.GetComponent<Rigidbody>(), target.GetComponent<Transform>(), verhalten1, parallel1);

        verhalten1.startZwei();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
