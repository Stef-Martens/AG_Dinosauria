using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InitializeButton : MonoBehaviour
{
    Selectable lastSelect;

    void Start()
    {
        lastSelect = null;
    }

    void Update()
    {
        if (EventSystem.current.currentSelectedGameObject == null)
        {
            if (lastSelect != null)
            {
                EventSystem.current.SetSelectedGameObject(lastSelect.gameObject);
            }
        }
        else
        {
            lastSelect = EventSystem.current.currentSelectedGameObject.GetComponent<Selectable>();
        }
    }
}