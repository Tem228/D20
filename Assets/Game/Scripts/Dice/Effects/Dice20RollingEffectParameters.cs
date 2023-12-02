using DG.Tweening;
using UnityEngine;

[System.Serializable]
public struct Dice20RollingEffectParameters
{
    [field : SerializeField]
    public int StepAmount { get; private set; }

    [field: SerializeField]
    public float Duration { get; private set; }

    [field: SerializeField]
    public float Angle { get; private set; }

    [field: SerializeField]
    public RotateMode RotateMode { get; private set; }
}
