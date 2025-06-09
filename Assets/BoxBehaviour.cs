using UnityEngine;

public class BoxBehaviour : MonoBehaviour
{
    public GameObject coin;
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Projectile"))
        {
            Debug.Log("Box has been hit.");
            var spawnedCoin = Instantiate(coin, transform.position, coin.transform.rotation);
            Destroy(other.gameObject); // Destroy the projectile
            Destroy(this.gameObject); // Destroy the box
        }
    }
}
