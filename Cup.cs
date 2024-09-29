using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Cup : MonoBehaviour
{
    public string quality = "clean";
    public string[] variants = { "clean", "dirty", "coffee", "dirtycoffee" };
    // Start is called before the first frame update
    void Start()
    {
        quality = variants[1];
    }
    public void GetCleaned() {
        this.quality = "clean";
    }
    public void CoffeeReady()
    {
     if (this.quality == "clean")
        {
            this.quality = "coffee";
        }
     if (this.quality == "dirty")
        {
            this.quality = "dirtycoffee";
        }
    }
    public void Drank()
    {
        this.quality = "dirty";
    }
    public void switchquality(){
        int current = Array.IndexOf(this.variants, this.quality);
    int nextIndex = current + 1;
    if(current>=variants.Length){nextIndex = 0;}
this.quality = variants[nextIndex];

}

}
