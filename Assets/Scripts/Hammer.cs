using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hammer : Interacable
{
    public bool onHand = false;

    //Triggered by player and make it speical status

    // Start is called before the first frame update

    [SerializeField]
    private GameObject fracDrumPrefabs;

    [SerializeField]
    private ParticleSystem hammerStateDeathFX;

    MeshRenderer meshRenderer;

    private void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        if(!meshRenderer)
        {
            return;
        }
        else
        {
            meshRenderer.enabled = false;
        }
    }

    public override void OnHammerTime()
    {
        onHand = true;
        meshRenderer.enabled = true;
    }

    public override void OffHammerTime()
    {
        onHand = false;
        meshRenderer.enabled = false;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Drum" && onHand)
        {
            other.gameObject.SetActive(false);
            print("Hammer is working");
            //var frucDrum = Instantiate(fracDrumPrefabs, transform.position, Quaternion.identity);
            //Destroy(frucDrum, 5.0f);
        }

        if(other.gameObject.tag == "Enemy" && onHand)
        {
            other.gameObject.SetActive(false);
            var deathFX = Instantiate(hammerStateDeathFX, transform.position, Quaternion.identity);
            Destroy(deathFX, 5f);
            
        }
    }
}
