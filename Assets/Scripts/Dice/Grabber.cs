using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grabber : MonoBehaviour
{

    private GameObject selectedObject;
    private bool isDragging = false;
    public Player grabbingPlayer;
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if(selectedObject == null)
            {
                RaycastHit hit = castRay();
                if(hit.collider != null)
                {
                    if (!hit.collider.CompareTag("Dice"))
                    {
                        return;
                    }
                    
                    selectedObject = hit.collider.gameObject;
                    selectedObject.GetComponent<Rigidbody>().useGravity = false;
                    Cursor.visible = false;
                    
                }
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            if(selectedObject != null)
            {
                
                Vector3 position = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.WorldToScreenPoint(selectedObject.transform.position).z);
                Vector3 worldPosition = Camera.main.ScreenToWorldPoint(position);
                selectedObject.transform.position = new Vector3(worldPosition.x, 0.5f, worldPosition.z);
                selectedObject.GetComponent<Rigidbody>().useGravity = true;
                dice.diceInstance.throwDice();

                selectedObject = null;
                Cursor.visible = true;
            }
        }
        if(selectedObject != null)
        {
            Vector3 position = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.WorldToScreenPoint(selectedObject.transform.position).z);
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(position);
            selectedObject.transform.position = new Vector3(worldPosition.x, 0.5f, worldPosition.z);
        }
    }
    private RaycastHit castRay()
    {
        Vector3 screenMousePosFar = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.farClipPlane);
        Vector3 screenMousePosNear = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane);
        Vector3 worldMousePosFar = Camera.main.ScreenToWorldPoint(screenMousePosFar);
        Vector3 worldMousePosNear = Camera.main.ScreenToWorldPoint(screenMousePosNear);
        RaycastHit hit;
        Physics.Raycast(worldMousePosNear, worldMousePosFar - worldMousePosNear, out hit);
        return hit;
    }
}
