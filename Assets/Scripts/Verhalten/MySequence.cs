using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MySequence : ChildNode, ParentNode {

    ParentNode parent;
    int currentChild;
    List<ChildNode> children;

    public MySequence(ParentNode p_parent)
    {
        children = new List<ChildNode>();

        parent = p_parent;
        parent.addChild(this);
        currentChild = -1;
    }

    public void aktivieren()
    {
        Debug.Log("Sequence aktiviert");
        if(children.Count > 0)
        {
            currentChild = 0;
            children[0].aktivieren();
        }
    }

    public void deaktivieren()
    {

    }

    public void addChild(ChildNode c)
    {
        children.Add(c);
    }

    public void childTerminated(ChildNode c, bool result)
    {
        if(!result)
        {
            Debug.Log("Sequence nicht erfolgreich beendet");
            parent.childTerminated(this, false);
        }
        else
        {
            currentChild++;
            if(currentChild < children.Count)
            {
                Debug.Log("Sequence aktiviert nächste Option");
                children[currentChild].aktivieren();
            }
            else
            {
                Debug.Log("Sequence erfolgreich beendet");
                parent.childTerminated(this, true);
            }
        }
    }
}
