using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using UnityEngine;

public class invent1 : MonoBehaviour
{
    public Transform targetObject;  
    public float radius = 1f;  
    public KeyCode attachKey = KeyCode.E; 
    public LayerMask objectLayer; 
    public GameObject holdingObject;

    public ObjectReceiver ObjectReceiverScript; 
    public bool isCollidingWithObjectReceiver = false; 

    

    void Update()
    {
        if (Input.GetKeyDown(attachKey) && holdingObject == null)
        {
            AttachNearest();
        } 
        if (isCollidingWithObjectReceiver && Input.GetKeyDown(attachKey) && holdingObject != null)
        {
            holdingObject.transform.parent = null;  
            ObjectReceiverScript.ReceiveHoldingObject(holdingObject);
            UnityEngine.Debug.Log("SENT.");
            holdingObject = null;
        } 
    }

    void AttachNearest()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, radius, objectLayer);
        if (colliders.Length == 0)
        {
            UnityEngine.Debug.Log("No objects found within radius.");
            return;
        }
         
        Collider2D nearestCollider = colliders
            .OrderBy(c => Vector3.Distance(transform.position, c.transform.position))
            .FirstOrDefault();

        if (nearestCollider != null)
        { 
            nearestCollider.transform.position = targetObject.position;
            nearestCollider.transform.SetParent(targetObject);  
            
            holdingObject = nearestCollider.gameObject;
            UnityEngine.Debug.Log("Attached: " + holdingObject.name);
        }
    }
     
    private void OnTriggerEnter2D(Collider2D other)
    {
        UnityEngine.Debug.Log("OnTriggerEnter"); 
        ObjectReceiverScript = other.GetComponent<ObjectReceiver>();
        if (ObjectReceiverScript != null)
        {
            isCollidingWithObjectReceiver = true;
            UnityEngine.Debug.Log("OnTriggerEnter");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        UnityEngine.Debug.Log("OnTriggerExit"); 
        if (other.GetComponent<ObjectReceiver>() != null)
        {
            isCollidingWithObjectReceiver = false;
            ObjectReceiverScript = null;
            UnityEngine.Debug.Log("OnTriggerExit");
        }
    }


    void OnDrawGizmosSelected()
    { 
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}





