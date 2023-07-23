using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual void GetDamage()
    {

    }

    public virtual void Die()
    {
        Destroy(this.gameObject);
    }

    public virtual void FlipSprite(Vector3 dir)
    {
        
    }
}
