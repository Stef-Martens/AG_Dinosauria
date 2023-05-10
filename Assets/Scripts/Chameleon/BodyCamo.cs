using System.Collections.Generic;
using UnityEngine;

public class BodyCamo : MonoBehaviour
{
    public bool IsCamo = false;

    private GameObject _base;
    private GameObject _camoBrown;
    private GameObject _camoGreen;

    private List<GameObject> _hidingSpotsList = new List<GameObject>();

    void Start()
    {
        _base = this.transform.GetChild(0).gameObject;
        _camoBrown = this.transform.GetChild(1).gameObject;
        _camoGreen = this.transform.GetChild(2).gameObject;

        foreach (BaseHidingSpot baseHidingSpot in FindObjectsOfType<BaseHidingSpot>())
            _hidingSpotsList.Add(baseHidingSpot.gameObject);
    }

    void Update()
    {
        ChangeCamoColor();
    }

    private void ChangeCamoColor()
    {
        foreach (GameObject hidingspot in _hidingSpotsList)
        {
            if (hidingspot.GetComponent<Trunk>())
            { 
                if(hidingspot.GetComponent<Trunk>().TriggerHitObject != null) 
                {
                    ChangeToBrown();
                    break;
                }
                else
                {
                    ChangeToBase();
                }
            }

            if (hidingspot.GetComponent<LeavesPile>())
            {
                if (hidingspot.GetComponent<LeavesPile>().TriggerHitObject != null)
                {
                    ChangeToGreen();
                    break;
                }
                else
                {
                    ChangeToBase();
                }
            }
        }
    }

    private void ChangeToBase()
    {
        _base.SetActive(true);
        _camoBrown.SetActive(false);
        _camoGreen.SetActive(false);

        IsCamo = false;
    }

    private void ChangeToBrown()
    {
        _base.SetActive(false);
        _camoBrown.SetActive(true);
        _camoGreen.SetActive(false);

        IsCamo = true;
    }

    private void ChangeToGreen()
    {
        _base.SetActive(false);
        _camoBrown.SetActive(false);
        _camoGreen.SetActive(true);

        IsCamo = true;
    }
}