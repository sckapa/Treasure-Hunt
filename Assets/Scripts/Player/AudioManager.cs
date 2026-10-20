using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField]
    private List<AudioClip> backgroundMusic;

    private void Start()
    {
        Shuffle();
        StartCoroutine(PlayBackgroundMusic());
    }

    public void Shuffle()
    {
        int n = backgroundMusic.Count;
        for (int i = n - 1; i > 0; i--)
        {
            int randomIndex = Random.Range(0, i + 1);

            var temp = backgroundMusic[i];
            backgroundMusic[i] = backgroundMusic[randomIndex];
            backgroundMusic[randomIndex] = temp;
        }
    }

    private IEnumerator PlayBackgroundMusic()
    {
        var audioSource = GetComponent<AudioSource>();
        int index = 0;

        while (true)
        {
            audioSource.clip = backgroundMusic[index];
            audioSource.Play();

            yield return new WaitForSeconds(audioSource.clip.length);

            index++;
            if (index >= backgroundMusic.Count)
            {
                index = 0;
            }
        }
    }
}
