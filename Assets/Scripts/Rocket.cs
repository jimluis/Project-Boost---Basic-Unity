using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    enum State {Alive, Dying, Trasending};
    State state = State.Alive;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        //todo stop sound on death
        if(state == State.Alive)
        {
            Thrust();
            Rotate();
        }
    }

    void Rotate()
    {
        rigidbody.freezeRotation = true; // Take manual control of rotatio
        float rotationThisFrame = rcsThrust * Time.deltaTime; //Time frame independent


        if (Input.GetKey(KeyCode.A))
        {
            // print("rcsThrust: "+ rcsThrust + " - Time.deltaTime: " + Time.deltaTime);
            // print("rotationThisFrame: " + rotationThisFrame);
            // print("transform.Rotate: " + Vector3.forward * rotationThisFrame);
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
          //  print("Thrusting");
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
        //print("Collision");

        if (state != State.Alive)
            return;

        switch(collision.gameObject.tag)
        {
            case "Friendly":
                print("OK");
                break;
            case "finish":
                //  print("Hit finish");
                state = State.Trasending;
              //Loads the method passed as a string param
              //After one second '1f'
                Invoke("LoadNextScene", 1f);
                break;
            default:
                print("Hit something deadly");
                state = State.Dying;
                //Loads the method passed as a string param
                //After one second '1f'
                Invoke("LoadFirstLevel", 1f);
                break;
        }

    }

    private void LoadNextLevel()
    {
        SceneManager.LoadScene(1); // The number is the index of the scene
    }

    private void LoadFirstLevel()
    {
        SceneManager.LoadScene(0); // The number is the index of the scene
    }
}
