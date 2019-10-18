
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Advertisements;
public class Game_Manager : MonoBehaviour
{

    public float restart = 1f;

    public GameObject wheel;
    public Material Tire;
    public Material Gold;
    public Material Silver;
    public Material Bronze;


    private void Awake()
    {
        int x = PlayerPrefs.GetInt("wheel");
        switch (x)
        {
            case 0:
                wheel.transform.GetChild(0).gameObject.GetComponent<MeshRenderer>().material = Tire;
                break;
            case 1000:
                wheel.transform.GetChild(0).gameObject.GetComponent<MeshRenderer>().material = Bronze;
                break;
            case 10000:
                wheel.transform.GetChild(0).gameObject.GetComponent<MeshRenderer>().material = Silver;
                break;
            case 100000:
                wheel.transform.GetChild(0).gameObject.GetComponent<MeshRenderer>().material = Gold;
                break;
            default:
                wheel.transform.GetChild(0).gameObject.GetComponent<MeshRenderer>().material = Tire;
                break;
        }
        int random = Random.Range(2, 4);
        if (random == 2)
        {
            //  FindObjectOfType<AdManager>().PlayBanner();
            Advertisement.Banner.Show();
        }
        Invoke("StopBannerAd", 10);
    }

public void EndGame()
    {


     Invoke("Restart", restart);
           
      
    }

    void Restart()
    {
        StopBannerAd();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void StopBannerAd()
    {
        Advertisement.Banner.Hide();
    }
}
