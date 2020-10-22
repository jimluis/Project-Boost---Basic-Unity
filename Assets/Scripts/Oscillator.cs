using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent] // It tells the script that we only allow one script to be part of the object
public class Oscillator : MonoBehaviour
{

    [SerializeField] Vector3 movementVector;// = new Vector3(10f, 10f, 10f);

    //todo remove from inspector later
    float movementFactor; // 0 for not moved, 1 for fully moved
    Vector3 startingPos;
    [SerializeField] float period = 2f; // the period is the time to complete one full cycle
    const float tau = Mathf.PI * 2f; //Tau is 2 Pi (a circle), so this is about 6.28 

    // Start is called before the first frame update
    void Start()
    {
        startingPos = transform.position;
       // print("startingPos: "+ startingPos);
    }

    // Update is called once per frame
    void Update()
    {
        if(period >= Mathf.Epsilon)
        {
            float playTime = Time.time; // Time.time - Time since the beginning of the game
                                        //If the playTime is 10 and the period is 2 then that will be 5 cycles
            float cycles = playTime / period; //Grows continually from 0
            //print("cycles: " + cycles);
            float rawSinWave = Mathf.Sin(cycles * tau);
            //print("rawSinWave: " + rawSinWave);

            movementFactor = rawSinWave / 2f + 0.5f;
            Vector3 offset = movementVector * movementFactor;
            transform.position = startingPos + offset;
        }

    }
}
