using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyParallelTask : ChildNode, ParentNode {

    ParentNode parent;
    List<ChildNode> children;
    List<ChildNode> activeChildren;

    public MyParallelTask(ParentNode p_parent)
    {
        parent = p_parent;

        children = new List<ChildNode>();
        activeChildren = new List<ChildNode>();

        parent.addChild(this);
    }

    public void aktivieren()
    {
        Debug.Log("ParallelTask aktiviert");
        for(int i = 0; i < children.Count; i++)
        {
            activeChildren.Add(children[i]);
            children[i].aktivieren();
        }
    }

    public void deaktivieren()
    {
        Debug.Log("ParallelTask deaktiviert");
        for (int i = 0; i < children.Count; i++)
        {
            activeChildren[0].deaktivieren();
            activeChildren.RemoveAt(0);
        }
    }

    public void addChild(ChildNode c)
    {
        children.Add(c);
    }

    public void childTerminated(ChildNode c, bool result)
    {
        if (!result)
        {
            deaktivieren();
            parent.childTerminated(this, false);
        }
        else
        {
            activeChildren.Remove(c);

            if(activeChildren.Count == 0)
            {
                Debug.Log("Paralleltask erfolgreich beendet");
                parent.childTerminated(this, true);
            }
        }
    }

}
