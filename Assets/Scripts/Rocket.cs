using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Rocket : MonoBehaviour
{

    // [SerializeField] makes the variable as a member variable, and it acts as public, 
    // it can be accessed from the inspector, but
    // the only difference is that this variable can not be
    // modified from other scripts 
    [SerializeField] float levelLoadTime = 1f;
    [SerializeField] float rcsThrust = 100f;
    [SerializeField] float thrustPower = 100f;
    [SerializeField] AudioClip mainEngineSound;
    [SerializeField] AudioClip deadSound;
    [SerializeField] AudioClip nextLevelSound;

    [SerializeField] ParticleSystem mainEngineParticles;
    [SerializeField] ParticleSystem deadSoundParticles;
    [SerializeField] ParticleSystem nextLevelSoundParticles;

    Rigidbody rigidbody;
    AudioSource audioSource;
    bool isAudioPlaying = false;


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
            RespondToThrustInput();
            RespondToRotateInput();
        }
    }

    void RespondToRotateInput()
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

    void RespondToThrustInput()
    {

        //float tPower = thrustPower * Time.deltaTime;

        if (Input.GetKey(KeyCode.Space))
        {
            //  print("Thrusting");
            ApplyingThrust();
        }
        else
        {
            //print("Thrust sound stopped");
            audioSource.Stop();
            mainEngineParticles.Stop();
        }
    }

    private void ApplyingThrust()
    {
        rigidbody.AddRelativeForce(Vector3.up * thrustPower);

        if (!audioSource.isPlaying)
        {
            //print("Thrust sound playing");
            audioSource.PlayOneShot(mainEngineSound);
        }

        mainEngineParticles.Play();
    }

    private void OnCollisionEnter(Collision collision)
    {
        //print("Collision");

        if (state != State.Alive)
            return;

        switch(collision.gameObject.tag)
        {
            case "Friendly":
                // print("OK");
                break;
            case "finish":
                StartSuccessSequence();
                break;
            default:
                StartDeathSequence();
                break;
        }

    }

    private void StartDeathSequence()
    {
        //print("Hit something deadly");
        state = State.Dying;
        audioSource.Stop();
        audioSource.PlayOneShot(deadSound);
        deadSoundParticles.Play();
        //Loads the method passed as a string param
        //After one second '1f'
        Invoke("LoadFirstLevel", levelLoadTime);
    }

    private void StartSuccessSequence()
    {
        //  print("Hit finish");
        state = State.Trasending;
        audioSource.Stop();
        audioSource.PlayOneShot(nextLevelSound);
        nextLevelSoundParticles.Play();
        //Loads the method passed as a string param
        //After one second '1f'
        Invoke("LoadNextLevel", levelLoadTime);
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
