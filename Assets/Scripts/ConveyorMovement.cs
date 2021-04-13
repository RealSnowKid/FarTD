using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorMovement : Building {
    private float speed = 2f;
    Vector3 lastPosition = Vector3.zero;

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
            float time = 1.8f;
            float passed = 0;
            while (time >= passed)
            {
                PushForward(collider.gameObject);
                passed += 0.1f;
            }
        }
    }

    IEnumerator Inertia(GameObject go)
    {
        float time = 0.5f;
        float passed = 0;
        while (time >= passed)
        {
            yield return new WaitForSeconds(0.1f);
            PushForward(go);
            passed += 0.1f;
        }
    }

    private void PushForward(GameObject go)
    {
        //for (int i = 0; i < 25; i++)
        //{
            go.transform.Translate(transform.forward * 2 * Time.fixedDeltaTime, Space.World);
        //}
    }
}
