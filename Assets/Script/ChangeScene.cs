using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().name == "Start" && Input.GetMouseButtonDown(0))
        {
            SceneManager.LoadScene("Main");
        }

        if(SceneManager.GetActiveScene().name == "End" && Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene("Main");
        }
    }
}
