using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hook : MonoBehaviour
{
    public float speed = 6f;
    public float minY = 0f;
    public float maxY = 20f;
    private bool movingDown = true;

    void Start()
    {
        maxY = transform.position.y;
    }

    void Update()
    {
        // Move the object up or down depending on its current position
        if (movingDown)
        {
            transform.Translate(Vector3.down * speed * Time.deltaTime);
            if (transform.position.y <= minY)
            {
                movingDown = false;
            }
        }
        else
        {
            transform.Translate(Vector3.up * speed * Time.deltaTime);
            /*if (transform.position.y >= maxY && FindObjectOfType<FishForward>().OnHook && FindObjectOfType<FishForward>().Hookske == transform.GetChild(0).transform)
            {
                FindObjectOfType<ButtonMashing>().currentTaps = 0;
                FindObjectOfType<ButtonMashing>().circleImage.enabled = false;
                FindObjectOfType<FishForward>().OnHook = false;
                FindObjectOfType<Hooks>().PlayerHit();
                FindObjectOfType<FishForward>().StartTimer();
                Destroy(gameObject);
            }*/
        }
    }
}
