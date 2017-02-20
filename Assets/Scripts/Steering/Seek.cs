using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seek : TaskNode {

    Rigidbody seekerRigidbody;
    Transform seekerTransform;

    [SerializeField]
    Transform targetTransform;

    Verhalten verhalten;

    ParentNode parent;

    bool isActive;

    public Seek(Rigidbody p_seekerRigidbody,  Transform p_seekerTransform, Transform p_targetTransform, Verhalten p_verhalten, ParentNode p_parent)
    {
        seekerRigidbody = p_seekerRigidbody;
        seekerTransform = p_seekerTransform;

        targetTransform = p_targetTransform;

        verhalten = p_verhalten;
        parent = p_parent;

        parent.addChild(this);
        isActive = false;
    }


    public void run()
    {
        if (isActive)
        {
            Vector3 direction = targetTransform.position - seekerTransform.position;

            Vector3 newDirection = Vector3.RotateTowards(seekerTransform.forward, direction, 1.0f, 0.0f);

            seekerTransform.rotation = Quaternion.LookRotation(newDirection);

            seekerRigidbody.velocity = seekerTransform.forward;

            if(direction.magnitude < 2)
            {
                seekerRigidbody.velocity = new Vector3(0, 0.6f, 0);
                parent.childTerminated(this, true);
                deaktivieren();
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
