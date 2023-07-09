using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckListManager : MonoBehaviour
{
    public GridLayoutGroup CheckListContainer;
    public GameObject TaskDescriptionPrefab;

    public List<TaskInstance> activeTasks = new List<TaskInstance>();
    public List<TaskInstance> completedTasks = new List<TaskInstance>();

    public void RegisterTask(TaskInstance task) {
        activeTasks.Add(task);
        UpdateUI();
    }

    public void CompleteTask(TaskInstance task) {
        activeTasks.Remove(task);
        completedTasks.Add(task);
        UpdateUI();
    }

    private void UpdateUI() {
        foreach (Transform child in CheckListContainer.transform) {
            Destroy(child.gameObject);
        }

        int chests = 0;
        int trash = 0;

        foreach (var item in activeTasks) {
            if (item.kind == TaskKind.FillChest) chests++;
            if (item.kind == TaskKind.Trash) trash++;
        }

        if (chests > 0) {
            var taskDescription = Instantiate(TaskDescriptionPrefab, CheckListContainer.transform);
            taskDescription.GetComponent<Text>().text = $"TODO  Fill {chests} chests";
            taskDescription.transform.SetParent(CheckListContainer.gameObject.transform);
        }
        if (trash > 0) {
            var taskDescription = Instantiate(TaskDescriptionPrefab, CheckListContainer.transform);
            taskDescription.GetComponent<Text>().text = $"TODO  Clean {trash} trash";
            taskDescription.transform.SetParent(CheckListContainer.gameObject.transform);
        }

        chests = 0;
        trash = 0;

        foreach (var item in completedTasks) {
            if (item.kind == TaskKind.FillChest) chests++;
            if (item.kind == TaskKind.Trash) trash++;
        }

        if (chests > 0) {
            var taskDescription = Instantiate(TaskDescriptionPrefab, CheckListContainer.transform);
            taskDescription.GetComponent<Text>().text = $"DONE  Fill {chests} chests";
            taskDescription.transform.SetParent(CheckListContainer.gameObject.transform);
        }
        if (trash > 0) {
            var taskDescription = Instantiate(TaskDescriptionPrefab, CheckListContainer.transform);
            taskDescription.GetComponent<Text>().text = $"DONE  Clean {trash} trash";
            taskDescription.transform.SetParent(CheckListContainer.gameObject.transform);
        }
    }
}
