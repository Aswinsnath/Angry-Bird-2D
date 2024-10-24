using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    [SerializeField] private float secondsToWaiteBeforeDeathCheck = 3f;
    [SerializeField] private GameObject restartScreenObject;
    [SerializeField]private SlingShotHandler slingShotHandler;

    public static GameManager Instance;

    public int MaxNumberOfShots = 3;

    private int useNumberOfShots;

    private IconHandler iconHandler;

    private List<BaddiePig>_baddies = new List<BaddiePig>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        iconHandler = GameObject.FindObjectOfType<IconHandler>();

        BaddiePig[] baddies = FindObjectsOfType<BaddiePig>();

        for (int i = 0; i < baddies.Length; i++)
        {
            _baddies.Add(baddies[i]);
        }

    }


    public void UseShot()
    {
        useNumberOfShots ++;
        iconHandler.UseShot(useNumberOfShots);

        CheckForLastShot();

    }

    public bool HasIneafShots()
    {
        if(useNumberOfShots < MaxNumberOfShots)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void CheckForLastShot()
    {
        if(useNumberOfShots == MaxNumberOfShots )
        {
            StartCoroutine(CheakafterWaitTime());
        }
    }

    private IEnumerator CheakafterWaitTime()
    {
        yield return new WaitForSeconds( secondsToWaiteBeforeDeathCheck );

        if (_baddies.Count==0)
        {
            winGame();
        }
        else
        {
            RestartGame();
        }
    }

    public void RemoveBaddies(BaddiePig baddie)
    {
        _baddies.Remove(baddie);
    }


    private void CheakForAllDeadBaddies()
    {
        if (_baddies.Count == 0)
        {
            winGame();
        }
    }

    #region win/Lose

    private void winGame()
    {
        restartScreenObject.SetActive(true);
        slingShotHandler.enabled = false;
       
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }


    #endregion
}
