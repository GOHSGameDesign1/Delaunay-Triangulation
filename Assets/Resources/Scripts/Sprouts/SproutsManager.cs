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

    [SerializeField]
    private List<Dot> dots = new List<Dot>();

    [SerializeField]
    private List<Boundary> boundaries = new List<Boundary>();

    [SerializeField]
    private List<Region> regions = new List<Region>();

    public string dot1ID;
    public string dot2ID;

    // Start is called before the first frame update
    void Awake()
    {
        SetupGame();
    }

    private void Start()
    {
        FindTwoBoundaryMove();
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

        PrintGameState();
    }

    public void FindTwoBoundaryMove()
    {
        Dot dot1 = dots.Find(x => x.ID == dot1ID);
        Dot dot2 = dots.Find(x => x.ID == dot2ID);

        if (dot1 == null)
        {
            Debug.LogError("Dot1 could not be found!");
            return;
        }
        if (dot2 == null)
        {
            Debug.LogError("Dot2 could not be found!");
            return;
        }

        if (TwoBoundaryViable(dot1, dot2))
        {
            Debug.Log("Two boundary move available between " + dot1.ID + " and " + dot2.ID);
            TwoBoundaryMove(dot1, dot2);
        }
    }

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

    private void TwoBoundaryMove(Dot dot1, Dot dot2)
    {
        Region moveRegion = dot1.availableRegions.FindAll(x => dot2.availableRegions.Contains(x))[0];
        Boundary bound1 = dot1.availableBoundaries.Find(x => x.refRegion == moveRegion);
        Boundary bound2 = dot2.availableBoundaries.Find(x => x.refRegion == moveRegion);



        Boundary newBoundary = new Boundary(moveRegion, bound1, bound2);
        boundaries.Add(newBoundary);

        Dot newDot = new Dot((dots.Count+1).ToString(), 2, moveRegion, newBoundary);
        dots.Add(newDot);

        Tuple<string, string> newSeg1 = new Tuple<string,string>(dot1.ID, newDot.ID);
        newBoundary.AddSegment(newSeg1);

        Tuple<string, string> newSeg2 = new Tuple<String, String>(dot2.ID, newDot.ID);
        newBoundary.AddSegment(newSeg2);

        dot1.SetRefBoundary(newBoundary);
        dot2.SetRefBoundary(newBoundary);

        boundaries.Remove(bound1);
        boundaries.Remove(bound2);

        PrintGameState();
    }

    private void PrintGameState()
    {
        Debug.Log("# of Regions: " + regions.Count);
        Debug.Log("# of Boundaries: " + boundaries.Count);
        Debug.Log("# of Dots: " + dots.Count);

        for(int i = 0; i < regions.Count; i++)
        {
            List<Boundary> currentRegionBoundaries = boundaries.FindAll(x => x.refRegion == regions[i]);
            for (int j = 0; j < boundaries.Count; j++)
            {
                string segmentString = String.Join(" ,", currentRegionBoundaries[j].segments);
                Debug.Log("Region " + i + " segments:" + segmentString);
            }
        }
    }
}
