using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorMovement : Building {
    private float speed = 2f;

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.GetComponent<OreDrop>() != null)
        {
            other.gameObject.transform.Translate(transform.forward * speed * Time.fixedDeltaTime, Space.World);
        }
    }
}
