using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[RequireComponent(typeof(BoxCollider))]
public class clickItem : MonoBehaviour
{
    public delegate void clickEffect();
    clickEffect myEffect;


    [Header("Required")]
    [SerializeField] SpriteRenderer effectRenderer;

    public enum items
    { 
        fireExtinguisher,
        anotherItem,
    }

    [Header("Item type")]
    [SerializeField] items myType;


    private bool doingEffect = false; //Temporary trash

    private List<Gaurd> enemiesInRange = new List<Gaurd>();

    private void Start()
    {
        if (effectRenderer)
        {
            effectRenderer.color = new Color(1, 1, 1, 0);
        }

        switch (myType)
        {
            case items.fireExtinguisher:
                {
                    myEffect = spriteFadeInOut;
                    myEffect += stunGaurd;
                    break;
                }


            default: Debug.Log("Item " + name + " does not have a type rip"); break;
        }
    }

    // Called when clicked
    public void wasClicked()
    {
        if (doingEffect)
            return;

        myEffect();
    }


    private void OnTriggerEnter(Collider other)
    {
        Gaurd inRangeEn = other.GetComponent<Gaurd>();
        
        if (!inRangeEn)
            return;
        Debug.Log("Gaurd in range");
        enemiesInRange.Add(inRangeEn);
    }

    private void OnTriggerExit(Collider other)
    {
        Gaurd inRangeEn = other.GetComponent<Gaurd>();

        if (!inRangeEn || !enemiesInRange.Contains(inRangeEn))
            return;

        enemiesInRange.Remove(inRangeEn);
    }


    #region effects

    // Just makes the sprite fade in and out :)
    void spriteFadeInOut()
    {
        doingEffect = true;
        StartCoroutine(fadeInOut());
    }

    // Makes the gaurd stop moving and seeing anything. This injury will cause long time effects, he will no longer be able to play with his children. His boss at the Zoo will drop his pay to compensate for his slowness after this injury.
    // The hospital bills alone will make his bank run dry. His family will go hungry
    void stunGaurd()
    {
        for (int x = 0; x < enemiesInRange.Count; x++)
        {
            enemiesInRange[x].getStunned();
        }
    }

    #endregion

    IEnumerator fadeInOut()
    {
        const float speed = 0.5f;

        // Fade in
        while (effectRenderer.color.a < 1)
        {
            Color thisColour = effectRenderer.color;
            thisColour.a += speed * Time.deltaTime;
            effectRenderer.color = thisColour;
            yield return null;
        }

        // Wait a sec
        float timeNow = Time.time;
        while (Time.time - timeNow < 1.5f)
        {
            yield return null;
        }

        // Fade Out
        while (effectRenderer.color.a > 0)
        {
            Color thisColour = effectRenderer.color;
            thisColour.a -= speed * Time.deltaTime;
            effectRenderer.color = thisColour;
            yield return null;
        }

        //Effect done
        doingEffect = false;
    }
} 
