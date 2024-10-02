using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteoroScript : MonoBehaviour
{
    public List<GameObject> prefabs;
    public float interval = 4f;
    public float cooldown = 3f;
    [HideInInspector] public Vector3 originPoint = new Vector3(0, 0, 0);
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.isGameOver) return;

        cooldown -= Time.deltaTime;
        if (cooldown <= 0)
        {
            cooldown = interval;
            SpawnMeteroro();
        }
        
    }


    private void SpawnMeteroro()
    {
        int prefabIndex = Random.Range(0, prefabs.Count);
        GameObject prefab = prefabs[prefabIndex];

        float gameWidth = GameManager.Instance.gameWidth;
        float xOffset = Random.Range(-gameWidth / 2f, gameWidth / 2f);
        float yOffset = Random.Range(-gameWidth / 2f, gameWidth / 2f);

        Vector3 position = originPoint + new Vector3(xOffset, 40, yOffset);
        Quaternion rotation = prefab.transform.rotation;
        Instantiate(prefab, position, rotation);

        
    }
}
