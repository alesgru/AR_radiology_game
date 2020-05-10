using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.MixedReality.Toolkit.Utilities.Solvers;

public class PlacementScript : MonoBehaviour
{
    private Solver SolverComponent;
    // Deactivates a solver on a chosen game object
    // maybe implement a more general form of this script later
    public void DeactivateSolver(string objectName)
    {
        GameObject gameObject = GameObject.Find(objectName);
        SolverComponent = gameObject.GetComponent<Solver>();
        SolverComponent.enabled = !SolverComponent.enabled;
    }
}
