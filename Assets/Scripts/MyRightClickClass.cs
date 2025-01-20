using UnityEngine;
using UnityEngine.EventSystems;

public class EventTriggerRightClick : MonoBehaviour
{
    public void OnClick(BaseEventData data)
    {
        PointerEventData pointerData = data as PointerEventData;

        if (pointerData != null && pointerData.button == PointerEventData.InputButton.Right)
        {
            Debug.Log($"Right-click detected on {gameObject.name}");
            // Add your functionality here
        }
    }
}
