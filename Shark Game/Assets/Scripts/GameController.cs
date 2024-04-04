using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class GameController : MonoBehaviour
{
    public UiCintroller uiController;
    public SharkController sharkController;
    public List<Transform> spawnPoints;
    public Transform fishPool;
    public GameObject[] fishPrefabs;
    public List<NPCController> fishesSpawned;
    public float maxNoOfFishes;
    Vector2 waterMoveFrom = new Vector2(1, 0);
    Vector2 waterMoveTo = new Vector2(0, 0);
    public MeshRenderer water;

    public static GameController instance;
    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    IEnumerator Start()
    {
        StartCoroutine(WaveAnimations());
        yield return StartCoroutine(SpawnFishPool());
        InitialSpawn();
    }

    void InitialSpawn()
    {
        for (int i = 0; i < 3; i++)
        {
            SpawnFish();
        }
    }

    IEnumerator SpawnFishPool()
    {
        for (int i = 0; i < maxNoOfFishes; i++)
        {
            GameObject fishPrefab = fishPrefabs[Random.Range(0, fishPrefabs.Length)];

            GameObject newFish = Instantiate(fishPrefab, fishPool.position, fishPool.rotation);

            NPCController fishController = newFish.GetComponent<NPCController>();

            fishController.gameObject.SetActive(false);

            fishesSpawned.Add(fishController);

            yield return null;
        }
    }

    public void SpawnFish()
    {
        List<NPCController> inactiveFishes = new List<NPCController>();
        foreach (NPCController fish in fishesSpawned)
        {
            if (!fish.gameObject.activeInHierarchy)
            {
                inactiveFishes.Add(fish);
            }
        }
        if (inactiveFishes.Count > 0)
        {
            Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Count)];
            NPCController fishToSpawn = inactiveFishes[Random.Range(0, inactiveFishes.Count)];
            fishToSpawn.transform.position = spawnPoint.position;
            fishToSpawn.transform.rotation = spawnPoint.rotation;
            fishToSpawn.gameObject.SetActive(true);
        }
    }

    IEnumerator WaveAnimations()
    {
        while (true)
        {
            yield return null;
            if (water.material.mainTextureOffset == waterMoveTo)
            {
                water.material.DOOffset(waterMoveFrom, 10);
            }
            if (water.material.mainTextureOffset == waterMoveFrom)
            {
                water.material.DOOffset(waterMoveTo, 10);
            }
        }
    }
}
