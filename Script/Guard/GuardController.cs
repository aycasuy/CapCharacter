
using System.Collections;
using UnityEngine;


public class GuardController : MonoBehaviour
{
    AudioSource alarmAudio;
    bool alarmPlaying = false;

    public float speed = 5f;
    public float waitTime = .3f;
    public float turnSpeed = 10f;
    public Transform pathHolder;
    public Light spotLight;
    public float viewDistance;
    float viewAngle;
    Transform player;
    public LayerMask viewMask;
    Color originalSpotLightColor;
    Animator animator;
    public float damagePerSecond = 10f;

    Vector3[] waypoints;
    int currentWaypointIndex = 0;
    bool chasing = false;
    public float chaseDuration = 5f;

    void Start()
    {
        animator = GetComponent<Animator>();
        alarmAudio = GetComponent<AudioSource>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        viewAngle = spotLight.spotAngle;
        originalSpotLightColor = spotLight.color;

        waypoints = new Vector3[pathHolder.childCount];
        for (int i = 0; i < waypoints.Length; i++)
        {
            waypoints[i] = pathHolder.GetChild(i).position;
            waypoints[i] = new Vector3(waypoints[i].x, transform.position.y, waypoints[i].z);
        }

        transform.position = waypoints[0];
        StartCoroutine(FollowPath());
    }

  
    [System.Obsolete]
    void Update()
{
    bool spotted = CanSeePlayer();

    // Stealth görevini buraya bağladım
    FindObjectOfType<StealthSurvivalTracker>().SetSpotted(spotted);

    if (spotted)
    {
        // Spot ışığı kırmızıya dönsün
        spotLight.color = Color.red;

        if (!alarmPlaying)
        {
            alarmAudio.Play();
            alarmPlaying = true;
        }

        player.GetComponent<PlayerHealth>().TakeDamage(damagePerSecond * Time.deltaTime);

        if (!chasing)
        {
            StartCoroutine(ChasePlayer());
        }
    }
    else
    {
        spotLight.color = originalSpotLightColor;

        if (alarmPlaying && !chasing)
        {
            alarmAudio.Stop();
            alarmPlaying = false;
        }
    }
}


    bool CanSeePlayer()
    {
        if (Vector3.Distance(transform.position, player.position) < viewDistance)
        {
            Vector3 directionToPlayer = (player.position - transform.position).normalized;
            float angleBetweenGuardAndPlayer = Vector3.Angle(transform.forward, directionToPlayer);

            if (angleBetweenGuardAndPlayer < viewAngle / 2f)
            {
                if (!Physics.Linecast(transform.position, player.position, viewMask))
                {
                    return true;
                }
            }
        }
        return false;
    }

    IEnumerator FollowPath()
    {
        while (true)
        {
            if (!chasing)
            {
                Vector3 targetWaypoint = waypoints[currentWaypointIndex];
                float distance = Vector3.Distance(transform.position, targetWaypoint);

                if (distance > 0.05f)
                {
                    animator.SetFloat("Speed", speed);
                    transform.position = Vector3.MoveTowards(transform.position, targetWaypoint, speed * Time.deltaTime);
                }
                else
                {
                    animator.SetFloat("Speed", 0);
                    currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
                    yield return new WaitForSeconds(waitTime);
                    yield return StartCoroutine(TurnToFace(waypoints[currentWaypointIndex]));
                }
            }
            yield return null;
        }
    }

    IEnumerator ChasePlayer()
    {
        chasing = true;
        float timer = 0f;

        while (timer < chaseDuration)
        {
            animator.SetFloat("Speed", speed + 2f);
            transform.LookAt(player.position);
            transform.position = Vector3.MoveTowards(transform.position, player.position, (speed + 2f) * Time.deltaTime);
            timer += Time.deltaTime;
            yield return null;
        }

        chasing = false;
        animator.SetFloat("Speed", 0);
        alarmAudio.Stop();
        alarmPlaying = false;

        // En yakın waypoint'e dön
        float minDist = Vector3.Distance(transform.position, waypoints[0]);
        int closestIndex = 0;

        for (int i = 1; i < waypoints.Length; i++)
        {
            float dist = Vector3.Distance(transform.position, waypoints[i]);
            if (dist < minDist)
            {
                minDist = dist;
                closestIndex = i;
            }
        }

         currentWaypointIndex = closestIndex;
         yield return StartCoroutine(MoveToPosition(waypoints[currentWaypointIndex]));
        
        
    }

    IEnumerator MoveToPosition(Vector3 target)
    {
        animator.SetFloat("Speed", speed);
        while (Vector3.Distance(transform.position, target) > 0.05f)
        {
            transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
            yield return null;
        }
        animator.SetFloat("Speed", 0);
    }

    IEnumerator TurnToFace(Vector3 lookTarget)
    {
        Vector3 direction = (lookTarget - transform.position).normalized;
        float targetAngle = 90 - Mathf.Atan2(direction.z, direction.x) * Mathf.Rad2Deg;

        while (Mathf.Abs(Mathf.DeltaAngle(transform.eulerAngles.y, targetAngle)) > 0.05f)
        {
            float angle = Mathf.MoveTowardsAngle(transform.eulerAngles.y, targetAngle, turnSpeed * Time.deltaTime);
            transform.eulerAngles = Vector3.up * angle;
            yield return null;
        }
    }

    void OnDrawGizmos()
    {
        if (pathHolder == null) return;

        Vector3 startPosition = pathHolder.GetChild(0).position;
        Vector3 previousPosition = startPosition;
        foreach (Transform waypoint in pathHolder)
        {
            Gizmos.DrawSphere(waypoint.position, .3f);
            Gizmos.DrawLine(previousPosition, waypoint.position);
            previousPosition = waypoint.position;
        }
        Gizmos.DrawLine(previousPosition, startPosition);
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, transform.forward * viewDistance);
    }
}


