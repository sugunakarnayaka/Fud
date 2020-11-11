﻿using frame8.ScrollRectItemsAdapter.MultiplePrefabsExample;
using UnityEngine.UI;
using UnityEngine;
using System;
using TMPro;


public class PortfolioActvityCell : MonoBehaviour
{
    public RemoteImageBehaviour profileImage;

    public TextMeshProUGUI titleText;

    public TextMeshProUGUI descriptionText;

    public Image statusTag;

    public TextMeshProUGUI statusText;


    PortfolioActivityPopUp activityPopUp;

    PortfolioActivityModel activityModel;

    Action<PortfolioActivityModel> OnStatusUpdated;

    ETabType tabType;


    bool isOwnAlbum = false;


    public void Load(PortfolioActivityModel model, PortfolioActivityPopUp activityPopUp, ETabType tabType, Action<PortfolioActivityModel> OnStatusUpdated) 
    {
        this.activityModel = model;

        this.activityPopUp = activityPopUp;

        this.OnStatusUpdated = OnStatusUpdated;

        this.tabType = tabType;

        titleText.text = model.Portfolio.title;

        descriptionText.text = model.Portfolio.description;

        statusTag.gameObject.SetActive(tabType == ETabType.Altered);

        profileImage.Load(activityModel.Portfolio.Users?.profile_image);

        if (tabType == ETabType.Altered)
        {
            UpdateStatusTag();
        }
    }

    void OnPopUpClose(int updatedStatus)
    {
        switch (updatedStatus)
        {
            case 1:
            case 8:
                OnStatusUpdated?.Invoke(activityModel);
                break;
        }
    }

    public void OnTapAction()
    { 
        activityPopUp?.Load(activityModel, OnPopUpClose, tabType);
    }

    void UpdateStatusTag()
    {
        int statusValue = isOwnAlbum ? activityModel.sender_status : activityModel.reciever_status;

        EStatusType statusType = (EStatusType)statusValue;

        statusTag.sprite = Resources.Load<Sprite>("Images/StatusTags/" + statusType);

        statusText.text = statusType.ToString();
    }
}
