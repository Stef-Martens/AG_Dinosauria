using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroPlayer : MonoBehaviour
{
    private Rigidbody2D rb;
    private Collider2D coll;

    public GameObject inventory;

    public bool canMove = true;

    public AudioSource JumpSound;
    public AudioSource InteractSound;
    public string NextScene;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collider2D>();
    }

    private void Update()
    {
        if (canMove)
        {
            Move();
            if (Input.GetKeyDown(KeyCode.E))
            {
                Interact();
            }
        }
        else
        {

            if (Input.GetKeyDown(KeyCode.E))
            {
                FindObjectOfType<DialogueSystem>().NextLine();
                InteractSound.Play();
            }

        }

    }

    private void Move()
    {
        float hDirection = Input.GetAxis("Horizontal");

        if (hDirection < 0)
        {
            rb.velocity = new Vector2(-5, rb.velocity.y);
        }
        else if (hDirection > 0)
        {
            rb.velocity = new Vector2(5, rb.velocity.y);
        }

        if (Input.GetKeyDown(KeyCode.Space) && coll.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            JumpSound.Play();
            rb.velocity = new Vector2(rb.velocity.x, 10f);
        }
    }

    private void Interact()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 1f);
        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("MinigameAnimal"))
            {
                FindObjectOfType<SceneSwitch>().ChangeScene(NextScene);
            }
            if (collider.CompareTag("Animal"))
            {
                canMove = false;
                FindObjectOfType<DialogueSystem>().animal = collider.gameObject.GetComponent<AnimalIntro>();
                FindObjectOfType<DialogueSystem>().StartDialogue();
                InteractSound.Play();
                //collider.gameObject.GetComponent<AnimalIntro>().DoAction();
                break;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Item"))
        {
            inventory = collision.gameObject;
            Destroy(collision.gameObject);
            Debug.Log("Picked up an item! Inventory count: " + inventory);
        }
    }
}
