using Unity.VisualScripting;
using UnityEngine;



public class TriggerCheck2D : MonoBehaviour
{
    public LayerMask mLayerMask;
    public int triggerCount;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (CheckLayerMask(other.GameObject()))
        {
            triggerCount++;
        }
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        if (CheckLayerMask(other.GameObject()))
        {
            triggerCount--;
        }
    }

    private bool CheckLayerMask(GameObject other)
    {
        return mLayerMask == (mLayerMask | (1 << other.layer));
    }
    
    public bool Triggered()
    {
        return triggerCount > 0;
    }
}
