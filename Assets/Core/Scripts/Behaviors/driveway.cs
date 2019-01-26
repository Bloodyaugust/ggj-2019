using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class driveway : MonoBehaviour
{
    public int drivewayNum = 1; //keep track of which player the driveway belongs to.

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
            puckSlide puck = collision.gameObject.GetComponent<puckSlide>();
            Debug.Log(puck.Value);
            //pass the puck value and the driveway number to whatever is keeping track of score

            //waiting a moment and then destroying the puck
            StartCoroutine(destroyPuck(collision.gameObject));
        }
    }

    IEnumerator destroyPuck(GameObject puck)
    {
        yield return new WaitForSeconds(2);
        Destroy(puck);
    }
}
