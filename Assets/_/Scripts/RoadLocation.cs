using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DB.Police
{
    public class RoadLocation : MonoBehaviour
    {
        private void Start()
        {
            RoadSystem.instance.AddLocation(this);
        }
    }
}