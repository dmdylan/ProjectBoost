using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    [SerializeField] float rcsThrust = 100f;
    [SerializeField] float mainThrust = 100f;
    Rigidbody rigidBody;
    new AudioSource audio;
    
    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        Rotate();
        Thrust();
    }

    void OnCollisionEnter(Collision collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Friendly":
                print("ok"); // todo remove 
                break;
            case "Fuel":
                print("fuel");
                UnityEngine.Object.Destroy(GameObject.Find("Fuel"));
                break;
            default:
                print("Dead");
                Destroy(this.gameObject);
                break;
        }
    }

    private void Rotate()
    {
        rigidBody.freezeRotation = true; //manual control of rotation
        float rotationThisFrame = rcsThrust * Time.deltaTime;

        if (Input.GetKey(KeyCode.A))
        {      
            transform.Rotate(Vector3.forward * rotationThisFrame);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(-Vector3.forward * rotationThisFrame);
        }

        rigidBody.freezeRotation = false; //physics takes control of rotation
    }

    private void Thrust()
    {
        float thrustThisFrame = mainThrust * Time.deltaTime;

        if (Input.GetKey(KeyCode.Space))
        {
            rigidBody.AddRelativeForce(Vector3.up * thrustThisFrame);
            if (!audio.isPlaying)
            {
                audio.Play();
            }
        }
        else
        {
            audio.Stop();
        }
    }
}
