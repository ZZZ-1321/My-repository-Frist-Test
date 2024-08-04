using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward*Time.deltaTime*Input.GetAxis("Vertical")*5);
        transform.Rotate(Vector3.up*Time.deltaTime*Input.GetAxis("Horizontal")*100);
    }
}
