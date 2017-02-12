using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Verhalten : MonoBehaviour, ParentNode {

    ChildNode root;
    bool isRunning;

    List<TaskNode> activeTasks;
    ArrayList tasksToRemove;
    List<TaskNode> tasksToAdd;

    // Use this for initialization
    void Awake() {
        activeTasks = new List<TaskNode>();
        tasksToRemove = new ArrayList();
        tasksToAdd = new List<TaskNode>();

        isRunning = false;
    }

    // Update is called once per frame
    void Update() {
        if(!isRunning)
        {
            startZwei();
        }

        foreach(TaskNode t in tasksToRemove)
        {
            activeTasks.Remove(t);
        }

        tasksToRemove.Clear();

        foreach(TaskNode t in tasksToAdd)
        {
            activeTasks.Add(t);
        }

        tasksToAdd.Clear();

        foreach(TaskNode t in activeTasks)
        {
            t.run();
        }
    }

    public void addChild(ChildNode c)
    {
        root = c;
    }

    public void childTerminated(ChildNode c, bool result)
    {
        isRunning = false;
    }

    public void startZwei()
    {
        if (root != null)
        {
            root.aktivieren();
            isRunning = true;
        }
    }

    public void activateTask(TaskNode t)
    {
        if(tasksToRemove.Contains(t))
        {
            tasksToRemove.Remove(t);
        }
        else
        {
            tasksToAdd.Add(t);
        }
    }

    public void deactivateTask(TaskNode t)
    {
        if(tasksToRemove.Contains(t))
        {
            tasksToRemove.Remove(t);
        }
        else
        {
            tasksToRemove.Add(t);
        }
    }
}
