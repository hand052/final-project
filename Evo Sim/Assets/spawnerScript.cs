using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnerScript : MonoBehaviour
{
    public float waveTime = 10f;
    public GameObject creature1;
    public float mutatateRate = 10f;

    public GameObject[] currentCreatures;

    void Start()
    {
        FirstSpawn();
    }

    //Purely Randomized Spawn
    void FirstSpawn()
    {
        for (int i = 0; i < 10; i++)
        {
            GameObject creature = (GameObject)Instantiate(creature1, transform.position, Quaternion.identity);

            //Set layers of creature bodyparts
            creature.transform.GetChild(0).gameObject.layer = i + 8;
            creature.transform.GetChild(1).gameObject.layer = i + 8;
            creature.transform.GetChild(2).gameObject.layer = i + 8;
            creature.transform.GetChild(3).gameObject.layer = i + 8;
            creature.transform.GetChild(4).gameObject.layer = i + 8;


            creatureScript CS = creature.GetComponentInChildren<creatureScript>();
            //randomly set locomotion values
            CS.RA_f1 = Random.Range(10f, 500f);
            CS.RA_t1 = Random.Range(0f, 2f);
            CS.RA_f2 = Random.Range(10f, 500f);
            CS.RA_t2 = Random.Range(0f, 2f);

            CS.LA_f1 = Random.Range(10f, 500f);
            CS.LA_t1 = Random.Range(0f, 2f);
            CS.LA_f2 = Random.Range(10f, 500f);
            CS.LA_t2 = Random.Range(0f, 2f);

            CS.RL_f1 = Random.Range(10f, 500f);
            CS.RL_t1 = Random.Range(0f, 2f);
            CS.RL_f2 = Random.Range(10f, 500f);
            CS.RL_t2 = Random.Range(0f, 2f);

            CS.LL_f1 = Random.Range(10f, 500f);
            CS.LL_t1 = Random.Range(0f, 2f);
            CS.LL_f2 = Random.Range(10f, 500f);
            CS.LL_t2 = Random.Range(0f, 2f);
        }
        StartCoroutine("NextWave");
    }


    IEnumerator NextWave()
    {
        yield return new WaitForSeconds(waveTime);
        currentCreatures = GameObject.FindGameObjectsWithTag("Creature1");
        
        GameObject FarthestCreature;
        GameObject SecondFarthestCreature;
        float farthestDistance = 0;
        float secondfarthestDistance = 0;
        for (int i = 0; i < currentCreatures.Length; i++)
        {
            float currentDistance = currentCreatures[i].GetComponentInChildren<creatureScript>().distance;
            if (currentDistance > farthestDistance)
            {
                secondfarthestDistance = farthestDistance;
                farthestDistance = currentDistance;

                SecondFarthestCreature = FarthestCreature;

                FarthestCreature = currentCreatures[i];
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
