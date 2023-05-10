using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IntroPlayer : MonoBehaviour
{
    private Rigidbody2D rb;
    private Collider2D coll;

    public ItemInventory inventory;

    public bool canMove = true;

    public AudioSource JumpSound;
    public AudioSource InteractSound;
    public string NextScene;

    private Vector3 defaultScale;

    public float speed = 8f;

    public Sprite EmptySprite;

    public Image ItemImage;
    public Text ItemText;

    public void ClearInventory()
    {
        inventory = new ItemInventory(EmptySprite);
    }


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collider2D>();
        defaultScale = transform.localScale;
        ClearInventory();
    }

    private void Update()
    {
        ItemImage.sprite = FindObjectOfType<IntroPlayer>().inventory.Image;
        ItemText.text = FindObjectOfType<IntroPlayer>().inventory.Name;
        if (canMove)
        {
            Move();
            if (Input.GetKeyDown(KeyCode.E))
            {
                Interact();
            }
            /*if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift))
            {
                if()
            }*/
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
            rb.velocity = new Vector2(-speed, rb.velocity.y);
            Vector3 newScale = defaultScale;
            newScale.x = -defaultScale.x;
            transform.localScale = newScale;
        }
        else if (hDirection > 0)
        {
            rb.velocity = new Vector2(speed, rb.velocity.y);
            Vector3 newScale = defaultScale;
            newScale.x = defaultScale.x;
            transform.localScale = newScale;

        }

        if (Input.GetKeyDown(KeyCode.Space) && coll.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            JumpSound.Play();
            rb.velocity = new Vector2(rb.velocity.x, 10f);
        }
    }

    private void Interact()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 3f);
        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("MinigameAnimal"))
            {
                FindObjectOfType<SceneSwitch>().ChangeScene(NextScene);
            }
            if (collider.CompareTag("Animal"))
            {
                canMove = false;

                if (collider.gameObject.name == "Skunk" && inventory.Name == "Apple")
                {
                    FindObjectOfType<Skunk>().CurrentDialogue = FindObjectOfType<Skunk>().secondDialogue;
                }

                if (collider.gameObject.name == "Beetle" && inventory.Name == "Ladybug")
                {
                    FindObjectOfType<Beetle>().CurrentDialogue = FindObjectOfType<Beetle>().secondDialogue;
                }

                if (collider.gameObject.name == "Kangaroo" && inventory.Name == "Ball")
                {
                    FindObjectOfType<Kangeroo>().CurrentDialogue = FindObjectOfType<Kangeroo>().secondDialogue;
                }

                if (collider.gameObject.name == "RedPanda" && inventory.Name == "Milk")
                {
                    FindObjectOfType<RedPanda>().CurrentDialogue = FindObjectOfType<RedPanda>().secondDialogue;
                }

                if (collider.gameObject.name == "Lemur" && (inventory.Name == "Honey" || inventory.Name == "Fruit"))
                {
                    FindObjectOfType<Lemur>().CurrentDialogue = FindObjectOfType<Lemur>().secondDialogue;
                }


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

    }
}
