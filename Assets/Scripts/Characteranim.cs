
using UnityEngine;

public class Characteranim : MonoBehaviour
{

    [SerializeField] private Vector3 finalposition;
    private Vector3 initialPosition;

   
    private void Awake()
    {
      


        initialPosition = transform.position;   

    }

    private void Update()
    {
        transform.position = Vector3.Lerp(transform.position, finalposition, 0.1f);

    }

    private void OnDisable()
    {
        transform.position = initialPosition;
    }

   
}
