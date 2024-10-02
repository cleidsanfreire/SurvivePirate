using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombScripts : MonoBehaviour
{
    public float explosionDelay = 5f;
    public GameObject ExplosionPrefab;

    public GameObject WoodBreakingPrefab;
    public float BlastRadius = 4f;
    public int BlastDamage = 20;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ExplosionCoroutine());
    }

    // Update is called once per frame
    void Update()
    {

    }
    private IEnumerator ExplosionCoroutine()
    {
        //Wait
        yield return new WaitForSeconds(explosionDelay);

        // Explode
        Explode();
    }

    void OnTriggerEnter(Collider other)
    {
        GameObject gameObject = other.gameObject;
        if (gameObject.CompareTag("Ocean"))
        {
            GameManager.Instance.splashSfx.Play();
        }
    }

    private void Explode()
    {
        //Create VFX // Create SFX
        Instantiate(ExplosionPrefab, transform.position, ExplosionPrefab.transform.rotation);

        //Destroy platforms
        Collider[] colliders = Physics.OverlapSphere(transform.position, BlastRadius);
        foreach (Collider collider in colliders)
        {
            GameObject hitObject = collider.gameObject;
            if (hitObject.CompareTag("Platform"))
            {
                lifeScript lifeScript = hitObject.GetComponent<lifeScript>();
                if (lifeScript != null)
                {

                    float distance = (hitObject.transform.position - transform.position).magnitude;
                    float distanceRate = Mathf.Clamp(distance / BlastRadius, 0, 1);
                    float damageRate = 1f - Mathf.Pow(distanceRate, 4);
                    int damage = (int)Mathf.Ceil(damageRate * BlastDamage);
                    lifeScript.health -= damage;
                    if (lifeScript.health <= 0)
                    {
                        Instantiate(WoodBreakingPrefab, hitObject.transform.position, WoodBreakingPrefab.transform.rotation);
                        Destroy(collider.gameObject);
                    }
                }
            }
        }


        // Destroy Bomb
        Destroy(gameObject);
    }
}
