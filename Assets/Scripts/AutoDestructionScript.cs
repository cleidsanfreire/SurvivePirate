using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDestructionScript : MonoBehaviour
{
    public float Delay = 5f;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(BeginSelfDesctrution());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator BeginSelfDesctrution() {
        yield return new WaitForSeconds(Delay);
        Destroy(gameObject);
    }
}
