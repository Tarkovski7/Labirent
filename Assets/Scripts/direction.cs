using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class direction : MonoBehaviour
{
    bool isDragging;
    public LayerMask slotlayer;
    Vector3 startpos;

    public int direction_; // 1 sag 2 sol 3 yukarý 4 asagý

    private void Start()
    {
        startpos = transform.position;
    }
    private void Update()
    {
        if (!isDragging)
        {
            if (collisionObj == null)
            {
                transform.position = startpos;
            }
        }
        if (selectedslot!=null&&selectedslot.GetComponent<slotmanager>())
        {
            selectedslot.GetComponent<slotmanager>().selectedDirection = direction_;
        }
    }
    public void Drag()
    {
     //   Vector3 pos = Camera.main.ScreenToViewportPoint(Input.mousePosition);
        transform.position = Input.mousePosition;
        isDragging = true;
        print(isDragging);
    }
    public void DragExit()
    {
        isDragging = false;
        print(isDragging);
        if (selectedslot == null) return;
        
    }
    public GameObject selectedslot;

    public GameObject collisionObj;
    private void OnTriggerStay2D(Collider2D collision)
    {
        print(collision.gameObject.name);

        if(collision.gameObject.CompareTag("reset_col") || collision.gameObject.name== "reset_col")
        {
            collisionObj = null;
        }
        else
        {
            collisionObj = collision.gameObject;

        }
        if (isDragging) return;
     
        if (collision.gameObject.CompareTag("slot"))
        {
            selectedslot = collision.gameObject;
            
            

            transform.position = startpos;
        }
      
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        collisionObj = null;
        if (collision.gameObject.CompareTag("slot"))
        {
            selectedslot = null;
        }
    }
}
