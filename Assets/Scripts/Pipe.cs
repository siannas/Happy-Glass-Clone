using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pipe : MonoBehaviour
{
    /// <note> Objek "water" dari folder Prefabs </note>
    public GameObject waterPrefab;

    /// <note> Posisi untuk memunculkan air </note>
    public Transform spawner;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(WaitAndSpray(100, 0.05f));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <note> 
    /// Fungsi untuk memunculkan 1 bulir air
    /// </note>
    private void SpawnWater()
    {
        Vector3 pos = new Vector3(spawner.position.x + Random.Range(-0.3f, 0.3f) , spawner.position.y, 0f);

        GameObject clone = GameObject.Instantiate(waterPrefab, pos, Quaternion.identity, this.transform);
    }

    /// <note> 
    /// Fungsi untuk memunculkan bulir air secara berulang, dengan jeda waktu tertentu
    /// </note>
    private IEnumerator WaitAndSpray(int total, float waitTime)
    {
        while (total > 0)
        {
            yield return new WaitForSeconds(waitTime);
            SpawnWater();
            total--;
        }
    }
}
