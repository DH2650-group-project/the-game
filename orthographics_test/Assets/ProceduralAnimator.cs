using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProceduralAnimator : MonoBehaviour
{

    struct ProceduralLimb
    {
        public Transform IKTarget;
        public Vector3 defaultPosition;
        public Vector3 lastPosition;
        public bool is_moving;
    }



    [Header("Ground Detection")]
    [SerializeField] private LayerMask groundLayerMask = default;


    [Header("Movement")]

    [SerializeField]
    private Transform[] IKTargets;

    [Range(0.0f, 10f)]
    public float stepSize = 1f;

    [Range(0.0f, 50f)] public float feetOffset = 0.0f;
    [Range(0, 32)] public int stepSmoothness = 1;
    [Range(0.0f, 5.0f)] public float stepHeight = 1f;

    [Range(0.0f, 5.0f)] public float raycastRange = 2;

    [Range(0.0f, 20f)] public float overshootFactor = 1f;



    private int limbCount;
    private ProceduralLimb[] limbs;

    private Vector3 lastBodyPosition;
    private Vector3 velocity;

    private bool full_rest;

    private bool any_leg_moving;

    void Start()
    {

        limbCount = IKTargets.Length;
        limbs = new ProceduralLimb[limbCount];
        Transform limb;
        for (int i = 0; i < limbCount; i++)
        {
            limb = IKTargets[i];
            limbs[i] = new ProceduralLimb()
            {
                IKTarget = limb,
                defaultPosition = transform.InverseTransformPoint(limb.position),
                lastPosition = limb.position,
                is_moving = false
            };
        }

        lastBodyPosition = transform.position;
        full_rest = true;
        any_leg_moving = false;
    }

    void FixedUpdate()
    {
        velocity = (transform.position - lastBodyPosition);

        if (velocity.magnitude > Mathf.Epsilon)
            HandleIKMovement();
        else if (!full_rest)
            BackToRest();

        if (limbCount > 3)
        {
            Vector3 v1 = limbs[0].IKTarget.position - limbs[1].IKTarget.position;
            Vector3 v2 = limbs[2].IKTarget.position - limbs[3].IKTarget.position;
            Vector3 normal = Vector3.Cross(v1, v2);
            Vector3 up = Vector3.Lerp(transform.up, normal, 1f / (float)(stepSmoothness + 1f));
            // transform.up = up;
        }

    }

    void HandleIKMovement()
    {
        lastBodyPosition = transform.position;

        Vector3[] desiredPositions = new Vector3[limbCount];
        float greatestDistance = stepSize;
        int limbToMove = -1;

        for (int i = 0; i < limbCount; i++)
        {
            if (limbs[i].is_moving) continue;

            desiredPositions[i] = transform.TransformPoint(limbs[i].defaultPosition);
            float dist = Vector3.ProjectOnPlane(desiredPositions[i] + velocity * overshootFactor - limbs[i].lastPosition, transform.up).magnitude;

            if (dist > greatestDistance)
            {
                greatestDistance = dist;
                limbToMove = i;
            }
        }

        // keep non moving limbs in place
        for (int i = 0; i < limbCount; i++)
            if (i != limbToMove)
                limbs[i].IKTarget.position = limbs[i].lastPosition;

        // move the selected leg to its "desired" position
        if (limbToMove != -1 && !any_leg_moving)
        {
            Vector3 targetPoint = desiredPositions[limbToMove] + Mathf.Clamp(velocity.magnitude * overshootFactor, 0f, 1.5f) * (desiredPositions[limbToMove] - limbs[limbToMove].lastPosition) + velocity * overshootFactor;
            targetPoint = RaycastToGround(targetPoint, transform.up);
            targetPoint += transform.up * feetOffset;
            any_leg_moving = true;
            full_rest = false;
            StartCoroutine(Stepping(limbToMove, targetPoint));
        }
    }

    private void BackToRest()
    {
        Vector3 targetPoint;
        float dist;
        for (int i = 0; i < limbCount; i++)
        {
            if (limbs[i].is_moving) continue;


            targetPoint = RaycastToGround(
                transform.TransformPoint(limbs[i].defaultPosition),
                transform.up) + transform.up * feetOffset;

            dist = (targetPoint - limbs[i].lastPosition).magnitude;
            if (dist > 0.005f)
            {
                StartCoroutine(Stepping(i, targetPoint));
                return;
            }
        }
        full_rest = true;
    }

    private Vector3 RaycastToGround(Vector3 pos, Vector3 up)
    {
        Vector3 point = pos;

        Ray ray = new(pos + raycastRange * up, -up);
        if (Physics.Raycast(ray, out RaycastHit hit, 2f * raycastRange, groundLayerMask))
            point = hit.point;
        return point;
    }

    private IEnumerator Stepping(int limbIndex, Vector3 targetPos)
    {
        limbs[limbIndex].is_moving = true;
        Vector3 startPos = limbs[limbIndex].lastPosition;
        float t;
        for (int i = 0; i < stepSmoothness; i++)
        {
            t = i / (stepSmoothness + 1f);

            limbs[limbIndex].IKTarget.position = Vector3.Lerp(startPos, targetPos, t) + Mathf.Sin(t * Mathf.PI) * stepHeight * transform.up;
            yield return new WaitForFixedUpdate();
        }
        limbs[limbIndex].IKTarget.position = targetPos;
        limbs[limbIndex].lastPosition = targetPos;
        limbs[limbIndex].is_moving = false;
        any_leg_moving = false;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        for (int i = 0; i < limbCount; i++)
        {
            Debug.LogFormat("IKTarget position: {0}", limbs[i].IKTarget.position);
            Gizmos.DrawSphere(limbs[i].IKTarget.position, 0.1f);
        }

        Gizmos.color = Color.blue;
        for (int i = 0; i < limbCount; i++)
        {
            Gizmos.DrawSphere(transform.TransformPoint(limbs[i].defaultPosition), 0.1f);
        }
    }

}
