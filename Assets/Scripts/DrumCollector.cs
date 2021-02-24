using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrumCollector : MonoBehaviour
{
    [SerializeField]
    Spawner spawner;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Drum")
        {
            other.gameObject.GetComponent<MeshRenderer>().enabled = true;
            other.gameObject.GetComponent<MeshCollider>().enabled = true;
            other.gameObject.transform.position = spawner.gameObject.transform.position;
            other.gameObject.transform.rotation = Quaternion.identity;
            other.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
            //other.gameObject.SetActive(false);
            //turn on mesh and collider in drum
        }
    }
}
