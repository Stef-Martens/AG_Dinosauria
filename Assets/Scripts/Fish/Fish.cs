using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fish : MonoBehaviour
{
    public float speed = 5f;
    public float rotationSpeed = 150f;
    //public Animator animator;
    public Rigidbody2D rb;
    public Transform AttachedHook;

    private StateMachine<IFishState> stateMachine;

    public float CurrentTaps = 0;
    public float RequiredTaps = 50;
    public Image CircleImage;

    public AudioSource FishSound;
    public AudioSource PickupSound;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        stateMachine = new StateMachine<IFishState>(new SwimmingState());
        stateMachine.CurrentState.OnEnterState(this);
    }

    private void Update()
    {
        stateMachine.CurrentState.Update(this);
    }

    private void FixedUpdate()
    {
        stateMachine.CurrentState.FixedUpdate(this);

    }

    public void ChangeState(IFishState newState)
    {
        stateMachine.ChangeState(newState);
    }

    void OnTriggerEnter2D(Collider2D Collider)
    {
        switch (Collider.gameObject.tag)
        {
            case "Hook":
                if (stateMachine.CurrentState.GetType() == typeof(SwimmingState))
                {
                    AttachedHook = Collider.gameObject.transform;
                    ChangeState(new FishHookState());
                }
                break;

            case "Plastic":
                break;

            default:
                if (stateMachine.CurrentState.GetType() == typeof(SwimmingState))
                {
                    FindObjectOfType<FishManager>().AlgaeCount++;
                    FindObjectOfType<FishManager>().EditPickupsText();
                    Destroy(Collider.gameObject);
                    PickupSound.Play();
                }
                break;
        }

    }

    public void UpdateCircleImage()
    {
        // Calculate the fill amount based on the current number of taps
        float fillAmount = CurrentTaps / RequiredTaps;
        CircleImage.fillAmount = fillAmount;

        // Check if the circle is fully filled
        if (fillAmount >= 1)
        {
            Destroy(AttachedHook.gameObject);
            CurrentTaps = 0;
            CircleImage.fillAmount = 0;
            ChangeState(new CooldownState());
        }
    }
}
