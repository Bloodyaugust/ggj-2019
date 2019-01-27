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
        toolbox.Score.Invoke(new ScoreData(puck.GetComponent<puckStart>().selectedPuck.value, drivewayNum));
        Destroy(puck);
    }
}
