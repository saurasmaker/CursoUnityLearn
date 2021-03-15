using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class asdf : MonoBehaviour
{
    public static int reloadNumber = 0;

    void Start()
    {
        Debug.Log("Reload number " + reloadNumber);
        reloadNumber++;
        StartCoroutine(WaitAndReload());
    }


    IEnumerator WaitAndReload()
    {
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
