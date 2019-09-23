using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    
    public float speed = 500;
    public float speedM = 100;

    public GameController controller;

    private Rigidbody rbody;

    private void Start()
    {
        rbody = GetComponent<Rigidbody>();
    }
    void FixedUpdate()
    {
        if (!controller.IsPlaying)
            return;
        if (Input.touchCount > 0)
        {
            var touch = Input.GetTouch(0);
            Vector3 movement = new Vector3(touch.deltaPosition.x, 0.5f, touch.deltaPosition.y);
            rbody.AddForce(movement * speedM * Time.deltaTime);
        }
        else {
            var hor = Input.GetAxis("Horizontal");
            var ver = Input.GetAxis("Vertical");
            Vector3 movement = new Vector3(hor, 0.5f, ver);
            rbody.AddForce(movement * speed * Time.deltaTime);
        }
        
    }

    [System.Obsolete]
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "pickup")
        {
            other.gameObject.SetActive(false);
            controller.Increase();
        }
    }
    public Rigidbody GetRigidbody() {
        return rbody;
    }

    
}
