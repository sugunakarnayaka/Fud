﻿using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UpdateStoryVersionView : MonoBehaviour
{
    public RectTransform galleryPanel;

    public TMP_InputField storyTitleField;

    public TMP_InputField subTitleField;

    public TMP_Dropdown dropdown;

    public TMP_InputField descriptionField;

    public TextMeshProUGUI contentType;

    public TextMeshProUGUI filePath;

    public TextMeshProUGUI statusText;

    public TextMeshProUGUI canSupportMultipleText;

    List<Genre> genres;

    List<string> imageUrls;

    StoryVersion storyVersion;


    public void Load(StoryVersion storyVersion)
    {
        this.storyVersion = storyVersion;

        SetView();

        canSupportMultipleText.text = "Can Support Text " + NativeGallery.CanSelectMultipleFilesFromGallery().ToString();
    }

    void SetView()
    {
       /* storyTitleField.text = storyVersion.t;

        subTitleField.text = storyVersion.story_line;*/

        descriptionField.text = storyVersion.description;

        PopulateDropdown();
    }

    void PopulateDropdown()
    {
        genres = DataManager.Instance.genres;

        Genre requiredGenre = genres.Find(genre => genre.id == storyVersion.genre_id);

        Genre selectedGenre = genres.Find(genre => genre.name.Equals(requiredGenre.name));

        List<string> options = new List<string>();

        foreach (var option in genres)
        {
            options.Add(option.name);
        }

        dropdown.ClearOptions();

        dropdown.AddOptions(options);

        dropdown.value = dropdown.options.FindIndex(option => options.Equals(selectedGenre.name));
    }

    public void OnUploadAction()
    {
        ShowGalleryPanel();
    }

    public void OnBackButtonAction()
    {
        Destroy(gameObject);
    }

    public void OnSubmitAction()
    {
        string selectedGenreText = dropdown.options[dropdown.value].text;

        Genre selectedGenre = genres.Find(genre => genre.name.Equals(selectedGenreText));

        Debug.Log("Genre Id = " + selectedGenre.id);

        GameManager.Instance.apiHandler.UpdateStory(storyTitleField.text, subTitleField.text, descriptionField.text, selectedGenre.id, (status, response) => {

            if (status)
            {
                Destroy(gameObject);
                Debug.Log("Story Uploaded Successfully");
            }
            else
            {
                Debug.LogError("Story Updation Failed");
            }
        });
    }

    public void OnScreenShotAction()
    {
        GetScreenShot();
    }

    void ShowGalleryPanel()
    {
        SlideGalleryView(true);
    }

    void SlideGalleryView(bool canShow)
    {
        float panelPosition = galleryPanel.anchoredPosition.y;

        float targetPostion = panelPosition += canShow ? galleryPanel.rect.height : -galleryPanel.rect.height;

        galleryPanel.DOAnchorPosY(targetPostion, 0.4f);
    }

    public void OnMediaButtonAction(int mediaType)
    {
        EMediaType selectedType = (EMediaType)mediaType;

        SlideGalleryView(false);

        switch (selectedType)
        {
            case EMediaType.Image:
                GalleryManager.Instance.PickImages(OnImagesUploaded);
                break;
            case EMediaType.Audio:
                GalleryManager.Instance.GetAudiosFromGallery();
                break;
            case EMediaType.Video:
                GalleryManager.Instance.GetVideosFromGallery();
                break;
        }
    }

    void OnImagesUploaded(bool status, List<string> imageUrls)
    {
        if (status)
        {
            this.imageUrls = imageUrls;
        }
    }

    public void OnCancelAction()
    {
        SlideGalleryView(false);
    }

    void Reset()
    {
        //storyTitleField.text = string.Empty;
        gameObject.SetActive(false);

        storyTitleField.text = string.Empty;

        subTitleField.text = string.Empty;

        descriptionField.text = string.Empty;
    }

    void GetScreenShot()
    {

    }

    void GetGalleryVideos()
    {

    }
}