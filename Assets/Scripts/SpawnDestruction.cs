using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnDestruction : MonoBehaviour
{

    public GameObject[] destruction;
    public Transform spawnPoint;
    public int spawnCount;
    public float startTime, waveTime, spawnTime;

    private float min = 2.5f;
    private float max = 3.6f;

    void Start()
    {
        StartCoroutine(Destruction());
    }

    IEnumerator Destruction(){
        yield return new WaitForSeconds(startTime);
        while(true){
            for(int i = 0; i < spawnCount; i++){
                transform.position = new Vector3(spawnPoint.position.x, Random.Range(min, max), spawnPoint.position.y);
                Quaternion spawnRotation = Quaternion.identity;
                int randomNumber = Random.Range(0, 2);
                Instantiate(destruction[randomNumber], transform.position, spawnRotation);
                yield return new WaitForSeconds(spawnTime);
            }
            yield return new WaitForSeconds(waveTime);
        }
    }
}
