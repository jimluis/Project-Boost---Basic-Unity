using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{

    Rigidbody rigidbody;
    AudioSource audioSource;
    bool isAudioPlaying = false;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessInput();
    }

    void ProcessInput()
    {



        if (Input.GetKey(KeyCode.Space))
        {
            print("Thrusting");
            rigidbody.AddRelativeForce(Vector3.up);


            if (!audioSource.isPlaying)
                audioSource.Play();

        }
        else
        {
            audioSource.Stop();
        }

        // audioSource.Stop();

        if (Input.GetKey(KeyCode.A))
        {
            print("Rotating left");
            transform.Rotate(Vector3.forward);
        }

        else if(Input.GetKey(KeyCode.D))
        {
            print("Rotating Right");
            transform.Rotate(-Vector3.forward);
        }
    }
}
