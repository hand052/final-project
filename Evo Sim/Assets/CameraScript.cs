using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class CameraScript : MonoBehaviour
{
    public static CameraScript Instance { get; private set; }
    public float minZoomDistance = 10;
    public float maxPossibleDistance = 999;
    public float smoothing = 0.5f;
    public float minY = 10f;
    public float maxY = 50f;

    public List<Transform> targets = new List<Transform>();

    public Vector3 velocity;
    

    // Start is called before the first frame update
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {

        StartCoroutine("SelectTargets");

        Move();
        Zoom();
    }

    IEnumerator SelectTargets()
    {
        GameObject[] curCreatures = GameObject.FindGameObjectsWithTag("Creature1");

        foreach (GameObject go in curCreatures)
        {


            targets.Add(go.transform.GetChild(0).transform);
        }

        yield return new WaitForSeconds(10f);

        targets.Clear();

        StartCoroutine("SelectTargets");
    }
    private void Move()
    {
        Vector3 centerPoint = GetCenterPoint();

        centerPoint.y = transform.position.y;

        transform.position = Vector3.SmoothDamp(transform.position, centerPoint, ref velocity, smoothing);
    }
    private void Zoom()
    {
        float greatestDistance = GetGreatestDistance();
        if (greatestDistance < minZoomDistance) { greatestDistance = 0f; }
        float newY = Mathf.Lerp(minY, maxY, greatestDistance / maxPossibleDistance);

        transform.position = new Vector3(transform.position.x, Mathf.Lerp(transform.position.y, newY, Time.deltaTime), transform.position.z);
    }

    float GetGreatestDistance() 
    {
        Bounds bounds = EncapsulateTargets();

        return bounds.size.x > bounds.size.z ? bounds.size.x : bounds.size.z;
    }
    private Vector3 GetCenterPoint()
    {
        if (targets.Count == 1)
        {
            return targets[0].position;

        }
        Bounds bounds = EncapsulateTargets();
        Vector3 center = bounds.center;
        center.y = 0f;

        return center;
    }

    private Bounds EncapsulateTargets()
    {
        Bounds bounds = new Bounds(targets[0].position, Vector3.zero);

        foreach (Transform target in targets)
        {
            bounds.Encapsulate(target.position);
        }

        return bounds;
    }
}
