using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Vehicle : MonoBehaviour {

    #region Fields
    public Vector3 vehiclePosition;     //the position of the vehicle
    public Vector3 velocity;            //the velocity of the vehicle
    public Vector3 direction;           //the direction vector of the vehicle
    public Vector3 acceleration;        //the acceleration vector of the vehicle
    public float mass;                  //the mass of the vehicle
    public float maxSpeed;              //the maximum speed of the vehicle
    public float maxForce;              //the maximum force of the vehicle
    public Vector3 ultimateForce;       //the sum of all the forces in the vehicle

    public float frictCoeff;            //a friction coefficient for use in friction calculations

    //public Terrain terrain;             //the current terrain
    //public Vector3 terrainDimensions;   //the dimensions of the terrain

    public Material forwardMaterial;
    public Material rightMaterial;
    public Material futureMaterial;

    public bool showDebugLines;

    public List<GameObject> obstacles;

    public float radius; 
    public float safeRadius;

    public float currentAngle;
    public float previousAngle;
    public float wanderStrength;

    //Gameplay Fields
    [SerializeField]
    protected float health;

    protected bool paused;

    public bool inTrigger;
    public List<int> triggeredObstacles;
    #endregion

    #region Properties
    public bool Paused
    {
        get { return paused; }
        set { paused = value; }
    }
    public float Health
    {
        get { return health; }
        set { health = value; }
    }
    #endregion

    // Use this for initialization
    protected void Start()
    {
        vehiclePosition = transform.position;
    }

    // Update is called once per frame
    protected void Update()
    {
        CalcSteeringForces();
        //ultimateForce += Separation();
        //ultimateForce += ObstacleAvoidance();
        //ultimateForce += GenerateFriction(frictCoeff);

        //ApplyForce(ultimateForce);
        gameObject.GetComponent<Rigidbody2D>().AddForce(ultimateForce); 

        //velocity += acceleration * Time.deltaTime;
        //vehiclePosition += velocity * Time.deltaTime;

        direction = gameObject.GetComponent<Rigidbody2D>().velocity.normalized;
        //direction = velocity.normalized;
        //acceleration = Vector3.zero;

        //transform.position = vehiclePosition;
        //transform.Translate(velocity * Time.deltaTime);
        //gameObject.GetComponent<Rigidbody2D>().MovePosition(vehiclePosition);

        if (velocity.sqrMagnitude < 0.0125) velocity = Vector3.zero;

        //OrientAgent();

        //Debug.DrawLine(transform.position, transform.position + transform.forward, Color.green);
        //Debug.DrawLine(transform.position, transform.position + transform.right, Color.blue);
    }

    /// <summary>
    /// Applies a force to the vehicle
    /// </summary>
    /// <param name="force">The force being applied</param>
    public void ApplyForce(Vector3 force)
    {
        acceleration += force / mass;
    }

    /// <summary>
    /// Applies friction to the vehicle
    /// </summary>
    /// <param name="coeff">The coefficient of friction</param>
    public Vector3 GenerateFriction(float coeff)
    {
        Vector3 friction = velocity * -1;
        friction.Normalize();
        friction = friction * coeff;
        return friction;
    }

    ///// <summary>
    ///// Keeps the vehicle within the bounds of the terrain
    ///// </summary>
    //public Vector3 StayWithinBounds()
    //{
    //    Vector3 futurePosition = transform.position + velocity * Time.deltaTime;
    //    if (futurePosition.x < 0 || futurePosition.x > terrainDimensions.x || futurePosition.y < 0 || futurePosition.y > terrainDimensions.y)
    //    {
    //        Vector3 terrainCenter = new Vector3(terrainDimensions.x / 2, 0, terrainDimensions.y / 2);
    //        Debug.DrawLine(transform.position, terrainCenter);
    //        return Seek(new Vector3(25,0,25)) * 2;
    //    }
    //    return Vector3.zero;
    //}

    /// <summary>
    /// Seeks a target position
    /// </summary>
    /// <param name="targetPosition">The position of the target to seek</param>
    /// <returns>A force vector to apply to the vehicle to move it towards the target</returns>
    public Vector3 Seek(Vector3 targetPosition)
    {
        Vector3 desiredVelocity = targetPosition - transform.position;
        desiredVelocity.z = 0; 
        desiredVelocity = desiredVelocity.normalized * maxSpeed;

        Vector3 seekingForce = desiredVelocity - velocity;

        return seekingForce;
    }

    /// <summary>
    /// Seeks a target gameobject
    /// </summary>
    /// <param name="target">The gameobject to seek</param>
    /// <returns>A force vector to apply to the vehicle to move it towards the target</returns>
    public Vector3 Seek(GameObject target)
    {
        return Seek(target.transform.position);
    }

    /// <summary>
    /// Flees from a target position
    /// </summary>
    /// <param name="targetPosition">The position of the target to flee from</param>
    /// <returns>A force vector to apply to the vehicle to move it away from the target</returns>
    public Vector3 Flee(Vector3 targetPosition)
    {
        Vector3 desiredVelocity = transform.position - targetPosition;
        desiredVelocity.y = 0;
        desiredVelocity = desiredVelocity.normalized * maxSpeed;

        Vector3 fleeingForce = desiredVelocity - velocity;

        return fleeingForce;
    }

    /// <summary>
    /// Flees from a target gameobject
    /// </summary>
    /// <param name="target">The gameobject to flee from</param>
    /// <returns>A force vector to apply to the vehicle to move it away from the target</returns>
    public Vector3 Flee(GameObject target)
    {
        return Flee(target.transform.position);
    }

    /// <summary>
    /// Seeks the target's future position
    /// </summary>
    /// <param name="target">The gameobject to pursue</param>
    /// <returns>A force vector to apply to the vehicle to move it towards the target's future position</returns>
    public Vector3 Pursuit(GameObject target)
    {
        Vector3 targetPosition = target.transform.position + target.GetComponent<Vehicle>().velocity * Time.deltaTime;
        return Seek(targetPosition);
    }

    /// <summary>
    /// Flees from the target's future position
    /// </summary>
    /// <param name="target">The gameobject to evade</param>
    /// <returns>A force vector to apply to the vehicle to move it away from the target's future position</returns>
    public Vector3 Evade(GameObject target)
    {
        Vector3 targetPosition = target.transform.position + target.GetComponent<Vehicle>().velocity * Time.deltaTime;
        return Flee(targetPosition);
    }

    /// <summary>
    /// An abstract method to make the vehicle separate from other vehicles
    /// </summary>
    /// <returns>Returns a vector to apply to the vehicle to keep it away from its neighbors</returns>
    public abstract Vector3 Separation();

    /// <summary>
    /// Orients the objects in the direction of their direction vector
    /// </summary>
    public void OrientAgent()
    {
        float rotation = Mathf.Atan2(direction.x, direction.y);
        rotation = (rotation * Mathf.Rad2Deg);

        transform.rotation = Quaternion.Euler(0, 0, -rotation);
    }

    /// <summary>
    /// An abstract method to calculate all of the steering forces acting on a vehicle
    /// That are exclusive to that type of vehicle
    /// </summary>
    public abstract void CalcSteeringForces();

    public Vector3 ObstacleAvoidance()
    {
        Vector3 ultimateAvoidanceForce = Vector3.zero;

        for (int i = 0; i < obstacles.Count; i++)
        {
            Vector3 vToC = new Vector3(obstacles[i].transform.position.x, obstacles[i].transform.position.y, 0) - transform.position;
            //if (Vector3.Dot(vToC,transform.up) < 0)
            //{
            //    continue;
            //}

            //if (vToC.sqrMagnitude > Mathf.Pow(safeRadius, 2))
            //{
            //    continue;
            //}

            float rightProj = Vector3.Dot(vToC, transform.right);

            //if (Mathf.Abs(rightProj) > (radius + obstacles[i].GetComponent<Obstacle>().radius))

            if (!inTrigger)
            {
                continue;
            }

            //Debug.Log(obstacles[i].GetComponent<Obstacle>().obstacleIndex); 
            if (!triggeredObstacles.Contains(i)) continue; 

            Debug.DrawLine(transform.position, obstacles[i].transform.position, Color.black);

            Vector3 desiredVelocity = Vector3.zero; 

            if (rightProj < 0)
            {
                desiredVelocity = transform.right * maxSpeed; 
            }
            else //if (rightProj > 0)
            {
                desiredVelocity = -transform.right * maxSpeed;
            }
            //else
            //{
            //    desiredVelocity = -velocity * 2;
            //}

            ultimateAvoidanceForce += ((desiredVelocity - velocity) * (2 / vToC.magnitude));
        }

        return ultimateAvoidanceForce;
    }

    /// <summary>
    /// Makes a vehicle wander in a direction similar to the direction it is going currently with slight alterations
    /// By constraining the wander to a point on a circle ahead of it
    /// </summary>
    /// <returns></returns>
    public Vector3 Wander()
    {
        //find circle position by scaling forward vector and adding it to position
        Vector3 circlePosition = transform.position + transform.forward * 2;

        //find a random angle based on the previous angle (within a certain random increment)
        currentAngle = (previousAngle + Random.Range(-20f, 20f)) * Mathf.Deg2Rad;

        //find the placement on the circle using sin and cos (sin = z, cos = x)
        Vector3 posOnCircle = new Vector3(Mathf.Cos(currentAngle), 0, Mathf.Sin(currentAngle));

        //multiply that vector by wander strength (circle radius)
        posOnCircle *= wanderStrength;

        //add that vector to the circle's position
        circlePosition += posOnCircle;

        //seek that vector
        previousAngle = currentAngle * Mathf.Rad2Deg;

        //Debug.DrawLine(transform.position, circlePosition, Color.black);
        return Seek(circlePosition);
    }
}
