using System.Collections;
using UnityEngine;

public class ButtonDoorScript : MonoBehaviour
{
    public GameObject obstacle; // The obstacle to disable
    public TextMesh timerTextMesh; // The TextMesh object to display the timer
    public AudioClip tickingSound; // The ticking sound to play every second
    [SerializeField] private float tickingSoundStartTime;
    [SerializeField] private float tickingSoundEndTime;
    private AudioSource audioSource;
    private bool isPressed = false;
    private ButtonTrigger buttonTrigger;

    private void Start()
    {
        // Find the ButtonTrigger component on the same object
        buttonTrigger = GetComponent<ButtonTrigger>();
        audioSource = GetComponent<AudioSource>();

        // Debug log to check component assignment
        if (buttonTrigger == null)
        {
            Debug.LogError("ButtonTrigger component not found on the same object.");
        }
        if (audioSource == null)
        {
            Debug.LogError("AudioSource component not found.");
        }
        else if (tickingSound == null)
        {
            Debug.LogError("Ticking sound not assigned.");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isPressed)
        {
            isPressed = true;
            StartCoroutine(DisableObstacle());
        }
    }

    private IEnumerator DisableObstacle()
    {
        obstacle.SetActive(false);
        float timeLeft = 5f;

        while (timeLeft > 0)
        {
            timerTextMesh.text = Mathf.CeilToInt(timeLeft).ToString();
            PlayTickingSound(tickingSoundStartTime, tickingSoundEndTime);
            yield return new WaitForSeconds(1f);
            timeLeft -= 1f;
        }

        timerTextMesh.text = "";
        obstacle.SetActive(true);
        isPressed = false;

        // Stop ticking sound and reset the button sprite
        StopTickingSound();
        if (buttonTrigger != null)
        {
            buttonTrigger.ResetSprite();
        }
    }

    private void PlayTickingSound(float startTime, float endTime)
    {
        if (audioSource != null && tickingSound != null)
        {
            audioSource.clip = tickingSound;
            audioSource.time = startTime;
            audioSource.Play();
            StartCoroutine(StopTickingSoundAfterDuration(endTime - startTime));
        }
    }

    private void StopTickingSound()
    {
        if (audioSource != null && audioSource.isPlaying)
        {
            audioSource.Stop();
        }
    }

    private IEnumerator StopTickingSoundAfterDuration(float duration)
    {
        yield return new WaitForSeconds(duration);
        audioSource.Stop();
    }
}

