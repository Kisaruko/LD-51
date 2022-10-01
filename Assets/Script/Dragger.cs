using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dragger : MonoBehaviour
{

    private Vector3 dragOffset;
    private Camera cam;

    public float speed;

    public bool bombOverMouse;

    public bool cantBeDragAgain = false;

    private void Awake()
    {
        cam = Camera.main;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            IsMouseOverBomb();
        }

            IsMouseOverBomb();

    }

    public bool IsMouseOverBomb()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity);
        if(hit.collider != null)
        {
            if(hit.collider.CompareTag("Red") || hit.collider.CompareTag("Black")) 
            {
                bombOverMouse = true;
                return bombOverMouse;
            }
            if(!hit.collider.CompareTag("Red") || hit.collider.CompareTag("Black"))
            {
                bombOverMouse = false;
                return bombOverMouse;
            }
            return false;
        }
        return false;
    }

    private void OnMouseDown()
    {
        dragOffset = transform.position - GetMousePos();
    }

    void OnMouseDrag()
    {
        if(!cantBeDragAgain)
            transform.position = Vector3.MoveTowards(transform.position, GetMousePos() + dragOffset, speed * Time.deltaTime);
    }

    Vector3 GetMousePos()
    {
        Vector3 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;
        return mousePos;
    }
}
