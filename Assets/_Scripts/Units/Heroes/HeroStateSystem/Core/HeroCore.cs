using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroCore : MonoBehaviour
{
    protected Collisions collisions;
    private Movements movements;
    public Rigidbody2D playerRB;
    private int score;
    protected HeroInput heroInput;
    public Movements Movements
    {
        get => GenericNotImplementedError<Movements>.TryGet(movements, "Movements");
        private set => movements = value;
    }
    public int Score
    {
        get => score;
        private set => score = value;
    }
    public Collisions Collisions
    {
        get => GenericNotImplementedError<Collisions>.TryGet(collisions, "Collisions");
        private set => collisions = value;
    }
    public HeroInput HeroInput
    {
        get => GenericNotImplementedError<HeroInput>.TryGet(heroInput, "HeroInput");
        private set => heroInput = value;
    }
    private void Awake()
    {
        Collisions = GetComponent<Collisions>();
        Movements = GetComponent<Movements>();
        HeroInput = GetComponent<HeroInput>();

    }

    public void LogicUpdate()
    {
        Collisions.CheckCollision();
        Movements.LogicUpdate();
    }
}
