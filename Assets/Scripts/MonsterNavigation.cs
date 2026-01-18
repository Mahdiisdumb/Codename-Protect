using System.Collections;
using UnityEngine;
using UnityEngine.AI;
public class MonsterNavigation : MonoBehaviour
{
    public float DetectionRange = 5f;
    public float MonsterSpeedWander = 5f;
    public float MonsterSpeedChase = 7.5f;
    public NavMeshAgent agent;
    public string waypointTag = "Point";
    public string playerTag = "Player";
    private Transform[] points;
    private int currentPoint = 0;
    private Renderer[] renderers;
    private bool isDying = false;
    [Header("Flash Damage Settings")]
    public int flashesToDieMin = 1;
    public int flashesToDieMax = 3;
    private int flashesTaken = 0;
    private int flashesRequired;
    [Header("Flash Hit Delay")]
    public float hitDelay = 0.5f;
    private float lastFlashHitTime = -10f;
    void Start()
    {
        if (agent == null)
            agent = GetComponentInParent<NavMeshAgent>();
        if (agent == null)
            Debug.LogError("Agent is null in MonsterNavigation script.");
        renderers = GetComponentsInChildren<Renderer>();
        flashesRequired = Random.Range(flashesToDieMin, flashesToDieMax + 1);
        GameObject[] pointObjects = GameObject.FindGameObjectsWithTag(waypointTag);
        points = new Transform[pointObjects.Length];
        for (int i = 0; i < pointObjects.Length; i++)
            points[i] = pointObjects[i].transform;
        agent.speed = MonsterSpeedWander;
        if (points.Length > 0) Wander();
    }
    void Update()
    {
        if (isDying) return;
        agent.enabled = true;
        HandleMovement();
        DetectActiveFlashlight();
    }
    void HandleMovement()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag(playerTag);
        GameObject target = null;
        float minDistance = float.MaxValue;
        foreach (GameObject player in players)
        {
            float distance = Vector3.Distance(transform.position, player.transform.position);
            if (distance < DetectionRange && distance < minDistance)
            {
                minDistance = distance;
                target = player;
            }
        }
        if (target != null)
        {
            agent.speed = MonsterSpeedChase;
            agent.destination = target.transform.position;
        }
        else if (points.Length > 0 && !agent.pathPending && agent.remainingDistance < 0.5f)
        {
            agent.speed = MonsterSpeedWander;
            Wander();
        }
    }
    void Chase(Transform target)
    {
        agent.destination = target.position;
    }
    void Wander()
    {
        if (points.Length == 0) return;
        currentPoint = Random.Range(0, points.Length);
        agent.destination = points[currentPoint].position;
    }
    void DetectActiveFlashlight()
    {
        if (isDying) return;
        if (Time.time - lastFlashHitTime < hitDelay) return;
        Flashlight[] flashes = FindObjectsOfType<Flashlight>();
        foreach (Flashlight flash in flashes)
        {
            if (!flash.flashlight.enabled) continue;
            Vector3 toMonster = transform.position - flash.transform.position;
            if (toMonster.magnitude > flash.flashlight.range) continue;
            float angle = Vector3.Angle(flash.transform.forward, toMonster);
            if (angle <= flash.flashlight.spotAngle / 2f)
            {
                flashesTaken++;
                lastFlashHitTime = Time.time;
                Debug.Log($"{name} hit by flash {flashesTaken}/{flashesRequired}");
                if (flashesTaken >= flashesRequired)
                    StartCoroutine(FlashThenDie());
                break;
            }
        }
    }
    private IEnumerator FlashThenDie()
    {
        isDying = true;
        agent.enabled = false;
        float flashDuration = 0.5f;
        float elapsed = 0f;
        Color[] originalColors = new Color[renderers.Length];
        for (int i = 0; i < renderers.Length; i++)
            originalColors[i] = renderers[i].material.color;
        while (elapsed < flashDuration)
        {
            foreach (Renderer r in renderers) r.material.color = Color.red;
            yield return null;
            for (int i = 0; i < renderers.Length; i++)
                renderers[i].material.color = originalColors[i];
            yield return null;
            elapsed += Time.deltaTime;
        }
        Destroy(agent.gameObject);
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, DetectionRange);
    }
}