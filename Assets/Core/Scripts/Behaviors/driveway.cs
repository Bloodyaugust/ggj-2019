using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class driveway : MonoBehaviour
{
    public int drivewayNum = 1; //keep track of which player the driveway belongs to.
    Toolbox toolbox;

    private void Awake()
    {
        toolbox = Toolbox.Instance;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Puck")
        {
            StartCoroutine(destroyPuck(collision.gameObject));
        }
    }

    IEnumerator destroyPuck(GameObject puck)
    {
        yield return new WaitForSeconds(1);
        int value = puck.GetComponent<puckStart>().selectedPuck.value;
        if (value > 0)
            toolbox.playOneShotClip(2);
        else
            toolbox.playOneShotClip(1);
        toolbox.Score.Invoke(new ScoreData(value, drivewayNum));
        Destroy(puck);
    }
}
