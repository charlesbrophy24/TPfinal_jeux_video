using UnityEngine;

public class CarCollision : MonoBehaviour
{


void OnTriggerEnter(Collider other){

       if (other.CompareTag("Enemy")){

            //player.PointsNB += 5

            Destroy(other.gameObject);

        }

    }

}