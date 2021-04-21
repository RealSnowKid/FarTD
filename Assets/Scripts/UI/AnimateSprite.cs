using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimateSprite : MonoBehaviour
{
    [SerializeField] List<Sprite> frames = new List<Sprite>();
    public Image animatedObj;
    private int currentFrame;
    private float timer;
    private float frameRate = .1f;

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= frameRate)
        {
            timer -= frameRate;
            currentFrame = (currentFrame + 1) % frames.Count;
            animatedObj.sprite = frames[currentFrame];
        }
    }
}
