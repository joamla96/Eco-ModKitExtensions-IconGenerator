using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Eco.Client
{
    public static class Utilities
    {
        public static List<GameObject> GetChildren(this GameObject gameObject)
        {
            var children = new List<GameObject>();

            var childCount = gameObject.transform.childCount;

            for (int i = 0; i < childCount; i++)
            {
                var child = gameObject.transform.GetChild(i);
                var go = child.gameObject;

                children.Add(go);
            }

            return children;
        }
    }
}
