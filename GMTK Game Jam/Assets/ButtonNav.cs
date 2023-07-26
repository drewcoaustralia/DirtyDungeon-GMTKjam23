using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Selectable))]
public class ButtonNav : MonoBehaviour, IPointerEnterHandler, IDeselectHandler, IPointerExitHandler, IMoveHandler
{
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!EventSystem.current.alreadySelecting)
            EventSystem.current.SetSelectedGameObject(this.gameObject);
    }
 
    public void OnDeselect(BaseEventData eventData)
    {
        this.GetComponent<Selectable>().OnPointerExit(null);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (EventSystem.current.currentSelectedGameObject == this.gameObject)
            if (!EventSystem.current.alreadySelecting)
                EventSystem.current.SetSelectedGameObject(null);
    }

    public void OnMove(AxisEventData eventData)
    {
        if(Input.GetAxis("Mouse X") == 0 && Input.GetAxis("Mouse Y") == 0)
            DisableMouse();
    }

    public void DisableMouse()
    {
        Cursor.visible = false;
        // Cursor.lockState = CursorLockMode.Locked;
    }

}