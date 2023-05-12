using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelComplete : MonoBehaviour
{
    public FlyCount FlyCount;
    public bool LevelFinished = false;


    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //if (FlyCount.FliesTakenCount == 4) { FindObjectOfType<SceneSwitch>().ChangeScene("QuizChameleon"); };
    }
}
