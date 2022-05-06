using UnityEngine;

public class PlayerController : MonoBehaviour {
    
    [SerializeField] private InputProvider provider;
    
    private Laurie laurie;
    
    private Vector2 _movementDirection;
    private bool _isSprinting;

    private Vector2 position;
    
    private Vector2 resultPosition;
    private Vector2 targetPosition;
    private Vector2 initialPosition;
    private Vector2 targetDelta;
    private Vector2 actualDelta;

    private float angle;
    private Vector2 reconstructedMovement;
    private Rigidbody2D rb;

    private void Start() {
        laurie = GetComponent<Laurie>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update() {
        _movementDirection = provider.inputState.movementDirection;
        _isSprinting = provider.inputState.isSprinting;

        Move(Time.fixedDeltaTime);
    }

    private void Move(float d) {
        if (_movementDirection.Equals(new Vector2(0, 0))) return;

        Debug.Log(_isSprinting);

        position = transform.position;
        position = PixelPerfectClamp(position, 16f);

        angle = (float)(Mathf.Atan2(_movementDirection.y, _movementDirection.x));

        reconstructedMovement = new Vector2(Mathf.Cos(angle) * laurie.movementSp, Mathf.Sin(angle) * laurie.movementSp);
        reconstructedMovement = PixelPerfectClamp(reconstructedMovement, 16f);
        
        rb.MovePosition(new Vector2(position.x, position.y) + ((reconstructedMovement * laurie.movementSp) * d));
        resultPosition = transform.position;

        targetPosition = new Vector2(position.x, position.y) + ((reconstructedMovement * laurie.movementSp) * d);

        targetDelta = targetPosition - initialPosition;
        actualDelta = resultPosition - initialPosition;

        initialPosition = transform.position;
    }
    
    private Vector2 PixelPerfectClamp(Vector2 moveVector, float pixelsPerUnit) {

        Vector2 vectorInPixels = new Vector2(
            Mathf.RoundToInt(moveVector.x * pixelsPerUnit),
            Mathf.RoundToInt(moveVector.y * pixelsPerUnit)
        );
        
        return vectorInPixels / pixelsPerUnit;
    }
}
