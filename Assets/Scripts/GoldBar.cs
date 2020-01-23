using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldBar : MonoBehaviour
{
    // Start is called before the first frame update
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
        if(other.tag == "Player")
        {
            FindObjectOfType<GameManager>().AddGold(5);

            collect.transform.position = transform.position;
            collect.Play();

            Destroy(gameObject);

        }
    }
}
