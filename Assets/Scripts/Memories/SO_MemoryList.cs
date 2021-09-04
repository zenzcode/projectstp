using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "so_memoryList", menuName = "Scriptable Objects/Memory/MemoryList")]
public class SO_MemoryList : ScriptableObject
{
    [SerializeField] public List<Memory> memories;
}
