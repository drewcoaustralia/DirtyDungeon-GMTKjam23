using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckListManager : MonoBehaviour
{
    public GameObject CheckListContainer;
    public GameObject TaskDescriptionPrefab;

    public List<Task> Tasks = new List<Task>();
    public List<Task> ActiveTasks = new List<Task>();



    void AddTask(Task task)
    {

    }
}
