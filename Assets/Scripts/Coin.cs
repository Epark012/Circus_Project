using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : Interacable
{
    [SerializeField]
    private float rotationSpeed = 1.0f;

    Transform tr;

    [SerializeField]
    private ParticleSystem coinFX;

    [SerializeField]
    private int cointPoints = 10;

    // Start is called before the first frame update
    void Start()
    {
        tr = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        tr.RotateAround(transform.position, transform.up, Time.deltaTime * 90f * rotationSpeed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            Destroy(gameObject);
            var coin = Instantiate(coinFX, this.gameObject.transform.position, Quaternion.identity);
            Destroy(coin, 3f);

            //Add point via ScoreManager
            ScoreManager.instance.AddPoint(cointPoints);
        }
    }

    public override void OnHammerTime()
    {
        cointPoints = 20;
        rotationSpeed = 2.0f;
    }

    public override void OffHammerTime()
    {
        cointPoints = 10;
        rotationSpeed = 1.0f;
    }
}
