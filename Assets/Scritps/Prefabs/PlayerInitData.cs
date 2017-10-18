using Entitas.CodeGeneration.Attributes;
using UnityEngine;

[Game, Unique, CreateAssetMenu]
public class PlayerInitData : ScriptableObject
{
    public float Speed = 2.0f;
}
