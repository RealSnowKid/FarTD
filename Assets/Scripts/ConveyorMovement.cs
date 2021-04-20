using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorMovement : Building {
    private float speed = 2f;
    GameObject go;

    private void OnTriggerStay(Collider collider)
    {
        if (collider.gameObject.GetComponent<OreDrop>() != null)
        {
            collider.gameObject.transform.Translate(transform.forward * speed * Time.fixedDeltaTime, Space.World);
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.GetComponent<OreDrop>() != null)
        {
            go = collider.gameObject;
            if (!IsInvoking())
            {
                InvokeRepeating("PushForward", 0, 0.02f);
            }
            StartCoroutine("CancelPushInvoke");
        }
    }

    IEnumerator CancelPushInvoke()
    {
        yield return new WaitForSeconds(0.4f);
        CancelInvoke("PushForward");
    }

    private void PushForward()
    {
        if(go != null)
            go.transform.Translate(transform.forward * speed * Time.fixedDeltaTime, Space.World);
    }
}
