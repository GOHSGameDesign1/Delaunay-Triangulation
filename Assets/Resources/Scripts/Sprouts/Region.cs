using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Region 
{
    public List<Boundary> boundaries { get; } = new List<Boundary>();

    public Region(List<Boundary> startBoundaries)
    {
        if (startBoundaries == null)
        {
            Debug.LogError("No Boundaries to make the region!");
            return;
        }

        foreach (Boundary b in startBoundaries) 
        { 
            boundaries.Add(b);
        }
    }

    public void SetBoundaries(List<Boundary> setBoundaries)
    {
        if (setBoundaries == null)
        {
            Debug.LogError("No Boundaries to make the region!");
            return;
        }

        foreach (Boundary b in setBoundaries)
        {
            boundaries.Add(b);
        }
    }
}
