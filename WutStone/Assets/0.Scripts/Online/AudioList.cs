using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New AudioList", menuName = "AudioList")]
public class AudioList : ScriptableObject
{
    public AudioClip[] audioList;
}
