using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class GalleryManager : MonoBehaviour
{

    [System.Serializable]
    public class GalleryItem
    {
        public int requiredPoints;
        public Sprite artSprite;
        public Button button;
    }
    public List<GalleryItem> galleryItems;
    public Image artDisplay;
    private int playerPoints;

    void Start()
    {
        playerPoints = PlayerPrefs.GetInt("bestscore", 0);
        UpdateGallery();
    }

    void UpdateGallery()
    {
        foreach (var item in galleryItems)
        {
            bool unlocked = playerPoints >= item.requiredPoints;
            item.button.interactable = unlocked;
            Color buttonColor = item.button.image.color;
            buttonColor.a = unlocked ? 1f : 0.4f;
            item.button.image.color = buttonColor;
            item.button.onClick.RemoveAllListeners();
            if (unlocked)
            {
                item.button.onClick.AddListener(() => ShowArt(item.artSprite));
            }
        }
    }

    void ShowArt(Sprite art)
    {
        artDisplay.sprite = art;
        artDisplay.gameObject.SetActive(true);
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
