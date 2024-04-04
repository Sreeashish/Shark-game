using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCController : MonoBehaviour
{
    public enum FishType { Edible, Poisonous}
    public FishType fishType;
    public float swimSpeed;
    public Animation fishAnimation;

    private void OnEnable()
    {
        StartCoroutine(Swim());
    }

    IEnumerator Swim()
    {
        while(gameObject.activeInHierarchy)
        {
            yield return null;
            fishAnimation.Play("Swim");
            transform.Translate(transform.forward * swimSpeed * Time.deltaTime, Space.World);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("End"))
        {
            ResetFish();
        }
    }

    public void ResetFish()
    {
        GameController.instance.SpawnFish();
        gameObject.SetActive(false);
    }
}
