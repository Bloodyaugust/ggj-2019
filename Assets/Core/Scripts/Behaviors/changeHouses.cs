using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class changeHouses : MonoBehaviour
{
    public List<Sprite> houseImages = new List<Sprite>();
    public bool facingDown = true;
    public int level = 1;
    public int houseNumber = 1;
    Sprite houseSprite;
    bool start = true;
    Toolbox toolbox;

    private void Awake()
    {
        toolbox = Toolbox.Instance;
    }

    Toolbox _toolbox;

    void Awake () {
      _toolbox = Toolbox.Instance;
    }

    // Start is called before the first frame update
    void Start()
    {
        houseSprite = gameObject.GetComponent<SpriteRenderer>().sprite;
        UpdateSprite(1);

        _toolbox.HouseLevelChange.AddListener(UpdateSprite);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void UpdateSprite(int inLevel)
    {
        level = inLevel;
        if (facingDown)
        {
            switch (level)
            {
                case 1:
                    houseSprite = houseImages[0];
                    break;
                case 2:
                    houseSprite = houseImages[1];
                    break;
                case 3:
                    houseSprite = houseImages[2];
                    break;
                case 4:
                    houseSprite = houseImages[3];
                    break;
            }
        }
        else
        {
            switch (level)
            {
                case 1:
                    houseSprite = houseImages[4];
                    break;
                case 2:
                    houseSprite = houseImages[5];
                    break;
                case 3:
                    houseSprite = houseImages[6];
                    break;
                case 4:
                    houseSprite = houseImages[7];
                    break;
            }
        }

        if (start)
            start = false;
        else
        {
            toolbox.playOneShotClip(0);
        }
    }
}
