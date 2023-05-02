using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boat : MonoBehaviour
{
    public float speed = 2f;
    public float leftBound = -6f;
    public float rightBound = 6f;
    public int startDirection = 1;

    private Vector3 moveDirection;
    private SpriteRenderer spriteRenderer;

    public GameObject Hook;

    private void Start()
    {
        moveDirection = (startDirection > 0) ? Vector3.right : Vector3.left;
        spriteRenderer = GetComponent<SpriteRenderer>();
        Invoke("SpawnHook", Random.Range(1f, 3f));
    }

    private void Update()
    {
        transform.position += moveDirection * speed * Time.deltaTime;

        if (transform.position.x <= leftBound || transform.position.x >= rightBound)
        {
            moveDirection *= -1;
            spriteRenderer.flipX = !spriteRenderer.flipX;
        }
    }

    public void HookAtTop()
    {
        Invoke("SpawnHook", Random.Range(1f, 3f));
    }

    void SpawnHook()
    {
        GameObject SpawnedHook = Instantiate(Hook, transform.position, Quaternion.identity);
        SpawnedHook.transform.parent = gameObject.transform;
    }

}
