using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class Laser : MonoBehaviour
{
    public float direction;
    public GameObject head;

    private LineRenderer lineRenderer;

    protected void OnValidate()
    {
        lineRenderer = GetComponent<LineRenderer>();
        RaycastHit2D hit = Physics2D.Raycast(head.transform.position, DirVector, 100);
        SetLine(hit);
    }

    protected void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    protected void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(head.transform.position, DirVector, float.PositiveInfinity);
        SetLine(hit);
        try
        {
            Destroy(hit.collider.GetComponent<Player>().gameObject);
        }
        catch( System.NullReferenceException )
        { }

    }

    protected Vector2 DirVector => new Vector2(Mathf.Cos(direction * Mathf.Deg2Rad), Mathf.Sin(direction * Mathf.Deg2Rad));
    protected void SetLine(RaycastHit2D hit)
    {
        head.transform.rotation = Quaternion.AngleAxis(direction - 90, Vector3.forward);
        lineRenderer.SetPosition(0, head.transform.position);
        lineRenderer.SetPosition(1, hit.collider != null ? (Vector3)hit.point + (Vector3)((hit.point-(Vector2)transform.position).normalized * lineRenderer.widthCurve.keys[0].value) : transform.position + (Vector3)DirVector * 100);
    }

}
