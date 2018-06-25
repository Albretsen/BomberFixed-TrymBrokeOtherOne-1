using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Objective : MonoBehaviour {

    Rigidbody2D rb;
    public GameObject explosionPrefab;
    public static bool objectiveDead;

    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody2D>();
        Objective.objectiveDead = false;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Hit(Vector2 dir)
    {
        var explodingPlayer = (GameObject)Instantiate(explosionPrefab, rb.position, transform.rotation);
        Destroy(gameObject);
    }
}
