using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sprite_Touch : MonoBehaviour
{
    public List<Sprite> mySprite = new List<Sprite>();
    int SpriteIndex = 0;

    private void OnMouseUp()
    {
        if (SpriteIndex < mySprite.Count)
        {
            GetComponent<Image>().sprite = mySprite[SpriteIndex];
            SpriteIndex++;
        }
        else
            print("끝");
    }
}
