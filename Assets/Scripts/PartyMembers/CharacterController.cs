using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController 
{
    public GameStateManager gameManager;
    public InputProvider provider;

    public Vector2 movementDirection;
    public float movementSp;
    public bool _isSprinting;
    public bool isDashing;

    public Vector2 position;
    
    public Vector2 resultPosition;
    public Vector2 targetPosition;
    public Vector2 initialPosition;
    public Vector2 targetDelta;
    public Vector2 actualDelta;

    public float angle;
    public Vector2 reconstructedMovement;
    public Rigidbody2D rb;

    public float sprintDuration;
}
