using UnityEngine;

public class PlayerController : MonoBehaviour {
    
    [SerializeField] private InputProvider provider;
    
    private Laurie laurie;
    private Player player;
    private PlayerCasting casting;
    private PlayerAbilities abilities;
    
    public Vector2 _movementDirection;
    [SerializeField] private float movementSp;
    [SerializeField] private bool _isSprinting;
    [SerializeField] private bool isDashing;


    private Vector2 position;
    
    private Vector2 resultPosition;
    private Vector2 targetPosition;
    private Vector2 initialPosition;
    private Vector2 targetDelta;
    private Vector2 actualDelta;

    private float angle;
    public Vector2 reconstructedMovement;
    private Rigidbody2D rb;

    public float sprintDuration;

    private void Start() {
        laurie = GetComponent<Laurie>();
        player = GetComponent<Player>();
        casting = GetComponent<PlayerCasting>();
        abilities = GetComponentInChildren<PlayerAbilities>();
        rb = GetComponent<Rigidbody2D>();

        provider.OnPrimary += OnPrimary_PrimaryCast;
        provider.OnSecondary += OnSecondary_SecondaryCast;
        provider.OnAuxMove += OnAuxMove_AuxillaryMovement;
    }

    private void Update() {
        _movementDirection = provider.inputState.movementDirection;
        _isSprinting = provider.inputState.isSprinting;

        Move(Time.fixedDeltaTime);
    }

    private void Move(float d) {
        if (_movementDirection.Equals(new Vector2(0, 0))) {
            player.movementType = MovementState.Idle;
            sprintDuration = 0f;
            isDashing = false;
            return;
        }

        if (_isSprinting) {
            if (sprintDuration >= 7f) {
                player.movementType = MovementState.Dash;
                isDashing = true;
                movementSp = laurie.dashSp;
            }else {
                player.movementType = MovementState.Sprint;
                movementSp = laurie.sprintSp;
                isDashing = false;
            }
            sprintDuration += Time.deltaTime;
        }else {
            player.movementType = MovementState.Walk;
            movementSp = laurie.walkSp;
            sprintDuration = 0f;
        }

        position = transform.position;
        position = PixelPerfectClamp(position, 16f);

        angle = (float)(Mathf.Atan2(_movementDirection.y, _movementDirection.x));

        reconstructedMovement = new Vector2(Mathf.Cos(angle) * movementSp, Mathf.Sin(angle) * movementSp);
        reconstructedMovement = PixelPerfectClamp(reconstructedMovement, 16f);
        
        rb.MovePosition(new Vector2(position.x, position.y) + ((reconstructedMovement * movementSp) * d));
        resultPosition = transform.position;

        targetPosition = new Vector2(position.x, position.y) + ((reconstructedMovement * movementSp) * d);

        targetDelta = targetPosition - initialPosition;
        actualDelta = resultPosition - initialPosition;

        initialPosition = transform.position;
    }

    public void OnPrimary_PrimaryCast() {
        casting.PrimaryCast();
    }

    public void OnSecondary_SecondaryCast() {
        casting.SecondaryCast();
    }

    public void OnAuxMove_AuxillaryMovement() {
        abilities.AuxMove();
    }
    
    private Vector2 PixelPerfectClamp(Vector2 moveVector, float pixelsPerUnit) {

        Vector2 vectorInPixels = new Vector2(
            Mathf.RoundToInt(moveVector.x * pixelsPerUnit),
            Mathf.RoundToInt(moveVector.y * pixelsPerUnit)
        );
        
        return vectorInPixels / pixelsPerUnit;
    }
}
