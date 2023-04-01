using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Unity.Rendering.HybridV2;
using UnityEngine;

public class SproutsManager : MonoBehaviour
{
    [SerializeField]
    private int startingDots;

    [SerializeField] public static List<Dot> dots { get; private set; } = new List<Dot>();

    [SerializeField]
    public static List<Boundary> boundaries { get; } = new List<Boundary>();

    [SerializeField]
    public static List<Region> regions { get; } = new List<Region>();

    public List<string> selectedDotIDs = new List<string>();

    private List<Dot> selectedDots = new List<Dot>();

    // Start is called before the first frame update
    void Awake()
    {
        SetupGame();
    }

    private void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void SetupGame()
    {

        Region startRegion = new Region();

        regions.Add(startRegion);

        for (int i = 0; i < startingDots; i++)
        {
            Boundary newBoundary = new Boundary(startRegion);
            boundaries.Add(newBoundary);

            Dot newDot = new Dot((i + 1).ToString(), 0, regions[0], boundaries[i]);
            dots.Add(newDot);
        }

        //PrintGameState();
    }

    public void SelectDots()
    {
        PrintGameState();

        selectedDots.Clear();

        foreach(string ID in selectedDotIDs)
        {
            Dot selectedDot = dots.Find(x => x.ID == ID);
            if (selectedDot == null) 
            {
                Debug.LogWarning("Dot with ID: " + ID + " could not be found!");
            }

            selectedDots.Add(selectedDot);

        }

        Debug.Log("Dots selected");

    }

    // Check if dots occur in same region and are in diff boundaries
    private bool TwoBoundaryViable(Dot dot1, Dot dot2)
    {

        bool sameRegion = dot1.availableRegions.Any(x => dot2.availableRegions.Contains(x));

        if (!sameRegion)
        {
            return false;
        }


        bool sameBoundary = dot1.availableBoundaries.Any(x => dot2.availableBoundaries.Contains(x));

        if (sameBoundary)
        {
            return false;
        }

        return true;
    }

    public void TwoBoundaryMove()
    {

        if(selectedDots.Count != 2)
        {
            Debug.LogWarning("Need only 2 dots selected for 2 boundary move");
            return;
        }

        // Get dots from selection
        Dot dot1 = selectedDots[0];
        Dot dot2 = selectedDots[1];

        if(!TwoBoundaryViable(dot1 , dot2 ))
        {
            Debug.Log("Cannot make a two boundary move with these points!");
            return;
        }

        // Find reference to common region and dot boundaries.
        Region moveRegion = dot1.availableRegions.FindAll(x => dot2.availableRegions.Contains(x))[0];
        Boundary bound1 = dot1.availableBoundaries.Find(x => x.refRegion == moveRegion);
        Boundary bound2 = dot2.availableBoundaries.Find(x => x.refRegion == moveRegion);


        
        Boundary newBoundary = new Boundary(moveRegion, bound1, bound2); // Generate new boundary on common region
        boundaries.Add(newBoundary);

        Dot newDot = new Dot((dots.Count+1).ToString(), 2, moveRegion, newBoundary); // Generate new dot
        dots.Add(newDot);

        Tuple<string, string> newSeg1 = new Tuple<string,string>(dot1.ID, newDot.ID); // Add new segment added from new dot
        newBoundary.AddSegment(newSeg1);

        Tuple<string, string> newSeg2 = new Tuple<String, String>(dot2.ID, newDot.ID); // Add new segment added from new dot
        newBoundary.AddSegment(newSeg2);

        dot1.SetRefBoundary(newBoundary); // Change OG dot refBoundaries
        dot2.SetRefBoundary(newBoundary);

        boundaries.Remove(bound1); // Remove old boundaries
        boundaries.Remove(bound2);

        PrintGameState();
    }

    public void OneBoundaryTypeDecider()
    {
        if (selectedDots.Count == 0) 
        { 
            Debug.LogWarning("Not enough selected dots!"); 
            return; 
        }



        if(selectedDots.Count == 1)
        {
            if (selectedDots[0].numOfLines <= 1)
            {

            }
        }

        List<Region> sharedRegions = FindSharedRegions(selectedDots);

        Debug.Log(sharedRegions.Count);

        // Checking for self-looping move
        if (selectedDots[0].numOfLines <= 1)
        {
            if(selectedDots.Count == 1)
            {

            }
        }
    }

    private List<Region> FindSharedRegions(List<Dot> dots)
    {
        List<List<Region>> regionLists = new List<List<Region>>();

        foreach (Dot dot in dots)
        {
            regionLists.Add(dot.availableRegions);
        }

        List<Region> sharedRegions = new List<Region>();

        foreach(Region compareRegion in regionLists[0])
        {
            foreach(List<Region> compareList in regionLists)
            {
                if (!compareList.Contains(compareRegion))
                {
                    break;
                }
                sharedRegions.Add(compareRegion);
            }
        }
        return sharedRegions;
    }

    private void PrintGameState()
    {
        Debug.Log("# of Regions: " + regions.Count);
        Debug.Log("# of Boundaries: " + boundaries.Count);
        Debug.Log("# of Dots: " + dots.Count);

        for(int i = 0; i < regions.Count; i++)
        {
            List<Boundary> currentRegionBoundaries = boundaries.FindAll(x => x.refRegion == regions[i]);
            string segmentString = "";
            for (int j = 0; j < boundaries.Count; j++)
            {
                segmentString += String.Join(" ,", currentRegionBoundaries[j].segments);
            }

            Debug.Log("Region " + i + " segments:" + segmentString);
        }
    }
}
