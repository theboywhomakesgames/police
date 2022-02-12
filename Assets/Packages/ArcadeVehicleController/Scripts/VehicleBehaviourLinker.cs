using UnityEngine;

namespace e23.VehicleController
{
    /// <summary>
    /// Add this component to a GameObject for easy linking to a VehicleBehaviour.
    /// Example use: When using sphere collider with Rigidbody that doesn't have the VehicleBehaviour as a child, this makes it easier to use OnCollision and OnTrigger
    /// You can do other.gameObject.GetComponent<VehicleBehaiourLinker>().VehicleBehaviour
    /// </summary>
    public class VehicleBehaviourLinker : MonoBehaviour
    {
#pragma warning disable 0649
        [SerializeField] private VehicleBehaviour vehicleBehaviour;
#pragma warning restore 0649

        public VehicleBehaviour VehicleBehaviour => vehicleBehaviour;
    }
}