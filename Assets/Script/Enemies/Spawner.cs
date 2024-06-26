using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SpawnInfo
{
    public GameObject enemy;
    public float spawnChance;
}

public class Spawner : MonoBehaviour
{
    [SerializeField] private List<SpawnInfo> spawnInfos; //Liste des informations du monstre � faire spawn
    [SerializeField] private GameObject defaultEnemy;
    [SerializeField] private GameObject bossPrefab;
    [SerializeField] private float spawnRadius = 10f;
    public int spawnAmount = 3;
    public int newSpawnAmount = 3;
    public bool bossSpawnedForCurrentLevel = false;

    private static Spawner instance;

    public static Spawner GetInstance() => instance;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        float totalPercentage = 0f;
        foreach (var info in spawnInfos)
        {
            totalPercentage += info.spawnChance;
        }

        //V�RIFIE QUE LE TOTAL DES POURCENTAGES NE D�PASSE PAS 100%
        if (totalPercentage > 100f)
        {
            Debug.LogWarning("TOTAL SPAWN CHANCE EXCEED 100%, SOME ENEMY WONT SPAWN | TOTALPERCENTAGE : " + totalPercentage);
        }

        StartWave();
    }

    public void StartWave()
    {
        //Coroutine est une m�thode qui peut inclure des d�lais de temps
        StartCoroutine(SpawnEnemy());
    }

    public IEnumerator SpawnEnemy()
    {
        while (!Player.GetInstance().isDead)
        {
            if (ShouldSpawnBoss())
            {
                SpawnBoss();
            }
            else
            {
                for (int i = 0; i < newSpawnAmount; i++)
                {
                    GameObject enemy = ObjectPool.GetInstance().GetPooledObject(SelectEnemyToSpawn());
                    enemy.transform.position = RandomPositionAroundPlayer();

                    enemy.GetComponent<IPoolable>().Reset();
                    enemy.SetActive(true);
                }

            }

            yield return new WaitForSeconds(5);
            newSpawnAmount = Player.GetInstance().playerLVL * spawnAmount / 2;
        }
    }

    private bool ShouldSpawnBoss()
    {
        int playerLvl = Player.GetInstance().playerLVL;
        if(playerLvl % 10 == 0 && !bossSpawnedForCurrentLevel) 
        {
            return true;
        }
        return false;
    }

    private void SpawnBoss()
    {
        GameObject boss = ObjectPool.GetInstance().GetPooledObject(bossPrefab);
        boss.transform.position = RandomPositionAroundPlayer();
        boss.GetComponent<IPoolable>().Reset();
        boss.SetActive(true);
        bossSpawnedForCurrentLevel= true;
    }

    private GameObject SelectEnemyToSpawn()
    {
        float randomPoint = UnityEngine.Random.value * 100;
        float currentSum = 0f;
        foreach (var info in spawnInfos)
        {
            currentSum += info.spawnChance;
            if (currentSum >= randomPoint)
            {
                return info.enemy;
            }

        }
        return defaultEnemy; //Retourne le monstre de base si aucun monstre Sp�cial est choisi
    }


    private Vector3 RandomPositionAroundPlayer()
    {
        Vector3 playerPosition = Player.GetInstance().transform.position;
        Vector2 randomDirection = UnityEngine.Random.insideUnitCircle.normalized * spawnRadius;
        return new Vector3(playerPosition.x + randomDirection.x, playerPosition.y + randomDirection.y);
    }
}
