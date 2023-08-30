using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HomeButtonManager : MonoBehaviour
{
    public void MoveToMainMenu()
    {
        SceneManager.LoadSceneAsync(0);
    }
}
