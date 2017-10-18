using Entitas.CodeGeneration.Attributes;
using UnityEngine;

[Game, Unique, CreateAssetMenu]
public class PlayerInitData : ScriptableObject
{
    public GameObject Prefab;

    public float Speed = 2.0f;
}
