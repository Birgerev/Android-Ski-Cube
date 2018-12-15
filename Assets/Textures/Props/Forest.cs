using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Forest : MonoBehaviour {

    public GameObject prefab;

    public int minAmount = 0;
    public int maxAmount = 1;

    public float maxDistance = 10;

	// Use this for initialization
	void Start () {
        int amount = Random.Range(minAmount, maxAmount);

        for (int i = 0; i < amount; i++)
        {
            GameObject obj = Instantiate(prefab);
            obj.transform.position = transform.position;

            if (!obj.GetComponent<Decoration>())
            {
                Debug.LogError("Forest prefab doesn't contain Decoration script, cancelling forest creation");

                break;
            }

            obj.GetComponent<Decoration>().maxRandomDistance = maxDistance;
        }

        Destroy(gameObject);
	}
}
