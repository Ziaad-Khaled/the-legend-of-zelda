using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceCubesController : MonoBehaviour
{
    public GameObject[] iceCubes;
    private int cryonisOnIceCube = -1;
    public GameObject[] gates;

    // Update is called once per frame
    void Update()
    {
            //if the cryonis is applied on a cube and it is still rising, raise it and raise the gate if it is under gate
            if (cryonisOnIceCube !=-1 && iceCubes[cryonisOnIceCube].transform.position.y < 2)
            {
                iceCubes[cryonisOnIceCube].transform.position += new Vector3(0, 0.1f * Time.timeScale, 0);
                if (cryonisOnIceCube == 2 || cryonisOnIceCube == 3 || cryonisOnIceCube == 4)
                    gates[cryonisOnIceCube - 2].transform.position += new Vector3(0, 0.1f * Time.timeScale, 0);
            }
        
    }

    public void perfromCryonisOnCube(int i)
    {
        //set the cube number that link will perform cryonis on

        cryonisOnIceCube = i;
        iceCubes[i].SetActive(true);

    }
    public void unperformCryonis()
    {
        if (cryonisOnIceCube == -1) return;

        iceCubes[cryonisOnIceCube].transform.position -= new Vector3(0, 5f , 0);
        if(cryonisOnIceCube == 2|| cryonisOnIceCube == 3|| cryonisOnIceCube == 4)
           gates[cryonisOnIceCube - 2].transform.position -= new Vector3(0, 5f, 0);
        iceCubes[cryonisOnIceCube].SetActive(false);

        //No Cryonis is applied
        cryonisOnIceCube = -1;


    }
}
