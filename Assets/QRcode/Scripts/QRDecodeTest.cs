using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TBEasyWebCam;

public class QRDecodeTest : MonoBehaviour
{
	public QRCodeDecodeController e_qrController;

	public Text       UiText;
	public GameObject resetBtn;
	public GameObject scanLineObj;


    public Canvas LEDProperties;
    public Canvas QRScanner;
    public Text   SelectedLEDList;


    public GameObject ScannerCameraPlane;


	#if UNITY_ANDROID && !UNITY_EDITOR
	bool isTorchOn = false;
	#endif
	public Sprite torchOnSprite;
	public Sprite torchOffSprite;
	public Image  torchImage;

    public void ScanQRCode()
    {
        QRScanner.gameObject.SetActive(true);
        LEDProperties.gameObject.SetActive(false);
        ScannerCameraPlane.SetActive(true);
        Play();
    }
    public void ShowLEDProperties()
    {
        Stop();
        ScannerCameraPlane.SetActive(false);
        QRScanner.gameObject.SetActive(false);
        LEDProperties.gameObject.SetActive(true);        
    }
    public void ClearLEDList()
    {
        SelectedLEDList.text = "";
    }
    public void NotifyAllCheckIn()
    {
        
    }
    public void NotifyAllWorking()
    {
        
    }
    public void NotifyAllBroken()
    {
        
    }

	private void Start()
	{
		if (this.e_qrController != null)
		{
			this.e_qrController.onQRScanFinished += new QRCodeDecodeController.QRScanFinished(this.qrScanFinished);
		}
	}

	private void qrScanFinished(string dataText)
	{
        string LEDs = SelectedLEDList.text;
        if (LEDs.Equals(""))
        {
            LEDs += dataText;
        } else {
            LEDs += "\n" + dataText;
        }
        SelectedLEDList.text = LEDs;

                       
        ShowLEDProperties();


		//if (isOpenBrowserIfUrl) {
		//	if (Utility.CheckIsUrlFormat(dataText))
		//	{
		//		if (!dataText.Contains("http://") && !dataText.Contains("https://"))
		//		{
		//			dataText = "http://" + dataText;
		//		}
		//		Application.OpenURL(dataText);
		//	}
		//}
		//this.UiText.text = dataText;
		//if (this.resetBtn != null)
		//{
		//	this.resetBtn.SetActive(true);
		//}
		//if (this.scanLineObj != null)
		//{
		//	this.scanLineObj.SetActive(false);
		//}

	}


	public void Play()
	{
		if (this.e_qrController != null)
		{
			this.e_qrController.StartWork();
		}
	}

	public void Stop()
	{
		if (this.e_qrController != null)
		{
			this.e_qrController.StopWork();
		}

		if (this.resetBtn != null)
		{
			this.resetBtn.SetActive(false);
		}
		if (this.scanLineObj != null)
		{
			this.scanLineObj.SetActive(false);
		}
	}

	//public void GotoNextScene(string scenename)
	//{
	//	if (this.e_qrController != null)
	//	{
	//		this.e_qrController.StopWork();
	//	}
	//	//Application.LoadLevel(scenename);
	//	SceneManager.LoadScene(scenename);
	//}

	/// <summary>
	/// Toggles the torch by click the ui button
	/// note: support the feature by using the EasyWebCam Component 
	/// </summary>
	public void toggleTorch()
	{
		#if UNITY_ANDROID && !UNITY_EDITOR
		if (EasyWebCam.isActive) {
			if (isTorchOn) {
				torchImage.sprite = torchOffSprite;
				EasyWebCam.setTorchMode (TBEasyWebCam.Setting.TorchMode.Off);
			} else {
				torchImage.sprite = torchOnSprite;
				EasyWebCam.setTorchMode (TBEasyWebCam.Setting.TorchMode.On);
			}
			isTorchOn = !isTorchOn;
		}
		#endif
	}

}
