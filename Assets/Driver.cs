using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Driver : MonoBehaviour
{

    [SerializeField] float turnSpeed = 1f;
    [SerializeField] float moveSpeed = 1f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float steerInput = (Input.GetAxis("Horizontal"))*(-80f)*Time.deltaTime;
        float moveInput = (Input.GetAxis("Vertical"))*(10f)*Time.deltaTime;
        transform.Rotate(0, 0, steerInput);
        transform.Translate(0, moveInput, 0);
        
    }
}
