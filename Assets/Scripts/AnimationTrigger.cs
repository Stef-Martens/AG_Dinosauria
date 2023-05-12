using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationTrigger : MonoBehaviour
{
    [SerializeField]
    private Animator _animator;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<IntroPlayer>() != null)
        {
            _animator.enabled = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<IntroPlayer>() != null)
        {
            _animator.enabled = false;
        }
    }
}
