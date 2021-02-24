using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drum : MonoBehaviour
{
    [SerializeField]
    private GameObject effect;

    [SerializeField]
    private GameObject fracDrumPrefabs;

    MeshRenderer mesh;
    Collider drumCollider;

    private Spawner spawner;
    //Instantiated by KingKong animator
    //is forced to move by rigidboy

    // Start is called before the first frame update
    void Start()
    {
        spawner = FindObjectOfType<Spawner>();
        mesh = GetComponent<MeshRenderer>();
        drumCollider = GetComponent<MeshCollider>();

        effect.SetActive(false);
        Rigidbody rigidbody = GetComponent<Rigidbody>();
        //addforce for x
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        print("Hit Ground");
        effect.SetActive(true);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "FootCollider")
        {
            //turn off mesh and collider
            mesh.enabled = false;
            drumCollider.enabled = false;
        }
    }
    private void OnDisable()
    {
        var frucDrum = Instantiate(fracDrumPrefabs, transform.position, transform.rotation);
        Destroy(frucDrum, 5.0f);

        gameObject.transform.position = spawner.gameObject.transform.position;
        gameObject.transform.rotation = Quaternion.identity;
        gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
    }
}
