using System.Collections;
using System.Collections.Generic;
using UnityEngine;


using TMPro;

public class gameTimer : MonoBehaviour
{
    TextMeshProUGUI myText;
    bool shouldRun = true;
    float timeOffset = 0f;
    // Start is called before the first frame update
    void Start()
    {
        myText = gameObject.GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        if (shouldRun)
        {
            myText.text = "Time in game - " + (Time.time - timeOffset).ToString("F1") + " sec";
        }
    }

    public void Reset()
    {
        shouldRun = false;
        myText.text = "Time in game - 0 sec";
    }

    public void Begin()
    {
        shouldRun = true;
        timeOffset = Time.time;
    }
}
