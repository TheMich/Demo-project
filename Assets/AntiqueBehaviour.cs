using UnityEngine;

public class AntiqueBehaviour : MonoBehaviour
{
    public void BeCollected()
    {
        Destroy(gameObject);
    }
}
