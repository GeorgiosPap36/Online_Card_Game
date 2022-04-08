using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    public AudioList audioList;

    void PlaySound(int n)
    {
        StartCoroutine(PlaySoundRoutine(n));
    }

    IEnumerator PlaySoundRoutine(int n)
    {
        transform.GetChild(0).GetComponent<AudioSource>().clip = audioList.audioList[n];
        transform.GetChild(0).gameObject.SetActive(true);
        yield return new WaitForSeconds(audioList.audioList[n].length);
        transform.GetChild(0).gameObject.SetActive(false);
    }
}
