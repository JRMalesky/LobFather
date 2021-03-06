﻿using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

[RequireComponent (typeof (BoxCollider2D))]

public class Controller2D : MonoBehaviour
{
    public LayerMask collisionmask; // mask used to identify obstacles the player should collide with
    public LayerMask enemyMask; // mask used to identify enemies the player can attack
    const float skinWidth = .015f;
    public int horizontalRayCount = 4;
    public int verticalRayCount = 4;

    float maxClimbAngle = 160;

    float horizontalRaySpacing;
    float verticalRaySpacing; 

    BoxCollider2D collider;
    RaycastOrigins raycastOrigins;

    public CollisionInfo collisions; 
	
    // Use this for initialization
	void Start ()
    {
        collider = GetComponent<BoxCollider2D>();
        CalculateRaySpacing();
    }

    void UpdateRaycastOrigins()
    {
        Bounds bounds = collider.bounds;
        bounds.Expand(skinWidth * -2);

        raycastOrigins.bottomLeft = new Vector2(bounds.min.x, bounds.min.y);
        raycastOrigins.bottomRight = new Vector2(bounds.max.x, bounds.min.y);
        raycastOrigins.topLeft = new Vector2(bounds.min.x, bounds.max.y);
        raycastOrigins.topRight = new Vector2(bounds.max.x, bounds.max.y);
    }

    void CalculateRaySpacing()
    {
        Bounds bounds = collider.bounds;
        bounds.Expand(skinWidth * -2);

        horizontalRayCount = Mathf.Clamp(horizontalRayCount, 2, int.MaxValue);
        verticalRayCount = Mathf.Clamp(verticalRayCount, 2, int.MaxValue);

        horizontalRaySpacing = bounds.size.y / (horizontalRayCount - 1);
        verticalRaySpacing = bounds.size.x / (verticalRayCount - 1); 
    }
    struct RaycastOrigins
    {
        public Vector2 topLeft, topRight;
        public Vector2 bottomLeft, bottomRight; 
    }

    public struct CollisionInfo
    {
        public bool above, below;
        public bool left, right;

        public bool climbingSlope;
        public float slopeAngle, slopeAngleOld;
        public void Reset()
        {
            above = below = false;
            left = right = false;
            climbingSlope = false;
            slopeAngleOld = slopeAngle;
            slopeAngle = 0;
        }
    }

    public void Move(Vector3 velocity)
    {
        collisions.Reset();
        UpdateRaycastOrigins();
        if (velocity.x != 0)
        {
            HorizontalCollisions(ref velocity);
        }
        if (velocity.y != 0)
        {
            VerticalCollisions(ref velocity);
        }
        transform.Translate(velocity);
    }
    public void Attack(Vector3 velocity, float damage, bool dir)
    {
        float directionX = (dir) ? -1 : 1; 
        float rayLength = .50f;
        if (SceneManager.GetActiveScene().name == "Level5")
            rayLength = 1.0f;
        Vector2 rayOrigin = (directionX == -1) ? raycastOrigins.bottomLeft : raycastOrigins.bottomRight;
        rayOrigin += Vector2.up * (horizontalRaySpacing);
        RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.right * directionX, rayLength, enemyMask);

        Debug.DrawRay(rayOrigin, Vector2.right * directionX * rayLength, Color.green);


        if(hit)
        {
            if(hit.transform.gameObject.GetComponent<scr_enemyHealth>())
            {
                Debug.Log("HIT ENEMY");
                hit.transform.gameObject.GetComponent<scr_enemyHealth>().ApplyDamage(damage);
            }
            else if (hit.transform.gameObject.tag == "Projectile")
            {
                hit.transform.gameObject.GetComponent<scr_ThrowCane>().bl_hitback = true;
            }
            else
            {
                Debug.Log("HIT OBSTACLE");
            }
        }
    }
    void HorizontalCollisions(ref Vector3 velocity)
    {
        float directionX = Mathf.Sign(velocity.x);
        float rayLength = Mathf.Abs(velocity.x) + skinWidth;

        for (int i = 0; i < horizontalRayCount; ++i)
        {
            Vector2 rayOrigin = (directionX == -1) ? raycastOrigins.bottomLeft : raycastOrigins.bottomRight;
            rayOrigin += Vector2.up * (horizontalRaySpacing * i);
            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.right * directionX, rayLength, collisionmask );

            Debug.DrawRay(rayOrigin, Vector2.right * directionX * rayLength, Color.red);

            if (hit)
            {
                float slopeAngle = Vector2.Angle(hit.normal, Vector2.up);
                if(i==0 && slopeAngle+90<=maxClimbAngle)
                {
                    float distanceToSlopeStart = 0;
                    if(slopeAngle != collisions.slopeAngleOld)
                    {
                        distanceToSlopeStart = hit.distance - skinWidth;
                        velocity.x -= distanceToSlopeStart * directionX; 
                    }
                    ClimbSlope(ref velocity, slopeAngle);
                    velocity.x += distanceToSlopeStart * directionX;
                }
                if(!collisions.climbingSlope || slopeAngle > maxClimbAngle)
                {
                    float hitRounded = Mathf.Round(hit.distance * 1000.0f) / 1000.0f;
                    velocity.x = (hitRounded - skinWidth) * directionX;
                    rayLength = hit.distance;

                    if(collisions.climbingSlope)
                    {
                        velocity.y = Mathf.Tan(collisions.slopeAngle * Mathf.Deg2Rad) * Mathf.Abs(velocity.x);
                    }

                    collisions.left = directionX == -1;
                    collisions.right = directionX == 1;
                }
                
            }
        }
    }
    void VerticalCollisions(ref Vector3 velocity)
    {
        float directionY = Mathf.Sign(velocity.y);
        float rayLength = Mathf.Abs(velocity.y) + skinWidth; 

        for (int i = 0; i < verticalRayCount; ++i)
        {
            Vector2 rayOrigin = (directionY == -1) ? raycastOrigins.bottomLeft : raycastOrigins.topLeft; 
            rayOrigin += Vector2.right * (verticalRaySpacing * i + velocity.x);
            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.up * directionY, rayLength, collisionmask);

            Debug.DrawRay(rayOrigin, Vector2.up * directionY * rayLength, Color.red);

            if (hit)
            {
                float hitRounded = Mathf.Round(hit.distance * 1000.0f) / 1000.0f;
                velocity.y = (hitRounded - skinWidth) * directionY;
                rayLength = hit.distance;

                if(collisions.climbingSlope)
                {
                    velocity.x = velocity.y / Mathf.Tan(collisions.slopeAngle * Mathf.Deg2Rad) * Mathf.Sign(velocity.x);
                }
                collisions.below = directionY == -1;
                collisions.above  = directionY == 1;
            }
        }
    }

    void ClimbSlope(ref Vector3 velocity, float slopeAngle)
    {
        float moveDistance = Mathf.Abs(velocity.x);
        float climbVelocityY = Mathf.Sin(slopeAngle * Mathf.Deg2Rad) * moveDistance;

        if(velocity.y<=climbVelocityY)
        { 
            velocity.y = climbVelocityY;
            velocity.x = Mathf.Cos(slopeAngle * Mathf.Deg2Rad) * moveDistance * Mathf.Sign(velocity.x);
            collisions.below = true;
            collisions.climbingSlope = true;
            collisions.slopeAngle = slopeAngle;
        }

    }
}
