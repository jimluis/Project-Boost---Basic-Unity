using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{

   // [SerializeField] float frequency;
    Vector3 startingPos;

    void Start()
    {
        startingPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        float x = Mathf.Sin(Time.time);
        print("x: " + x+ " - Time.time: "+ Time.time);
        startingPos = new Vector3(x, 0f, 0f);
        transform.position = startingPos;
        print(">>> startingPos: "+ startingPos);
    }
}
