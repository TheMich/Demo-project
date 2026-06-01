using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class NavPoint : MonoBehaviour
{

    private void Start()
    {
        GetComponent<MeshRenderer>().enabled = false;
    }

    private void OnEnable()
    {
        References.navPoints.Add(this);
    }

    private void OnDisable()
    {
        References.navPoints.Remove(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
