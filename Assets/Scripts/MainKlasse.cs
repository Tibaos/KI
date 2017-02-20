using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainKlasse : MonoBehaviour {

    public Verhalten verhalten1;

    MySelector selektor1;
    MyParallelTask parallel1;
    MySelector selector1;
    MySequence sequence1;
    MyUntilFail untilfail1;

    Seek seek1;
    Seek seek2;
    Wander wander1;
    SaveWanderer saveWanderer1;
    Drop drop1;
    CollectTrash collectTrash1;

    public GameObject seeker;
    public GameObject target;
    public GameObject dropper;
    public GameObject trash;
    public GameObject collector;
    public GameObject ladeStation;

	// Use this for initialization
	void Start () {

        //selektor1 = new MySelector(verhalten1);
        parallel1 = new MyParallelTask(verhalten1);
        untilfail1 = new MyUntilFail(parallel1);
        selector1 = new MySelector(untilfail1);
        sequence1 = new MySequence(selector1);

        collectTrash1 = new CollectTrash(collector.GetComponent<Rigidbody>(), collector.GetComponent<Transform>(), verhalten1, parallel1);

        seek2 = new Seek(dropper.GetComponent<Rigidbody>(), dropper.GetComponent<Transform>(), ladeStation.GetComponent<Transform>(), verhalten1, selector1);
        drop1 = new Drop(dropper.GetComponent<Rigidbody>(), dropper.GetComponent<Transform>(), trash, verhalten1, sequence1);
        //wander1 = new Wander(target.GetComponent<Rigidbody>(),target.GetComponent<Transform>(), verhalten1, sequence1);
        seek1 = new Seek(seeker.GetComponent<Rigidbody>(), seeker.GetComponent<Transform>(), dropper.GetComponent<Transform>(), verhalten1, sequence1);
        saveWanderer1 = new SaveWanderer(seeker.GetComponent<Rigidbody>(), seeker.GetComponent<Transform>(), dropper.GetComponent<Rigidbody>(), dropper.GetComponent<Transform>(), new Vector3(0, 0.6f, 0), verhalten1, sequence1);

        verhalten1.startZwei();
	}
}
