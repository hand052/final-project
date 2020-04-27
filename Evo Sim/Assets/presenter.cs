using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class presenter : MonoBehaviour
{
    public GameObject offspring;
    public float spawntime = 20f;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("spawn");
    }
    IEnumerator spawn()
    {
        GameObject currentSpawn = Instantiate(offspring, transform.position, Quaternion.identity);
        currentSpawn.SetActive(true);
        yield return new WaitForSeconds(spawntime);
        Destroy(currentSpawn);
        StartCoroutine("spawn");
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
