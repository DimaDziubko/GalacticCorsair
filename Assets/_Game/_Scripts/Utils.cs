using System.Collections.Generic;
using UnityEngine;

namespace _Game._Scripts
{
    public class Utils : MonoBehaviour
    {
        static public Material[] GetAllMaterials(GameObject go)
        {
            Renderer[] rends = go.GetComponentsInChildren<Renderer>();

            List<Material> mats = new List<Material>();

            foreach(Renderer rend in rends)
            {
                if (!(rend.material.name.StartsWith("Particle") || rend.material.name.StartsWith("M_Y_RG") || rend.material.name.StartsWith("Default")))
                {
                    mats.Add(rend.material);
                }
            }

            return (mats.ToArray());
        }
    }
}
