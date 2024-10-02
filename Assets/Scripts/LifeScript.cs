using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lifeScript : MonoBehaviour
{
    public int maxHealth;
    public int health;

    void Start() {
        health = maxHealth;
    }
}
