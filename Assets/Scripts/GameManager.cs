using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class GameManager : MonoBehaviour
{
    public Soru[] sorular;

    private static List<Soru> cevaplanmamisSorular;

    private Soru gecerliSoru;

    [SerializeField]
    private Text soruText;

    [SerializeField]
    private Text dogruCevapText, yanlisCevapText;

    [SerializeField]
    private GameObject dogruButon, yanlisButon;

    [SerializeField]
    private GameObject sonucPaneli;

    int dogruAdet, yanlisAdet;

    int toplamPuan;

    SonucManager sonucManager;

    // Start is called before the first frame update
    void Start ( )
    {
        if ( cevaplanmamisSorular == null || cevaplanmamisSorular.Count == 0 )
        {
            cevaplanmamisSorular = sorular.ToList<Soru>();
        }

        yanlisAdet = 0;

        dogruAdet = 0;

        toplamPuan = 0;

        RastgeleSoruSec();

        Debug.Log( "þu anki soru:" + gecerliSoru.soru + "ve cevabý:" + gecerliSoru.dogrumu );

    }

    // Update is called once per frame
    void Update ( )
    {

    }

    void RastgeleSoruSec ( )
    {
        yanlisButon.GetComponent<RectTransform>().DOLocalMoveX( 470f, .2f );

        dogruButon.GetComponent<RectTransform>().DOLocalMoveX( -470f, .2f );

        int randomSoruIndex = Random.Range( 0, cevaplanmamisSorular.Count );

        gecerliSoru = cevaplanmamisSorular[randomSoruIndex];

        soruText.text = gecerliSoru.soru;

        if (gecerliSoru.dogrumu)
        {
            dogruCevapText.text = "DOÐRU CEVAPLADINIZ";

            yanlisCevapText.text = "YANLIÞ CEVAPLADINIZ";
        }
        else
        {
            dogruCevapText.text = "YANLIÞ CEVAPLADINIZ";

            yanlisCevapText.text = "DOÐRU CEVAPLADINIZ";
        }
    }

    IEnumerator SorularArasiBeklemeRoutine()
    {
        cevaplanmamisSorular.Remove( gecerliSoru );

        yield return new WaitForSeconds( 1f );

        //SceneManager.LoadScene( SceneManager.GetActiveScene().buildIndex );

        if(cevaplanmamisSorular.Count<=0)
        {
            //Debug.Log( dogruAdet );

            //Debug.Log( yanlisAdet );

            sonucPaneli.SetActive( true );

            sonucManager = Object.FindObjectOfType<SonucManager>();

            sonucManager.SonuclariYazdir( dogruAdet, yanlisAdet, toplamPuan );
        }
        else
        {
            RastgeleSoruSec();
        }
    }

    public void dogruButonaBasildi ( )
    {
        if ( gecerliSoru.dogrumu )
        {
            dogruAdet++;
            //Debug.Log( "dogru cevapladýnýz" );
            toplamPuan += 100;
        }
        else
        {
            yanlisAdet++;
            //Debug.Log( "yanlýþ cevapladýnýz" );
        }

        yanlisButon.GetComponent<RectTransform>().DOLocalMoveX( 1500f, .2f );

        StartCoroutine( SorularArasiBeklemeRoutine() );
    }

    public void yanlisButonaBasildi ( )
    {
        if ( gecerliSoru.dogrumu == false )
        {
            dogruAdet++;
            //Debug.Log( "dogru cevapladýnýz" );
            toplamPuan += 100;
        }
        else
        {
            yanlisAdet++;
            //Debug.Log( "yanlýþ cevapladýnýz" );
        }

        dogruButon.GetComponent<RectTransform>().DOLocalMoveX( -1500f, .2f );

        StartCoroutine( SorularArasiBeklemeRoutine() );
    }
}
