using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SklllInfo : MonoBehaviour
{

    public Transform skill_O, skill_K, skill_L, skill_I, skill_U, skill_J, skill_M;
    bool activeO, activeK, activeL, activeI, activeU, activeJ, activeM;

    void Start()
    {
        activeK = false;
        activeL = false;
        activeI = false;
        activeU = false;
        activeJ = false;
        activeM = false;
        activeO = false;
    }
    public void displayO()
    {
        Debug.Log("skillO");
        skill_K.gameObject.SetActive(false);
        skill_L.gameObject.SetActive(false);
        skill_I.gameObject.SetActive(false);
        skill_U.gameObject.SetActive(false);
        skill_J.gameObject.SetActive(false);
        skill_M.gameObject.SetActive(false);

        activeK = false;
        activeL = false;
        activeI = false;
        activeU = false;
        activeJ = false;
        activeM = false;

        if (activeO)
        {
            skill_O.gameObject.SetActive(false);
            activeO = false;
        }
        else 
        {
            skill_O.gameObject.SetActive(true);
            activeO = true;
        }
    }

    public void displayK()
    {
        Debug.Log("skillK");
        skill_O.gameObject.SetActive(false);
        skill_L.gameObject.SetActive(false);
        skill_I.gameObject.SetActive(false);
        skill_U.gameObject.SetActive(false);
        skill_J.gameObject.SetActive(false);
        skill_M.gameObject.SetActive(false);

        activeO = false;
        activeL = false;
        activeI = false;
        activeU = false;
        activeJ = false;
        activeM = false;

        if (activeK)
        {
            skill_K.gameObject.SetActive(false);
            activeK = false;
        }
        else if (!activeK)
        {
            skill_K.gameObject.SetActive(true);
            activeK = true;
        }
    }
    public void DisplayL()
    {
        skill_K.gameObject.SetActive(false);
        skill_O.gameObject.SetActive(false);
        skill_I.gameObject.SetActive(false);
        skill_U.gameObject.SetActive(false);
        skill_J.gameObject.SetActive(false);
        skill_M.gameObject.SetActive(false);

        activeK = false;
        activeO = false;
        activeI = false;
        activeU = false;
        activeJ = false;
        activeM = false;

        if (activeL)
        {
            skill_L.gameObject.SetActive(false);
            activeL = false;
        }
        else if (!activeL)
        {
            skill_L.gameObject.SetActive(true);
            activeL = true;
        }
    }
    public void displayI()
    {
            skill_K.gameObject.SetActive(false);
            skill_L.gameObject.SetActive(false);
            skill_O.gameObject.SetActive(false);
            skill_U.gameObject.SetActive(false);
            skill_J.gameObject.SetActive(false);
            skill_M.gameObject.SetActive(false);

            activeK = false;
            activeL = false;
            activeO = false;
            activeU = false;
            activeJ = false;
            activeM = false;

            if (activeI)
            {
                skill_I.gameObject.SetActive(false);
                activeI = false;
            }
            else if (!activeI)
            {
                skill_I.gameObject.SetActive(true);
                activeI = true;
            }
    }

    public void displayU()
    {
        skill_K.gameObject.SetActive(false);
        skill_L.gameObject.SetActive(false);
        skill_I.gameObject.SetActive(false);
        skill_O.gameObject.SetActive(false);
        skill_J.gameObject.SetActive(false);
        skill_M.gameObject.SetActive(false);

        activeK = false;
        activeL = false;
        activeI = false;
        activeO = false;
        activeJ = false;
        activeM = false;

        if (activeU)
        {
            skill_U.gameObject.SetActive(false);
            activeU = false;
        }
        else if (!activeU)
        {
            skill_U.gameObject.SetActive(true);
            activeU = true;
        }
    }

    public void displayJ()
    {
        skill_K.gameObject.SetActive(false);
        skill_L.gameObject.SetActive(false);
        skill_I.gameObject.SetActive(false);
        skill_U.gameObject.SetActive(false);
        skill_O.gameObject.SetActive(false);
        skill_M.gameObject.SetActive(false);

        activeK = false;
        activeL = false;
        activeI = false;
        activeU = false;
        activeO = false;
        activeM = false;

        if (activeJ)
        {
            skill_J.gameObject.SetActive(false);
            activeJ = false;
        }
        else if (!activeJ)
        {
            skill_J.gameObject.SetActive(true);
            activeJ = true;
        }
    }

    public void displayM()
    {
        skill_K.gameObject.SetActive(false);
        skill_L.gameObject.SetActive(false);
        skill_I.gameObject.SetActive(false);
        skill_U.gameObject.SetActive(false);
        skill_J.gameObject.SetActive(false);
        skill_O.gameObject.SetActive(false);

        activeK = false;
        activeL = false;
        activeI = false;
        activeU = false;
        activeJ = false;
        activeO = false;

        if (activeM)
        {
            skill_M.gameObject.SetActive(false);
            activeM = false;
        }
        else if (!activeM)
        {
            skill_M.gameObject.SetActive(true);
            activeM = true;
        }
    }
}

