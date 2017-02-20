using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectTrash : TaskNode
{

    Rigidbody seekerRigidbody;
    Transform seekerTransform;

    Verhalten verhalten;

    ParentNode parent;

    bool isActive;

    bool trashCollected;

    public CollectTrash(Rigidbody p_seekerRigidbody, Transform p_seekerTransform, Verhalten p_verhalten, ParentNode p_parent)
    {
        seekerRigidbody = p_seekerRigidbody;
        seekerTransform = p_seekerTransform;

        verhalten = p_verhalten;
        parent = p_parent;

        parent.addChild(this);
        isActive = false;
        trashCollected = false;
    }


    public void run()
    {
        if (isActive)
        {
            GameObject trash = GameObject.Find("Trash(Clone)");

            if(trash != null)
            {
                if (!trashCollected)
                {
                    Vector3 direction = trash.GetComponent<Transform>().position - seekerTransform.position;

                    Vector3 newDirection = Vector3.RotateTowards(seekerTransform.forward, direction, 1.0f, 0.0f);

                    seekerTransform.rotation = Quaternion.LookRotation(newDirection);

                    seekerRigidbody.velocity = seekerTransform.forward;

                    if (direction.magnitude < 1)
                    {
                        trashCollected = true;
                        trash.SetActive(false);
                    }
                }
            }
            if (trashCollected)
            {
                Vector3 direction = new Vector3(0, 0, 0) - seekerTransform.position;

                Vector3 newDirection = Vector3.RotateTowards(seekerTransform.forward, direction, 1.0f, 0.0f);

                seekerTransform.rotation = Quaternion.LookRotation(newDirection);

                seekerRigidbody.velocity = seekerTransform.forward;

                if (direction.magnitude < 1)
                {
                    trashCollected = false;
                    seekerRigidbody.velocity = new Vector3(0, 0, 0);
                }
            }
        }
    }

    public void aktivieren()
    {
        Debug.Log("Aktiviere Seek Task");
        verhalten.activateTask(this);
        isActive = true;
    }

    public void deaktivieren()
    {
        if (isActive)
        {
            Debug.Log("Deaktiviere Seek Task");
            verhalten.deactivateTask(this);
            isActive = false;
        }
    }
}
