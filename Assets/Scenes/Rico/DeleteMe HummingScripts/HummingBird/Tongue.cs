using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tongue : MonoBehaviour
{
    [SerializeField]
    private GameObject _container;
    private SpriteRenderer _renderer;
    [SerializeField]
    private Collider2D _collider;

    private Bird _bird;
    private HingeJoint2D _hingeJoint;

    private void Start()
    {
        _bird = FindObjectOfType<Bird>();
        _hingeJoint = GetComponent<HingeJoint2D>();
        _renderer= GetComponent<SpriteRenderer>();
        _renderer.enabled = false;
        _collider.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Nectar nectar = collision.gameObject.GetComponent<Nectar>();
        SinusoidalMove sinusMove = collision.gameObject.GetComponent<SinusoidalMove>();


        if (nectar != null || sinusMove != null)
        {
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.CompareTag("Insect"))
        {
            collision.gameObject.transform.parent = _container.transform;
            collision.gameObject.transform.position = _container.transform.position;
            Destroy(collision.gameObject, 0.5f);
        }
    }

    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if(collision.gameObject.CompareTag("Insect"))
    //    {
    //        collision.gameObject.transform.parent = _container.transform;
    //        collision.gameObject.transform.position = _container.transform.position;
    //        Destroy(collision.gameObject, 0.5f);
    //    }
    //}


    private void Update()
    {
        if(Input.GetKey(KeyCode.Space)/*&& _bird.insideFlower == true*/)
        {
            _renderer.enabled = true;
            _collider.enabled = true;
        }
        else
        {
            _collider.enabled = false;
            _renderer.enabled = false;
        }
    }
}
