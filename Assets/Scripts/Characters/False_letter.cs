using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class False_letter : MonoBehaviour
{
    public ParticleSystem collect;
    // Start is called before the first frame update
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
            FindObjectOfType<GameManager>().AddLetter("false");
            collect.transform.position = transform.position;
            collect.Play();
            Destroy(gameObject);

        }
    }

}
