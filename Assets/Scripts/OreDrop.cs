using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OreDrop : MonoBehaviour {
    public Ore ore;
    public Item Item;

    private Vector3 lastPosition = new Vector3();

    private void Start()
    {
        InvokeRepeating("CheckIfMoved", 0, 2);
    }

    void CheckIfMoved()
    {
        lastPosition = this.transform.position;
        StartCoroutine("DidItMove");
    }

    IEnumerator DidItMove()
    {
        yield return new WaitForSeconds(1f);
        if (this.transform.position == lastPosition)
        {
            Destroy(gameObject);
        }
    }
}
