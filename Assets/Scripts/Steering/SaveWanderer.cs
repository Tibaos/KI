using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveWanderer : TaskNode {

    Rigidbody seekerRigid;
    Transform seekerTrans;

    Rigidbody targetRigid;
    Transform targetTrans;

    Vector3 saveSpot;

    Verhalten verhalten;

    ParentNode parent;

    bool isActive;

    public SaveWanderer(Rigidbody p_SeekerRigid, Transform p_SeekerTrans, Rigidbody p_TargetRigid, Transform p_TargetTrans, Vector3 p_saveSpot, Verhalten p_verhalten, ParentNode p_parent)
    {
        seekerRigid = p_SeekerRigid;
        seekerTrans = p_SeekerTrans;

        targetRigid = p_TargetRigid;
        targetTrans = p_TargetTrans;

        saveSpot = p_saveSpot;

        verhalten = p_verhalten;

        parent = p_parent;
        parent.addChild(this);

        isActive = false;
    }

    public void run()
    {

        //Seeker
        Vector3 direction = saveSpot - seekerTrans.position;

        Vector3 newDirection = Vector3.RotateTowards(seekerTrans.forward, direction, 1.0f, 0.0f);

        seekerTrans.rotation = Quaternion.LookRotation(newDirection);

        seekerRigid.velocity = seekerTrans.forward;

        //target
        Vector3 directionTar = saveSpot - targetTrans.position;

        Vector3 newDirectionTar = Vector3.RotateTowards(targetTrans.forward, directionTar, 1.0f, 0.0f);

        targetTrans.rotation = Quaternion.LookRotation(newDirectionTar);

        targetRigid.velocity = targetTrans.forward;

        if (direction.magnitude < 0.5)
        {
            seekerRigid.velocity = new Vector3(0, 0, 0);
            parent.childTerminated(this, false);
            deaktivieren();
        }
    }

    public void aktivieren()
    {
        Debug.Log("Aktiviere SaveWanderer Task");
        verhalten.activateTask(this);
        isActive = true;
    }

    public void deaktivieren()
    {
        if (isActive)
        {
            Debug.Log("Deaktiviere SaveWanderer Task");
            verhalten.deactivateTask(this);
            isActive = false;
        }
    }
}
