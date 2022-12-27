using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlassTrigger : MonoBehaviour
{
    public int count;

    private int water_layer;

    private HashSet<Collider2D> set;

    void Start() 
    {
        water_layer = LayerMask.NameToLayer("Water");

        set = new HashSet<Collider2D>();
    }
    
    // when the water collide
    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.layer != water_layer 
                || set.Contains(col)) return;

        set.Add(col);

        count++;
    }
}
