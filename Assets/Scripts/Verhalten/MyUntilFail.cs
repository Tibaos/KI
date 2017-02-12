using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyUntilFail : ChildNode, ParentNode {

    ParentNode parent;
    ChildNode child;

    public MyUntilFail(ParentNode p_parent)
    {
        parent = p_parent;
        parent.addChild(this);
        child = null;
    }

    public void aktivieren()
    {
        child.aktivieren();
    }

    public void deaktivieren()
    {
        child.deaktivieren();
    }

    public void addChild(ChildNode c)
    {
        if(child == null)
        {
            child = c;
        }
    }

    public void childTerminated(ChildNode c, bool result)
    {
        if(result)
        {
            child.aktivieren();
        }
        else
        {
            parent.childTerminated(this, false);
        }
    }
}
