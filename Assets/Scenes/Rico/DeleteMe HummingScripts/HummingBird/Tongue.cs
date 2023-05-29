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

    private float _currentStamina;
    private float _maxStamina = 100f;
    [SerializeField]
    private StaminaBar _staminaBar;
    [SerializeField]
    private GameObject _gameOver;

    private void Start()
    {
        _currentStamina = _maxStamina;
        if (_staminaBar) _staminaBar.UpdateStaminaBar(_maxStamina, _currentStamina);
        _renderer = GetComponent<SpriteRenderer>();

        _renderer.enabled = false;
        _collider.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Nectar nectar = collision.gameObject.GetComponent<Nectar>();
        SinusoidalMove sinusMove = collision.gameObject.GetComponent<SinusoidalMove>();

        if (nectar != null)
        {
            if (_currentStamina >= _maxStamina)
            {
                return;
            }
            else
            {
                _currentStamina += 5f;
            }
            _nectarAudioSource.Play();
            Destroy(collision.gameObject);
        }

        if (sinusMove != null)
        {
            //collision.gameObject.transform.parent = _container.transform;
            //collision.gameObject.transform.position = _container.transform.position;
            if (_currentStamina >= _maxStamina)
            {
                return;
            }
            else
            {
                _currentStamina += 10f;
            }
            Destroy(collision.gameObject);
            FindObjectOfType<PickupsText>().ShowText("Mosquito");
            _eatingAudioSource.Play();
        }
    }

    private void Update()
    {
        _currentStamina -= 5f * Time.deltaTime;
        if (_staminaBar) _staminaBar.UpdateStaminaBar(_maxStamina, _currentStamina);

        if (_currentStamina <= 0)
        {
            _gameOver.SetActive(true);
            GameOver gameOver = _gameOver.GetComponent<GameOver>();
            gameOver.StartGameOver("Oh no you ran out of enery :(");
        }

        if (Input.GetKey(KeyCode.Space))
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
