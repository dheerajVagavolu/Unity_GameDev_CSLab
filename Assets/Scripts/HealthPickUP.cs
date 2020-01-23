using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickUP : MonoBehaviour
{
    public ParticleSystem collect;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            FindObjectOfType<Health>().Heal();

            collect.transform.position = transform.position;
            collect.Play();

            Destroy(gameObject);

        }
    }
}