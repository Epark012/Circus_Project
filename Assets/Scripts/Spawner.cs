using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    public GameObject prefabs;
    public List<GameObject> listOfPrefabs;
    public int spawnTime = 5;
    public float repeatTime = 5;
    public int numPrefabs = 0;

    [SerializeField]
    private int currentIndex = 0;

    [SerializeField]
    private Transform parent;

    private void Awake()
    {
        /*for(int i = 0; i < numPrefabs; i++)
        {
            var instance = Instantiate(prefabs, this.gameObject.transform.position, Quaternion.identity);
            instance.transform.parent = parent.transform;
            instance.gameObject.SetActive(false);
            listOfPrefabs.Add(instance);
        }*/
        StartCoroutine(InstantiateObject());
    }

    private void Update()
    {
    }

    IEnumerator InstantiateObject()
    {
        while (listOfPrefabs.Count < numPrefabs)
        {
            yield return new WaitForSeconds(5f);
            var instance = Instantiate(prefabs, this.gameObject.transform.position, Quaternion.identity);
            instance.transform.parent = parent.transform;
            listOfPrefabs.Add(instance);
        }
    }

    void Start()
    {
        //InvokeRepeating("Spawn", spawnTime, repeatTime);
    }

    /*void Spawn()
    {
        if (!listOfPrefabs[currentIndex].activeSelf)
            listOfPrefabs[currentIndex].SetActive(true);
        else
        {
            listOfPrefabs[currentIndex].transform.position = this.gameObject.transform.position;
            listOfPrefabs[currentIndex].GetComponent<Rigidbody>().velocity = Vector3.zero;
            listOfPrefabs[currentIndex].transform.rotation = Quaternion.identity;
        }
        if(currentIndex >= numPrefabs - 1)
        {
            currentIndex = 0;
        }
        else
        {
            currentIndex++;
        }
    }*/
}
