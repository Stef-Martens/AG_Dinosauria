using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class GenderManager : MonoBehaviour
{
    public GameObject Male;
    public GameObject Female;
    void Start()
    {
        Male.SetActive(false);
        Female.SetActive(false);

        if (FindObjectOfType<FirebaseManager>().MadeUser.male)
        {
            Male.SetActive(true);
            FindObjectOfType<CinemachineVirtualCamera>().Follow = Male.transform;
        }

        else
        {
            Female.SetActive(true);
            FindObjectOfType<CinemachineVirtualCamera>().Follow = Female.transform;
        }



    }

    // Update is called once per frame
    void Update()
    {

    }
}
