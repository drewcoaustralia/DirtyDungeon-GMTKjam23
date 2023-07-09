using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TaskKind {
    NotSet,
    FillChest,
    Trash,
}

public class TaskInstance : MonoBehaviour {
    private CheckListManager cm;

    public string description;
    public TaskKind kind;

    void Start() {
        cm = GameObject.Find("Checklist Manager").GetComponent<CheckListManager>();
        cm.RegisterTask(this);
    }

    void OnDestroy() {
        Complete();
    }

    public void Complete() {
        cm.CompleteTask(this);
    }
}
