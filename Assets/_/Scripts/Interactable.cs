using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace DB.Police{
    public class Interactable : MonoBehaviour
    {
        public UnityEvent<PlayerReference> OnInteraction;

        public void Interact(PlayerReference pr){
            OnInteraction?.Invoke(pr);
        }
    }
}
