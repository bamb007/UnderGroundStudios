using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrokenBits : MonoBehaviour
{

    public GameObject prefab;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Projectile")
        {
            Fragment();
        }
    }

    private void Fragment()
    {
        GameObject.Destroy(gameObject);
        GameObject obj = Instantiate(prefab);

        obj.transform.position = gameObject.transform.position;

        Rigidbody2D[] bodies = obj.transform.GetComponentsInChildren<Rigidbody2D>();

        for (int i = 0; i < bodies.Length; i++)
        {
            bodies[i].AddForce(new Vector2(Random.Range(-20, 20), Random.Range(-20, 20)), ForceMode2D.Impulse);
        }

    }
}
