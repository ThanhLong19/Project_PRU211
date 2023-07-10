using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Joystick : MonoBehaviour, IPointerUpHandler, IDragHandler, IPointerDownHandler
{
    private RectTransform joystickTransform;

    [SerializeField] private float dragThreshold = 0.6f;
    [SerializeField] private int dragMovementDistance = 30;
    [SerializeField] private int dragOffsetDistance = 100;
    private Vector2 offset = Vector2.zero;
    private bool isJoystickMoving = false;

    public event Action<Vector2> OnMove;

    public void OnDrag(PointerEventData eventData)
    {
        isJoystickMoving = true;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            joystickTransform,
            eventData.position,
            null,
            out offset);
        offset = Vector2.ClampMagnitude(offset, dragOffsetDistance) / dragOffsetDistance;
        joystickTransform.anchoredPosition = offset * dragMovementDistance;

        Vector2 inputVector = CalculateMovement(offset);
        OnMove?.Invoke(inputVector);
    }

    private Vector2 CalculateMovement(Vector2 offset)
    {
        float x = Mathf.Abs(offset.x) > dragThreshold ? offset.x : 0;
        float y = Mathf.Abs(offset.y) > dragThreshold ? offset.y : 0;
        return new Vector2(x, y);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        offset = Vector2.zero;
        joystickTransform.anchoredPosition = Vector2.zero;
        OnMove?.Invoke(Vector2.zero);
        isJoystickMoving = false;
    }

    private void Awake()
    {
        joystickTransform = (RectTransform)transform;
    }

    public Vector2 GetJoystickPosition()
    {
        return offset;
    }

}
