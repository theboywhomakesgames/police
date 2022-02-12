using UnityEngine;
using UnityEngine.EventSystems;

namespace e23.VehicleController.Demo
{
    public class ExampleTouchScreen : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
#pragma warning disable 0649
        [SerializeField] private VehicleBehaviour vehicleBehaviour;
        [SerializeField] private bool accelerate;
        [SerializeField] private bool brake;
        [SerializeField] private bool steerLeft;
        [SerializeField] private bool steerRight;
#pragma warning restore 0649

        private VehicleSwapper vehicleSwapper;
        private bool doAction = false;

        public VehicleBehaviour VehicleBehaviour
        {
            get { return vehicleBehaviour; }
            set { vehicleBehaviour = value; }
        }

        private void Awake()
        {
            vehicleSwapper = FindObjectOfType<VehicleSwapper>();
            RegisterActions(true);
        }

        private void OnEnable()
        {
            UpdateVehicleBehaviour();
        }

        private void OnDestroy()
        {
            RegisterActions(false);
        }

        private void RegisterActions(bool register)
        {
            vehicleSwapper.onVehicleSwapped -= UpdateVehicleBehaviour;

            if (register == false) { return; }

            vehicleSwapper.onVehicleSwapped += UpdateVehicleBehaviour;
        }

        private void UpdateVehicleBehaviour()
        {
            VehicleBehaviour = vehicleSwapper.ActiveVehicle;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            doAction = true;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            doAction = false;
        }

        private void Update()
        {
            if (doAction)
            {
                // Acceleration
                if (accelerate) { VehicleBehaviour.ControlAcceleration(); }
                if (brake) { VehicleBehaviour.ControlBrake(); }

                // Steering
                if (steerLeft) { VehicleBehaviour.ControlTurning(-1); }
                if (steerRight) { VehicleBehaviour.ControlTurning(1); }
            }
        }
    }
}