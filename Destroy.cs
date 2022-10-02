using UnityEngine;

public class Destroy : MonoBehaviour
{
    [SerializeField] float lifeTime = 3;

    void OnEnable(){ Destroy(gameObject, lifeTime); }
}
