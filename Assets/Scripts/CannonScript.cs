using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonScript : MonoBehaviour
{
    // Prefab da bomba
    public List<GameObject> bombPrefabs;

    // Intervalo em Vector2
    public Vector2 timeInterval = new Vector2(1,1);

    // Referencia pro ponto de origem da bomba (GameObject)
    public GameObject spwanPoint;
    // Referencia ao nosso alvo
    public GameObject target;

    // Forca (Vector2)
    public Vector2 force;

    public float arcDregrees = 45f;

    // Range (float)
    public float rangeInDegress;

    private float cooldown;
    // Start is called before the first frame update
    void Start()
    {
        cooldown = Random.Range(timeInterval.x, timeInterval.y);
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.Instance.isGameOver) return;
        cooldown -= Time.deltaTime;
        if (cooldown <= 0) {
            cooldown = Random.Range(timeInterval.x, timeInterval.y);
        // Fire
        Fire();
        }
        //Temporizador (cooldown, interval, DeltaTime)
    }
    private void Fire() {
        // Get Prefab
        GameObject bombPrefab = bombPrefabs[Random.Range(0,bombPrefabs.Count)];
        //Create bomb
        GameObject bomb = Instantiate(bombPrefab, spwanPoint.transform.position,bombPrefab.transform.rotation);
        
        //Apply Force
        Rigidbody bombRigidbody = bomb.GetComponent<Rigidbody>();
        Vector3 impulseVector = target.transform.position - spwanPoint.transform.position;
        impulseVector.Scale(new Vector3(1,0,1));
        impulseVector.Normalize();
        impulseVector += new Vector3(0, arcDregrees / 45f ,0);
        impulseVector.Normalize();
        //impulseVector += new Vector3(0,0.5f,0);
        //impulseVector = Quaternion.AngleAxis(arcDregrees,Vector3.left) * impulseVector;
        impulseVector = Quaternion.AngleAxis(rangeInDegress * Random.Range(-1f,1f) , Vector3.up) * impulseVector;

        impulseVector *= Random.Range(force.x, force.y);
        bombRigidbody.AddForce(impulseVector, ForceMode.Impulse);
    }
}
