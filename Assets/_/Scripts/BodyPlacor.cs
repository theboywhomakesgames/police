using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DB.Utils
{
    public abstract class BodyPlacor
    {
        public abstract Quaternion CalculateLocalRotation(Vector3 input, Transform body);
        public abstract Vector3 CalculateLocalPosition(Vector3 input, Transform body);
    }

    public class BasicPlacor : BodyPlacor
    {
        public override Vector3 CalculateLocalPosition(Vector3 input, Transform body)
        {
            return body.localPosition;
        }

        public override Quaternion CalculateLocalRotation(Vector3 input, Transform body)
        {
            body.forward = Vector3.Lerp(body.forward, input, rotationSpeed);
            return body.localRotation;
        }

        [SerializeField] private float rotationSpeed = 1;
    }

    public class CopterPlacor : BodyPlacor
    {
        public override Vector3 CalculateLocalPosition(Vector3 input, Transform body)
        {
            return body.localPosition;
        }

        public override Quaternion CalculateLocalRotation(Vector3 input, Transform body)
        {
            Quaternion tmp = body.localRotation;
            Vector3 frw = new Vector3(input.x, 0, input.z);
            body.forward = frw;

            if (input.y > 0)
            {
                Vector3 axis = Vector3.Cross(input, Vector3.up);
                if (input.z > 0)
                {
                    axis = Vector3.Cross(input, -Vector3.up);
                }
                body.Rotate(axis, 20f);
            }

            Quaternion desiredRot = body.localRotation;
            body.localRotation = Quaternion.Slerp(tmp, desiredRot, Time.deltaTime);
            return body.localRotation;
        }
    }
}