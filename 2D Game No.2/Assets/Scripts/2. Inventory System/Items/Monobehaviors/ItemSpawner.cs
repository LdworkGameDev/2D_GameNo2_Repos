using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    public GameObject itemToSpawn;
    public Vector2 spawnForce;
    public Vector2 offsetPosition;

    public void SpawnItem()
    {
        GameObject item = Instantiate(itemToSpawn, gameObject.transform.position + new Vector3(offsetPosition.x, offsetPosition.y, 0), Quaternion.identity);

        Rigidbody2D rigid = item.GetComponent<Rigidbody2D>();
        int direction = (Random.value < 0.5f) ? 1 : -1;
        rigid.AddForce(spawnForce * new Vector2(direction, 1));
    }
}
