using UnityEngine;

public class HorizontalEnemyController : BaseUnitController
{
    [SerializeField] private Collider2D _mainCollider;
    [SerializeField] private LayerMask _wallLayer;

    private const float WallsRaycastLength = 0.15f;

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        CheckWallsHorizontal();
        Move(FacingDirection, false);
    }

    private void CheckWallsHorizontal()
    {

        var horizontalBooundsPoint = (_mainCollider.bounds.extents.x + 0.01f) * Vector3.right * FacingDirection + (Vector3) _mainCollider.offset;

        var result = (bool) Physics2D.Raycast(transform.position + horizontalBooundsPoint, FacingDirection * Vector2.right, WallsRaycastLength, _wallLayer);

        if (result)
            Flip();
    }

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();

        var horizontalBooundsPoint = transform.position + (_mainCollider.bounds.extents.x + 0.01f) * Vector3.right * FacingDirection + (Vector3) _mainCollider.offset;

        Gizmos.color = Color.white;
        Gizmos.DrawSphere(horizontalBooundsPoint, 0.05f);

        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(horizontalBooundsPoint, horizontalBooundsPoint + FacingDirection * Vector3.right * WallsRaycastLength);
    }
}
