using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MySelector : ChildNode, ParentNode {

    ParentNode parent;
    List<ChildNode> children;
    int currentChild;

    public MySelector(ParentNode p_parent)
    {
        children = new List<ChildNode>();
        parent = p_parent;
        currentChild = -1;

        parent.addChild(this);
    }

    void Start()
    {

    }

    public void aktivieren()
    {
        Debug.Log("Selektor aktiviert");
        if(children.Count > 0)
        {
            currentChild = 0;
            children[0].aktivieren();
        }
    }

    public void deaktivieren()
    {
        Debug.Log(" Selektor deaktiviert");
    }

    public void addChild(ChildNode c)
    {
        children.Add(c);
    }

    public void childTerminated(ChildNode c, bool result)
    {
        if(result)
        {
            Debug.Log("Selector erfolgreich beendet");
            parent.childTerminated(this, true);
        }
        else
        {
            currentChild++;
            if(currentChild < children.Count)
            {
                Debug.Log("Selector versucht nächste Option");
                children[currentChild].aktivieren();
            }
            else
            {
                Debug.Log("Selector nicht erfolgrreich beendet");
                parent.childTerminated(this, false);
            }
        }
    }
}
