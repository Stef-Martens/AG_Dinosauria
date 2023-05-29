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

    public float Speed = 10f;

    public Sprite EmptySprite;

    public Image ItemImage;
    public Text ItemText;

    [SerializeField]
    private LayerMask _layerMask;
    public float Jump = 15;
    private Animator _animator;
    private SpriteRenderer _spriteRenderer;
    private bool _isFacingRight;

    public void ClearInventory()
    {
        inventory = new ItemInventory(EmptySprite);
    }


    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
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
        _animator.SetFloat("Speed", Mathf.Abs(hDirection));

        if (hDirection < 0)
        {
            rb.velocity = new Vector2(-Speed, rb.velocity.y);
            _spriteRenderer.flipX = true;
        }
        else if (hDirection > 0)
        {
            rb.velocity = new Vector2(Speed, rb.velocity.y);
            _spriteRenderer.flipX = false;
        }

        if (hDirection < 0 && !_isFacingRight)
        {
            Flip();
        }

        if (hDirection > 0 && _isFacingRight)
        {
            Flip();
        }

        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
        {
            JumpSound.Play();
            rb.AddForce(new Vector2(0f, Jump), ForceMode2D.Impulse);
            /* rb.velocity = new Vector2(rb.velocity.x, Jump);
             rb.gravityScale = 2f;*/
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

                if (collider.gameObject.name == "Ant" && (inventory.Name == "Honey" || inventory.Name == "Fruit"))
                {
                    FindObjectOfType<Ant>().CurrentDialogue = FindObjectOfType<Ant>().secondDialogue;
                }

                if (collider.gameObject.name == "Dolphin" && inventory.Name == "Ball" && FindObjectOfType<BabyTurtle>().finished)
                {
                    FindObjectOfType<Dolphin>().CurrentDialogue = FindObjectOfType<Dolphin>().secondDialogue;
                }


                FindObjectOfType<DialogueSystem>().animal = collider.gameObject.GetComponent<AnimalIntro>();
                FindObjectOfType<DialogueSystem>().StartDialogue();
                InteractSound.Play();
                //collider.gameObject.GetComponent<AnimalIntro>().DoAction();
                break;
            }
        }
    }

    bool IsGrounded()
    {
        float extraHeight = 0.5f;
        RaycastHit2D raycasthit = Physics2D.Raycast(coll.bounds.center, Vector2.down, coll.bounds.extents.y + extraHeight, _layerMask);
        if (raycasthit.collider != null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

    }

    private void Flip()
    {
        _isFacingRight = !_isFacingRight;
    }
}
