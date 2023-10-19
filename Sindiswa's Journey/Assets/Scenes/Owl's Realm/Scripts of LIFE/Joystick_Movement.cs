using System.Collections;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Joystick_Movement : MonoBehaviour
{
    [SerializeField] private GameObject joystickBackground;
    [SerializeField] private GameObject joystickHandle;

    public Vector2 joystick_Vector;
    private Vector2 joystick_OriginalPos;
    private Vector2 joystick_TouchPos;
    private float joystickRadius;

    void Start()
    {
        joystick_OriginalPos = joystickBackground.transform.position;
        joystickRadius = joystickBackground.GetComponent<RectTransform>().sizeDelta.y / 2.5f;
        StickDisabled();
    }
    
    public void StickDisabled()
    {
        joystickBackground.GetComponent<Image>().color = Color.clear;
        joystickHandle.GetComponent<Image>().color = Color.clear;
    }
    public void StickVisible()
    {
        joystickBackground.GetComponent<Image>().color = Color.white;
        joystickHandle.GetComponent<Image>().color = Color.white;
    }

    public void TouchDownOnScreen()
    {
        StickVisible();
        joystickHandle.transform.position = Input.mousePosition;
        joystickBackground.transform.position = Input.mousePosition;
        joystick_TouchPos = Input.mousePosition;
    }
    public void Drag(BaseEventData baseEventData)
    {
        PointerEventData pointerEventData = baseEventData as PointerEventData;
        Vector2 dragPos = pointerEventData.position;
        joystick_Vector = (dragPos - joystick_TouchPos).normalized;

        float joystick_Distance = Vector2.Distance(dragPos, joystick_TouchPos);

        if(joystick_Distance < joystickRadius)
        {
            joystickHandle.transform.position = joystick_TouchPos + joystick_Vector * joystick_Distance;
        }
        else
        {
            joystickHandle.transform.position = joystick_TouchPos + joystick_Vector * joystickRadius;
        }
    }
    public void TouchUpFromScreen()
    {
        StickDisabled();
        joystick_Vector = Vector2.zero;
        joystickHandle.transform.position = joystick_OriginalPos;
        joystickBackground.transform.position = joystick_OriginalPos;
    }
}
