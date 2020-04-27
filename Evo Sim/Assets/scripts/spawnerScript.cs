using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class spawnerScript : MonoBehaviour
{
    public float waveTime = 10f;
    public GameObject creature1;
    public float mutatateRate = 10f;
    public int populationSize = 10;
    public float startForce = 500f;
    public float scaleMutateFactor = 30f;

    public GameObject[] currentCreatures;

    void Start()
    {
        FirstSpawn();
    }

    //Purely Randomized Spawn
    void FirstSpawn()
    {
        for (int i = 0; i < populationSize; i++)
        {
            GameObject creature = (GameObject)Instantiate(creature1, transform.position, Quaternion.identity);

            //Set layers of creature bodyparts
            creature.transform.GetChild(0).gameObject.layer = i + 8;
            creature.transform.GetChild(1).gameObject.layer = i + 8;
            creature.transform.GetChild(2).gameObject.layer = i + 8;
            creature.transform.GetChild(3).gameObject.layer = i + 8;
            creature.transform.GetChild(4).gameObject.layer = i + 8;


            creatureScript CS = creature.GetComponentInChildren<creatureScript>();
            //randomly set values
            CS.RA_f1 = Random.Range(0f, startForce);
            CS.RA_t1 = Random.Range(0f, 2f);
            CS.RA_f2 = Random.Range(0f, startForce);
            CS.RA_t2 = Random.Range(0f, 2f);
            CS.RA_Scale = new Vector3(Random.Range(0.5f,1f), Random.Range(0.5f, 1f), Random.Range(0.5f, 1f));

            CS.LA_f1 = Random.Range(0f, startForce);
            CS.LA_t1 = Random.Range(0f, 2f);
            CS.LA_f2 = Random.Range(0f, startForce);
            CS.LA_t2 = Random.Range(0f, 2f);
            CS.LA_Scale = new Vector3(Random.Range(0.5f, 1f), Random.Range(0.5f, 1f), Random.Range(0.5f, 1f));

            CS.RL_f1 = Random.Range(0f, startForce);
            CS.RL_t1 = Random.Range(0f, 2f);
            CS.RL_f2 = Random.Range(0f, startForce);
            CS.RL_t2 = Random.Range(0f, 2f);
            CS.RL_Scale = new Vector3(Random.Range(0.5f, 1f), Random.Range(0.5f, 1f), Random.Range(0.5f, 1f));

            CS.LL_f1 = Random.Range(0f, startForce);
            CS.LL_t1 = Random.Range(0f, 2f);
            CS.LL_f2 = Random.Range(0f, startForce);
            CS.LL_t2 = Random.Range(0f, 2f);
            CS.LL_Scale = new Vector3(Random.Range(0.5f, 1f), Random.Range(0.5f, 1f), Random.Range(0.5f, 1f));
        }
        StartCoroutine("FirstWaveBuffer");
    }
    IEnumerator FirstWaveBuffer()
    {
        yield return new WaitForSeconds(waveTime);
        StartCoroutine("NextWave");
        currentCreatures = GameObject.FindGameObjectsWithTag("Creature1");
    }

    public GameObject FarthestCreature;
    public GameObject SecondFarthestCreature;

    void CheckBestCreatures()
    {
        currentCreatures = GameObject.FindGameObjectsWithTag("Creature1");
        float farthestDistance = 0;
        float secondfarthestDistance = 0;
        for (int i = 0; i < currentCreatures.Length; i++)
        {
            float currentDistance = currentCreatures[i].GetComponentInChildren<creatureScript>().distance;
            if (currentDistance > farthestDistance)
            {
                FarthestCreature = currentCreatures[i];
                secondfarthestDistance = farthestDistance;
                farthestDistance = currentDistance;

                SecondFarthestCreature = FarthestCreature;

                FarthestCreature = currentCreatures[i];
            }
        }
    }
    IEnumerator NextWave()
    {
       
        


        //find best creature
       
        CheckBestCreatures();
        

        foreach (GameObject go in currentCreatures)
        {
            go.gameObject.tag = "oldCreature";
        }
        //Spawn Second Generation

        //Best Creature with Mutations
        for (int i = 0; i < populationSize/2; i++)
        {
            GameObject creature = (GameObject)Instantiate(creature1, transform.position, Quaternion.identity);
            
            //Set layers of creature bodyparts
            creature.transform.GetChild(0).gameObject.layer = i + 8;
            creature.transform.GetChild(1).gameObject.layer = i + 8;
            creature.transform.GetChild(2).gameObject.layer = i + 8;
            creature.transform.GetChild(3).gameObject.layer = i + 8;
            creature.transform.GetChild(4).gameObject.layer = i + 8;



            creatureScript CS = creature.GetComponentInChildren<creatureScript>();
            creatureScript BestCS = FarthestCreature.GetComponentInChildren<creatureScript>();
            creatureScript SecondBestCS = SecondFarthestCreature.GetComponentInChildren<creatureScript>();
            
            //locomotion values
            CS.RA_f1 = BestCS.RA_f1 + Random.Range(-mutatateRate * 5, mutatateRate *5);
            CS.RA_t1 = BestCS.RA_t1 + Random.Range(-mutatateRate / 50, mutatateRate / 50);
            CS.RA_f2 = BestCS.RA_f2 + Random.Range(-mutatateRate * 5, mutatateRate * 5); 
            CS.RA_t2 = BestCS.RA_t2 + Random.Range(-mutatateRate / 50, mutatateRate / 50);
            CS.RA_Scale = BestCS.RA_Scale + -new Vector3(Random.Range(-mutatateRate / scaleMutateFactor, mutatateRate / scaleMutateFactor), Random.Range(-mutatateRate / scaleMutateFactor, mutatateRate / scaleMutateFactor), Random.Range(-mutatateRate / scaleMutateFactor, mutatateRate / scaleMutateFactor));

            CS.LA_f1 = BestCS.LA_f1 + Random.Range(-mutatateRate * 5, mutatateRate * 5);
            CS.LA_t1 = BestCS.LA_t1 + Random.Range(-mutatateRate / 50, mutatateRate / 50); 
            CS.LA_f2 = BestCS.LA_f2 + Random.Range(-mutatateRate * 5, mutatateRate * 5);
            CS.LA_t2 = BestCS.LA_t2 + Random.Range(-mutatateRate / 50, mutatateRate / 50);
            CS.LA_Scale = BestCS.LA_Scale + -new Vector3(Random.Range(-mutatateRate / scaleMutateFactor, mutatateRate / scaleMutateFactor), Random.Range(-mutatateRate / scaleMutateFactor, mutatateRate / scaleMutateFactor), Random.Range(-mutatateRate / scaleMutateFactor, mutatateRate / scaleMutateFactor));

            CS.RL_f1 = BestCS.RL_f1 + Random.Range(-mutatateRate * 5, mutatateRate * 5);
            CS.RL_t1 = BestCS.RL_t1 + Random.Range(-mutatateRate / 50, mutatateRate / 50); 
            CS.RL_f2 = BestCS.RL_f2 + Random.Range(-mutatateRate * 5, mutatateRate * 5);
            CS.RL_t2 = BestCS.RL_t2 + Random.Range(-mutatateRate / 50, mutatateRate / 50);
            CS.RL_Scale = BestCS.RL_Scale + -new Vector3(Random.Range(-mutatateRate / scaleMutateFactor, mutatateRate / scaleMutateFactor), Random.Range(-mutatateRate / scaleMutateFactor, mutatateRate / scaleMutateFactor), Random.Range(-mutatateRate / scaleMutateFactor, mutatateRate / scaleMutateFactor));

            CS.LL_f1 = BestCS.LL_f1 + Random.Range(-mutatateRate * 5, mutatateRate * 5);
            CS.LL_t1 = BestCS.LL_t1 + Random.Range(-mutatateRate / 50, mutatateRate / 50); 
            CS.LL_f2 = BestCS.LL_f2 + Random.Range(-mutatateRate * 5, mutatateRate * 5);
            CS.LL_t2 = BestCS.LL_t2 + Random.Range(-mutatateRate / 50, mutatateRate / 50);
            CS.LL_Scale = BestCS.LL_Scale + -new Vector3(Random.Range(-mutatateRate / scaleMutateFactor, mutatateRate / scaleMutateFactor), Random.Range(-mutatateRate / scaleMutateFactor, mutatateRate / scaleMutateFactor), Random.Range(-mutatateRate / scaleMutateFactor, mutatateRate / scaleMutateFactor));
        }

        //Second Best Creature with Mutations
        for (int i = 0; i < populationSize / 2; i++)
        {
            GameObject creature = (GameObject)Instantiate(creature1, transform.position, Quaternion.identity);

            //Set layers of creature bodyparts
            creature.transform.GetChild(0).gameObject.layer = i + 8;
            creature.transform.GetChild(1).gameObject.layer = i + 8;
            creature.transform.GetChild(2).gameObject.layer = i + 8;
            creature.transform.GetChild(3).gameObject.layer = i + 8;
            creature.transform.GetChild(4).gameObject.layer = i + 8;



            creatureScript CS = creature.GetComponentInChildren<creatureScript>();
            creatureScript BestCS = FarthestCreature.GetComponentInChildren<creatureScript>();
            creatureScript SecondBestCS = SecondFarthestCreature.GetComponentInChildren<creatureScript>();

            //randomly set locomotion values
            CS.RA_f1 = SecondBestCS.RA_f1 + Random.Range(-mutatateRate * 5, mutatateRate * 5);
            CS.RA_t1 = SecondBestCS.RA_t1 + Random.Range(-mutatateRate / 50, mutatateRate / 50);
            CS.RA_f2 = SecondBestCS.RA_f2 + Random.Range(-mutatateRate * 5, mutatateRate * 5);
            CS.RA_t2 = SecondBestCS.RA_t2 + Random.Range(-mutatateRate / 50, mutatateRate / 50);
            CS.RA_Scale = BestCS.RA_Scale + -new Vector3(Random.Range(-mutatateRate / scaleMutateFactor, mutatateRate / scaleMutateFactor), Random.Range(-mutatateRate / scaleMutateFactor, mutatateRate / scaleMutateFactor), Random.Range(-mutatateRate / scaleMutateFactor, mutatateRate / scaleMutateFactor));

            CS.LA_f1 = SecondBestCS.LA_f1 + Random.Range(-mutatateRate * 5, mutatateRate * 5);
            CS.LA_t1 = SecondBestCS.LA_t1 + Random.Range(-mutatateRate / 50, mutatateRate / 50);
            CS.LA_f2 = SecondBestCS.LA_f2 + Random.Range(-mutatateRate * 5, mutatateRate * 5);
            CS.LA_t2 = SecondBestCS.LA_t2 + Random.Range(-mutatateRate / 50, mutatateRate / 50);
            CS.LA_Scale = BestCS.LA_Scale + -new Vector3(Random.Range(-mutatateRate / scaleMutateFactor, mutatateRate / scaleMutateFactor), Random.Range(-mutatateRate / scaleMutateFactor, mutatateRate / scaleMutateFactor), Random.Range(-mutatateRate / scaleMutateFactor, mutatateRate / scaleMutateFactor));

            CS.RL_f1 = SecondBestCS.RL_f1 + Random.Range(-mutatateRate * 5, mutatateRate * 5);
            CS.RL_t1 = SecondBestCS.RL_t1 + Random.Range(-mutatateRate / 50, mutatateRate / 50);
            CS.RL_f2 = SecondBestCS.RL_f2 + Random.Range(-mutatateRate * 5, mutatateRate * 5);
            CS.RL_t2 = SecondBestCS.RL_t2 + Random.Range(-mutatateRate / 50, mutatateRate / 50);
            CS.RL_Scale = BestCS.RL_Scale + -new Vector3(Random.Range(-mutatateRate / scaleMutateFactor, mutatateRate / scaleMutateFactor), Random.Range(-mutatateRate / scaleMutateFactor, mutatateRate / scaleMutateFactor), Random.Range(-mutatateRate / scaleMutateFactor, mutatateRate / scaleMutateFactor));

            CS.LL_f1 = SecondBestCS.LL_f1 + Random.Range(-mutatateRate * 5, mutatateRate * 5);
            CS.LL_t1 = SecondBestCS.LL_t1 + Random.Range(-mutatateRate / 50, mutatateRate / 50);
            CS.LL_f2 = SecondBestCS.LL_f2 + Random.Range(-mutatateRate * 5, mutatateRate * 5);
            CS.LL_t2 = SecondBestCS.LL_t2 + Random.Range(-mutatateRate / 50, mutatateRate / 50);
            CS.LL_Scale = BestCS.LL_Scale + -new Vector3(Random.Range(-mutatateRate / scaleMutateFactor, mutatateRate / scaleMutateFactor), Random.Range(-mutatateRate / scaleMutateFactor, mutatateRate / scaleMutateFactor), Random.Range(-mutatateRate / scaleMutateFactor, mutatateRate / scaleMutateFactor));

        }
        
        
        //Destroy Previous Generation
        GameObject[] oldCreatures = GameObject.FindGameObjectsWithTag("oldCreature");
        foreach (GameObject go in oldCreatures)
        {
            Destroy(go);
        }
        yield return new WaitForSeconds(waveTime);
        //Repeat
        StartCoroutine("NextWave");

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
