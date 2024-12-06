using System;
using UnityEngine;

namespace _Scripts.Managers
{
    public class BackgroundController : MonoBehaviour
    {
        public GameObject stars;
        public GameObject clouds;
        [SerializeField] private float starSpeed = 1f;
        [SerializeField] private float cloudSpeed = 0.2f;

        private void Update()
        {
            stars.transform.position += Vector3.down * (starSpeed * Time.deltaTime);
            clouds.transform.position += Vector3.down * (cloudSpeed * Time.deltaTime);
            
            if (stars.transform.position.y < -10f)
            {
                stars.transform.position = new Vector3(0, 10, 0);
            }
            if (clouds.transform.position.y < -10f)
            {
                clouds.transform.position = new Vector3(0, 10, 0);
            }
        }
    }
}