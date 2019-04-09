using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{

    Rigidbody rigidbody;
    AudioSource audioSource;
    bool isAudioPlaying = false;
    // [SerializeField] makes the variable as a member variable, and it acts as public, 
    // it can be accessed from the inspector, but
    // the only difference is that this variable can not be
    // modified from other scripts 
    [SerializeField] float rcsThrust = 100f;
    [SerializeField] float thrustPower = 100f;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        Thrust();
        Rotate();
    }

    void Rotate()
    {
        rigidbody.freezeRotation = true; // Take manual control of rotatio
        float rotationThisFrame = rcsThrust * Time.deltaTime; //Time frame independent


        if (Input.GetKey(KeyCode.A))
        {
            print("rcsThrust: "+ rcsThrust + " - Time.deltaTime: " + Time.deltaTime);
            print("rotationThisFrame: " + rotationThisFrame);
            print("transform.Rotate: " + Vector3.forward * rotationThisFrame);
            transform.Rotate(Vector3.forward * rotationThisFrame);
        }

        else if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(-Vector3.forward * rotationThisFrame);
        }

        rigidbody.freezeRotation = false; // resumes physics control of rotation
    }

    void Thrust()
    {

        //float tPower = thrustPower * Time.deltaTime;

        if (Input.GetKey(KeyCode.Space))
        {
            print("Thrusting");
            rigidbody.AddRelativeForce(Vector3.up * thrustPower);


            if (!audioSource.isPlaying)
                audioSource.Play();

        }
        else
        {
            audioSource.Stop();
        }
    }


    private void OnCollisionEnter(Collision collision)
    {
        print("Collision");

        switch(collision.gameObject.tag)
        {
            case "Friendly":
                print("OK");
                break;
            case "Platform":
                print("OK");
                break;
            case "Ground":
                print("Dead");
                break;
            default:
                print("Dead");
                break;
        }

    }
}
