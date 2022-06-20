using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.XR;

public class Joystick : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    [SerializeField]
    private GameObject Background, Handle;
    Touch oneTouch;
    private Vector2 touchPosition;
    private Vector2 moveDirection;
    public float maxRadius;
    [HideInInspector]
    public bool IsTouched;
    [HideInInspector]
    public float Horizontal, Vertical;
    public float speed = 0.02f;
    public bool showJoystick;

    void Start()
    {
        Background.SetActive(false);
        Handle.SetActive(false);
    }

    public virtual void OnPointerDown(PointerEventData eventData)
    {
        OnDrag(eventData);
    }

    public void OnDrag(PointerEventData eventData)
    {
#if UNITY_EDITOR
        if (eventData.IsPointerMoving())
        {
            touchPosition = Input.mousePosition;
            if (!IsTouched)
            {
                IsTouched = true;
                if (showJoystick) Background.SetActive(true);
                if (showJoystick) Handle.SetActive(true);
                Background.transform.position = touchPosition;
                Handle.transform.position = touchPosition;
            }
            Move();
        }
#else
            if (eventData.IsPointerMoving() && eventData.pointerId == 0)
            {
                oneTouch = Input.GetTouch(0);
                touchPosition = oneTouch.position;

                if (!IsTouched)
                {
                    IsTouched = true;
                    if(showJoystick) Background.SetActive(true);
                    if(showJoystick) Handle.SetActive(true);
                    Background.transform.position = touchPosition;
                    Handle.transform.position = touchPosition;
                }
                Move();
            }
#endif
    }

    public virtual void OnPointerUp(PointerEventData eventData)
    {
        IsTouched = false;
        Background.SetActive(false);
        Handle.SetActive(false);
    }

    private void Move()
    {
        Handle.transform.position = touchPosition;
        Handle.transform.localPosition = Vector2.ClampMagnitude(Handle.transform.localPosition, maxRadius);
        moveDirection = (Handle.transform.position - Background.transform.position) * speed;
        Horizontal = moveDirection.x;
        Vertical = moveDirection.y;
    }
}

