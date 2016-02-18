using UnityEngine;
using System.Collections;

public class SpawnSyncedObject : MonoBehaviour
{

    public GameObject ObjectToSpawn;

    void Start()
    {
        Instantiate(ObjectToSpawn, transform.position, Quaternion.identity);
    }
}
