using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorSystem : MonoBehaviour
{
    public float enemiesLeft = 3f;

    void Update()
    {
        if (enemiesLeft <= 0)
        {
            gameObject.SetActive(false);
        }
    }
}
