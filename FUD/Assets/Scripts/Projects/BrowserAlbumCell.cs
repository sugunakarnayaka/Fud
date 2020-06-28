﻿using UnityEngine.UI;
using UnityEngine;
using System;

public class BrowserAlbumCell : MonoBehaviour
{
    public RawImage albumImage;

    public GameObject selectObject;


    PortfolioModel portfolioModel;

    Action<bool, PortfolioModel> OnSelectAction;

    EMediaType mediaType;


    public void SetView(PortfolioModel portfolioModel, Action<bool, PortfolioModel> OnSelectAction)
    {
        this.portfolioModel = portfolioModel;

        this.OnSelectAction = OnSelectAction;
        
        //PortfolioAlbumModel _albumModel = portfolioModel.PortfolioMedia.Find(item => DataManager.Instance.GetMediaType(item.media_type) == EMediaType.Video);

        //if (_albumModel != null)
        //{
        //    portfolioModel.onScreenModel = albumModel;

        //    SetVideoThumbnail(null);
        //}
    }

    public void SetVideoThumbnail(PortfolioAlbumModel portfolioModel)
    {
        if (mediaType == EMediaType.Video)
        {
            VideoStreamer.Instance.GetThumbnailImage(portfolioModel.content_url, (texture) =>
            {
                //Rect rect = new Rect(0, 0, albumImage.rectTransform.rect.width, albumImage.rectTransform.rect.height);

                //Sprite sprite = Sprite.Create(texture.ToTexture2D(), rect, new Vector2(0.5f, 0.5f));

                albumImage.texture = texture;
            });
        }
    }

    public void OnAlbumButtonAction()
    {
        OnSelectAction?.Invoke(true, portfolioModel);
    }

    public void OnSelectToggleAction(Toggle toggle)
    {
        selectObject.SetActive(toggle.isOn);

        OnSelectAction?.Invoke(false, portfolioModel);
    }
}