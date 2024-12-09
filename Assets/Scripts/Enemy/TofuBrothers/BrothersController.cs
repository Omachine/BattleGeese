using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrothersController : MonoBehaviour
{
    // Public gameobjects for the brothers from inspector
    public OtoutoBt otoutoBt1;
    public OtoutoBt otoutoBt2;
    public Transform avoidant;
    public Animator animator1;
    public Animator animator2;

    // isAgresive is a bool that is set to true for one brother and false for the other
    // every 5 seconds, the bool is switched
    public float timeToSwitch = 5f;

    private enum Phase { None, Aggressive, Avoiding }
    private Phase currentPhase = Phase.None;

    private void Start()
    {
        otoutoBt1 = transform.Find("Otouto").GetComponent<OtoutoBt>();
        otoutoBt2 = transform.Find("Ani").GetComponent<OtoutoBt>();
        animator1 = transform.Find("Otouto/Sprite").GetComponent<Animator>();
        animator2 = transform.Find("Ani/Sprite").GetComponent<Animator>();
        otoutoBt1.isAgresive = true;
        otoutoBt2.isAgresive = false;
        StartCoroutine(SwitchAgressive());
    }

    private IEnumerator SwitchAgressive()
    {
        while (true)
        {
            yield return new WaitForSeconds(timeToSwitch);
            otoutoBt1.isAgresive = !otoutoBt1.isAgresive;
            otoutoBt2.isAgresive = !otoutoBt2.isAgresive;

            Debug.Log("yes");
            if (animator1.GetBool("isAngry") == true)
                animator1.SetBool("isAngry", false);
            if (animator1.GetBool("isAvoidant") == true)
                animator1.SetBool("isAvoidant", false);
            if (animator2.GetBool("isAngry") == false)
                animator2.SetBool("isAngry", true);
            if (animator2.GetBool("isAvoidant") == false)
                animator2.SetBool("isAvoidant", true);

            // Trigger dash on phase change
            if (otoutoBt1.isAgresive)
            {
                ChangePhase(otoutoBt1, Phase.Aggressive);
                ChangePhase(otoutoBt2, Phase.Avoiding);
            }
            else
            {
                ChangePhase(otoutoBt1, Phase.Avoiding);
                ChangePhase(otoutoBt2, Phase.Aggressive);
            }
        }
    }

    private void ChangePhase(OtoutoBt otoutoBt, Phase newPhase)
    {
        if (currentPhase != newPhase)
        {
            currentPhase = newPhase;
            otoutoBt.hasDashes = true; // Reset hasDashes when the phase changes
        }
    }
}





