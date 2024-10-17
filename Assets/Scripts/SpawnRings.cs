using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnRings : MonoBehaviour
{
    public GameObject ring;
    public Transform spawnPoint;
    public int spawnCount;
    public float startTime, waveTime, spawnTime;

    private float min = -2f;
    private float max = 2f;

    void Start()
    {
        StartCoroutine(Rings());
    }

    IEnumerator Rings(){
        yield return new WaitForSeconds(startTime);
        while(true){
            for(int i = 0; i < spawnCount; i++){
                transform.position = new Vector3(spawnPoint.position.x, Random.Range(min, max), spawnPoint.position.y);
                Quaternion spawnRotation = Quaternion.identity;
                Instantiate(ring, transform.position, spawnRotation);
                yield return new WaitForSeconds(spawnTime);
            }
            yield return new WaitForSeconds(waveTime);
        }
    }
}
