using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColourSpawningExperiments : MonoBehaviour
{
    public Texture2D sourceTex;
    Color[] pix;

    private const float Tolerance = 0.1f;
    public int MapSizeBase = 100;
    public int ImageSizeBase;
    private float dilutionFactor;
    public int xOffset = 0;
    public int zOffset = 0;
    private const int SpawnHeight = 50;

    // Start is called before the first frame update
    void Start()
    {
        pix = sourceTex.GetPixels();
        //int i = 0;

        //int r = 0, g = 0, b = 0, y = 0;

        //Debug.Log($"Red: {Color.red}");
        //Debug.Log($"Green: {Color.green}");
        //Debug.Log($"Blue: {Color.blue}");

        /*foreach (Color c in pix)
        {
            if (c.Equals(Color.red))
            {
                r++;
            }
            else if (c.Equals(Color.green))
            {
                g++;
            }
            else if (c.Equals(Color.blue))
            {
                b++;
            }
            else if (c.Equals(new Color(1.0f,1.0f,0.0f,1.0f)))
            {
                y++;
            }
            else if(c.Equals(new Color(0f,0f,0f,1f)) || c.Equals(new Color(1f,0f,1f,1f)) || c.Equals(new Color(1f,1f,1f,1f)) || c.Equals(new Color(0f,1f,1f,1f)))
            {
                //Do nothing
            }
            else
            {
                //Debug.Log($"Coloour {i} {c}");
            }
            i++;
        }
        Debug.Log($"The number of pixels found was { i } R {r} G {g} B {b} Y {y}");*/

        dilutionFactor = ImageSizeBase / MapSizeBase;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private Vector3 CoordinateConverter(Vector3 point)
    {
        return new Vector3((point.x + xOffset) * dilutionFactor, point.y, (point.z + zOffset) * dilutionFactor);
    }

    private Vector3 CoordinateUnconverter(Vector3 point)
    {
        return new Vector3((point.x / dilutionFactor) - xOffset, point.y, (point.z / dilutionFactor) - zOffset);
    }

    public Color GetColorAtPoint(Vector3 point)
    {
        //Debug.Log($"((((int)CoordinateConverter(point).z) * ImageSizeBase) + ((int)CoordinateConverter(point).x) < ImageSizeBase * ImageSizeBase) {((((int)CoordinateConverter(point).z) * ImageSizeBase) + ((int)CoordinateConverter(point).x) < ImageSizeBase * ImageSizeBase)}");
        //Debug.Log($"(((((int)CoordinateConverter(point).z) * ImageSizeBase) + ((int)CoordinateConverter(point).x)) >= 0) {(((((int)CoordinateConverter(point).z) * ImageSizeBase) + ((int)CoordinateConverter(point).x)) >= 0)}");
        if(((((int)CoordinateConverter(point).z) * ImageSizeBase) + ((int)CoordinateConverter(point).x) < ImageSizeBase * ImageSizeBase) && (((((int)CoordinateConverter(point).z) * ImageSizeBase) + ((int)CoordinateConverter(point).x)) >= 0))
        {
            //Debug.Log((((int)point.z - 1) * ImageSizeBase) + ((int)point.x - 1));
            return pix[(((int)CoordinateConverter(point).z) * ImageSizeBase) + ((int)CoordinateConverter(point).x)];
        }
        else
        {
            Debug.Log($"Outside of bounds {point} conv: {CoordinateConverter(point)}");
            return new Color(0f,0f,0f,1f);
        }
    }

    public List<Vector3> GetPointsByColour(Color colour)
    {
        List<Vector3> points = new List<Vector3>();

        int i = 0;
        foreach(Color c in pix)
        {
            if(c.Equals(colour))
            {
                points.Add(CoordinateUnconverter(new Vector3(i % ImageSizeBase, SpawnHeight, i / ImageSizeBase)));
            }
            i++;
        }

        return points;
    }

    public List<Vector3> GetPointsByColourByOne(Color colour, int colourIndex)
    {
        List<Vector3> points = new List<Vector3>();

        Debug.Log($"This? {colour.maxColorComponent}");
        int i = 0;

        switch (colourIndex)
        {
            case 0:
                foreach (Color c in pix)
                {
                    if (c.r >= colour.r)
                    {
                        points.Add(CoordinateUnconverter(new Vector3(i % ImageSizeBase, SpawnHeight, i / ImageSizeBase)));
                    }
                    i++;
                }
                break;
            case 1:
                foreach (Color c in pix)
                {
                    if (c.g >= colour.g)
                    {
                        points.Add(CoordinateUnconverter(new Vector3(i % ImageSizeBase, SpawnHeight, i / ImageSizeBase)));
                    }
                    i++;
                }
                break;
            case 2:
                foreach (Color c in pix)
                {
                    if (c.b >= colour.b)
                    {
                        points.Add(CoordinateUnconverter(new Vector3(i % ImageSizeBase, SpawnHeight, i / ImageSizeBase)));
                    }
                    i++;
                }
                break;
            default:
                foreach (Color c in pix)
                {
                    if (c.Equals(colour))
                    {
                        points.Add(CoordinateUnconverter(new Vector3(i % ImageSizeBase, SpawnHeight, i / ImageSizeBase)));
                    }
                    i++;
                }
                break;
        }
        /*int i = 0;
        foreach (Color c in pix)
        {
            if (c.Equals(colour))
            {
                points.Add(new Vector3(i % 100, 50, i / 100));
            }
            i++;
        }*/

        return points;
    }

    public List<Vector3> GetPointsByColourTolerance(Color colour, int colourIndex, float tolerance = Tolerance)
    {
        List<Vector3> points = new List<Vector3>();
        
        int i = 0;

        bool rTolerance;
        bool gTolerance;
        bool bTolerance;

        foreach (Color c in pix)
        {
            rTolerance = ((colour.r + tolerance) >= c.r) && ((colour.r - tolerance) <= c.r);
            gTolerance = ((colour.g + tolerance) >= c.g) && ((colour.g - tolerance) <= c.g);
            bTolerance = ((colour.b + tolerance) >= c.b) && ((colour.b - tolerance) <= c.b);

            if (rTolerance && gTolerance && bTolerance)
            {
                points.Add(CoordinateUnconverter(new Vector3(i % ImageSizeBase, SpawnHeight, i / ImageSizeBase)));
            }
            i++;
        }

        return points;
    }

    public List<Vector3> GetPointsByColourByOneTolerance(Color colour, int colourIndex, float tolerance = Tolerance)
    {
        List<Vector3> points = new List<Vector3>();

        int i = 0;

        bool rTolerance;
        bool gTolerance;
        bool bTolerance;

        switch (colourIndex)
        {
            case 0:
                foreach (Color c in pix)
                {
                    rTolerance = ((colour.r + tolerance) >= c.r) && ((colour.r - tolerance) <= c.r);
                    
                    if (rTolerance)
                    {
                        points.Add(CoordinateUnconverter(new Vector3(i % ImageSizeBase, SpawnHeight, i / ImageSizeBase)));
                    }
                    i++;
                }
                break;
            case 1:
                foreach (Color c in pix)
                {
                    gTolerance = ((colour.g + tolerance) >= c.g) && ((colour.g - tolerance) <= c.g);
                    if (gTolerance)
                    {
                        points.Add(CoordinateUnconverter(new Vector3(i % ImageSizeBase, SpawnHeight, i / ImageSizeBase)));
                    }
                    i++;
                }
                break;
            case 2:
                foreach (Color c in pix)
                {
                    bTolerance = ((colour.b + tolerance) >= c.b) && ((colour.b - tolerance) <= c.b);
                    if (bTolerance)
                    {
                        points.Add(CoordinateUnconverter(new Vector3(i % ImageSizeBase, SpawnHeight, i / ImageSizeBase)));
                    }
                    i++;
                }
                break;
            default:
                foreach (Color c in pix)
                {
                    if (c.Equals(colour))
                    {
                        points.Add(CoordinateUnconverter(new Vector3(i % ImageSizeBase, SpawnHeight, i / ImageSizeBase)));
                    }
                    i++;
                }
                break;
        }

        return points;
    }

    public List<Vector3> GetPointsByColourBySeveralTolerance(Color colour, float rTolerance = Tolerance, float gTolerance = Tolerance, float bTolerance = Tolerance)
    {
        List<Vector3> points = new List<Vector3>();

        int i = 0;

        bool rToleranceGood;
        bool gToleranceGood;
        bool bToleranceGood;

        foreach (Color c in pix)
        {
            if (c.Equals(Color.black))
            {
                //Debug.Log("Black skip");
                i++;
                continue;
            }
                
            
            rToleranceGood = ((colour.r + rTolerance) >= c.r) && ((colour.r - rTolerance) <= c.r);
            gToleranceGood = ((colour.g + gTolerance) >= c.g) && ((colour.g - gTolerance) <= c.g);
            bToleranceGood = ((colour.b + bTolerance) >= c.b) && ((colour.b - bTolerance) <= c.b);

            if (rToleranceGood && gToleranceGood && bToleranceGood)
            {
                //Debug.Log($"{i} {CoordinateUnconverter(new Vector3((i % ImageSizeBase), SpawnHeight, (int)(((float)i / (float)ImageSizeBase))))}");
                points.Add(CoordinateUnconverter(new Vector3((i % ImageSizeBase), SpawnHeight, (int)(((float)i / (float)ImageSizeBase)))));
            }
            i++;
        }
        
        return points;
    }

}
