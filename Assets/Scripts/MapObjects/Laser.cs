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
        lineRenderer.SetPosition(0, transform.position);
        lineRenderer.SetPosition(1, hit.collider != null ? (Vector3)hit.point : transform.position + (Vector3)DirVector * 100);
    }

    private void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, DirVector, float.PositiveInfinity);
        lineRenderer.SetPosition(0, transform.position);
        lineRenderer.SetPosition(1, hit.collider != null ? (Vector3)hit.point : transform.position + (Vector3)DirVector*100 );
        try
        {
            Destroy(hit.collider.GetComponent<Player>().gameObject);
        }
        catch( System.NullReferenceException )
        { }

    }

    private Vector2 DirVector => new Vector2(Mathf.Cos(direction * Mathf.Deg2Rad), Mathf.Sin(direction * Mathf.Deg2Rad));
}
