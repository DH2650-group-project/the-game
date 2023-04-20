using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIFollowObject : MonoBehaviour
{

    public GameObject target;
    public Vector3 offset = new(0, 1, 0);


    // Update is called once per frame
    void Update()
    {
        transform.position = Camera.main.WorldToScreenPoint(target.transform.position + offset);
    }
}
