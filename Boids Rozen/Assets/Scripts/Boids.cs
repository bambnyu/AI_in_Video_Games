using System.Collections.Generic;
using UnityEngine;

public class Boid : MonoBehaviour
{
    [Header("Boid Settings")]
    public float maxSpeed = 5f;
    public float maxForce = 1f;
    public float neighborRadius = 2.5f;
    public float separationRadius = 1.5f;

    [Header("Boundary Settings")]
    public Vector2 boxSize = new Vector2(10, 10); // Size of the bounding box

    [Header("Flee Settings")]
    public float fleeRadius = 3.0f; // Radius to start fleeing from the wolf

    private Vector2 velocity;
    private List<Boid> allBoids;
    private Transform wolfTransform; // Reference to the wolf's transform

    private void Start()
    {
        velocity = Random.insideUnitCircle.normalized * maxSpeed;
        allBoids = new List<Boid>(FindObjectsOfType<Boid>());

        // Locate the wolf by its tag (set the wolf's tag to "Wolf" in Unity)
        GameObject wolf = GameObject.FindGameObjectWithTag("Wolf");
        if (wolf != null)
        {
            wolfTransform = wolf.transform;
        }
    }

    private void FixedUpdate()
    {
        Vector2 alignment = CalculateAlignment() * 1.5f;
        Vector2 cohesion = CalculateCohesion() * 1.0f;
        Vector2 separation = CalculateSeparation() * 1.5f;

        Vector2 steering = alignment + cohesion + separation;
        steering = Vector2.ClampMagnitude(steering, maxForce);

        velocity = Vector2.ClampMagnitude(velocity + steering, maxSpeed);

        // Apply boundary constraint
        Vector2 boundaryForce = KeepInsideBox();
        velocity += boundaryForce;

        // Apply flee behavior if the wolf is within the flee radius
        if (wolfTransform != null)
        {
            Vector2 fleeForce = FleeFromWolf();
            velocity += fleeForce;
        }

        transform.position += (Vector3)velocity * Time.fixedDeltaTime;
        transform.up = velocity;
    }

    private Vector2 FleeFromWolf()
    {
        Vector2 fleeForce = Vector2.zero;
        if (wolfTransform != null)
        {
            float distanceToWolf = Vector2.Distance(transform.position, wolfTransform.position);
            if (distanceToWolf < fleeRadius)
            {
                // Calculate the direction away from the wolf
                Vector2 fleeDirection = (Vector2)(transform.position - wolfTransform.position);
                fleeDirection = fleeDirection.normalized * maxSpeed;
                fleeForce = fleeDirection - velocity;
                fleeForce = Vector2.ClampMagnitude(fleeForce, maxForce);
            }
        }
        return fleeForce;
    }

    private Vector2 KeepInsideBox()
    {
        Vector2 force = Vector2.zero;
        Vector2 halfBoxSize = boxSize / 2;

        if (transform.position.x > halfBoxSize.x)
            force.x = -maxForce;
        else if (transform.position.x < -halfBoxSize.x)
            force.x = maxForce;

        if (transform.position.y > halfBoxSize.y)
            force.y = -maxForce;
        else if (transform.position.y < -halfBoxSize.y)
            force.y = maxForce;

        return force;
    }

    private Vector2 CalculateAlignment()
    {
        Vector2 alignmentForce = Vector2.zero;
        int neighborCount = 0;

        foreach (Boid boid in allBoids)
        {
            if (boid == this) continue;

            float distance = Vector2.Distance(transform.position, boid.transform.position);
            if (distance < neighborRadius)
            {
                alignmentForce += boid.velocity;
                neighborCount++;
            }
        }

        if (neighborCount > 0)
        {
            alignmentForce /= neighborCount;
            alignmentForce = alignmentForce.normalized * maxSpeed;
            alignmentForce -= velocity;
            alignmentForce = Vector2.ClampMagnitude(alignmentForce, maxForce);
        }

        return alignmentForce;
    }

    private Vector2 CalculateCohesion()
    {
        Vector2 cohesionForce = Vector2.zero;
        int neighborCount = 0;

        foreach (Boid boid in allBoids)
        {
            if (boid == this) continue;

            float distance = Vector2.Distance(transform.position, boid.transform.position);
            if (distance < neighborRadius)
            {
                cohesionForce += (Vector2)boid.transform.position;
                neighborCount++;
            }
        }

        if (neighborCount > 0)
        {
            cohesionForce /= neighborCount;
            cohesionForce -= (Vector2)transform.position;
            cohesionForce = cohesionForce.normalized * maxSpeed;
            cohesionForce -= velocity;
            cohesionForce = Vector2.ClampMagnitude(cohesionForce, maxForce);
        }

        return cohesionForce;
    }

    private Vector2 CalculateSeparation()
    {
        Vector2 separationForce = Vector2.zero;
        int neighborCount = 0;

        foreach (Boid boid in allBoids)
        {
            if (boid == this) continue;

            float distance = Vector2.Distance(transform.position, boid.transform.position);
            if (distance < separationRadius && distance > 0)
            {
                Vector2 diff = (Vector2)(transform.position - boid.transform.position);
                diff /= distance; // Weight by distance
                separationForce += diff;
                neighborCount++;
            }
        }

        if (neighborCount > 0)
        {
            separationForce /= neighborCount;
            separationForce = separationForce.normalized * maxSpeed;
            separationForce -= velocity;
            separationForce = Vector2.ClampMagnitude(separationForce, maxForce);
        }

        return separationForce;
    }
}
