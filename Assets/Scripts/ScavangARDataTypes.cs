using UnityEngine;
using System.Collections;

public struct Rangedfloat{

    float min;
    float max;
}

public struct GpsLocation {
    public float lat {
        get { return lat;}
        private set { return; }
    }

    public float lon {
        get { return lon; }
        private set { return; }
    }

    public float alt {
        get { return alt; }
        private set { return; }
    }

    public GpsLocation(float lat, float lon, float alt) {
        this.lat = lat;
        this.lon = lon;
        this.alt = alt;
    }
}
