using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckListManager : MonoBehaviour
{
    public RectTransform money;
    public float moneyMinSize;
    public float moneyMaxSize;
    public float moneySpeed;
    public bool moneyIncreasing = true;
    public RectTransform broom;
    public float broomMinRot;
    public float broomMaxRot;
    public float broomSpeed;
    public bool broomIncreasing = true;
    public Slider moneySlider;
    public Slider broomSlider;

    private int coinSlots = 0;
    private int coinsDone = 0;
    private int trash = 0;
    private int trashDone = 0;

    public void AddCoinSlot(int amount = 1)
    {
        coinSlots+= amount;
        UpdateUI();
    }

    public void AddCoinDone(int amount = 1)
    {
        coinsDone+= amount;
        UpdateUI();
    }

    public void AddTrash(int amount = 1)
    {
        trash+= amount;
        UpdateUI();
    }

    public void AddTrashDone(int amount = 1)
    {
        trashDone+= amount;
        UpdateUI();
    }

    private void UpdateUI()
    {
        if (coinSlots == 0) moneySlider.value = 0;
        else moneySlider.value = (float)((float)coinsDone / (float)coinSlots);
        if (trash == 0) broomSlider.value = 0;
        else broomSlider.value = (float)((float)trashDone / (float)trash);
    }

    void Update()
    {
        Vector3 moneyChange = new Vector3(moneySpeed * Time.deltaTime, moneySpeed * Time.deltaTime, 0);
        if (!moneyIncreasing) moneyChange *= -1f;
        money.localScale += moneyChange;
        if (money.localScale.x >= moneyMaxSize) moneyIncreasing = false;        
        if (money.localScale.x <= moneyMinSize) moneyIncreasing = true;        

        Vector3 broomChange = new Vector3(0, 0, broomSpeed * Time.deltaTime);
        if (!broomIncreasing) broomChange *= -1f;
        broom.localEulerAngles += broomChange;
        if (broom.localEulerAngles.z >= broomMaxRot) broomIncreasing = false;        
        // if (broom.localEulerAngles.z <= broomMinRot) broomIncreasing = true;        
        if (broom.localEulerAngles.z >= 300) broomIncreasing = true;        
    }
}
