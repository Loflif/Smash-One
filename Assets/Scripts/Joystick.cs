using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.OnScreen;
using UnityEngine.InputSystem.Layouts;

public class Joystick : OnScreenControl, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    [SerializeField] Transform stick;
    public Vector3 offset;
    private Mouse mouse;

    [SerializeField]
    private string m_ControlPath;

    private Vector3 m_StartPos;
    private Vector2 m_PointerDownPos;

    protected override string controlPathInternal
    {
        get => m_ControlPath;
        set => m_ControlPath = value;
    }

    // Start is called before the first frame update
    void Start()
    {
        mouse = Mouse.current;
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void OnPointerDown(PointerEventData eventData)
    {
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 mouseClickPosition = Camera.main.ScreenToWorldPoint(mouse.position.ReadValue());
        Vector2 origo = transform.position;
        offset = Vector2.ClampMagnitude(mouseClickPosition - origo, 1);
        

        stick.position = (Vector3)origo+offset;
        SendValueToControl((Vector2)offset);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        stick.position = transform.position;
        offset = new Vector3(0, 0, 0);
        SendValueToControl((Vector2)offset);
    }
}
