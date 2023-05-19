using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using GoogleMobileAds.Api;

public class Initializer : MonoBehaviour
{
    void Start()
    {
        Application.targetFrameRate = 60;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        MobileAds.Initialize(initStatus => {});
    }

}
