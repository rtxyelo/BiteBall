using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseController : MonoBehaviour
{
    public void PauseCheck()
    {
        if (Time.timeScale == 0f)
            Time.timeScale = 1.0f;

        else if (Time.timeScale == 1f)
            Time.timeScale = 0f;
    }
}
