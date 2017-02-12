using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wander : TaskNode{

    Rigidbody wanderRigidbody;
    Transform wanderTransform;
    float wandererAngle;

    Verhalten verhalten;
    ParentNode parent;

    bool isActive;

    public Wander(Rigidbody p_wanderRigidbody, Transform p_wanderTransform, Verhalten p_verhalten, ParentNode p_parent)
    {
        wanderRigidbody = p_wanderRigidbody;
        wanderTransform = p_wanderTransform;

        parent = p_parent;
        verhalten = p_verhalten;

        parent.addChild(this);

        isActive = false;
    }

    public void run()
    {
        if (isActive)
        {
            float rotationAngle = Random.value;

            if (Random.value > 0.5)
            {
                rotationAngle *= 1;
            }
            else
            {
                rotationAngle *= -1;
            }

            rotationAngle = Mathf.Clamp(rotationAngle, -0.1f, 0.1f);

            wandererAngle += rotationAngle;

            wandererAngle = Mathf.Clamp(wandererAngle, -1f, 1f);

            wanderTransform.Rotate(new Vector3(0.0f, 1.0f, 0.0f), wandererAngle);

            wanderRigidbody.velocity = wanderTransform.forward;
        }
    }

    public void aktivieren()
    {
        Debug.Log("Aktiviere Wander Task");
        verhalten.activateTask(this);
        isActive = true;
    }

    public void deaktivieren()
    {
        if (isActive)
        {
            Debug.Log("Deaktiviere Wander Task");
            verhalten.deactivateTask(this);
            isActive = false;
        }
    }
}
