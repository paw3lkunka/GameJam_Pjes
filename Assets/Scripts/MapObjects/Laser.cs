using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class Laser : MonoBehaviour
{
    public float direction;

    private LineRenderer lineRenderer;

    private void OnValidate()
    {
        lineRenderer = GetComponent<LineRenderer>();
        RaycastHit2D hit = Physics2D.Raycast(transform.position, DirVector, 100);
        setLine(hit);
    }

    private void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, DirVector, float.PositiveInfinity);
        setLine(hit);
        try
        {
            Destroy(hit.collider.GetComponent<Player>().gameObject);
        }
        catch( System.NullReferenceException )
        { }

    }

    private Vector2 DirVector => new Vector2(Mathf.Cos(direction * Mathf.Deg2Rad), Mathf.Sin(direction * Mathf.Deg2Rad));
    private void setLine(RaycastHit2D hit)
    {
        lineRenderer.SetPosition(0, transform.position);
        lineRenderer.SetPosition(1, hit.collider != null ? (Vector3)hit.point + (Vector3)((hit.point-(Vector2)transform.position).normalized * lineRenderer.widthCurve.keys[0].value) : transform.position + (Vector3)DirVector * 100);
    }

}
