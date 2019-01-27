using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class changeHouses : MonoBehaviour
{
    public List<Sprite> houseImages = new List<Sprite>();
    public bool facingDown = true;
    public int level = 0;
    public int PlayerIndex;
    SpriteRenderer houseSpriteRenderer;
    bool start = true;
    Toolbox toolbox;

    private void Awake()
    {
        toolbox = Toolbox.Instance;
    }

    // Start is called before the first frame update
    void Start()
    {
        houseSpriteRenderer = gameObject.GetComponent<SpriteRenderer>();

        toolbox.HouseLevelChange.AddListener(UpdateSprite);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void UpdateSprite(HouseChangeData changeData) {
      Sprite newSprite;

      if (PlayerIndex == changeData.PlayerIndex) {
        if (facingDown) {
          newSprite = houseImages[changeData.Level];
        } else {
          newSprite = houseImages[changeData.Level + 4];
        }

        if (start) {
          start = false;
        } else {
          toolbox.playOneShotClip(0);
        }

        houseSpriteRenderer.sprite = newSprite;
      }
    }
}
