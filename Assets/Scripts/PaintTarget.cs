using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class PaintTarget : MonoBehaviour
{
    private Material targetMaterial; 
    private EventTrigger eventTrigger;
    private FlexibleColorPicker flexibleColorPicker;
    private Transform targetTransform;
    
    public void BindTarget(FlexibleColorPicker fcp, Action<Vector3> pickCallback)
    {
        targetTransform = transform;
        
        flexibleColorPicker = fcp;
        MeshRenderer[] mRenderers = GetComponentsInChildren<MeshRenderer>();
        targetMaterial = Instantiate(mRenderers[0].material);
        foreach (var mRenderer in mRenderers)
        {
            mRenderer.material = targetMaterial;
        }
        
        eventTrigger = GetComponent<EventTrigger>();
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerClick;
        entry.callback.AddListener((data) =>
        {
            pickCallback(targetTransform.position);
            Paint((PointerEventData)data);
        });
        eventTrigger.triggers.Add(entry);
    }

    private void Paint(PointerEventData data)
    {
        targetMaterial.color = flexibleColorPicker.color;
    }
}
