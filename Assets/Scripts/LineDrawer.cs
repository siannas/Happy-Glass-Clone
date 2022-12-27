using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.EventSystems;// Required when using Event data.
using UnityEngine.Events;

public class LineDrawer : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public Material LineMat;

    private bool isDrawing = false;

    private GameObject LineGO;

    private LineRenderer LR;

    private int currentIndex = 0;

    private Vector3 pointerPosition;
    
    private Camera cam;

    public UnityEvent drawCallback = new UnityEvent();


    // Start is called before the first frame update
    private void Start()
    {
        cam = Camera.main;
    }

    /// <note> 
    /// Untuk menambahkan Collider dan Rigidbody2D pada line yang akan dibuat
    /// </note>
    private void GenerateCollider(LineRenderer lr)
    {
        Mesh mesh = new Mesh();

        try
        {
            lr.BakeMesh(mesh, cam, false);
        }
        catch(System.Exception e)
        {
            return;
        }
        
        PolygonCollider2D collider = LineGO.AddComponent<PolygonCollider2D>();

        collider.pathCount = mesh.triangles.Length/3;

        for(int i=0; i<mesh.triangles.Length/3; i++)
        {
            Vector2[] points = new Vector2[3];

            for(int j=0; j<3; j++)
            {
                int index = mesh.triangles[i*3+j];
                points[j] = mesh.vertices[index];
            }

            collider.SetPath(i, points);
        }

        Rigidbody2D rb2d = LineGO.AddComponent<Rigidbody2D>();

        /// <note> Buat agar lebih berat </note>
        rb2d.mass = 10f;

        drawCallback?.Invoke();
    }

    //Do this when the mouse is clicked over the selectable object this script is attached to.
    public void OnPointerDown(PointerEventData eventData)
    {
        pointerPosition = Input.mousePosition;

        LineGO = new GameObject();

        LR = LineGO.AddComponent<LineRenderer>();

        LR.alignment = LineAlignment.TransformZ;
        
        LR.startWidth = 0.2f;

        LR.sharedMaterial = LineMat;

        LR.useWorldSpace = false;

        isDrawing = true;
    }

    //Do this when the mouse click on this selectable UI object is released.
    public void OnPointerUp(PointerEventData eventData)
    {
        isDrawing = false;

        GenerateCollider(LR);

        currentIndex = 0;
    }

    private void Update() 
    {
        if(isDrawing == false) return;

        Vector3 dist = pointerPosition - Input.mousePosition;

        float Distance_SqrMag = dist.magnitude;

        if(Distance_SqrMag <= 10f ) return;

        Vector3 worldPos = cam.ScreenToWorldPoint(pointerPosition);

        worldPos.z = 0;

        LR.SetPosition(currentIndex,  worldPos);

        pointerPosition = Input.mousePosition;

        currentIndex++;

        LR.positionCount = currentIndex + 1;

        worldPos = cam.ScreenToWorldPoint(pointerPosition);

        worldPos.z = 0;

        LR.SetPosition(currentIndex,  worldPos);
    }
}
