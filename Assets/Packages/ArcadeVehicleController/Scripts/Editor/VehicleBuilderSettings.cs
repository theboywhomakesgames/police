using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace e23.VehicleController.Editor
{
    public class VehicleBuilderSettings : ScriptableObject
    {
        [HideInInspector] [SerializeField] private string vehicleName;
        [HideInInspector] [SerializeField] private GameObject vehicleModel;
        [HideInInspector] [SerializeField] private VehicleType vehicleType;
        [HideInInspector] [SerializeField] private PhysicMaterial physicsMaterial;
        [HideInInspector] [SerializeField] private string bodyName;
        [HideInInspector] [SerializeField] private string frontLeftWheelName;
        [HideInInspector] [SerializeField] private string frontRightWheelName;
        [HideInInspector] [SerializeField] private string backLeftWheelName;
        [HideInInspector] [SerializeField] private string backRightWheelName;
        [HideInInspector] [SerializeField] private VehicleBehaviourSettings vehicleSettings;
        [HideInInspector] [SerializeField] private bool addEffectsComponent;
        [HideInInspector] [SerializeField] private GameObject smokeParticleSystemPrefab;
        [HideInInspector] [SerializeField] private int smokeCount;
        [HideInInspector] [SerializeField] private GameObject trailRendererPrefab;
        [HideInInspector] [SerializeField] private int trailCount;
        [HideInInspector] [SerializeField] private bool addExampleInput;
        [HideInInspector] [SerializeField] private List<MonoScript> monoBehaviours;

        public string VehicleName { get { return vehicleName; } set { vehicleName = value; } }
        public GameObject VehicleModel { get { return vehicleModel; } set { vehicleModel = value; } }
        public VehicleType VehicleType { get { return vehicleType; } set { vehicleType = value; } }
        public PhysicMaterial PhysicsMaterial { get { return physicsMaterial; } set { physicsMaterial = value; } }
        public string BodyName { get { return bodyName; } set { bodyName = value; } }
        public string FrontLeftWheelName { get { return frontLeftWheelName; } set { frontLeftWheelName = value; } }
        public string FrontRightWheelName { get { return frontRightWheelName; } set { frontRightWheelName = value; } }
        public string BackLeftWheelName { get { return backLeftWheelName; } set { backLeftWheelName = value; } }
        public string BackRightWheelName { get { return backRightWheelName; } set { backRightWheelName = value; } }
        public VehicleBehaviourSettings VechicleSettings { get { return vehicleSettings; } set { vehicleSettings = value; } }
        public bool AddEffectsComponent { get { return addEffectsComponent; } set { addEffectsComponent = value; } }
        public GameObject SmokeParticleSystemPrefab { get { return smokeParticleSystemPrefab; } set { smokeParticleSystemPrefab = value; } }
        public int SmokeCount { get { return smokeCount; } set { smokeCount = value; } }
        public GameObject TrailRendererPrefab { get { return trailRendererPrefab; } set { trailRendererPrefab = value; } }
        public int TrailCount { get { return trailCount; } set { trailCount = value; } }
        public bool AddExampleInput { get { return addExampleInput; } set { addExampleInput = value; } }
        public List<MonoScript> MonoBehaviours { get { return monoBehaviours; } set { monoBehaviours = value; } }
    }
}