using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "so_SoundEffects", menuName = "Scriptable Objects/Sound/Create Sound Effects Object")]
public class SO_SoundEffects : ScriptableObject
{
    [SerializeField]
    public List<SoundEffect> soundEffects;
}
