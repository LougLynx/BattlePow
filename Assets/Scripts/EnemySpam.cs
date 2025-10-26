using System.Collections;
using UnityEngine;
public class EnemySpam : MonoBehaviour
{
    [SerializeField] private GameObject[] enemies;
    [SerializeField] private Transform[] spamPoints;
    [SerializeField] private float timeDelay = 1f;
    void Start()
    {
        StartCoroutine(spamEnemy());
    }
    void Update()
    {
        
    }

    private IEnumerator spamEnemy()
    {
        while (true) { 
            yield return new WaitForSeconds(timeDelay);
            GameObject enemy = enemies[Random.Range(0,enemies.Length)];
            Transform spamPoint = spamPoints[Random.Range(0, spamPoints.Length)];
            Instantiate(enemy, spamPoint.position,Quaternion.identity);
        }
    }
}
