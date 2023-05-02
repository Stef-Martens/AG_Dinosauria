using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FlyBehaviourV2 : MonoBehaviour
{
    public ChameleonMoveV2 ChameleonMoveV2;
    public GameObject HidingSpot;

    public float FlyCloserSpeed = 0.5f;

    private bool _isCommingCloser;

    public float RetreatSpeed = 0.5f;

    private Vector3 _startCommingCloserPos;

    private bool _canComeCloser = true;

    private bool _doOnce = true;


    // Update is called once per frame
    void Update()
    {
        if (!ChameleonMoveV2.IsHiding)
        {
            Vector3 moveTowards = Vector3.MoveTowards(this.transform.position,
                                               _startCommingCloserPos,
                                               Time.deltaTime * RetreatSpeed);

            this.transform.position = moveTowards;

            if (this.transform.position == _startCommingCloserPos)
                _doOnce = false;

            if (!_doOnce)
            {
                _isCommingCloser = false;
                _canComeCloser = true;
            }
        }

        if (ChameleonMoveV2.IsHiding)
        {
            ComeCloser();
            _isCommingCloser = true;
        }

        if (_canComeCloser)
        {
            _startCommingCloserPos = this.transform.position;
            _canComeCloser = false;
            _doOnce = true;
        }
    }

    private void ComeCloser()
    {
        var pos = new Vector3(this.transform.position.x,
            _startCommingCloserPos.y - 4,
            0);

        Vector3 moveTowards = Vector3.MoveTowards(this.transform.position,
                                                       pos,
                                                       Time.deltaTime * FlyCloserSpeed);

        this.transform.position = moveTowards;
    }
}
