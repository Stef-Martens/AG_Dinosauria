using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hook : MonoBehaviour
{
    public float speed = 2f;

    private bool movingDown = true;

    void Update()
    {
        // Move the object up or down depending on its current position
        if (movingDown)
        {
            transform.Translate(Vector3.down * speed * Time.deltaTime);
            if (transform.position.y <= -3)
            {
                movingDown = false;
            }
        }
        else
        {
            transform.Translate(Vector3.up * speed * Time.deltaTime);
            if (gameObject.transform.position.y >= transform.parent.gameObject.transform.position.y - 2)
            {

                // hook at top


                if (transform == FindObjectOfType<Fish>().AttachedHook)
                {
                    FindObjectOfType<FishManager>().Lifes--;
                    FindObjectOfType<Fish>().CurrentTaps = 0;
                    FindObjectOfType<Fish>().UpdateCircleImage();
                    FindObjectOfType<Fish>().gameObject.transform.position -= new Vector3(0, -2f, 0);
                    FindObjectOfType<Fish>().ChangeState(new CooldownState());
                    FindObjectOfType<Fish>().CircleImage.transform.GetChild(0).gameObject.SetActive(false);
                }

                Destroy(gameObject);
                transform.parent.gameObject.GetComponent<Boat>().HookAtTop();

            }
        }


    }
}
