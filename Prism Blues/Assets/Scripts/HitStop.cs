using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitStop : MonoBehaviour
{
    bool waiting;
    public float cooldown = 1;
    public void Stop(float duration)
    {
        if (waiting) 
        {
            return;
        }
        
        StartCoroutine(Wait(duration));
    }

    IEnumerator Wait(float duration) 
    {
        Time.timeScale = 0.05f;
        waiting = true;
        yield return new WaitForSecondsRealtime(duration);
        Time.timeScale = 1.0f;
        yield return new WaitForSecondsRealtime(cooldown);
        waiting = false;
    }
}
