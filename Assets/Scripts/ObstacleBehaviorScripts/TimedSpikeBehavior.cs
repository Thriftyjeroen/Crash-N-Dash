using JetBrains.Annotations;
using System.Collections;
using System.Numerics;
using UnityEngine;

public class TimedSpikeBehavior : MonoBehaviour
{
    float timeSpikeIsRetracted = 1;
    float timeSpikeIsExtracted = 1;
    int spikeDamage = 40;
    string damageTag = "Enemy";
    string emptyTag = "Untagged";


    public bool spikesActive = false;
    public bool ActivateSpikesNow = true;


    [SerializeField] Sprite spriteActivated;
    [SerializeField] Sprite spriteRetracted;
    public SpriteRenderer spriteRenderer;
    BoxCollider2D boxCollider;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        timeSpikeIsExtracted = Random.Range(0.7f, 2);
        timeSpikeIsRetracted = Random.Range(0.7f, 4);


        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        //activates the spikes for the 1st time, deleting this will cause it to not work (?)
        ActivateSpikesNow = true;
        boxCollider = gameObject.GetComponent<BoxCollider2D>();

    }

    // Update is called once per frame
    void Update()
    {
        //if spikes should activate now
        if (ActivateSpikesNow == true)
        {
            //starts coroutine for activating the spikes
            StartCoroutine(ActivateSpikeCycle());
        }
    }

    IEnumerator ActivateSpikeCycle()
    {
        //spikes activated
        ActivateSpikesNow = false;
        spikesActive = true;

        //enables boxcollider so collission can take place
        boxCollider.enabled = true;
        //adjust the spikes sprite (active and deactivated)
        AdjustSpikeSprite();

        //countdown how long spikes can be active
        yield return new WaitForSeconds(timeSpikeIsExtracted);
        spikesActive = false;
        //disables collission so next time collider is turned on, damage can be applied again
        boxCollider.enabled = false;
        AdjustSpikeSprite();
        //countdown till spikes can activate again
        yield return new WaitForSeconds(timeSpikeIsRetracted);

        //activates bool so it can be run again in update
        ActivateSpikesNow = true;
    }


    //adjust the sprite according to if spikes is active
    void AdjustSpikeSprite()
    {
        if (spikesActive == true) spriteRenderer.sprite = spriteActivated; else spriteRenderer.sprite = spriteRetracted;
    }
}
