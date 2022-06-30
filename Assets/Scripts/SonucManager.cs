using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SonucManager : MonoBehaviour
{
    [SerializeField]
    private Text dogruTxt, yanlisTxt, puanTxt;

    [SerializeField]
    private GameObject birinciYildiz, ikinciYildiz, ucuncuYildiz;

    public void SonuclariYazdir(int dogruAdet, int yanlisAdet,int puan )
    {
        dogruTxt.text = dogruAdet.ToString();

        yanlisTxt.text = yanlisAdet.ToString();

        puanTxt.text = puan.ToString();

        birinciYildiz.SetActive( false );
        ikinciYildiz.SetActive( false );
        ucuncuYildiz.SetActive( false );

        if(dogruAdet==1)
        {
            birinciYildiz.SetActive( true );
        }else if(dogruAdet==2)
        {
            birinciYildiz.SetActive( true );
            ikinciYildiz.SetActive( true );
        }
        else
        {
            birinciYildiz.SetActive( true );
            ikinciYildiz.SetActive( true );
            ucuncuYildiz.SetActive( true );
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TekrarOyna ( )
    {
        SceneManager.LoadScene( "GamePlay" );
    }
}
