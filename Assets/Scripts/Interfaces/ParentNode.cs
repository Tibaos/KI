using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ParentNode
{
    void addChild(ChildNode c);

    void childTerminated(ChildNode c, bool result);
}

