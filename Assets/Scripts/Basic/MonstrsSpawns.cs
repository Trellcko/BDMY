using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonstrsSpawns : MonoBehaviour {
   float tUMER;
    public GameObject[] Monstrs;
    int n;

    private void Start()
    {
        StartCoroutine(Spawner());
    }
    IEnumerator Spawner()
    {
        while (true)
        {
            tUMER = (Random.Range(6, 8));
            tUMER /= Time.timeScale;

            n = Random.Range(0, Monstrs.Length);

            yield return new WaitForSeconds(Random.Range(6, 8));
            Instantiate(Monstrs[n]);
              }
     }
}
