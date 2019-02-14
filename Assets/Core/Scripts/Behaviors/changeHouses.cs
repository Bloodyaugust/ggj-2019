using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class changeHouses : MonoBehaviour
{
    public List<Sprite> houseImages = new List<Sprite>();
    public bool facingDown = true;
    public int level = 0;
    public int PlayerIndex;
    public AnimationCurve shrinkCurve;
    public AnimationCurve growCurve;
    SpriteRenderer houseSpriteRenderer;
    bool start = false;
    Toolbox toolbox;
    animStage _curStage = animStage.Wait;
    private float startTime;
    private float curveTime;
    Sprite newSprite;

    private enum animStage
    {
        Wait,
        Shrink,
        Grow
    }

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
        switch (_curStage)
        {
            case animStage.Shrink:
                float shrinkTime = shrinkCurve.Evaluate(DeltaTime());
                gameObject.transform.localScale = new Vector2(shrinkTime, shrinkTime);


                if (shrinkTime == 0)
                {
                    houseSpriteRenderer.sprite = newSprite;
                    startTime = Time.time;
                    _curStage = animStage.Grow;
                    curveTime = .3f;
                }
                break;
            case animStage.Grow:
                float growTime = growCurve.Evaluate(DeltaTime());
                gameObject.transform.localScale = new Vector2(growTime, growTime);
                if (growTime == 1)
                {
                    _curStage = animStage.Wait;
                }
                break;
            case animStage.Wait:
                break;
        }
    }

    void UpdateSprite(HouseChangeData changeData) {
      //Sprite newSprite;

      if (PlayerIndex == changeData.PlayerIndex) {
        if (facingDown) {
          newSprite = houseImages[changeData.Level];
        } else {
          newSprite = houseImages[changeData.Level + 4];
        }

        if (start)
        {
          start = false;
          houseSpriteRenderer.sprite = newSprite;
        }
          else
        {
          startTime = Time.time;
          curveTime = 0.1f;
          _curStage = animStage.Shrink;
          toolbox.playOneShotClip(0);
        }

      }
    }

    float DeltaTime()
    {
        float timeDelta = Time.time - startTime;

        if (timeDelta < curveTime)
        {
            return timeDelta / curveTime;
        }
        else
        {
            return 1;
        }
    }
}
