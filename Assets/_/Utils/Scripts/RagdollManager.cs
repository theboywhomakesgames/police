using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

namespace DB.Utils
{
    public class RagdollManager : MonoBehaviour
    {
        public Transform root;
        public UnityEvent OnActivation;

        [SerializeField] private bool _isActive = false;
        [SerializeField] private Collider _selfCollider;
        [SerializeField] private Rigidbody _mainRB, hipsRB;
        [SerializeField] private SkinnedMeshRenderer _mesh;
        [SerializeField] private Component[] _destroyOnActivation;

        private Rigidbody[] _rbs;
        private Collider[] _colliders;

        public void Activate(Vector3 dir, float speed)
        {
            hipsRB.velocity = dir * speed;
            Activate();
        }

        [Button("Activate")]
        public void Activate(float damper = 0f)
        {
            foreach(Component c in _destroyOnActivation)
            {
                Destroy(c);
            }

            //transform.parent = null;
            //_mesh.transform.parent = null;
            _selfCollider.enabled = false;
            _isActive = true;
            SetActivation(damper);
            OnActivation?.Invoke();
        }

        [Button("Deactivate")]
        public void Deactivate()
        {
            _isActive = false;
            SetActivation();
        }

        private void Start()
        {
            _rbs = GetComponentsInChildren<Rigidbody>().ToArray();
            _colliders = GetComponentsInChildren<Collider>().ToArray();

            SetActivation();
        }

        private void SetActivation(float damper = 0)
        {
            try
            {
                _mainRB.isKinematic = _isActive;
                _selfCollider.enabled = !_isActive;
            }
            catch { }

            foreach (Rigidbody rb in _rbs)
            {
                try
                {
                    bool active = !_isActive;
                    rb.isKinematic = active;
                    rb.interpolation = active ? RigidbodyInterpolation.None : RigidbodyInterpolation.Interpolate;
                    if (_isActive)
                    {
                        rb.drag = damper;
                    }
                }
                catch { }
            }

            foreach (Collider c in _colliders)
            {
                try
                {
                    if(!c.isTrigger)
                        c.enabled = _isActive;
                }
                catch { }
            }
        }
    }
}