using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteoroBombScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    
    }
    
    void OnCollisionEnter(Collision other) {
        
        if(other.gameObject.CompareTag("Platform")) {

            Destroy(other.gameObject);
            Destroy(gameObject);
            GameManager.Instance.splashSfx.Play();
        }
    }
    void OnTriggerEnter(Collider other)
    {
        GameObject gameObjectOcean = other.gameObject;
        if (gameObjectOcean.CompareTag("Ocean"))
        {
            Destroy(gameObject);
           
        }         
    }
}
