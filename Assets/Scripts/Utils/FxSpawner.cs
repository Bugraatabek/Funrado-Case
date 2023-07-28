using UnityEngine;

public class FxSpawner : MonoBehaviour 
{
    [SerializeField] GameObject fxToSpawn;

    public virtual void SpawnFX()
    {
        Instantiate(fxToSpawn, transform.position, Quaternion.identity);
    }
}