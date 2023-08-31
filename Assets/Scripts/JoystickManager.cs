using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System; 

public class JoystickManager : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{
    private RectTransform joystickTransform;

    [SerializeField]
    private float dragThreshold = 0.6f;
    [SerializeField]
    private float dragMovementDistance = 30;
    [SerializeField]
    private float dragDistance = 100;

    public event Action<Vector2> OnMove;

    public Vector2 inputVector;

    private void Awake()
    {
        joystickTransform = (RectTransform)transform;
    }

        public void OnDrag(PointerEventData eventData)
    {
        Vector2 offset;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            joystickTransform,
            eventData.position,
            null,
            out offset);
        offset = Vector2.ClampMagnitude(offset, dragDistance) / dragDistance;
        joystickTransform.anchoredPosition = offset * dragMovementDistance;

        inputVector = CalculateMovementInput(offset);
        // OnMove?.Invoke(inputVector);
    }

    private Vector2 CalculateMovementInput(Vector2 offset)
    {
        float x = Mathf.Abs(offset.x) > dragThreshold ? offset.x : 0;
        float y = Mathf.Abs(offset.y) > dragThreshold ? offset.y : 0;
        return new Vector2(x,y);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        joystickTransform.anchoredPosition = Vector2.zero;
        // OnMove?.Invoke(Vector2.zero);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        
    }

    private void Update()
    {
        OnMove?.Invoke(inputVector);
    }

}

// using System;
// using UnityEngine;
// using UnityEngine.EventSystems;
// using UnityEngine.UI;

// public class JoystickManager : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
// {
//     public event Action<Vector2> OnMove;

//     [SerializeField] private RectTransform background;
//     [SerializeField] private RectTransform handle;

//     private Vector2 joystickValue = Vector2.zero;

//     private void Awake()
//     {
//         background.gameObject.SetActive(false);
//     }

//     public void OnPointerDown(PointerEventData eventData)
//     {
//         background.position = eventData.position;
//         background.gameObject.SetActive(true);
//         OnDrag(eventData);
//     }

//     public void OnDrag(PointerEventData eventData)
//     {
//         Vector2 direction = (eventData.position - background.position);
//         joystickValue = direction.normalized;
//         handle.anchoredPosition = direction.normalized * Mathf.Min(direction.magnitude, background.sizeDelta.x * 0.5f);
//     }

//     public void OnPointerUp(PointerEventData eventData)
//     {
//         joystickValue = Vector2.zero;
//         handle.anchoredPosition = Vector2.zero;
//         background.gameObject.SetActive(false);
//     }

//     private void Update()
//     {
//         OnMove?.Invoke(joystickValue);
//     }
// }