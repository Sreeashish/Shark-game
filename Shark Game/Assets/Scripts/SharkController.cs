using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharkController : MonoBehaviour
{
    public float moveSpeed, life, score;
    public Transform spawnPoint;
    public bool isControllable;
    public Animation sharkAnimation;
    public float minYPosition, maxYPosition,minXPosition, maxXPosition;

    void Start()
    {
        TurnControlls(true);
        life = 100;
        score = 0;
        GameController.instance.uiController.PrintScore(score);
        GameController.instance.uiController.PrintLife(life);
        transform.position = spawnPoint.position;
    }

    void Update()
    {
        SharkControl();
    }

    void SharkControl()
    {
        if (isControllable)
        {
            sharkAnimation.Play("Attack");
            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");
            Vector3 moveDirection = new Vector3(horizontalInput, verticalInput, 0).normalized;
            transform.Translate(moveDirection * moveSpeed * Time.deltaTime, Space.World);
            transform.position = new Vector3(Mathf.Clamp(transform.position.x, minXPosition, maxXPosition), Mathf.Clamp(transform.position.y, minYPosition, maxYPosition), transform.position.z);
        }
    }

    void TurnControlls(bool on)
    {
        if(on)
        {
            isControllable = true;
        }
        else
        {
            isControllable=false;
        }
    }

    public void ResetPlayer()
    {
        GameController.instance.uiController.Instructions(true);
        life = 100;
        score = 0;
        GameController.instance.uiController.PrintScore(score);
        GameController.instance.uiController.PrintLife(life);
        transform.position = spawnPoint.position;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Fish"))
        {
            NPCController fish = collision.gameObject.GetComponent<NPCController>();
            if (fish.fishType == NPCController.FishType.Edible)
            {
                score += 10;
                GameController.instance.uiController.PrintScore(score);
                if(score >= 500)
                {
                    ResetPlayer();
                }
                fish.ResetFish();
            }
            else if(fish.fishType == NPCController.FishType.Poisonous)
            {
                life -= 10;
                GameController.instance.uiController.PrintLife(life);
                if(life <= 0)
                {
                    ResetPlayer();
                }
                fish.ResetFish();
            }
        }
    }
}
