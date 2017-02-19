using UnityEngine;
using System.Collections;

// This script is attached to the camera. It shakes it. By moving the local transform position.
// The camera is made the child of a empty gameobject which is its parent transform.
// here's a reference: http://www.xmptgames.co.uk/blog/?p=788
public class Shaker : MonoBehaviour
{
    public float shakeAmount = 0.25f;
    public float timeScale = 1.0f;

    //private Vector3 startPos;
    private float shakeTime = 0.0f;

    public void StartShake(float time)
    {
        // check not already shaking
        //if (shakeTime <= 0.0f) {
        // cache object position
        //startPos = transform.position;
        //}

        shakeTime = time;
    }

    void Update()
    {
        // the object's local position is a random value, scaled by the amount of shake, 
        // scaled by how long the object has been shaking.
        transform.localPosition = Random.insideUnitSphere * shakeAmount * shakeTime;

        shakeTime -= Time.deltaTime * timeScale;

        // check if it's time to stop shaking.
        if (shakeTime <= 0.0f)
        {
            // clamp back to zero.
            shakeTime = 0.0f;
            // reset position back to pre-shake value.
            transform.localPosition.Set(0.0f, 0.0f, 0.0f);
        }
    }
}