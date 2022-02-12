using UnityEngine;

namespace e23.VehicleController.Examples
{
    public class ExamplePropeller : MonoBehaviour
    {
        [SerializeField] private Transform propeller = null;
        [SerializeField] private Vector3 rotationSpeed = Vector3.zero;

        private void Update()
        {
            RotatePropeller();
        }

        private void RotatePropeller()
        {
            propeller.Rotate(rotationSpeed.x * Time.deltaTime, rotationSpeed.y * Time.deltaTime, rotationSpeed.z * Time.deltaTime);
        }
    }
}