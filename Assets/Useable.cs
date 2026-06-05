using UnityEngine;
using UnityEngine.Events;

public class Useable : MonoBehaviour
{
    public UnityEvent whenUsed;
    public bool canBeReused;

    public void Use()
    {
        whenUsed.Invoke();
        enabled = canBeReused;
    }

    private void OnEnable()
    {
        References.useables.Add(this);
    }

    private void OnDisable()
    {
        References.useables.Remove(this);
    }
}
