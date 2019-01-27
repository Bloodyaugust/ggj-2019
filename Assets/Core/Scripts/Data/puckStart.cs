using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class puckStart : MonoBehaviour
{
    public List<puckData> puckOptions;
    public puckData selectedPuck;

    // Start is called before the first frame update
    void Start()
    {
        int puckNum = Random.Range(0, puckOptions.Count);
        selectedPuck = puckOptions[puckNum];
        gameObject.GetComponent<SpriteRenderer>().sprite = selectedPuck.artwork;
    }
}
