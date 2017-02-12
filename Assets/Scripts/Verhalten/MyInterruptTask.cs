using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyInterruptTask : TaskNode {

    ParentNode parent;
    Verhalten verhalten;
    List<MyInterruptor> interrupts;

    public MyInterruptTask(ParentNode p_parent, Verhalten p_verhalten)
    {
        interrupts = new List<MyInterruptor>();

        parent = p_parent;
        verhalten = p_verhalten;

        parent.addChild(this);
    }

    public void aktivieren()
    {
        verhalten.activateTask(this);
        Debug.Log("InterruptTask aktiviert");
    }

    public void run()
    {
        deaktivieren();
        Debug.Log("Sende Unterbrechungen");
        foreach(MyInterruptor rupt in interrupts)
        {
            rupt.Interrupt();
        }
        parent.childTerminated(this, true);
    }

    public void deaktivieren()
    {
        verhalten.deactivateTask(this);
    }

    public void addInterrupt(MyInterruptor i)
    {
        interrupts.Add(i);
    }
}
