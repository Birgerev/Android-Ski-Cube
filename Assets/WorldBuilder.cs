using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldBuilder : MonoBehaviour {

    public static WorldBuilder instance;


    public GameObject[] slope;

    public float createDistance = 20;
    public Obstacle[] obstacles;

    private List<GameObject> earlierLandscapes = new List<GameObject>();
    private float currentDistance = 0;
    private Vector2 lastPos;

    public int slopeValue = 1;

    public float slopeChangeChance = 5;


    // Use this for initialization
    void Start () {
        instance = this;

    }
	
	// Update is called once per frame
	void Update () {
        if (currentDistance < createDistance + Player.instance.transform.position.x)
            Create();

        if(slopeValue == 0)
        {
            if(Random.Range(0, 100) < 20)
            {
                //turn it into either 1 or 2
                slopeValue = (int)Random.Range(1, 2+1);
            }
        }else
        {
            if (Random.Range(0, 100) < slopeChangeChance)
            {
                //turn it into either 1 or 2
                slopeValue = (int)Random.Range(1, 2 + 1);
            }
        }
	}

    public void Create()
    {
        GameObject prefab = slope[slopeValue];

        Obstacle obs = nextPiece();

        if (obs != null)
            prefab = obs.prefab;

        GameObject obj = Instantiate(prefab);

        Vector2 pos = new Vector2(lastPos.x, lastPos.y);
        obj.transform.position = pos;

        earlierLandscapes.Add(obj);

        if (obs != null)
            if (obs.isProp)
                Create();


        if (obs != null)
            if (obs.isProp)
                return;

        if (obj.transform.Find("end") == null)
        {
            Debug.LogError("Landscape Prefab has no 'end' object as child");
            return;
        }

        lastPos = obj.transform.Find("end").position;
        currentDistance = lastPos.x;
    }

    public Obstacle nextPiece()
    {
        Obstacle result = null;

        foreach (Obstacle o in shuffle(obstacles))
        {
            if(Random.Range(0f, 99.9f) < o.chance)
            {
                result = o;
                break;
            }
        }

        return result;
    }

   Obstacle[] shuffle(Obstacle[] list)
    {
        Obstacle[] ts = list;

        var count = ts.Length;
        var last = count - 1;
        for (var i = 0; i < last; ++i)
        {
            var r = UnityEngine.Random.Range(i, count);
            var tmp = ts[i];
            ts[i] = ts[r];
            ts[r] = tmp;
        }

        return ts;
    }


    public void Reset()
    {
        foreach(GameObject obj in earlierLandscapes)
        {
            Destroy(obj);
        }

        earlierLandscapes.Clear();
        currentDistance = 0;
        lastPos = Vector2.zero;
    }
}
