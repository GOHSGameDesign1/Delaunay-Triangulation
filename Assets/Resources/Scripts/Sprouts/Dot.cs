using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dot
{
    public string ID {  get; private set; }
    public int numOfLines { get; private set; }
    public List<Region> availableRegions { get; private set; } = new List<Region>();
    public List<Boundary> availableBoundaries { get; private set; } = new List<Boundary>();

    public Dot(string id, int startingLines, Region startingRegion, Boundary startingBoundary) 
    { 
        ID = id;
        numOfLines = startingLines;
        availableRegions.Add(startingRegion);
        availableBoundaries.Add(startingBoundary);
    }

    public Dot(string id, int startingLines, List<Region> startingRegions, List<Boundary> startingBoundaries)
    {
        ID = id;
        numOfLines = startingLines;

        if(startingRegions != null)
        {
            foreach(Region region in startingRegions)
            {
                availableRegions.Add(region);
            }
        }

        if(startingBoundaries != null)
        {
            foreach(Boundary boundary in startingBoundaries)
            {
                availableBoundaries.Add(boundary);
            }
        }
    }

    public void IncreaseLines(int numIncrease)
    {
        if((numOfLines + numIncrease) > 3)
        {
            Debug.Log("Dot lines will exceed 3. ID: " + ID);
            return;
        }

        numOfLines += numIncrease;

    }

    public void SetRefBoundary(Boundary refBoundary)
    {
        availableBoundaries.Clear();
        availableBoundaries.Add(refBoundary);
    }

}
