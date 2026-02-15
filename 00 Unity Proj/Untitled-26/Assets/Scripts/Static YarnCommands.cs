using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using Yarn.Unity;


public class Quit
{
    [YarnCommand("quitGame")]
    public static void ExitGame()
    {
        Application.Quit();
        Debug.Log("Game is exiting");
        //Just to make sure its working
    }
}

public class Music
{
    [YarnCommand("musicOn")]
    public static void MusicOn(AudioSource music)
    {
        music.Play();
        Debug.Log("Playing " + music.name);
    }

    [YarnCommand("musicOff")]
    public static void MusicOff(AudioSource music)
    {
        music.Stop();
        Debug.Log("Stopping " + music.name);
    }
}

public class Location
{
    static SpriteRenderer currentLocation;

    [YarnCommand("Swap")]
    public static void Swap(GameObject Location)
    {
        Debug.Log("The current Location is " + currentLocation);
        if (Location != null)
        {
            SpriteRenderer locationSprite = Location.GetComponent<SpriteRenderer>();
            SpriteRenderer lastLocation;


            lastLocation = currentLocation;
            currentLocation = locationSprite;
            currentLocation.enabled = true;
            Debug.Log(Location + " is now active");
            if (lastLocation != null)
            {
                lastLocation.enabled = false;
                Debug.Log(lastLocation + " is no longer active");
            }
        }
        else Debug.LogWarning("GameObject not found for command 'Swap'.");
    }
}

public class Animation
{
    static SpriteRenderer currentEmotion;
    static Animator currentAnimation;

    [YarnCommand("Animate")]
    public static void Animate(GameObject Emotion)
    {
        Debug.Log("The current animation is " + currentEmotion);
        if (Emotion != null)
        {
            SpriteRenderer emotionSprite = Emotion.GetComponent<SpriteRenderer>();
            //Animator emotionAnimation = Emotion.GetComponent<Animator>();
            SpriteRenderer lastEmotion;
            //Animator lastAnimation;


            lastEmotion = currentEmotion;
            //lastAnimation = currentAnimation;
            currentEmotion = emotionSprite;
            //currentAnimation = emotionAnimation;
            currentEmotion.enabled = true;
            //currentAnimation.enabled = true;
            Debug.Log(Emotion + " is now active");
            if (lastEmotion != null)
            {
                lastEmotion.enabled = false;
                //lastAnimation.enabled = false;
                Debug.Log(lastEmotion + " is no longer active");
            }
        }
        else Debug.LogWarning("GameObject not found for command 'Animate'.");
    }

    [YarnCommand("StopAnimation")]
    public static void StopAnimation()
    {
        Debug.Log("The current animation is " + currentEmotion);
        if (currentEmotion != null)
        {
            SpriteRenderer emotionSprite = currentEmotion.GetComponent<SpriteRenderer>();
            //Animator emotionAnimation = currentEmotion.GetComponent<Animator>();
            emotionSprite.enabled = false;
            //emotionAnimation.enabled = false;
            Debug.Log(emotionSprite + " is no longer active");
            currentEmotion = null;
            //currentAnimation = null;
        }
    }
}
