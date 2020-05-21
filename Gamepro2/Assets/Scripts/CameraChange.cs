using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraChange : MonoBehaviour
{
    public GameObject cameraOne;
    public GameObject cameraTwo;

    AudioListener camera1Aud;
    AudioListener camera2Aud;

    void Start()
    {
        camera1Aud = cameraOne.GetComponent<AudioListener>();
        camera2Aud = cameraTwo.GetComponent<AudioListener>();


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
