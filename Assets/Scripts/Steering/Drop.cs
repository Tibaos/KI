using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drop : TaskNode {

    Rigidbody dropRigid;
    Transform dropTrans;
    float dropAngle;

    GameObject trash;
    List<GameObject> trashlist;
    GameObject trash1;
    GameObject trash2;
    GameObject trash3;
    int trashcount;

    float maxTime;
    float timer;
    float lastTimer;

    ParentNode parent;
    Verhalten verhalten;

    bool isActive;

    public Drop(Rigidbody p_dropRigid, Transform p_dropTrans, GameObject p_trash, Verhalten p_verhalten, ParentNode p_parent)
    {
        parent = p_parent;
        verhalten = p_verhalten;

        dropRigid = p_dropRigid;
        dropTrans = p_dropTrans;

        trash = p_trash;



        trashlist = new List<GameObject>();
        trash1 = GameObject.Instantiate(trash);
        trash2 = GameObject.Instantiate(trash);
        trash3 = GameObject.Instantiate(trash);
        trashlist.Add(trash1);
        trashlist.Add(trash2);
        trashlist.Add(trash3);

        trashlist[0].SetActive(false);
        trashlist[1].SetActive(false);
        trashlist[2].SetActive(false);

        trashcount = 0;
        maxTime = 10;

        parent.addChild(this);
        isActive = false;
    }

    public void aktivieren()
    {
        Debug.Log("Aktiviere droptask");
        if(trashcount >= 3)
        {
            trashcount = 0;
        }
        verhalten.activateTask(this);
        isActive = true;
    }

    public void deaktivieren()
    {
        Debug.Log("deaktiviere droptask");
        verhalten.deactivateTask(this);
        isActive = false;
    }

    public void run()
    {
        if (isActive)
        {
            float rotationAngle = Random.value;

            if (trashcount >= 3)
            {
                deaktivieren();
                parent.childTerminated(this, false);
                dropRigid.velocity = new Vector3(0, 0, 0);
            }

            if (Random.value > 0.5)
            {
                rotationAngle *= 1;
            }
            else
            {
                rotationAngle *= -1;
            }

            if(timer > maxTime)
            {
                trashlist[trashcount].transform.position = dropTrans.position;
                trashlist[trashcount].SetActive(true);
                trashcount++;
                timer = 0;
            }

            timer += Time.deltaTime - lastTimer;

            rotationAngle = Mathf.Clamp(rotationAngle, -0.1f, 0.1f);

            dropAngle += rotationAngle;

            dropAngle = Mathf.Clamp(dropAngle, -1f, 1f);

            dropTrans.Rotate(new Vector3(0.0f, 1.0f, 0.0f), dropAngle);

            dropRigid.velocity = dropTrans.forward;

            if (dropTrans.position.z < -10 || dropTrans.position.z > 10 || dropTrans.position.x > 10 || dropTrans.position.x < -10)
            {
                deaktivieren();
                parent.childTerminated(this, true);
                dropRigid.velocity = new Vector3(0, 0, 0);
            }
        }
    }
}
