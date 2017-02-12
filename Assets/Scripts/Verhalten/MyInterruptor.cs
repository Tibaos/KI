using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyInterruptor : TaskNode {

    ParentNode parent;
    bool isActive;
    bool isInterrupted;
    Verhalten verhalten;

    public MyInterruptor(Verhalten p_verhalten, ParentNode p_parent)
    {
        parent = p_parent;
        verhalten = p_verhalten;

        parent.addChild(this);
        isInterrupted = false;
        isActive = false;
    }

    public void aktivieren()
    {
        isActive = true;
        isInterrupted = false;
        Debug.Log("Interruptor aktiviert");
        verhalten.activateTask(this);
    }

    public void run()
    {
        if(isInterrupted)
        {
            Debug.Log("Unterbrechung durchgeführt");
            deaktivieren();
            parent.childTerminated(this, false);
        }
    }

    public void deaktivieren()
    {
        if(isActive)
        {
            isActive = false;
            Debug.Log("Interruptor deaktiviert");
            verhalten.deactivateTask(this);
        }
    }

    public void Interrupt()
    {
        if(isActive)
        {
            Debug.Log("Unterbrechnung erhalten");
            isInterrupted = true;
        }
        else
        {
            Debug.Log("Unterbrechung ignoriert");
        }
    }



}
