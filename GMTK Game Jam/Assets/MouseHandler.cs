using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(StandaloneInputModule))]
public class MouseHandler : MonoBehaviour
{
    private StandaloneInputModule standaloneInputModule;
    private GameObject lastSelectedObject;
    public static MouseHandler instance;
 
    void Awake()
    {
        instance = this;
        standaloneInputModule = GetComponent<StandaloneInputModule>();
    }

    void Update()
    {
        if(!Cursor.visible && (Input.GetAxis("Mouse X") != 0 || Input.GetAxis("Mouse Y") != 0))
        {
            // Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            PointerEventData pointerEventData = new PointerEventData(EventSystem.current);
            pointerEventData.position = Input.mousePosition;

            List<RaycastResult> raycastResultList = new List<RaycastResult>();
            EventSystem.current.RaycastAll(pointerEventData, raycastResultList);
            for (int i=0; i < raycastResultList.Count; i++)
            {
                if (raycastResultList[i].gameObject.GetComponent<Selectable>() != null)
                {
                    EventSystem.current.SetSelectedGameObject(null);
                    EventSystem.current.SetSelectedGameObject(raycastResultList[i].gameObject);
                    break;
                }
            }
        }

        CacheLastSelectedObject();
 
        if (EventSystemHasObjectSelected())
            return;
 
        // If any axis/submit/cancel is pressed.
        // This looks at the input names defined in the attached StandaloneInputModule. You could use your own instead if you want.
        if ((Input.GetAxisRaw(standaloneInputModule.horizontalAxis) != 0) ||
             (Input.GetAxisRaw(standaloneInputModule.verticalAxis) != 0) ||
             (Input.GetButtonDown(standaloneInputModule.submitButton)) ||
             (Input.GetButtonDown(standaloneInputModule.cancelButton)))
        {
            // Reselect the cached 'lastSelectedObject'
            ReselectLastObject();
            return;
        }
    }
 
    // Called whenever a UI navigation/submit/cancel button is pressed.
    public static void ReselectLastObject()
    {
        // Do nothing if this is not active (maybe input objects were disabled)
        if (!instance.isActiveAndEnabled || !instance.gameObject.activeInHierarchy)
            return;
 
        // Otherwise we can proceed with setting the currently selected object to be 'lastSelectedObject'...
       
        // Current must be set to null first, otherwise it doesn't work properly because Unity UI is weird ¯\_(ツ)_/¯
        EventSystem.current.SetSelectedGameObject(null);
       
        // Set current to lastSelectedObject
        EventSystem.current.SetSelectedGameObject(instance.lastSelectedObject);
    }
 
    // Returns whether or not the EventSystem has anything selected
    static bool EventSystemHasObjectSelected()
    {
        if (EventSystem.current.currentSelectedGameObject == null)
            return false;
        else
            return true;
    }
 
    // Caches last selected object for later use
    void CacheLastSelectedObject()
    {
        // Don't cache if nothing is selected
        if (EventSystemHasObjectSelected() == false)
            return;
 
        lastSelectedObject = EventSystem.current.currentSelectedGameObject.gameObject;
    }
}
