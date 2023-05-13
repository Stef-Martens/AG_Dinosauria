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

    [SerializeField]
    private AudioSource _eatingAudioSource;
    [SerializeField]
    private AudioSource _nectarAudioSource;

    private void Start()
    {
        _renderer= GetComponent<SpriteRenderer>();

        _renderer.enabled = false;
        _collider.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Nectar nectar = collision.gameObject.GetComponent<Nectar>();
        SinusoidalMove sinusMove = collision.gameObject.GetComponent<SinusoidalMove>();

        if (nectar != null )
        {
            _nectarAudioSource.Play();
            Destroy(collision.gameObject);
        }

        if (sinusMove != null)
        {
            //collision.gameObject.transform.parent = _container.transform;
            //collision.gameObject.transform.position = _container.transform.position;
            Destroy(collision.gameObject);
            _eatingAudioSource.Play();
        }
    }

    private void Update()
    {
        if(Input.GetKey(KeyCode.Space))
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
