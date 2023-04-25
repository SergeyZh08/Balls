using UnityEngine;

public enum ItemType
{
    Empty,
    Ball,
    Box,
    Barrel,
    Stone,
    Dynamit,
    Star,
    Chest
}

public class Item : MonoBehaviour
{
    [field: SerializeField] public ItemType ItemType { get; private set; }
}
