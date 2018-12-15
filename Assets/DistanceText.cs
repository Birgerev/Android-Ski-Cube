using UnityEngine;
using UnityEngine.UI;

public class DistanceText : MonoBehaviour
{

    private Text text;

    private void Start()
    {
        text = transform.GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        text.text = GameManager.meters + "m";
    }
}
