using UnityEngine;
using UnityEngine.Events;

public class FT_InteractableRay : MonoBehaviour
{
    public bool isActive = true;

    public UnityEvent OnRayEnter;
    public UnityEvent OnRayExit;
    public UnityEvent OnRayClick;

    public virtual void RayEnter()
    {
        OnRayEnter?.Invoke();
        Debug.Log("射线进入:" + name);
    }

    public virtual void RayExit()
    {
        OnRayExit?.Invoke();
        Debug.Log("射线离开:" + name);
    }

    public virtual void RayClick()
    {
        OnRayClick?.Invoke();
        Debug.Log("射线点击:" + name);
    }
}