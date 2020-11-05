using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Camera Shake Youtube Renaissance Coders Unity 3D C# Scripting Camera Shake https://www.youtube.com/watch?v=GDatJi6HSYE&list=PLPL9_77MOo2L3BSNQgraIOByxsdEwE0sp&index=4&t=0s 10/11/2019 
public class CameraShake : MonoBehaviour
{
    public float Power = 0.1f;
    public float Duration = 0.2f;
    public Transform MainCamera;
    public float SlowDownAmount = 1.0f;
    public static bool ShouldShake = false;

    Vector3 StartPosition;
    float InitialDuration;

    // Start is called before the first frame update
    void Start()
    {
        
        MainCamera = Camera.main.transform;
        StartPosition = MainCamera.localPosition;
        InitialDuration = Duration;
    }

    // Update is called once per frame
    void Update()
    {
        if (ShouldShake)
        {
            if (Duration > 0)
            {
                MainCamera.localPosition = StartPosition + Random.insideUnitSphere * Power;
                Duration -= Time.deltaTime * SlowDownAmount;
            }

            else
            {
                ShouldShake = false;
                Duration = InitialDuration;
                MainCamera.localPosition = StartPosition;
            }
        }

    }
}