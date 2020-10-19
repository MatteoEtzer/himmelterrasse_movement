using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Security.Cryptography;
using UnityEngine.Networking;
using UnityEngine.UI;

public class DbCommunication : MonoBehaviour
{
    public GameObject searchPanel;
    public GameObject resultPanel;
    public GameObject resultList;
    public GameObject contentField;
    public GameObject foundElement;
    public InputScript IS;
    public InputField searchName;
    public Text errorMessage;
    public GameObject errorMessageHide;
    public Text nameText;
    public Text birthDate;
    public Text deathDate;
    public Text birthCity;
    public Text lifeCity;
    public Text jobs;
    public Text motto;
    public Text hobbies;
    public Text infos;
    public Image personImage;
    public float waitTime = 1f;

    private string secret = "tlg53lj5Ts5GH%nf3";
    private string pension = "";
    private Texture2D downloadTexture;
    private float imageAspect;
    private AspectRatioFitter aspectRatioFitter;
    private float contentRectScaleY;
    private RectTransform contentRect;
    private bool requestWaiting = true;

    private void Start()
    {
        resultPanel.SetActive(false);
        resultList.SetActive(false);
        searchPanel.SetActive(true);
        contentRect = contentField.GetComponent<RectTransform>();
        IS = GameObject.Find("Main Camera").GetComponent<InputScript>();

    }
    public void Update()
    {
        if (searchName.isFocused)
        {
            IS.OSK.SetActive(true);
            resultPanel.SetActive(false);
            errorMessageHide.SetActive(true);
        }
        else
        {
            errorMessageHide.SetActive(false);
        }
    }
    public void SendRequest()
    {
        if (searchName.text.Length > 2 && requestWaiting)
        {
            StartCoroutine(WaitForRequest());
        }
    }

    public void ClosePerson()
    {
        resultPanel.SetActive(false);
        searchPanel.SetActive(true);
        IS.OSK.SetActive(false);
        searchName.text = "";
    }

    public void CloseSearchPanel()
    {
        resultList.SetActive(false);
    }

    public void ClearSearch()
    {
        searchName.text = "";
    }

    IEnumerator WaitForRequest()
    {
        Debug.Log("false");
        requestWaiting = false;
        yield return new WaitForSeconds(waitTime);
        Debug.Log("true");
        requestWaiting = true;
        StartCoroutine(LoadPersonData(searchName.text, "select"));
    }
 
    IEnumerator LoadPersonData(string lastName, string method)
    {
        foreach (Transform child in contentField.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
        contentRectScaleY = 0f;
        string hashString = ComputeHash(lastName + secret);
        //Debug.Log(hashString);

        string url = "https://himmelsterrasse.byfive.at/dbConnect.php";

        WWWForm form = new WWWForm();
        form.AddField("lastName", lastName);
        form.AddField("method", method);
        form.AddField("hash", hashString);

        using (UnityWebRequest www = UnityWebRequest.Post(url, form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                string result = www.downloadHandler.text;
                Debug.Log(result);
                if (result[0] == '1')
                {

                    resultList.SetActive(true);
                    errorMessage.text = "";
                    float foundY = 70f;
                    if (result.Contains("<br"))
                    {
                        result = result.Replace("<br>", "§");
                        string[] resultArray = result.Split('§');
                        foreach (string resultElement in resultArray)
                        {
                            if (resultElement != "")
                            {
                                string[] returnArray = resultElement.Split('|');
                                GameObject foundObject = Instantiate(foundElement, contentField.transform);
                                foundY -= 160f;
                                foundObject.transform.localPosition = new Vector3(foundObject.transform.localPosition.x, foundY, foundObject.transform.localPosition.z);
                                contentRectScaleY += 170f;
                                foundObject.transform.GetChild(1).GetComponent<Text>().text = returnArray[4] + " " + returnArray[3] + " " + returnArray[2];
                                string[] birthDates = returnArray[5].Split('-');
                                foundObject.transform.GetChild(3).GetComponent<Text>().text = birthDates[2] + "." + birthDates[1] + "." + birthDates[0];
                                string[] deathDates = returnArray[6].Split('-');
                                foundObject.transform.GetChild(5).GetComponent<Text>().text = deathDates[2] + "." + deathDates[1] + "." + deathDates[0];
                                Image resultImage = foundObject.transform.GetChild(0).transform.GetChild(0).GetComponent<Image>();
                                StartCoroutine(DownloadImage(returnArray[15], resultImage));
                                foundObject.GetComponent<FoundPerson>().personId = returnArray[15];
                            }
                        }
                        contentRect.sizeDelta = new Vector2(0, contentRectScaleY);
                        contentRect.localPosition = Vector3.zero;
                    }
                }
                else
                {
                    errorMessage.text = result;
                }
            }
        }
    }
    IEnumerator LoadPersonById(string personId)
    {
        string url = "https://himmelsterrasse.byfive.at/dbConnect.php";

        WWWForm form = new WWWForm();
        form.AddField("personId", personId);
        form.AddField("method", "selectById");

        using (UnityWebRequest www = UnityWebRequest.Post(url, form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                string result = www.downloadHandler.text;
                Debug.Log(result);
                if (result[0] == '1')
                {
                    string[] returnArray = result.Split('|');
                    nameText.text = returnArray[4] + " " + returnArray[3] + " " + returnArray[2];
                    string[] birthDates = returnArray[5].Split('-');
                    birthDate.text = birthDates[2] + "." + birthDates[1] + "." + birthDates[0];
                    string[] deathDates = returnArray[6].Split('-');
                    deathDate.text = deathDates[2] + "." + deathDates[1] + "." + deathDates[0];
                    birthCity.text = returnArray[12];
                    lifeCity.text = returnArray[13];
                    if (returnArray[8] == "1")
                    {
                        pension = "  i.R.";
                    }
                    jobs.text = returnArray[7] + pension;
                    motto.text = returnArray[11];
                    hobbies.text = returnArray[9];
                    infos.text = returnArray[10];
                    StartCoroutine(DownloadImage(returnArray[15], personImage));
                    resultPanel.SetActive(true);
                    resultList.SetActive(false);
                    // searchPanel.SetActive(false);
                    IS.OSK.SetActive(false);
                }
                else
                {
                    errorMessage.text = result;
                }
            }
        }
    }
    public void LoadPerson(string personId)
    {
        StartCoroutine(LoadPersonById(personId));
        Debug.Log("Loading Person!");
    }
    IEnumerator DownloadImage(string personId, Image imageField)
    {
        imageField.enabled = false;
        string MediaUrl = "https://himmelsterrasse.byfive.at/images/" + personId + ".jpg";
        UnityWebRequest request = UnityWebRequestTexture.GetTexture(MediaUrl);
        yield return request.SendWebRequest();
        if (request.isNetworkError || request.isHttpError)
        { 
            Debug.Log(request.error);
        }
        else
        { 
            downloadTexture = ((DownloadHandlerTexture)request.downloadHandler).texture;
            float downloadWidth = downloadTexture.width;
            float downloadHeight = downloadTexture.height;
            imageAspect = downloadWidth / downloadHeight;
            imageField.sprite = Sprite.Create(downloadTexture, new Rect(0, 0, downloadWidth, downloadHeight), new Vector2(0, 0));
            aspectRatioFitter = imageField.transform.GetComponent<AspectRatioFitter>();
            aspectRatioFitter.aspectRatio = imageAspect;
            imageField.enabled = true;
        }
    }
    public static string ComputeHash(string s)
    {
        // Form hash
        MD5 h = MD5.Create();
        byte[] data = h.ComputeHash(System.Text.Encoding.Default.GetBytes(s));
        // Create string representation
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        for (int i = 0; i < data.Length; ++i)
        {
            sb.Append(data[i].ToString("x2"));
        }
        return sb.ToString();
    }
}
